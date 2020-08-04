using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemProperties
    {
        public int ID;
        public string Name;
        public float Weight;
    }
    static public List<ItemProperties> Item = new List<ItemProperties>()
    {
        new ItemProperties(){ ID = 0, Name = "Spoon", Weight = 2},
        new ItemProperties(){ ID = 1, Name = "Knife", Weight = 3},
        new ItemProperties(){ ID = 2, Name = "Beaf", Weight = 10},
        new ItemProperties(){ ID = 3, Name = "Onion", Weight = 4},
        new ItemProperties(){ ID = 4, Name = "Paella", Weight = 8},
        new ItemProperties(){ ID = 5, Name = "Salmon", Weight = 10},
        new ItemProperties(){ ID = 6, Name = "Glass", Weight = 50},
        new ItemProperties(){ ID = 7, Name = "Spatula", Weight = 38},
        new ItemProperties(){ ID = 8, Name = "Bread", Weight = 5}
    };

    static public ItemProperties CloneItem(ItemProperties _item)
    {
        ItemProperties TempItem = new ItemProperties()
        {
            ID = _item.ID,
            Name = _item.Name,
            Weight = _item.Weight
        };

        return TempItem;
    }

    static public ItemProperties GetItemByID(int _id)
    {
        for (int i = 0; i < Item.Count; i++)
        {
            if(Item[i].ID == _id)
            {
                return CloneItem(Item[i]);
            }
        }
        return null;
    }
}
