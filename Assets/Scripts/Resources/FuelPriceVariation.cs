using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPriceVariation : MonoBehaviour
{
    [SerializeField] private int maxFuelPrice = 500;
    [SerializeField] private int minFuelPrice = 200;
    [SerializeField] private int PriceStep = 50;
    [SerializeField] private float priceChangeInterval = 3f;

    [SerializeField] private ChangeText fuelPriceText;

    public int currentFuelPrice;
    private bool decresingPrice = true;

    public static FuelPriceVariation instance;
     
    private void Awake()
    {
        // Gestion du singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentFuelPrice = maxFuelPrice;
        StartCoroutine(AdjustFuelPrice());
    }

    private IEnumerator AdjustFuelPrice()
    {
        while (true)
        {
            yield return new WaitForSeconds(priceChangeInterval);

            if (decresingPrice)
            {
                currentFuelPrice -= PriceStep;
                if (currentFuelPrice <= minFuelPrice)
                {
                    currentFuelPrice = minFuelPrice;
                    decresingPrice = false;
                }
            }
            else
            {
                currentFuelPrice += PriceStep;
                if (currentFuelPrice >= maxFuelPrice)
                {
                    currentFuelPrice = maxFuelPrice;
                    decresingPrice = true;
                }
            }

            fuelPriceText.toText = $"Cost: ${currentFuelPrice}K";

            ConsoleManager.instance.Log($"Current Fuel Price: ${currentFuelPrice}K");
        }
    }
}
