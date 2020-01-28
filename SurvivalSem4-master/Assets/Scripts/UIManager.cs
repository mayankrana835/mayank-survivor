using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text hunger;
    public HealthSystem HealthSystem;
    public HungerSystem HungerSystem;
    public InventoryUI InventoryUI;
    void Start()
    {
    }
    void Update()
    {
        healthText.text = HealthSystem.GetHealth().ToString();
        hunger.text = HungerSystem.GetHungerLevel().ToString();
    }
    public void ToggleInventory()
    {
        InventoryUI.Toggle();
    }
}
