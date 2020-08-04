using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryControl : MonoBehaviour
{
    [System.Serializable]
    public class InventoryProperties
    {
        public ItemManager.ItemProperties Item;
        public int Amount;

        public InventoryProperties(ItemManager.ItemProperties _item, int _amount)
        {
            Item = _item;
            Amount = _amount;
        }
    }
    public List<InventoryProperties> Inventory;

    public Transform GridInventory;
    public GameObject ItemInfo;

    // Use this for initialization
    void Start()
    {
        

        LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) { AddItem(0, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) { AddItem(1, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { AddItem(2, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { AddItem(3, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { AddItem(4, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { AddItem(5, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) { AddItem(6, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) { AddItem(7, 1); }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) { AddItem(8, 1); }
    }

    private void AddItem(int _id, int _amount)
    {
        if (HasItem(_id) == true)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].Item.ID == _id)
                {
                    Inventory[i].Amount += _amount;
                    break;
                }
            }
        }
        else
            Inventory.Add(new InventoryProperties(ItemManager.GetItemByID(_id), _amount));

        PrintInventory();
    }

    private void PrintInventory()
    {
        for (int i = GridInventory.childCount - 1; i >= 0; i--)
        {
            Destroy(GridInventory.GetChild(i).gameObject);
        }

        for (int i = 0; i < Inventory.Count; i++)
        {
            GameObject NewItem = Instantiate(ItemInfo, GridInventory);
            NewItem.transform.Find("Name").GetComponent<Text>().text = Inventory[i].Item.Name;
            NewItem.transform.Find("Amount").GetComponent<Text>().text = "x" + Inventory[i].Amount.ToString();
            NewItem.transform.Find("Peso").GetComponent<Text>().text = (Inventory[i].Item.Weight * Inventory[i].Amount).ToString() + " Kg";

            int TempID = Inventory[i].Item.ID;
            NewItem.transform.Find("Delete").GetComponent<Button>().onClick.AddListener(delegate { DeleteItem(TempID); });

            NewItem.transform.Find("Preview").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + TempID);
        }
    }

    private bool HasItem(int _id)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Item.ID == _id)
            {
                return true;
            }
        }

        return false;
    }

    private void DeleteItem(int _id)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Item.ID == _id)
            {
                Inventory[i].Amount--;
                if (Inventory[i].Amount == 0)
                    Inventory.RemoveAt(i);

                break;

            }
        }
        PrintInventory();
    }

    public void SaveInventory()
    {
        int[] TempID = new int[Inventory.Count];
        int[] TempAmount = new int[Inventory.Count];
        for (int i = 0; i < Inventory.Count; i++)
        {
            TempID[i] = Inventory[i].Item.ID;
            TempAmount[i] = Inventory[i].Amount;
        }
        PlayerPrefsX.SetIntArray("InventoryID", TempID);
        PlayerPrefsX.SetIntArray("InventoryAmount", TempAmount);
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey("InventoryID") == true)
        {
            int[] TempID = PlayerPrefsX.GetIntArray("InventoryID");
            int[] TempAmount = PlayerPrefsX.GetIntArray("InventoryAmount");

            for (int i = 0; i < TempID.Length; i++)
            {
                AddItem(TempID[i], TempAmount[i]);
            }
        }
    }
}
