using UnityEngine;

namespace NT
{
    public class Enums : MonoBehaviour
    {

    }

    public enum WeaponType
    {
        Melee_Weapon,
        Ranged_Weapon
    }

    public enum WeaponClass
    {
        Unarmed,
        HandToHand,
        StraightSword,
        Katana
    }

    public enum CharacterAttackType
    {
        LightAttack01,
        LightAttack02,
        LightAttack03,
        HeavyAttack01,
        HeavyAttack02,
        HeavyAttack03,
    }
}