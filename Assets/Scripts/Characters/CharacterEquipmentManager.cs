using UnityEngine;

namespace NT
{
    public class CharacterEquipmentManager : MonoBehaviour
    {
        private CharacterManager character;

        public WeaponItem_SO unarmed_No_Weapon;

        [Header("Character Current Weapon Hold In Hands")]
        public WeaponItem_SO currentWeaponHoldInMainHand;
        public WeaponItem_SO currentWeaponHoldInOffHand;

        [Header("Character Has Store Weapons In Quick Slots")]
        public WeaponItem_SO[] weaponsInMainHandQuickSlots;
        public WeaponItem_SO[] weaponsInOffHandQuickSlots;

        [SerializeField] private int currentWeaponInMainHandIndex = 0;
        [SerializeField] private int currentWeaponInOffHandIndex = 0;

        [Header("Character Equipment Hand Slots")]
        protected WeaponInstantiateTransformWhenEquipped characterMainHand;
        protected WeaponInstantiateTransformWhenEquipped characterOffHand;

        //  JUST DEBUG TEST DAMAGE COLLIDERS
        private DamageMasterCollider mainHandWeaponDamageCollider;
        private DamageMasterCollider offHandWeaponDamageCollider;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();

            GetCharacterEquipmentSlotAtStart();
        }

        protected virtual void Start()
        {
            currentWeaponHoldInMainHand = unarmed_No_Weapon;
            currentWeaponHoldInOffHand = unarmed_No_Weapon;
        }

        public virtual void GetCharacterEquipmentSlotAtStart()
        {
            WeaponInstantiateTransformWhenEquipped[] equipmentSlots = GetComponentsInChildren<WeaponInstantiateTransformWhenEquipped>();

            foreach (var equipmentSlot in equipmentSlots)
            {
                if (equipmentSlot.isMainHand)
                    characterMainHand = equipmentSlot;
                else if (!equipmentSlot.isMainHand)
                    characterOffHand = equipmentSlot;
            }
        }

        public virtual void CharacterSwitchMainHandWeapon()
        {
            currentWeaponInMainHandIndex += 1;

            if (currentWeaponInMainHandIndex == 0 && weaponsInMainHandQuickSlots[0] != null)
            {
                currentWeaponHoldInMainHand = weaponsInMainHandQuickSlots[currentWeaponInMainHandIndex];
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);
            }
            else if (currentWeaponInMainHandIndex == 0 && weaponsInMainHandQuickSlots[0] == null)
            {
                currentWeaponInMainHandIndex += 1;
            }
            else if (currentWeaponInMainHandIndex == 1 && weaponsInMainHandQuickSlots[1] != null)
            {
                currentWeaponHoldInMainHand = weaponsInMainHandQuickSlots[currentWeaponInMainHandIndex];
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);
            }
            else
            {
                currentWeaponInMainHandIndex += 1;
            }

            if (currentWeaponInMainHandIndex > weaponsInMainHandQuickSlots.Length - 1)
            {
                currentWeaponInMainHandIndex = -1;
                currentWeaponHoldInMainHand = unarmed_No_Weapon;
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);
            }
        }

        public virtual void CharacterSwitchOffHandWeapon()
        {
            currentWeaponInOffHandIndex += 1;

            if (currentWeaponInOffHandIndex == 0 && weaponsInOffHandQuickSlots[0] != null)
            {
                currentWeaponHoldInOffHand = weaponsInOffHandQuickSlots[currentWeaponInOffHandIndex];
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInOffHand, false);
            }
            else if (currentWeaponInOffHandIndex == 0 && weaponsInOffHandQuickSlots[0] == null)
            {
                currentWeaponInOffHandIndex += 1;
            }
            else if (currentWeaponInOffHandIndex == 1 && weaponsInOffHandQuickSlots[1] != null)
            {
                currentWeaponHoldInOffHand = weaponsInOffHandQuickSlots[currentWeaponInOffHandIndex];
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInOffHand, false);
            }
            else
            {
                currentWeaponInOffHandIndex += 1;
            }

            if (currentWeaponInOffHandIndex > weaponsInOffHandQuickSlots.Length - 1)
            {
                currentWeaponInOffHandIndex = -1;
                currentWeaponHoldInOffHand = unarmed_No_Weapon;
                WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInOffHand, false);
            }
        }

        public virtual void WhichCharacterHandWeWantToLoadWeaponIn(WeaponItem_SO weapon, bool isMainHand)
        {
            if (isMainHand)
            {
                characterMainHand.LoadWeaponPrefabModelInCharacterHand(weapon);
                LoadDamageColliderOfCharacterMainHandWeapon();
            }
            else
            {
                characterOffHand.LoadWeaponPrefabModelInCharacterHand(weapon);
                LoadDamageColliderOfCharacterOffHandWeapon();
            }
        }

        //  JUST DEBUG TEST FOR NOW, TESTING DAMAGE COLLIDERS
        protected virtual void LoadDamageColliderOfCharacterMainHandWeapon()
        {
            mainHandWeaponDamageCollider = characterMainHand.weaponPrefabInstantiatedInThisHand.
                GetComponentInChildren<DamageMasterCollider>();
        }

        protected virtual void LoadDamageColliderOfCharacterOffHandWeapon()
        {
            offHandWeaponDamageCollider = characterOffHand.weaponPrefabInstantiatedInThisHand.
                GetComponentInChildren<DamageMasterCollider>();
        }

        //  OPEN/CLOSE DAMAGE COLLIDERS
        //  MAIN HAND/RIGHT HAND
        public virtual void OpenMainHandWeaponDamageCollider()
        {
            mainHandWeaponDamageCollider.EnableDamageCollider();
        }

        public virtual void CloseMainHandWeaponDamageCollider()
        {
            mainHandWeaponDamageCollider.DisableDamageCollider();
        }

        //  OFF HAND/LEFT HAND
        public virtual void OpenOffHandWeaponDamageCollider()
        {
            offHandWeaponDamageCollider.EnableDamageCollider();
        }

        public virtual void CloseOffHandWeaponDamageCollider()
        {
            offHandWeaponDamageCollider.DisableDamageCollider();
        }
    }
}