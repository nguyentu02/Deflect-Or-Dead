using UnityEngine;

namespace NT
{
    public class WeaponItem_SO : Item_SO
    {
        [Header("Weapon Animator Override Controller")]
        public AnimatorOverrideController weaponAnimator;

        [Header("Weapon Type")]
        public WeaponClass weaponClass;
        public WeaponType weaponType;

        [Header("Weapon Attack Animations")]
        public string oh_Light_Attack_01;
        public string oh_Light_Attack_02;
        public string oh_Light_Attack_03;

        public string oh_Heavy_Attack_01;
        public string oh_Heavy_Attack_02;
        public string oh_Heavy_Attack_03;

        public string th_Light_Attack_01;
        public string th_Light_Attack_02;
        public string th_Light_Attack_03;

        public string th_Heavy_Attack_01;
        public string th_Heavy_Attack_02;
        public string th_Heavy_Attack_03;

        [Header("Weapon Stamina Costs")]
        public float baseStaminaCost = 10f;

        public float oh_Light_Attack_01_Stamina_Multiplier = 1.0f;
        public float oh_Light_Attack_02_Stamina_Multiplier = 1.15f;
        public float oh_Light_Attack_03_Stamina_Multiplier = 1.3f;

        public float oh_Heavy_Attack_01_Stamina_multiplier = 1.5f;
        public float oh_Heavy_Attack_02_Stamina_multiplier = 1.7f;
        public float oh_Heavy_Attack_03_Stamina_multiplier = 1.9f;

        public float oh_Charge_Attack_01_Stamina_multiplier = 2.0f;
        public float oh_Charge_Attack_02_Stamina_multiplier = 2.25f;
        public float oh_Charge_Attack_03_Stamina_multiplier = 2.5f;
    }
}