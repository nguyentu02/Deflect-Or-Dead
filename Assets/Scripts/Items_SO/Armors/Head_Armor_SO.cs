using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Armors/Helmet")]
    public class Head_Armor_SO : ArmorItem_SO
    {
        [Header("Helmet Model Name")]
        public string helmet_Name = "";
    }
}