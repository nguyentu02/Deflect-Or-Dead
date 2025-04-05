using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Armors/Chestplate")]
    public class Body_Armor_SO : ArmorItem_SO
    {
        [Header("Chestplate Model Name")]
        public string chestplate_Name = "";
        public string upperArmL_Name = "";
        public string upperArmR_Name = "";
    }
}