using UnityEngine;

namespace NT
{
    public class CharacterEquipmentManager : MonoBehaviour
    {
        private CharacterManager character;

        public WeaponItem_SO unarmed_No_Weapon;

        [Header("Character Current Weapon Hold In Hands")]
        public WeaponItem_SO currentTwoHandingWeapon;
        public WeaponItem_SO currentWeaponHoldInMainHand;
        public WeaponItem_SO currentWeaponHoldInOffHand;

        [Header("Character Has Store Weapons In Quick Slots")]
        public WeaponItem_SO[] weaponsInMainHandQuickSlots;
        public WeaponItem_SO[] weaponsInOffHandQuickSlots;

        public int currentWeaponInMainHandIndex = 0;
        public int currentWeaponInOffHandIndex = 0;

        [Header("Character Equipment Hand Slots")]
        protected WeaponInstantiateTransformWhenEquipped characterMainHand;
        protected WeaponInstantiateTransformWhenEquipped characterOffHand;
        protected WeaponInstantiateTransformWhenEquipped characterBack;
        //  HIPS SLOT

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
            currentWeaponInMainHandIndex = 0;
            currentWeaponInOffHandIndex = 0;

            currentWeaponHoldInMainHand = weaponsInMainHandQuickSlots[currentWeaponInMainHandIndex];
            currentWeaponHoldInOffHand = weaponsInOffHandQuickSlots[currentWeaponInOffHandIndex];

            //  LOAD WEAPON INTO RIGHT HAND
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);

            //  LOAD WEAPON INTO LEFT HAND
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInOffHand, false);
        }

        public virtual void GetCharacterEquipmentSlotAtStart()
        {
            WeaponInstantiateTransformWhenEquipped[] equipmentSlots = GetComponentsInChildren<WeaponInstantiateTransformWhenEquipped>();

            foreach (var equipmentSlot in equipmentSlots)
            {
                switch (equipmentSlot.weaponInstantiateSlot)
                {
                    case WeaponInstantiateSlot.MainHandSlot:
                        characterMainHand = equipmentSlot;
                        break;
                    case WeaponInstantiateSlot.OffHandSlot:
                        characterOffHand = equipmentSlot;
                        break;
                    case WeaponInstantiateSlot.BackSlot:
                        characterBack = equipmentSlot;
                        break;
                    default:
                        break;
                }
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

        public virtual void CharacterTwoHandingMainWeapon()
        {
            if (!character.isTwoHanding)
                return;

            character.isTwoHanding_MainWeapon = true;

            currentTwoHandingWeapon = currentWeaponHoldInMainHand;

            characterOffHand.UnloadWeaponPrefab();
            characterMainHand.LoadWeaponPrefabModelInCharacterHand(currentTwoHandingWeapon);

            LoadDamageColliderOfCharacterMainHandWeapon();

            WhichSlotToMoveCharacterAnOtherWeaponAfterPickAnWeaponForTwoHandingStyle
                (currentWeaponHoldInOffHand, true);
        }

        public virtual void CharacterTwoHandingOffWeapon()
        {
            if (!character.isTwoHanding)
                return;

            character.isTwoHanding_OffWeapon = true;

            currentTwoHandingWeapon = currentWeaponHoldInOffHand;

            characterOffHand.UnloadWeaponPrefab();
            characterMainHand.LoadWeaponPrefabModelInCharacterHand(currentTwoHandingWeapon);

            LoadDamageColliderOfCharacterMainHandWeapon();

            WhichSlotToMoveCharacterAnOtherWeaponAfterPickAnWeaponForTwoHandingStyle
                (currentWeaponHoldInMainHand, true);
        }

        public virtual void CharacterUnTwoHandingWeapon()
        {
            character.isTwoHanding = false;
            character.isTwoHanding_MainWeapon = false;
            character.isTwoHanding_OffWeapon = false;

            characterBack.UnloadWeaponPrefab();
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, false);
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

        public virtual void WhichSlotToMoveCharacterAnOtherWeaponAfterPickAnWeaponForTwoHandingStyle
            (WeaponItem_SO weapon, bool isBack)
        {
            if (isBack)
            {
                characterBack.LoadWeaponPrefabModelInCharacterHand(weapon);
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