using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health = 100;
    public int GetHealth()
    {
        return health;
    }
    public void IncreaseHealth(int factor)
    {
        health = Mathf.Clamp(health + factor, 0, 100);
    }
    public void DecreaseHealth(int factor)
    {
        health = Mathf.Clamp(health - factor, 0, 100);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    /*public void Collect()
    {
        Player.HungerLevel -= 10;
        Player.DestroyFood = true;
        Destroy(Player.collided);
        Player.collided = null;
    }*/// Update is called once per frame
    void Update()
    {
        
    }
}
