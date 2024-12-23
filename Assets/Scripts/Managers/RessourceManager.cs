using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RessourceManager : MonoBehaviour
{
    public static RessourceManager instance { get; private set; }

    [SerializeField] private int Money;
    [SerializeField] private float fuel;
    [SerializeField] private int Ore;

    private List<Planet> planets = new List<Planet>();

    private int autoClickerCost = 2;
    private float autoClickerMultiplier = 1f; 
    private float baseClickerValue = 1f;
    public int marketingLevel = 1;
    public float marketingMultiplier = 1f;

    private float prodPerSec = 0f;
    private float salesPerSec = 0f;

    public event Action OnRessourceChanged;

    public int GetMoney() => Money;
    public float GetFuel() => fuel;
    public int GetOre() => Ore;
    public int GetAutoClickerCost() => autoClickerCost;
    public float GetBaseAutoClickerValue() => baseClickerValue;

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
        
        InvokeRepeating("ResetProdPerSec", 0, 1);
    }

    private void Start()
    {
        planets = new List<Planet>(GameObject.FindObjectsByType<Planet>(FindObjectsInactive.Include, FindObjectsSortMode.None));
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnRessourceChanged?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            OnRessourceChanged?.Invoke();
        }
        OnRessourceChanged?.Invoke();
    }

    public void GenerateOreAutoclick()
    {
        int autominerCount = 0;
        foreach (Planet planet in planets)
        {
            if (fuel > planet.GetFuelConsumption() * UpgradeManager.instance.FuelEfficiencyMultiplier)
            {
                AddOre(Mathf.FloorToInt(planet.GetAutominerProduction() * UpgradeManager.instance.AutoMinerMultiplier));
                RemoveFuel(planet.GetFuelConsumption() * UpgradeManager.instance.FuelEfficiencyMultiplier);
            }
        }
    }

    public void AddFuel(int amount)
    {
        fuel += amount;
        OnRessourceChanged?.Invoke();
    }

    public void RemoveFuel(float amount)
    {
        if (fuel >= amount)
        {
            fuel -= amount;
            OnRessourceChanged?.Invoke();
        }
    }

    public void AddOre(int amount)
    {
        Ore += amount;
        prodPerSec += amount;
        OnRessourceChanged?.Invoke();
    }

    public void RemoveOre(int amount)
    {
        if (Ore >= amount)
        {
            Ore -= amount;
            OnRessourceChanged?.Invoke();
        }
    }

    public int GetTotalAutoClickers()
    {
        int total = 0; 
        foreach (var planet in planets)
        {
            if (planet.unlocked) 
            {
                total += planet.GetLocalAutoClickers();
            }
        }
        return total;
    }

    private void ResetProdPerSec()
    {
        prodPerSec = 0f;
    }
    public float GetProdPerSec()
    {
        return prodPerSec;
    }

    public void RegisterPlanet(Planet planet)
    {
        if (!planets.Contains(planet))
        {
            planets.Add(planet);
        }
    }
}
