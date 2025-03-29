using UnityEngine;

namespace NT
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            //  RIGHT HAND
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInMainHand, true);

            //  LEFT HAND
            WhichCharacterHandWeWantToLoadWeaponIn(currentWeaponHoldInOffHand, false);
        }

        public override void WhichCharacterHandWeWantToLoadWeaponIn(WeaponItem_SO weapon, bool isMainHand)
        {
            if (isMainHand)
            {
                characterMainHand.LoadWeaponPrefabModelInCharacterHand(weapon);
                LoadDamageColliderOfCharacterMainHandWeapon();
                PlayerCanvasManager.instance.UpdatePlayerQuickSlotsIconImage_GUI(weapon, isMainHand);
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
                PlayerCanvasManager.instance.UpdatePlayerQuickSlotsIconImage_GUI(weapon, isMainHand);
            }
        }
    }
}