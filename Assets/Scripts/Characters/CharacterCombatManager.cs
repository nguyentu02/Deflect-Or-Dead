using UnityEngine;

namespace NT
{
    public class CharacterCombatManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Lock On Transform")]
        public CharacterManager currentLockedOnTarget;
        public Transform lockOnTransform;

        [Header("Character Weapon Being Used For Attack")]
        public WeaponItem_SO currentWeaponCharacterUsingForAttack;

        [Header("Character Attack Type")]
        public CharacterAttackType attackType;

        [Header("Last Attack Character Performed")]
        public string lastAttackAimationCharacterPerformed;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void CharacterPerformComboAttack(WeaponItem_SO weapon)
        {
            character.canDoComboAttack = false;

            CharacterPerformLightAttackCombo(weapon);
            CharacterPerformHeavyAttackCombo(weapon);

        }

        public virtual void CharacterPerformLightAttack(WeaponItem_SO weapon)
        {
            character.characterAnimationManager.CharacterPlayAttackAnimation
                (weapon.oh_Light_Attack_01,weapon, CharacterAttackType.LightAttack01, true, true);
            lastAttackAimationCharacterPerformed = weapon.oh_Light_Attack_01;
        }

        public virtual void CharacterPerformHeavyAttack(WeaponItem_SO weapon)
        {
            character.characterAnimationManager.CharacterPlayAttackAnimation
                (weapon.oh_Heavy_Attack_01, weapon, CharacterAttackType.HeavyAttack01, true, true);
            lastAttackAimationCharacterPerformed = weapon.oh_Heavy_Attack_01;
        }

        public virtual void CharacterPerformLightAttackCombo(WeaponItem_SO weapon)
        {
            if (lastAttackAimationCharacterPerformed == weapon.oh_Light_Attack_01)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Light_Attack_02, weapon, CharacterAttackType.LightAttack02, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Light_Attack_02;
            }
            else if (lastAttackAimationCharacterPerformed == weapon.oh_Light_Attack_02)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Light_Attack_03, weapon, CharacterAttackType.LightAttack03, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Light_Attack_03;
            }
        }

        public virtual void CharacterPerformHeavyAttackCombo(WeaponItem_SO weapon)
        {
            if (lastAttackAimationCharacterPerformed == weapon.oh_Heavy_Attack_01)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Heavy_Attack_02, weapon, CharacterAttackType.HeavyAttack02, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Heavy_Attack_02;
            }
            else if (lastAttackAimationCharacterPerformed == weapon.oh_Heavy_Attack_02)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Heavy_Attack_03, weapon, CharacterAttackType.HeavyAttack03, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Heavy_Attack_03;
            }
        }

        public virtual void EnableCanDoComboAttack()
        {
            character.canDoComboAttack = true;
        }

        public virtual void DisableCanDoComboAttack()
        {
            character.canDoComboAttack = false;
        }

        public virtual void DrainStaminaBasedOnCharacterAttackType()
        {
            switch (attackType)
            {
                case CharacterAttackType.LightAttack01:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost * 
                        currentWeaponCharacterUsingForAttack.oh_Light_Attack_01_Stamina_Multiplier);

                    break;
                case CharacterAttackType.LightAttack02:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Light_Attack_02_Stamina_Multiplier);

                    break;
                case CharacterAttackType.LightAttack03:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Light_Attack_03_Stamina_Multiplier);

                    break;
                case CharacterAttackType.HeavyAttack01:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Heavy_Attack_01_Stamina_multiplier);

                    break;
                case CharacterAttackType.HeavyAttack02:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Heavy_Attack_02_Stamina_multiplier);

                    break;
                case CharacterAttackType.HeavyAttack03:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Heavy_Attack_03_Stamina_multiplier);

                    break;
                default:
                    break;
            }

            PlayerCanvasManager.instance.playerStaminaPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (character.characterStatusManager.characterCurrentStamina);
        }
    }
}