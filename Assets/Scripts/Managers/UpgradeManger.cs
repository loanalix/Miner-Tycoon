using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UpgradeManager;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance { get; private set; }
    [SerializeField] protected List<Upgrade> _upgrades = new List<Upgrade>(); 
    private int currentUpgradeIndex = 0; 

    public float MarketingBonus = 0;
    public float AutoMinerMultiplier = 1f;
    public float AutoMinerCostMultiplier = 1f;
    public float FuelEfficiencyMultiplier = 1f;

    [SerializeField] private GameObject upgradeButtonPrefab;
    [SerializeField] private GameObject UpgradeStore;
    int maxUpgradeInStore = 3;

    public int GetCurrentUpgradeLevel() => currentUpgradeIndex;
    public string GetNextUpgradeName() => currentUpgradeIndex < _upgrades.Count ? _upgrades[currentUpgradeIndex].UpgradeName : "Max Level";
    public int GetNextUpgradeCost() => currentUpgradeIndex < _upgrades.Count ? _upgrades[currentUpgradeIndex].Cost : -1;

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

    private void Start()
    {
        UpdateStoreList();
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        UpgradeManager.instance.MarketingBonus += upgrade.MarketingBonus;
        AutoMinerMultiplier *= upgrade.AutoMinerMultiplier;
        AutoMinerCostMultiplier *= upgrade.AutoMinerCostMultiplier;
        FuelEfficiencyMultiplier *= upgrade.FuelEfficiencyMultiplier;

        if(MarketingBonus < 0)
        {
            MarketingBonus = 0;
        }
        UpdateStoreList();
    }

    public void UpdateStoreList()
    {
        bool wasActive = UpgradeStore.activeSelf;

        UpgradeStore.SetActive(false);
        while(UpgradeStore.transform.childCount < maxUpgradeInStore && _upgrades.Count > 0)
        {
            GameObject upgradeButton = Instantiate(upgradeButtonPrefab, UpgradeStore.transform);
            upgradeButton.GetComponent<UpgradeButton>().upgradeManager = this;
            upgradeButton.GetComponent<UpgradeButton>().SetUpgrade(_upgrades[0]);
            _upgrades.RemoveAt(0);
        }
        UpgradeStore.SetActive(wasActive);
    }

    [System.Serializable]
    public class Upgrade
    {
        public string UpgradeName;
        public string Description;
        public int Cost = 1; 
        public float AutoMinerMultiplier = 1;
        public float AutoMinerCostMultiplier = 1;
        public float FuelEfficiencyMultiplier = 1;
        public float MarketingBonus = 0;
    }
}
