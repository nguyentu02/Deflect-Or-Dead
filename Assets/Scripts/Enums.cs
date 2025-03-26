using UnityEngine;

namespace NT
{
    public class Enums : MonoBehaviour
    {

    }

    public enum TeamID
    {
        Team01, //  TEAM PLAYER
        Team02, //  TEAM ENEMY
    }

    public enum WeaponInstantiateSlot
    {
        MainHandSlot,
        OffHandSlot,
        BackSlot,
        HipsSlot
    }

    public enum WeaponType
    {
        Melee_Weapon,
        Ranged_Weapon,
        Seal,
        Staff
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
        ChargeAttack01,
        ChargeAttack02,
        ChargeAttack03,
    }
}