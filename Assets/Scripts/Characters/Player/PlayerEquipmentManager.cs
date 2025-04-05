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

            //  DEBUG ARMORS EQUIPPED
            EquipAllPlayerArmorsAtStart();
        }

        public void EquipAllPlayerArmorsAtStart()
        {
            EquipPlayerHelmetAtStart();
            EquipPlayerChestplateAtStart();
            EquipPlayerGauntletsAtStart();
            EquipPlayerGreavesAtStart();
        }

        //  PLAYER HELMET EQUIPMENT
        private void EquipPlayerHelmetAtStart()
        {
            helmetModelChanger.DisableAllGameObjectsInList();

            if (currentHelmetEquipped != null)
            {
                helmetModelChanger.EnableGameObjectWithStringName(currentHelmetEquipped.helmet_Name);

                player.playerStatusManager.characterPhysicalDamageAbsorptionOfHelmet = currentHelmetEquipped.armorPhysicalAbsorption;
                player.playerStatusManager.characterMagicDamageAbsorptionOfHelmet = currentHelmetEquipped.armorMagicAbsorption;
                player.playerStatusManager.characterFireDamageAbsorptionOfHelmet = currentHelmetEquipped.armorFireAbsorption;
                player.playerStatusManager.characterHolyDamageAbsorptionOfHelmet = currentHelmetEquipped.armorHolyAbsorption;
                player.playerStatusManager.characterLightningDamageAbsorptionOfHelmet = currentHelmetEquipped.armorLightningAbsorption;
            }
            else
            {
                nakedHeadGameObject.SetActive(true);
            }
        }

        //  PLAYER CHESTPLATE EQUIPMENT
        private void EquipPlayerChestplateAtStart()
        {
            chestplateModelChanger.DisableAllGameObjectsInList();
            upperArmModelChanger_L.DisableAllGameObjectsInList();
            upperArmModelChanger_R.DisableAllGameObjectsInList();

            if (currentChestplateEquipped != null)
            {
                chestplateModelChanger.EnableGameObjectWithStringName(currentChestplateEquipped.chestplate_Name);
                upperArmModelChanger_L.EnableGameObjectWithStringName(currentChestplateEquipped.upperArmL_Name);
                upperArmModelChanger_R.EnableGameObjectWithStringName(currentChestplateEquipped.upperArmR_Name);

                player.playerStatusManager.characterPhysicalDamageAbsorptionOfChestplate = currentChestplateEquipped.armorPhysicalAbsorption;
                player.playerStatusManager.characterMagicDamageAbsorptionOfChestplate = currentChestplateEquipped.armorMagicAbsorption;
                player.playerStatusManager.characterFireDamageAbsorptionOfChestplate = currentChestplateEquipped.armorFireAbsorption;
                player.playerStatusManager.characterHolyDamageAbsorptionOfChestplate = currentChestplateEquipped.armorHolyAbsorption;
                player.playerStatusManager.characterLightningDamageAbsorptionOfChestplate = currentChestplateEquipped.armorLightningAbsorption;
            }
            else
            {
                nakedBodyGameObject.SetActive(true);
                nakedUpperArmLGameObject.SetActive(true);
                nakedUpperArmRGameObject.SetActive(true);
            }
        }

        //  PLAYER HANDS EQUIPMENT
        private void EquipPlayerGauntletsAtStart()
        {
            lowerArmModelChanger_L.DisableAllGameObjectsInList();
            lowerArmModelChanger_R.DisableAllGameObjectsInList();
            gauntletsModelChanegr_L.DisableAllGameObjectsInList();
            gauntletsModelChanegr_R.DisableAllGameObjectsInList();

            if (currentGauntletsEquipped != null)
            {
                lowerArmModelChanger_L.EnableGameObjectWithStringName(currentGauntletsEquipped.lowerArmL_Name);
                lowerArmModelChanger_R.EnableGameObjectWithStringName(currentGauntletsEquipped.lowerArmR_Name);
                gauntletsModelChanegr_L.EnableGameObjectWithStringName(currentGauntletsEquipped.gauntletsL_Name);
                gauntletsModelChanegr_R.EnableGameObjectWithStringName(currentGauntletsEquipped.gauntletsR_Name);

                player.playerStatusManager.characterPhysicalDamageAbsorptionOfGauntlets = currentGauntletsEquipped.armorPhysicalAbsorption;
                player.playerStatusManager.characterMagicDamageAbsorptionOfGauntlets = currentGauntletsEquipped.armorMagicAbsorption;
                player.playerStatusManager.characterFireDamageAbsorptionOfGauntlets = currentGauntletsEquipped.armorFireAbsorption;
                player.playerStatusManager.characterHolyDamageAbsorptionOfGauntlets = currentGauntletsEquipped.armorHolyAbsorption;
                player.playerStatusManager.characterLightningDamageAbsorptionOfGauntlets = currentGauntletsEquipped.armorLightningAbsorption;
            }
            else
            {
                nakedLowerArmLGameObject.SetActive(true);
                nakedLowerArmRGameObject.SetActive(true);
                nakedHandLGameObject.SetActive(true);
                nakedHandRGameObject.SetActive(true);
            }
        }

        //  PLAYER LEGS EQUIPMENT
        private void EquipPlayerGreavesAtStart()
        {
            hipsModelChanger.DisableAllGameObjectsInList();
            greavesModelChanger_L.DisableAllGameObjectsInList();
            greavesModelChanger_R.DisableAllGameObjectsInList();

            if (currentGauntletsEquipped != null)
            {
                hipsModelChanger.EnableGameObjectWithStringName(currentGreavesEquipped.hips_Name);
                greavesModelChanger_L.EnableGameObjectWithStringName(currentGreavesEquipped.legL_Name);
                greavesModelChanger_R.EnableGameObjectWithStringName(currentGreavesEquipped.legR_Name);

                player.playerStatusManager.characterPhysicalDamageAbsorptionOfGreaves = currentGreavesEquipped.armorPhysicalAbsorption;
                player.playerStatusManager.characterMagicDamageAbsorptionOfGreaves = currentGreavesEquipped.armorMagicAbsorption;
                player.playerStatusManager.characterFireDamageAbsorptionOfGreaves = currentGreavesEquipped.armorFireAbsorption;
                player.playerStatusManager.characterHolyDamageAbsorptionOfGreaves = currentGreavesEquipped.armorHolyAbsorption;
                player.playerStatusManager.characterLightningDamageAbsorptionOfGreaves = currentGreavesEquipped.armorLightningAbsorption;
            }
            else
            {
                nakedHipsGameObject.SetActive(true);
                nakedLegLGameObject.SetActive(true);
                nakedLegRGameObject.SetActive(true);
            }
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