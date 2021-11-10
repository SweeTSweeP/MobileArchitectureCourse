using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopItem : MonoBehaviour
  {
    public Button BuyItemButton;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI SkullsQuantityText;
    public TextMeshProUGUI AvailablePurchasesLeftText;
    public Image Icon;

    private ProductDescription _productDescription;
    private IIAPService _iapService;
    private IAssetProvider _assets;

    public void Construct(IIAPService iapService, IAssetProvider assets, ProductDescription productDescription)
    {
      _productDescription = productDescription;
      _iapService = iapService;
      _assets = assets;
    }

    public async void Initialize()
    {
      BuyItemButton.onClick.AddListener(OnBuyItemButtonPressed);
      PriceText.text = _productDescription.Config.Price;
      SkullsQuantityText.text = _productDescription.Config.Quantity.ToString();
      AvailablePurchasesLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
      Icon.sprite = await _assets.Load<Sprite>(_productDescription.Config.Icon);
    }

    private void OnBuyItemButtonPressed() => 
      _iapService.StartPurchase(_productDescription.Id);
  }
}