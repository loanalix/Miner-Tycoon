using UnityEngine;

public class AddRessources : MonoBehaviour
{

    [SerializeField] private RessourceManager ressourceManager;
    [SerializeField] private FuelPriceVariation fuelPrice;
    
    private int buttonClickCount = 0;
    

    public int GetButtonClickCount()
    {
        return buttonClickCount;
    }


    // Update is called once per frame
    public void OnButtonClick()
    {
        ressourceManager.AddOre(1);
        buttonClickCount++;
    }

    public void OnFuelAdd(int ammount)
    {
        if (ressourceManager.GetMoney() >= fuelPrice.currentFuelPrice)
        {
            ressourceManager.AddFuel(ammount);
            ressourceManager.RemoveMoney(fuelPrice.currentFuelPrice);
        }
        else
        {
            Debug.Log("Not enouth money");
        }

    }
}
