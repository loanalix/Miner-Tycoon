using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text MonneyText;
    [SerializeField] private TMP_Text FuelText;
    [SerializeField] private TMP_Text OreText;
    [SerializeField] private TMP_Text UpgradeText;
    [SerializeField] private TMP_Text CliKText;
    [SerializeField] private TMP_Text AutoClikerNumText;
    [SerializeField] private TMP_Text TotalSellText;

    [SerializeField] private TMP_Text salesPerSecond;
    [SerializeField] private TMP_Text prodPerSecond;
    
    private RessourceManager ressourceManager;
    [SerializeField] private AddRessources addressourcess;

    void Start()
    {
        ressourceManager = RessourceManager.instance;
        ressourceManager.OnRessourceChanged += UpdateUI;

        UpdateUI();
    }

    private void FixedUpdate()
    {
        salesPerSecond.text = "Sales/s: " + OreToMoney.instance.GetSalesPerSec();
        prodPerSecond.text = "Production/s: " + RessourceManager.instance.GetProdPerSec();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        ressourceManager.OnRessourceChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        MonneyText.text = $"Money: {ressourceManager.GetMoney()}K";
        FuelText.text = ressourceManager.GetFuel() + "L";
        OreText.text = $"Ore: {ressourceManager.GetOre()}";

        string upgradeName = UpgradeManager.instance.GetNextUpgradeName();
        int upgradeCost = UpgradeManager.instance.GetNextUpgradeCost();

        UpgradeText.text = upgradeCost > 0
            ? $"Prochaine am�lioration: {upgradeName} (Co�t: {upgradeCost})"
            : "Toutes les am�liorations sont d�bloqu�es !";

        CliKText.text = $"Total clicks: {addressourcess.GetButtonClickCount()}";
        AutoClikerNumText.text = $"Total AutoMiners: {ressourceManager.GetTotalAutoClickers()}";
        TotalSellText.text = $"Total ore sold: {OreToMoney.instance.GetTotalSales()}";

        salesPerSecond.text = "Sales/s: " + OreToMoney.instance.GetSalesPerSec();
        prodPerSecond.text = "Production/s: " + RessourceManager.instance.GetProdPerSec();
    }
}
