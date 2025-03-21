using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text NoOFDiamonds;
    [SerializeField] private TMP_Text Pricing;


    internal void UpdatePricing(string price)
    {
        Pricing.text = price;
    }

    internal void UpdateNoOfDiamonds(string noOfDiamonds)
    {
        NoOFDiamonds.text = noOfDiamonds;
    }

}
