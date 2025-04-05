using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Armors/Gauntlets")]
    public class Hand_Armor_SO : ArmorItem_SO
    {
        [Header("Gauntlets Model Name")]
        public string lowerArmL_Name = "";
        public string lowerArmR_Name = "";
        public string gauntletsL_Name = "";
        public string gauntletsR_Name = "";
    }
}