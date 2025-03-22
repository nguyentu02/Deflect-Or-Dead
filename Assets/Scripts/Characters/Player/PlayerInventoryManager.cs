using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        private PlayerManager player;

        [Header("Player Inventory")]
        public List<Item_SO> playerInventories = new List<Item_SO>();

        [Header("Player Weapon Inventory")]
        public List<Item_SO> playerWeaponInventories = new List<Item_SO>();

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            
        }
    }
}