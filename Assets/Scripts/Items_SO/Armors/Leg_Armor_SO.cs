using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Armors/Greaves")]
    public class Leg_Armor_SO : ArmorItem_SO
    {
        [Header("Greaves Model Name")]
        public string hips_Name = "";
        public string legL_Name = "";
        public string legR_Name = "";
    }
}