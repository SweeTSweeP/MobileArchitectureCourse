﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using UnityEngine.Purchasing;

namespace CodeBase.Services.IAP
{
  public class IAPService : IIAPService
  {
    private readonly IAPProvider _iapProvider;
    private readonly IPersistentProgressService _progressService;

    public bool IsInitialized => _iapProvider.IsInitialized;
    public event Action Initialized;

    public IAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
    {
      _iapProvider = iapProvider;
      _progressService = progressService;
    }

    public void Initialize()
    {
      _iapProvider.Initialize(this);
      _iapProvider.Initialized += () => Initialized?.Invoke();
    }

    public List<ProductDescription> Products() =>
      ProductDescriptions().ToList();

    public void StartPurchase(string productId) =>
      _iapProvider.StartPurchase(productId);

    public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
    {
      ProductConfig productConfig = _iapProvider.Configs[purchasedProduct.definition.id];

      switch (productConfig.ItemType)
      {
        case ItemType.Skulls:
          _progressService.Progress.WorldData.LootData.Add(productConfig.Quantity);
          _progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
          break;
      }

      return PurchaseProcessingResult.Complete;
    }

    private IEnumerable<ProductDescription> ProductDescriptions()
    {
      PurchaseData purchaseData = _progressService.Progress.PurchaseData;

      foreach (string productId in _iapProvider.Products.Keys)
      {
        ProductConfig config = _iapProvider.Configs[productId];
        Product product = _iapProvider.Products[productId];

        BoughtIAP boughtIap = purchaseData.BoughtIAPs.Find(x => x.IAPid == productId);

        if (ProductBoughtOut(boughtIap, config))
          continue;

        yield return new ProductDescription
        {
          Id = productId,
          Product = product,
          Config = config,
          AvailablePurchasesLeft =
            boughtIap == null
              ? config.MaxPurchaseCount
              : config.MaxPurchaseCount - boughtIap.Count
        };
      }
    }

    private static bool ProductBoughtOut(BoughtIAP boughtIap, ProductConfig config) => 
      boughtIap != null && boughtIap.Count >= config.MaxPurchaseCount;
  }
}