using UnityEngine;

namespace NT
{
    public class CharacterCombatManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Lock On Transform")]
        public CharacterManager currentTargetCharacter;
        public Transform lockOnTransform;

        [Header("DEBUG_Character Combat Systems")]
        public LayerMask DEBUG_critical_Layer;
        public Transform DEBUG_CriticalAttackRayStartPosition;
        [SerializeField] Transform DEBUG_BackstabStandingPosition;
        [SerializeField] Transform DEBUG_RiposteStandingPosition;
        [SerializeField] float DEBUG_PendingCriticalDamage = 0f;
        [SerializeField] float DEBUG_dotValue;

        [Header("Character Combat Status")]
        public bool isDeflect = false;
        public bool isParrying = false;
        public bool isStanceBreak = false;
        public bool isCanBeBackstabbed = true;
        public bool isCanBeRiposted = false;
        public bool isUsingMainHand = false;
        public bool isUsingOffHand = false;
        public bool isRiposting = false;
        public bool isBackstabbing = false;
        public bool isBeingRiposted = false;
        public bool isBeingBackstabbed = false;
        public bool DEBUG_isAlreadyHasOffHandWeapon = false;

        [Header("Character Weapon Being Used For Attack")]
        public WeaponItem_SO currentWeaponCharacterUsingForAttack;

        [Header("Character Attack Type")]
        public CharacterAttackType attackType;

        [Header("Last Attack Character Performed")]
        public string lastAttackAimationCharacterPerformed;

        [Header("DEBUG Character Deflecting")]
        public float DEBUG_maxTimeDeflectPossibleBeforeBeingDefense = 0.23f;
        public float DEBUG_deflectTimeCount = 0f;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void CharacterPerformComboAttack(WeaponItem_SO weapon)
        {
            //  IF WE OUT OF STAMINA, WE CAN'T DO ANYTHING
            if (character.characterStatusManager.characterCurrentStamina <= 0)
                return;

            character.canDoComboAttack = false;

            CharacterPerformLightAttackCombo(weapon);
            CharacterPerformHeavyAttackCombo(weapon);
        }

        public virtual void CharacterPerformLightAttack(WeaponItem_SO weapon)
        {
            //  IF WE OUT OF STAMINA, WE CAN'T DO ANYTHING
            if (character.characterStatusManager.characterCurrentStamina <= 0)
                return;

            CharacterPerformCriticalAttack(weapon);

            //  IF CAN CRITICAL HIT, DON'T DO NORMAL ATTACK
            if (isBackstabbing)
                return;

            //  IF CAN CRITICAL HIT, DON'T DO NORMAL ATTACK
            if (isRiposting)
                return;

            if (character.isTwoHanding)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.th_Light_Attack_01, weapon, CharacterAttackType.LightAttack01, true, true);
                lastAttackAimationCharacterPerformed = weapon.th_Light_Attack_01;
            }
            else
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Light_Attack_01, weapon, CharacterAttackType.LightAttack01, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Light_Attack_01;
            }
        }

        public virtual void CharacterPerformHeavyAttack(WeaponItem_SO weapon)
        {
            //  IF WE OUT OF STAMINA, WE CAN'T DO ANYTHING
            if (character.characterStatusManager.characterCurrentStamina <= 0)
                return;

            //  IF CAN CRITICAL HIT, DON'T DO NORMAL ATTACK
            if (isBackstabbing)
                return;

            //  IF CAN CRITICAL HIT, DON'T DO NORMAL ATTACK
            if (isRiposting)
                return;

            if (character.isTwoHanding)
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.th_Heavy_Attack_01, weapon, CharacterAttackType.HeavyAttack01, true, true);
                lastAttackAimationCharacterPerformed = weapon.th_Heavy_Attack_01;
            }
            else
            {
                character.characterAnimationManager.CharacterPlayAttackAnimation
                    (weapon.oh_Heavy_Attack_01, weapon, CharacterAttackType.HeavyAttack01, true, true);
                lastAttackAimationCharacterPerformed = weapon.oh_Heavy_Attack_01;
            }
        }

        public virtual void CharacterPerformLightAttackCombo(WeaponItem_SO weapon)
        {
            if (character.isTwoHanding)
            {
                if (lastAttackAimationCharacterPerformed == weapon.th_Light_Attack_01)
                {
                    character.characterAnimationManager.CharacterPlayAttackAnimation
                        (weapon.th_Light_Attack_02, weapon, CharacterAttackType.LightAttack02, true, true);
                    lastAttackAimationCharacterPerformed = weapon.th_Light_Attack_02;
                }
                else if (lastAttackAimationCharacterPerformed == weapon.th_Light_Attack_02)
                {
                    character.characterAnimationManager.CharacterPlayAttackAnimation
                        (weapon.th_Light_Attack_03, weapon, CharacterAttackType.LightAttack03, true, true);
                    lastAttackAimationCharacterPerformed = weapon.th_Light_Attack_03;
                }
            }
            else
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
        }

        public virtual void CharacterPerformHeavyAttackCombo(WeaponItem_SO weapon)
        {
            if (character.isTwoHanding)
            {
                if (lastAttackAimationCharacterPerformed == weapon.th_Heavy_Attack_01)
                {
                    character.characterAnimationManager.CharacterPlayAttackAnimation
                        (weapon.th_Heavy_Attack_02, weapon, CharacterAttackType.HeavyAttack02, true, true);
                    lastAttackAimationCharacterPerformed = weapon.th_Heavy_Attack_02;
                }
                else if (lastAttackAimationCharacterPerformed == weapon.th_Heavy_Attack_02)
                {
                    character.characterAnimationManager.CharacterPlayAttackAnimation
                        (weapon.th_Heavy_Attack_03, weapon, CharacterAttackType.HeavyAttack03, true, true);
                    lastAttackAimationCharacterPerformed = weapon.th_Heavy_Attack_03;
                }
            }
            else
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
        }

        public virtual void CharacterPerformCriticalAttack(WeaponItem_SO weapon)
        {
            RaycastHit hit;

            if (Physics.Raycast(DEBUG_CriticalAttackRayStartPosition.position, 
                transform.TransformDirection(Vector3.forward)
                , out hit, 0.77f, DEBUG_critical_Layer))
            {
                CharacterManager characterDamaged = hit.transform.gameObject.GetComponent<CharacterManager>();
                Vector3 directionFromCharacterToTargetCharacter = 
                    transform.position - characterDamaged.transform.position;
                DEBUG_dotValue = Vector3.Dot
                    (directionFromCharacterToTargetCharacter, characterDamaged.transform.forward);

                if (characterDamaged.characterCombatManager.isCanBeRiposted)
                {
                    if (DEBUG_dotValue < 1.22f && DEBUG_dotValue > 0.66f)
                    {
                        //ATTEMPT RIPOSTE
                        AttemptToPerformRiposteStrike(hit, characterDamaged);
                        return;
                    }
                }

                if (DEBUG_dotValue > -0.77f && DEBUG_dotValue < 0.55f)
                {
                    //ATTEMPT BACKSTAB
                    AttemptToPerformBackstabStrike(hit, characterDamaged);
                }
            }
        }

        public virtual void AttemptToPerformBackstabStrike(RaycastHit hit, CharacterManager characterDamaged)
        {
            if (character.characterTeamID == characterDamaged.characterTeamID)
                return;

            if (character == characterDamaged)
                return;

            if (characterDamaged.isDead)
                return;

            if (!characterDamaged.characterCombatManager.isCanBeBackstabbed)
                return;

            character.characterController.Move
                (characterDamaged.characterCombatManager.DEBUG_BackstabStandingPosition.transform.position -
                character.transform.position);

            Vector3 rotateToCharacterDamaged = character.transform.rotation.eulerAngles;
            rotateToCharacterDamaged = hit.transform.position - character.transform.position;
            rotateToCharacterDamaged.y = 0f;
            rotateToCharacterDamaged.Normalize();

            Quaternion rotation = Quaternion.LookRotation(rotateToCharacterDamaged);
            character.transform.rotation = rotation;

            character.characterAnimationManager.CharacterPlayAttackAnimation
                ("core_main_backstab_01", character.characterEquipmentManager.currentWeaponHoldInMainHand,
                CharacterAttackType.CriticalAttack01, true, true);
            isBackstabbing = true;

            //  FIND WEAPON USING FOR CRITICAL HIT
            WeaponItem_SO weaponUseForBackstab = character.characterCombatManager.currentWeaponCharacterUsingForAttack;

            //  IF WE HOLD A RANGED WEAPON, DON'T DO A BACKSTAB/RIPOSTE CRITICAL HIT
            if (weaponUseForBackstab.weaponType != WeaponType.Melee_Weapon)
                return;

            characterDamaged.characterAnimationManager.CharacterPlayAnimation("core_main_backstab_victim_01", true);
            characterDamaged.characterCombatManager.isBeingBackstabbed = true;
            characterDamaged.characterCombatManager.isCanBeBackstabbed = false;

            //  DAMAGE OUTPUT IS FINAL DAMAGE BASED ON WEAPON, AND CRITICAL DAMAGE BASED ON WEAPON CRITICAL
            //  MULTIPLIER (AS 100 = 100% DAMAGE BASED ON OUTPUT DAMAGE)
            float criticalDamage =
                (weaponUseForBackstab.weaponPhysicalDamage +
                weaponUseForBackstab.weaponMagicDamage +
                weaponUseForBackstab.weaponFireDamage +
                weaponUseForBackstab.weaponHolyDamage +
                weaponUseForBackstab.weaponLightningDamage) *
                (1f + currentWeaponCharacterUsingForAttack.weaponCriticalDamageMultiplier / 100f);

            characterDamaged.characterCombatManager.DEBUG_PendingCriticalDamage = criticalDamage;
        }

        public virtual void AttemptToPerformRiposteStrike(RaycastHit hit, CharacterManager characterDamaged)
        {
            if (character.characterTeamID == characterDamaged.characterTeamID)
                return;

            if (character == characterDamaged)
                return;

            if (characterDamaged.isDead)
                return;

            if (!characterDamaged.characterCombatManager.isCanBeRiposted)
                return;

            character.characterController.Move
                (characterDamaged.characterCombatManager.DEBUG_RiposteStandingPosition.transform.position -
                character.transform.position);

            Vector3 rotateToCharacterDamaged = character.transform.rotation.eulerAngles;
            rotateToCharacterDamaged = hit.transform.position - character.transform.position;
            rotateToCharacterDamaged.y = 0f;
            rotateToCharacterDamaged.Normalize();

            Quaternion rotation = Quaternion.LookRotation(rotateToCharacterDamaged);
            character.transform.rotation = rotation;

            character.characterAnimationManager.CharacterPlayAttackAnimation
                ("core_main_riposte_01", character.characterEquipmentManager.currentWeaponHoldInMainHand,
                CharacterAttackType.CriticalAttack02, true, true);
            isRiposting = true;

            //  FIND WEAPON USING FOR CRITICAL HIT
            WeaponItem_SO weaponUseForBackstab = character.characterCombatManager.currentWeaponCharacterUsingForAttack;

            //  IF WE HOLD A RANGED WEAPON, DON'T DO A BACKSTAB/RIPOSTE CRITICAL HIT
            if (weaponUseForBackstab.weaponType != WeaponType.Melee_Weapon)
                return;

            characterDamaged.characterAnimationManager.CharacterPlayAnimation("core_main_riposte_victim_01", true);
            characterDamaged.characterCombatManager.isBeingRiposted = true;
            characterDamaged.characterCombatManager.isCanBeRiposted = false;

            //  DAMAGE OUTPUT IS FINAL DAMAGE BASED ON WEAPON, AND CRITICAL DAMAGE BASED ON WEAPON CRITICAL
            //  MULTIPLIER (AS 100 = 100% DAMAGE BASED ON OUTPUT DAMAGE)
            float criticalDamage =
                (weaponUseForBackstab.weaponPhysicalDamage +
                weaponUseForBackstab.weaponMagicDamage +
                weaponUseForBackstab.weaponFireDamage +
                weaponUseForBackstab.weaponHolyDamage +
                weaponUseForBackstab.weaponLightningDamage) *
                (1f + currentWeaponCharacterUsingForAttack.weaponCriticalDamageMultiplier / 100f);

            characterDamaged.characterCombatManager.DEBUG_PendingCriticalDamage = criticalDamage;
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
                case CharacterAttackType.ChargeAttack01:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Charge_Attack_01_Stamina_multiplier);

                    break;
                case CharacterAttackType.ChargeAttack02:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Charge_Attack_02_Stamina_multiplier);

                    break;
                case CharacterAttackType.ChargeAttack03:

                    character.characterStatusManager.characterCurrentStamina -=
                        (currentWeaponCharacterUsingForAttack.baseStaminaCost *
                        currentWeaponCharacterUsingForAttack.oh_Charge_Attack_03_Stamina_multiplier);

                    break;
                default:
                    break;
            }

            character.characterGUIManager.characterStaminaPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (character.characterStatusManager.characterCurrentStamina);
        }

        //  ANIMATION EVENTS
        public virtual void PendingCriticalDamageViaVictimCriticalAnimation()
        {
            character.characterDamageReceiverManager.CharacterDamageReceiver
                (DEBUG_PendingCriticalDamage, false, true);
            DEBUG_PendingCriticalDamage = 0f;
        }

        public virtual void EnableCanDoComboAttack()
        {
            character.canDoComboAttack = true;
        }

        public virtual void DisableCanDoComboAttack()
        {
            character.canDoComboAttack = false;
        }

        public virtual void EnableIsInvulnerable()
        {
            character.isInvulnerable = true;
        }

        public virtual void DisableIsInvulnerable()
        {
            character.isInvulnerable = false;
        }

        public virtual void EnableIsCanBeRiposted()
        {
            isCanBeRiposted = true;
        }

        public virtual void DisableIsCanBeRiposte()
        {
            isCanBeRiposted = false;
        }

        public virtual void EnableIsParrying()
        {
            isParrying = true;
        }

        public virtual void DisableIsParrying()
        {
            isParrying = false;
        }

        //  DEBUG FUNC
        public virtual void DeductStanceViaAnimation(int stanceDamage)
        {
            character.characterStatusManager.characterCurrentStance -= stanceDamage;
        }

        public virtual void DEBUG_TrackingCharacterDeflecting(bool isDefense)
        {
            if (isDefense)
            {
                DEBUG_deflectTimeCount += Time.deltaTime;

                if (DEBUG_deflectTimeCount <= DEBUG_maxTimeDeflectPossibleBeforeBeingDefense)
                    isDeflect = true;
                else
                    isDeflect = false;
            }
            else
            {
                DEBUG_deflectTimeCount = 0f;
            }
        }

        //  DEBUG FUNCTION USE FOR DEFENSE SYSTEM, IF WE HAS AT LEAST ONE WEAPON IN OFF HAND
        //  WE USE THAT WEAPON FOR DEFENSE, (NOT DEFLECTING WITH RIGHT HAND WEAPON)
        public virtual void DEBUG_TrackingIfCharacterAlreadyHasWeaponInOffHand()
        {
            if (character.characterEquipmentManager.currentWeaponHoldInOffHand == null)
            {
                DEBUG_isAlreadyHasOffHandWeapon = false;
                return;
            }

            if (character.characterEquipmentManager.currentWeaponHoldInOffHand.weaponClass == WeaponClass.Unarmed)
            {
                DEBUG_isAlreadyHasOffHandWeapon = false;
                return;
            }

            if (character.characterEquipmentManager.currentWeaponHoldInOffHand.weaponType == WeaponType.Ranged_Weapon)
            {
                DEBUG_isAlreadyHasOffHandWeapon = false;
                return;
            }

            DEBUG_isAlreadyHasOffHandWeapon = true;
        }
    }
}