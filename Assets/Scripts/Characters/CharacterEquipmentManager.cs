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

        [Header("Character Current Spell Item")]
        public SpellItem_SO currentSpellItem;

        [Header("Character Has Store Weapons In Quick Slots")]
        public WeaponItem_SO[] weaponsInMainHandQuickSlots;
        public WeaponItem_SO[] weaponsInOffHandQuickSlots;

        public int currentWeaponInMainHandIndex = 0;
        public int currentWeaponInOffHandIndex = 0;

        [Header("Character Equipment Hand Slots")]
        public WeaponInstantiateTransformWhenEquipped characterMainHand;
        [SerializeField] protected WeaponInstantiateTransformWhenEquipped characterOffHand;
        [SerializeField] protected WeaponInstantiateTransformWhenEquipped characterOffHand_Shield;
        [SerializeField] protected WeaponInstantiateTransformWhenEquipped characterBack;
        //  HIPS SLOT

        //  JUST DEBUG TEST DAMAGE COLLIDERS
        public DamageMasterCollider mainHandWeaponDamageCollider;
        public DamageMasterCollider offHandWeaponDamageCollider;

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

        public virtual void CharacterTryToPerformCastASpell()
        {
            if (currentSpellItem == null)
                return;

            currentSpellItem.TryToPerformCastASpell(character);
        }

        public virtual void CharacterSuccessfullyCastASpell()
        {
            if (currentSpellItem == null)
                return;

            currentSpellItem.SuccesfullyCastASpell(character);
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
                    case WeaponInstantiateSlot.OffHand_Shield_Slot:
                        characterOffHand_Shield = equipmentSlot;
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
                switch (weapon.weaponType)
                {
                    case WeaponType.Melee_Weapon:

                        characterOffHand.LoadWeaponPrefabModelInCharacterHand(weapon);

                        break;
                    case WeaponType.Shield_Weapon:

                        characterOffHand_Shield.LoadWeaponPrefabModelInCharacterHand(weapon);

                        break;
                    case WeaponType.Ranged_Weapon:

                        break;
                    case WeaponType.Seal:

                        characterOffHand.LoadWeaponPrefabModelInCharacterHand(weapon);

                        break;
                    case WeaponType.Staff:

                        characterOffHand.LoadWeaponPrefabModelInCharacterHand(weapon);

                        break;
                }

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

            //  LOAD DAMAGE TO WEAPON AFTER GET COMPONENT
            SetDamageForMainHandWeaponDamageColliderBasedOnWeaponItem(mainHandWeaponDamageCollider);
        }

        protected virtual void LoadDamageColliderOfCharacterOffHandWeapon()
        {
            //  DEBUG GET COMPONENT OF SHIELD, IF NOT SHIELD, JUST GET NORMAL
            if (currentWeaponHoldInOffHand.weaponClass != WeaponClass.MediumShield)
                offHandWeaponDamageCollider = characterOffHand.weaponPrefabInstantiatedInThisHand.
                    GetComponentInChildren<DamageMasterCollider>();
            //  OTHERWISE, WE GET COMPONENT IN SHIELD SLOT WE WAS INSTANTIATE A SHIELD
            else
                offHandWeaponDamageCollider = characterOffHand_Shield.weaponPrefabInstantiatedInThisHand.
                    GetComponentInChildren<DamageMasterCollider>();

            //  LOAD DAMAGE TO WEAPON AFTER GET COMPONENT
            SetDamageForOffHandWeaponDamageColliderBasedOnWeaponItem(offHandWeaponDamageCollider);
        }

        private void SetDamageForMainHandWeaponDamageColliderBasedOnWeaponItem(DamageMasterCollider damageCollider)
        {
            damageCollider.characterCausingDamage = character;

            damageCollider.weaponPhysicalDamage = currentWeaponHoldInMainHand.weaponPhysicalDamage;
            damageCollider.weaponMagicDamage = currentWeaponHoldInMainHand.weaponMagicDamage;
            damageCollider.weaponFireDamage = currentWeaponHoldInMainHand.weaponFireDamage;
            damageCollider.weaponHolyDamage = currentWeaponHoldInMainHand.weaponHolyDamage;
            damageCollider.weaponLightningDamage = currentWeaponHoldInMainHand.weaponLightningDamage;
        }

        private void SetDamageForOffHandWeaponDamageColliderBasedOnWeaponItem(DamageMasterCollider damageCollider)
        {
            damageCollider.characterCausingDamage = character;

            damageCollider.weaponPhysicalDamage = currentWeaponHoldInOffHand.weaponPhysicalDamage;
            damageCollider.weaponMagicDamage = currentWeaponHoldInOffHand.weaponMagicDamage;
            damageCollider.weaponFireDamage = currentWeaponHoldInOffHand.weaponFireDamage;
            damageCollider.weaponHolyDamage = currentWeaponHoldInOffHand.weaponHolyDamage;
            damageCollider.weaponLightningDamage = currentWeaponHoldInOffHand.weaponLightningDamage;
        }

        //  OPEN/CLOSE DAMAGE COLLIDERS
        //  MAIN HAND/RIGHT HAND
        public virtual void OpenMainHandWeaponDamageCollider()
        {
            character.characterCombatManager.isUsingMainHand = true;

            mainHandWeaponDamageCollider.EnableDamageCollider();
        }

        public virtual void CloseMainHandWeaponDamageCollider()
        {
            mainHandWeaponDamageCollider.DisableDamageCollider();
        }

        //  OFF HAND/LEFT HAND
        public virtual void OpenOffHandWeaponDamageCollider()
        {
            character.characterCombatManager.isUsingOffHand = true;

            offHandWeaponDamageCollider.EnableDamageCollider();
        }

        public virtual void CloseOffHandWeaponDamageCollider()
        {
            offHandWeaponDamageCollider.DisableDamageCollider();
        }
    }
}