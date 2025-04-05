using UnityEngine;

namespace NT
{
    public class ArmorItem_SO : Item_SO
    {
        [Header("Armor Defense Absorptions")]
        public float armorPhysicalAbsorption;
        public float armorMagicAbsorption;
        public float armorFireAbsorption;
        public float armorHolyAbsorption;
        public float armorLightningAbsorption;
    }
}