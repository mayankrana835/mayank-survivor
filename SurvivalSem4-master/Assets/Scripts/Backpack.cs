using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Backpack : MonoBehaviour
{
    public float MaxWeight = 2.0f;
    private float currentWeight = 0;
    private Dictionary<string, int> mapNameToCount = new Dictionary<string, int>();
    private Dictionary<string, GameObject> mapNameToObject = new Dictionary<string, GameObject>();
    public bool AddItem(GameObject go)
    {
        var item = go.GetComponent<Item>();
        if (!item)
        {
            return false;
        }
        if ((item.Data.Weight + currentWeight) <= MaxWeight)
        {
            if (!mapNameToCount.ContainsKey(item.Data.Name))
            {
                mapNameToCount.Add(item.Data.Name, 0);
            }
            mapNameToCount[item.Data.Name]++;
            currentWeight += item.Data.Weight;
            if (!mapNameToObject.ContainsKey(item.Data.Name))
            {
                var tempGO = Instantiate(go) as GameObject;
                tempGO.SetActive(false);
                mapNameToObject.Add(item.Data.name, tempGO);
            }

            return true;
        }
        else
        {
            Debug.Log("Backpack full");// TODO @set reason for rejection
            // Backpack is full
            return false;
        }
    }
    public void ReduceCount(Item k)
    {
        mapNameToCount[k.Data.Name]--;
    }
    public void ReduceWeight(Item k)
    {
        currentWeight -= k.Data.Weight;
    }
    public Sprite GetSprite(string name)
    {
        if (mapNameToObject.ContainsKey(name))
            return mapNameToObject[name].GetComponent<Item>().Data.Sprite;

        return null;
    }

    public ItemData GetItemData(string name)
    {
        if (mapNameToObject.ContainsKey(name))
            return mapNameToObject[name].GetComponent<Item>().Data;

        return null;
    }
    public Dictionary<string, int> GetItems()
    {
        return mapNameToCount;
    }
}
