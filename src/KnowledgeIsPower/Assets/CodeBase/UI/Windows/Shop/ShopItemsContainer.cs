using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.IAP;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopItemsContainer : MonoBehaviour
  {
    private const string ShopItemPath = "ShopItem";
    
    public GameObject[] ShopUnavailableObjects;
    public Transform Parent;
    private IIAPService _iapService;
    private IPersistentProgressService _progressService;
    private IAssetProvider _assets;
    private readonly List<GameObject> _shopItems = new List<GameObject>();

    public void Construct(IIAPService iapService, IPersistentProgressService progressService, IAssetProvider assets)
    {
      _iapService = iapService;
      _progressService = progressService;
      _assets = assets;
    }

    public void Initialize() => 
      RefreshAvailableItems();

    public void Subscribe()
    {
      _iapService.Initialized += RefreshAvailableItems;
      _progressService.Progress.PurchaseData.Changed += RefreshAvailableItems;
    }

    public void Cleanup()
    {
      _iapService.Initialized -= RefreshAvailableItems;
      _progressService.Progress.PurchaseData.Changed -= RefreshAvailableItems;
    }

    private async void RefreshAvailableItems()
    {
      UpdateShopUnavailableObjects();

      if (!_iapService.IsInitialized)
        return;
      
      ClearShopItems();

      await FillShopItems();
    }

    private async Task FillShopItems()
    {
      foreach (ProductDescription productDescription in _iapService.Products())
      {
        GameObject shopItemObject = await _assets.Instantiate(ShopItemPath, Parent);

        ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();
        shopItem.Construct(_iapService, _assets, productDescription);
        shopItem.Initialize();

        _shopItems.Add(shopItemObject);
      }
    }

    private void ClearShopItems()
    {
      foreach (GameObject shopItem in _shopItems)
        Destroy(shopItem);

      _shopItems.Clear();
    }

    private void UpdateShopUnavailableObjects()
    {
      foreach (GameObject unavailableObject in ShopUnavailableObjects)
        unavailableObject.SetActive(!_iapService.IsInitialized);
    }
  }
}