using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutobuyFuel : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private int buyAmmount;
    void Update()
    {
        if (toggle.isOn && RessourceManager.instance.GetFuel() < buyAmmount && RessourceManager.instance.GetMoney() > FuelPriceVariation.instance.currentFuelPrice)
        {
            RessourceManager.instance.AddFuel(buyAmmount);
            RessourceManager.instance.RemoveMoney(FuelPriceVariation.instance.currentFuelPrice);
        }
    }
}
