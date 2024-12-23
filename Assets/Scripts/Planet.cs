using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public string planetName = "Planet";
    public int unlockPrice = 1000;
    public string description = "This is a planet";
    [SerializeField] bool startUnlocked = false;
    public bool unlocked = false;
    
    [SerializeField] private int autominersCount = 0;
    [SerializeField] private TMP_Text autominerCountText;
    [SerializeField] private ChangeText autominerPriceText;
    [SerializeField] private int planetLevel = 1;
    [SerializeField] private LocalUpgradeManager localUpgradeManager;

    
    [SerializeField] private GameObject upgradeStore;

    void Start()
    {
        if(startUnlocked)
        {
            unlocked = true;
        }

        UpdateDisplay();
        RessourceManager.instance.RegisterPlanet(this);
    }

    public void TryUnlock()
    {
        if (unlocked)
            return;

        if (RessourceManager.instance.GetMoney() >= unlockPrice)
        {
            unlocked = true;
            RessourceManager.instance.RemoveMoney(unlockPrice);
            ConsoleManager.instance.Log("Unlocked " + planetName + " for $" + unlockPrice + "K");
            UpdateDisplay();
        } else
        {
            // Not enough money popup
            ConsoleManager.instance.Log("Not enough money to unlock " + planetName);
        }
    }

    public void TryBuyAutominer()
    {
        int autoClickerCost = Mathf.RoundToInt(Mathf.Pow(2, 1 + autominersCount) * planetLevel * localUpgradeManager.AutoMinerCostMultiplier);
        if (RessourceManager.instance.GetMoney() >= autoClickerCost)
        {
            autominersCount++;
            RessourceManager.instance.RemoveMoney(autoClickerCost);
            UpdateDisplay();
        }
        else
        {
            ConsoleManager.instance.Log("Not enough money to buy autominer");
        }
    }

    public void UpdateDisplay()
    {
        if (unlocked)
        {
            upgradeStore.SetActive(true);
            autominerCountText.transform.parent.gameObject.SetActive(true);
            autominerCountText.text = "Currently owned: " + autominersCount;
            autominerPriceText.toText = "$" + Mathf.RoundToInt(Mathf.Pow(2, 1 + autominersCount) * planetLevel * localUpgradeManager.AutoMinerCostMultiplier * UpgradeManager.instance.AutoMinerCostMultiplier) + "K";
        }
        else
        {
            upgradeStore.SetActive(false);
            autominerCountText.transform.parent.gameObject.SetActive(false);
        }
    }
    
    public int GetLocalAutoClickers()
    {
        return autominersCount; 
    }
    
    public float GetAutominerProduction()
    {
        if(!unlocked)
            return 0;
        return autominersCount * localUpgradeManager.AutoMinerMultiplier * planetLevel;
    }

    public float GetFuelConsumption()
    {
        if (!unlocked)
            return 0;
        return autominersCount * (1 / localUpgradeManager.FuelEfficiencyMultiplier);
    }

    public float GetMarketingBonus()
    {
        return localUpgradeManager.MarketingBonus;
    }
    public float GetAutominerMultiplier()
    {
        return localUpgradeManager.AutoMinerMultiplier;
    }
    public float GetAutominerCostMultiplier()
    {
        return localUpgradeManager.AutoMinerCostMultiplier;
    }
    public float GetFuelEfficiencyMultiplier()
    {
        return localUpgradeManager.FuelEfficiencyMultiplier;
    }
}
