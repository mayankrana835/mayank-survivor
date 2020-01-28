using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public RectTransform Content;
    public GameObject Item;
    public Backpack Backpack;
    public GameObject rawMeat;
    public GameObject pineapple;
    public GameObject gun;
    public GameObject bandaid;
    private GameObject rendered;
    public Text modelName;
    public Text desc;
    public Player player;
    public ItemUI j;
    public ItemUI k;
    void Start()
    {
    }
    public void Refresh()
    {
        while (Content.childCount > 0)
        {
            var child = Content.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        var items = Backpack.GetItems();
        foreach (var item in items)
        {
            var go = Instantiate(Item) as GameObject;
            go.transform.SetParent(Content);
            var itemUI = go.GetComponent<ItemUI>();
            itemUI.InIt(this, Backpack.GetItemData(item.Key), (item.Value));
            /*itemUI.ItemName = item.Key;
            itemUI.Icon.sprite = Backpack.GetSprite(item.Key);
            itemUI.Count.text = "x " + item.Value.ToString();*/
        }
    }
    public void Toggle()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            Refresh();
        }
        if(rendered)
            Destroy(rendered);
        modelName.text = "NA";
        desc.text = "NA";
    }

    public void Render3D(string name)
    {
        j.SetParent(this);
        k.SetParent(this);
        if (name == "Pineapple")
        {
            if (rendered)
                Destroy(rendered);
            rendered = Instantiate(pineapple) as GameObject;
            rendered.transform.position = new Vector3(0, -0.6f, 3.02f);
            var item = rendered.GetComponent<Item>();
            modelName.text = item.Data.Name;
            desc.text = item.Data.Description;
        }
        if (name == "Meat")
        {
            if (rendered)
                Destroy(rendered);
            rendered = Instantiate(rawMeat) as GameObject;
            rendered.transform.position = new Vector3(-0.5f, 0.45f, 3.8f);
            rendered.transform.Rotate(90f,90f,90f);
            var item = rendered.GetComponent<Item>();
            modelName.text = item.Data.Name;
            desc.text = item.Data.Description;
        }

        if (name == "Gun")
        {
            if (rendered)
                Destroy(rendered);
            rendered = Instantiate(gun) as GameObject;
            rendered.transform.position = new Vector3(0, 0f, 3.02f);
            var item = rendered.GetComponent<Item>();
            modelName.text = item.Data.Name;
            desc.text = item.Data.Description;
        }
        if (name == "Bandage")
        {
            if (rendered)
                Destroy(rendered);
            rendered = Instantiate(bandaid) as GameObject;
            rendered.transform.position = new Vector3(0, 0f, 3.02f);
            var item = rendered.GetComponent<Item>();
            modelName.text = item.Data.Name;
            desc.text = item.Data.Description;
        }
    }
    public void UseObj()
    {
        if (modelName.text == "NA")
            Debug.Log("No object selected to be used");
        else
        {
            var items = Backpack.GetItems();
            foreach (var item in items)
            {
                if (item.Key == modelName.text && item.Value > 0)
                {
                    player.Consume(rendered.GetComponent<Item>());
                    Backpack.ReduceWeight(rendered.GetComponent<Item>());
                    Backpack.ReduceCount(rendered.GetComponent<Item>());
                    Refresh();
                }
            }
        }
    }
    public void DropObj()
    {
        if (modelName.text == "NA")
            Debug.Log("No object selected to be dropped");
        if (modelName.text == "Pineapple")
        {
            var items = Backpack.GetItems();
            foreach (var item in items)
            {
                if (item.Key == modelName.text && item.Value > 0)
                {
                    player.Drop(pineapple);
                    Backpack.ReduceWeight(rendered.GetComponent<Item>());
                    Backpack.ReduceCount(rendered.GetComponent<Item>());
                    Refresh();
                }
            }
        }
        if (modelName.text == "Meat")
        {
            var items = Backpack.GetItems();
            foreach (var item in items)
            {
                if (item.Key == modelName.text && item.Value > 0)
                {
                    player.Drop(rawMeat);
                    Backpack.ReduceWeight(rendered.GetComponent<Item>());
                    Backpack.ReduceCount(rendered.GetComponent<Item>());
                    Refresh();
                }
            }
        }
        if (modelName.text == "Gun")
        {
            var items = Backpack.GetItems();
            foreach (var item in items)
            {
                //if (item.Key == modelName.text && item.Value > 0)
                {
                    player.Drop(gun);
                    Backpack.ReduceWeight(rendered.GetComponent<Item>());
                    Backpack.ReduceCount(rendered.GetComponent<Item>());
                    Refresh();
                }
            }
        }
        if (modelName.text == "Bandage")
        {
            var items = Backpack.GetItems();
            foreach (var item in items)
            {
                if (item.Key == modelName.text && item.Value > 0)
                {
                    player.Drop(bandaid);
                    Backpack.ReduceWeight(rendered.GetComponent<Item>());
                    Backpack.ReduceCount(rendered.GetComponent<Item>());
                    Refresh();
                }
            }
        }
    }
    private void Update()
    {
        if(rendered)
          rendered.transform.Rotate(0, 4* 10f * Time.deltaTime, 0);
    }
}
