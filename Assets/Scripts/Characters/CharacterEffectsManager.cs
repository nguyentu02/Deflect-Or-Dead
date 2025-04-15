using System.Collections;
using UnityEngine;

namespace NT
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Damages VFXs")]
        [SerializeField] GameObject characterBloodSplatVFX;

        [Header("Character Buildups/Full Buildups VFX Var")]
        //  POISON VAR
        public bool isPoisoned = false;
        public float poisonCurrentBuildup;
        public float poisonAmountBuildup = 100f;
        [SerializeField] GameObject characterPoisonedVFX;
        //  USED FOR CONSUMABLE/INCANTATION/SORCERY CLEANSE,
        //  IF CLEANSE, WE CAN DESTROY IS POISONED VFX RIGHT NOW.
        private GameObject DEBUG_StoreCharacterPoisonedVFX;

        //  ROT VAR
        public bool isRottened = false;
        public float rotCurrentBuildup;
        public float rotAmountBuildup = 100f;
        [SerializeField] GameObject characterRottenedVFX;
        //  USED FOR CONSUMABLE/INCANTATION/SORCERY CLEANSE,
        //  IF CLEANSE, WE CAN DESTROY IS ROTTENED VFX RIGHT NOW.
        private GameObject DEBUG_StoreCharacterRottenedVFX;

        //  FROST VAR
        public bool isFrosted = false;
        [SerializeField] bool isAlreadyDamageByFrostEffect = false;
        public float frostCurrentBuildup;
        public float frostAmountBuildup = 100f;
        [SerializeField] GameObject characterFrostedVFX;
        //  USED FOR CONSUMABLE/INCANTATION/SORCERY CLEANSE,
        //  IF CLEANSE, WE CAN DESTROY IS FROSTED VFX RIGHT NOW.
        private GameObject DEBUG_StoreCharacterFrostedVFX;

        //  BLEED VAR
        public float bleedCurrentBuildup;
        public float bleedAmountBuildup = 100f;
        [SerializeField] GameObject characterBleededVFX;

        //  SETTINGS BUILDUP VALUES
        public float maximumBuildupValue = 100f;
        [SerializeField] float defaultBuildupValue = 100f;
        [SerializeField] float buildupTimeTickDamages = 1f;
        private float buildupDamagesTimeCount;

        //  DEBUG TESTING...
        [Header("DEBUG_Character Deflect VFXs")]
        [SerializeField] GameObject characterDelfectImpactVFX;

        [Header("DEBUG_Tesing Var")]
        public GameObject DEBUG_FlaskModelInstantiatedInCharacterHand;
        [SerializeField] float DEBUG_delayTimeBeforeRecoveryResources = 0.4f;
        private Coroutine delayFlaskRecoveryCoroutine;
        private Coroutine delayBringBackWeaponCoroutine;

        [Header("DEBUG_Weapon VFXs Var")]
        public WeaponVFX mainHandWeaponVFX;
        public WeaponVFX offHandWeaponVFX;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void CharacterRecoveryResourcesFromFlask
            (CharacterManager characterUsing, bool whatFlaskType, float hpRecoveryAmounts, float fpRecoveryAmounts,
             GameObject recoveryHpVFX, GameObject recoveryFpVFX)
        {
            if (delayFlaskRecoveryCoroutine != null)
                StopCoroutine(delayFlaskRecoveryCoroutine);

            delayFlaskRecoveryCoroutine = StartCoroutine(DelayFlaskRecoveryEffectsCoroutine
                (characterUsing, whatFlaskType, hpRecoveryAmounts, fpRecoveryAmounts, recoveryHpVFX, recoveryFpVFX));
        }

        private IEnumerator DelayFlaskRecoveryEffectsCoroutine
            (CharacterManager characterUsing, bool whatFlaskType, float hpRecoveryAmounts, float fpRecoveryAmounts,
             GameObject recoveryHpVFX, GameObject recoveryFpVFX)
        {
            yield return new WaitForSeconds(DEBUG_delayTimeBeforeRecoveryResources);

            if (whatFlaskType)
            {
                character.characterStatusManager.characterCurrentHealth += hpRecoveryAmounts;
                character.characterGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (character.characterStatusManager.characterCurrentHealth);

                GameObject recoveryVFX = Instantiate(recoveryHpVFX, characterUsing.transform);

                DelayBringBackWeaponOfCharacter();
            }
            else
            {
                character.characterStatusManager.characterCurrentFocusPoints += fpRecoveryAmounts;
                character.characterGUIManager.characterFocusPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (character.characterStatusManager.characterCurrentFocusPoints);

                GameObject recoveryVFX = Instantiate(recoveryFpVFX, characterUsing.transform);

                DelayBringBackWeaponOfCharacter();
            }
        }

        //  BUILDUPS
        //  POISON
        public virtual void HandleCharacterAllBuildups()
        {
            if (character.isDead)
                return;

            HandleCharacterPoisonBuildup();
            HandleCharacterAlreadyIsPoisoned();

            HandleCharacterRotBuildup();
            HandleCharacterAlreadyIsRottened();

            HandleCharacterFrostBuildup();
            HandleCharacterAlreadyIsFrosted();

            HandleCharacterBleedBuildup();
        }

        protected virtual void HandleCharacterPoisonBuildup()
        {
            character.characterGUIManager.ShowUpPoisonBuildupForPlayer_GUI();

            if (isPoisoned)
                return;

            if (poisonCurrentBuildup > 0f && poisonCurrentBuildup < 100f)
            {
                poisonCurrentBuildup -= 1f * Time.deltaTime;
            }
            else if (poisonCurrentBuildup >= 100f)
            {
                isPoisoned = true;
                poisonCurrentBuildup = 0f;

                if (characterPoisonedVFX != null)
                {
                    if (character.characterCombatManager.lockOnTransform != null)
                    {
                        DEBUG_StoreCharacterPoisonedVFX = Instantiate
                            (characterPoisonedVFX, character.characterCombatManager.lockOnTransform);
                    }
                    else
                    {
                        Vector3 poisonPositionVFX = character.transform.position + Vector3.up * 1f;

                        DEBUG_StoreCharacterPoisonedVFX = Instantiate
                            (characterPoisonedVFX, poisonPositionVFX, Quaternion.identity);
                    }
                }
            }
        }

        protected virtual void HandleCharacterAlreadyIsPoisoned()
        {
            character.characterGUIManager.ShowUpPoisonAmountBuildupIfPlayerIsPoisoned_GUI();

            if (!isPoisoned)
                return;

            if (poisonAmountBuildup > 0)
            {
                poisonAmountBuildup -= 1f * Time.deltaTime;

                //  DEBUG TESTING POISONED DAMAGE 1% HEALTH PER SECONDS
                buildupDamagesTimeCount += Time.deltaTime;

                if (buildupDamagesTimeCount > buildupTimeTickDamages)
                {
                    float damage = character.characterStatusManager.characterMaxHealth * 0.01f;
                    character.characterDamageReceiverManager.CharacterFullBuildupDamageReceiver(damage, false);
                    buildupDamagesTimeCount = 0f;
                }
            }
            else
            {
                isPoisoned = false;
                poisonAmountBuildup = defaultBuildupValue;
            }
        }

        //  ROT
        protected virtual void HandleCharacterRotBuildup()
        {
            character.characterGUIManager.ShowUpRotBuildupForPlayer_GUI();

            if (isRottened)
                return;

            if (rotCurrentBuildup > 0f && rotCurrentBuildup < 100f)
            {
                rotCurrentBuildup -= 1f * Time.deltaTime;
            }
            else if (rotCurrentBuildup >= 100f)
            {
                isRottened = true;
                rotCurrentBuildup = 0f;

                if (characterRottenedVFX != null)
                {
                    if (character.characterCombatManager.lockOnTransform != null)
                    {
                        DEBUG_StoreCharacterRottenedVFX = Instantiate
                            (characterRottenedVFX, character.characterCombatManager.lockOnTransform);
                    }
                    else
                    {
                        Vector3 rotPositionVFX = character.transform.position + Vector3.up * 1f;

                        DEBUG_StoreCharacterRottenedVFX = Instantiate
                            (characterRottenedVFX, rotPositionVFX, Quaternion.identity);
                    }
                }
            }
        }

        protected virtual void HandleCharacterAlreadyIsRottened()
        {
            character.characterGUIManager.ShowUpRotAmountBuildupIfPlayerIsRottened_GUI();

            if (!isRottened)
                return;

            if (rotAmountBuildup > 0)
            {
                rotAmountBuildup -= 1f * Time.deltaTime;

                //  DEBUG TESTING POISONED DAMAGE 1% HEALTH PER SECONDS
                buildupDamagesTimeCount += Time.deltaTime;

                if (buildupDamagesTimeCount > buildupTimeTickDamages)
                {
                    float damage = character.characterStatusManager.characterMaxHealth * 0.035f;
                    character.characterDamageReceiverManager.CharacterFullBuildupDamageReceiver(damage, false);
                    buildupDamagesTimeCount = 0f;
                }
            }
            else
            {
                isRottened = false;
                rotAmountBuildup = defaultBuildupValue;
            }
        }

        //  FROST
        protected virtual void HandleCharacterFrostBuildup()
        {
            character.characterGUIManager.ShowUpFrostBuildupForPlayer_GUI();

            if (isFrosted)
                return;

            if (frostCurrentBuildup > 0f && frostCurrentBuildup < 100f)
            {
                frostCurrentBuildup -= 1f * Time.deltaTime;
            }
            else if (frostCurrentBuildup >= 100f)
            {
                isFrosted = true;
                frostCurrentBuildup = 0f;

                if (characterFrostedVFX != null)
                {
                    if (character.characterCombatManager.lockOnTransform != null)
                    {
                        DEBUG_StoreCharacterFrostedVFX = Instantiate
                            (characterFrostedVFX, character.characterCombatManager.lockOnTransform);
                    }
                    else
                    {
                        Vector3 frostPositionVFX = character.transform.position + Vector3.up * 1f;

                        DEBUG_StoreCharacterFrostedVFX = Instantiate
                            (characterFrostedVFX, frostPositionVFX, Quaternion.identity);
                    }
                }
            }
        }

        protected virtual void HandleCharacterAlreadyIsFrosted()
        {
            character.characterGUIManager.ShowUpFrostAmountBuildupIfPlayerIsFrosted_GUI();

            if (!isFrosted)
                return;

            if (frostAmountBuildup > 0)
            {
                frostAmountBuildup -= 1f * Time.deltaTime;

                if (!isAlreadyDamageByFrostEffect)
                {
                    isAlreadyDamageByFrostEffect = true;

                    float damage = character.characterStatusManager.characterMaxHealth * 0.1f + 30f;
                    character.characterDamageReceiverManager.CharacterFullBuildupDamageReceiver(damage, true);
                }
            }
            else
            {
                isFrosted = false;
                isAlreadyDamageByFrostEffect = false;
                frostAmountBuildup = defaultBuildupValue;
            }
        }

        //  BLEED
        protected virtual void HandleCharacterBleedBuildup()
        {
            character.characterGUIManager.ShowUpBleedBuildupForPlayer_GUI();

            if (bleedCurrentBuildup > 0f && bleedCurrentBuildup < 100f)
            {
                bleedCurrentBuildup -= 1f * Time.deltaTime;
            }
            else if (bleedCurrentBuildup >= 100f)
            {
                bleedCurrentBuildup = 0f;

                float damage = character.characterStatusManager.characterMaxHealth * 0.15f + 50f;
                character.characterDamageReceiverManager.CharacterFullBuildupDamageReceiver(damage, true);

                if (characterBleededVFX != null)
                {
                    if (character.characterCombatManager.lockOnTransform != null)
                    {
                        GameObject characterBleedVFX = Instantiate
                            (characterBleededVFX, character.characterCombatManager.lockOnTransform.position, Quaternion.identity);
                    }
                    else
                    {
                        Vector3 bleedPositionVFX = character.transform.position + Vector3.up * 1f;

                        GameObject characterBleedVFX = Instantiate
                            (characterBleededVFX, bleedPositionVFX, Quaternion.identity);
                    }
                }
            }
        }

        //
        private void DelayBringBackWeaponOfCharacter()
        {
            if (delayBringBackWeaponCoroutine != null)
                StopCoroutine(delayBringBackWeaponCoroutine);

            delayBringBackWeaponCoroutine = StartCoroutine
                (DelayBringBackCharacterWeaponInMainHandAfterUsingFlasksCoroutine());
        }

        private IEnumerator DelayBringBackCharacterWeaponInMainHandAfterUsingFlasksCoroutine()
        {
            yield return new WaitForSeconds(1f);

            Destroy(DEBUG_FlaskModelInstantiatedInCharacterHand);
            character.characterEquipmentManager.WhichCharacterHandWeWantToLoadWeaponIn
                (character.characterEquipmentManager.currentWeaponHoldInMainHand, true);
        }

        //  DEBUG TESTING PLAY VFX
        public virtual void CharacterPlayWeaponVFX(bool isMainHand)
        {
            if (isMainHand)
            {
                if (mainHandWeaponVFX == null)
                    return;

                mainHandWeaponVFX.PlayWeaponTrailVFX();
            }
            else
            {
                if (offHandWeaponVFX == null)
                    return;

                offHandWeaponVFX.PlayWeaponTrailVFX();
            }
        }

        public virtual void CharacterPlayBloodSplatVFX(Vector3 bloodSplatInstantiateTransform)
        {
            GameObject bloodSplat = Instantiate
                (characterBloodSplatVFX, bloodSplatInstantiateTransform, Quaternion.identity);
        }

        public virtual void CharacterPlayDeflectImpactVFX(Vector3 deflectImpactInstantiateTransform)
        {
            GameObject bloodSplat = Instantiate
                (characterDelfectImpactVFX, deflectImpactInstantiateTransform, Quaternion.identity);
        }
    }
}