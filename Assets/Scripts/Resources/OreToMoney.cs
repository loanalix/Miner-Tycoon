using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class OreToMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text sellSettingsText;
    [SerializeField] private GameObject marketingButton;

    [SerializeField] private int showButton;

    private int totalSales = 0;

    private float timer = 0f;
    private int sellPrice = 5;
    private float conversionInterval = 0.5f; 

    float publicDemand = 0f;

    [SerializeField] private const int minSellPrice = 1;
    private const int maxOreSoldPerInterval = 5;
    private float salesPerSec = 0f;

    public static OreToMoney instance;
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
        InvokeRepeating("ResetSalesPerSec", 0, 1);
    }
    
    private void Start()
    {
        UpdateSellSettingsUI();
        marketingButton.SetActive(false);
    }

    void Update()
    {
        //si pas de ressource 
        if (RessourceManager.instance.GetOre() <= 0)
        {
            return;
        }

        timer += Time.deltaTime;

        //tried to sell
        if (timer >= conversionInterval) 
        {
            TryConvertOreToMoney();
            timer -= conversionInterval;
            UpdateSellSettingsUI();
        }
    }

    private void ResetSalesPerSec()
    {
        salesPerSec = 0f;
    }
    public float GetSalesPerSec()
    {
        return salesPerSec;
    }
    
    private void TryConvertOreToMoney()
    {
        // Calcul de la probabilit� de vente bas�e uniquement sur le prix

        publicDemand = (Mathf.Pow(1.1f, RessourceManager.instance.marketingLevel - 1)) * (5000 / sellPrice) * (1 + UpgradeManager.instance.MarketingBonus);

        if (Random.value < (publicDemand / 100))
        {
            ConvertOreToMoney(Mathf.FloorToInt(0.7f * Mathf.Pow(publicDemand, 1.15f)));
        }
        else
        {
            //ConsoleManager.instance.Log("No sale: price too high! Probability: "+ publicDemand);
        }

    }

    private void ConvertOreToMoney(int oreToSell)
    {
        int availableOre = RessourceManager.instance.GetOre();
        int actualOreToSell = Mathf.Min(oreToSell, availableOre);

        if (actualOreToSell > 0) {

            RessourceManager.instance.RemoveOre(actualOreToSell);
            RessourceManager.instance.AddMoney(actualOreToSell * sellPrice);
            totalSales += actualOreToSell;

            ConsoleManager.instance.Log($"Sold {actualOreToSell} ore(s) for {actualOreToSell * sellPrice} money.");
            salesPerSec += actualOreToSell;
            if (totalSales >= showButton)
            {
                marketingButton.SetActive(true); // Affiche le bouton marketing
            }
        }
        else
        {
            //ConsoleManager.instance.Log("Not enough Ore to convert!");
        }
    }

    public void AdjustSellSettings(int newPrice)
    {
        // Ajuste le prix de vente
        sellPrice = Mathf.Max(minSellPrice, newPrice);
        
        // Met � jour l'interface utilisateur
        UpdateSellSettingsUI();
    }

    private void UpdateSellSettingsUI()
    {
        publicDemand = (Mathf.Pow(1.1f, RessourceManager.instance.marketingLevel - 1)) * (5000 / sellPrice) * (1 + UpgradeManager.instance.MarketingBonus);
        if (sellSettingsText != null)
        {
            // Calcul de la probabilit� pour l'interface utilisateur
            sellSettingsText.text = ($"Sell Price: {sellPrice}\nPublic Demand:" + publicDemand);
        }
    }

    public void IncreasePrice()
    {
        AdjustSellSettings(sellPrice * 2);
    }

    public void DecreasePrice()
    {
        if (sellPrice == minSellPrice)
        {
            ConsoleManager.instance.Log("Cannot decrease further: price is at its minimum!");
            return;
        }

        AdjustSellSettings(sellPrice / 2);
    }

    public int GetTotalSales()
    {
        return totalSales;
    }

}