using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    private UpgradeManager.Upgrade upgrade;
    private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescriptionText;
    [SerializeField] private ChangeText changeText;
    public UpgradeManager upgradeManager;

    private void Start()
    {
        upgradeButton = GetComponent<Button>();
        if (upgradeNameText == null)
            Debug.LogError("set the variable in the editor damnit");
        if (upgradeDescriptionText == null)
            Debug.LogError("set the variable in the editor damnit");

        if(changeText == null)
            changeText = GetComponent<ChangeText>();
        changeText.toText = "$" + upgrade.Cost.ToString() + "K";
    }

    public void TryBuyUpgrade()
    {
        //check money
        if (RessourceManager.instance.GetMoney() < upgrade.Cost)
        {
            ConsoleManager.instance.Log("Not enough money to buy " + upgrade.UpgradeName);
            return;
        }
        upgradeManager.ApplyUpgrade(upgrade);
        RessourceManager.instance.RemoveMoney(upgrade.Cost);
        ConsoleManager.instance.Log("Purchased " + upgrade.UpgradeName + " for $" + upgrade.Cost + "K");

        // Detach from parent before calling update, so its not counted
        gameObject.transform.SetParent(null);
        upgradeManager.UpdateStoreList();
        Destroy(gameObject);
    }

    public void SetUpgrade(UpgradeManager.Upgrade newUpgrade)
    {
        if(newUpgrade == null)
        {
            Debug.LogError("Upgrade is null");
            return;
        }

        upgrade = newUpgrade;
        //Debug.Log("Upgrde name: " + upgrade.UpgradeName);
        upgradeNameText.text = upgrade.UpgradeName;
        upgradeDescriptionText.text = upgrade.Description;
        changeText.toText = "$" + upgrade.Cost.ToString() + "K";
    }
}
