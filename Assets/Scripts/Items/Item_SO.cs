using UnityEngine;

namespace NT
{
    public class Item_SO : ScriptableObject
    {
        [Header("Item Information")]
        public GameObject itemModelPrefab;
        public Sprite itemIcon;
        public string itemName;
        public int itemID;
    }
}