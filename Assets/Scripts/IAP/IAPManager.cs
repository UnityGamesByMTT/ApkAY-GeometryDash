using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour
{


    [SerializeField] private BuyPanel panel1;


    public void OnProductFetched(Product product)
    {
        if(product.metadata.localizedTitle != null)
        {
            panel1.UpdateNoOfDiamonds(product.metadata.localizedTitle.ToString());
        }
        if(product.metadata.localizedPrice != null)
        {
            panel1.UpdatePricing(product.metadata.localizedPrice.ToString());

        }
    }

    public void OnPurchaseComplete(Product product)
    {
        PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds")+50);
    }

    public void OnPurchaseFailed(Product product,PurchaseFailureDescription description)
    {
        Debug.Log("IAPTEST :  The purchase of " + product.metadata.localizedTitle + " Failed.");
    }
}
