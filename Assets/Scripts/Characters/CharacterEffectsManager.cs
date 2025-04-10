using System.Collections;
using UnityEngine;

namespace NT
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Damages VFXs")]
        [SerializeField] GameObject characterBloodSplatVFX;

        [Header("Character Buildups Var")]
        //  POISON VAR
        public bool isPoisoned = false;
        public float poisonCurrentBuildup;

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
        public virtual void HandleCharacterAllBuildups()
        {
            if (character.isDead)
                return;

            HandleCharacterPoisonBuildup();
            HandleCharacterAlreadyIsPoisoned();
        }

        protected virtual void HandleCharacterPoisonBuildup()
        {
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
            }
        }

        protected virtual void HandleCharacterAlreadyIsPoisoned()
        {
            if (!isPoisoned)
                return;

            if (maximumBuildupValue > 0)
            {
                maximumBuildupValue -= 1f * Time.deltaTime;

                //  DEBUG TESTING POISONED DAMAGE 1% HEALTH PER SECONDS
                buildupDamagesTimeCount += Time.deltaTime;

                if (buildupDamagesTimeCount > buildupTimeTickDamages)
                {
                    float poisonDamage = character.characterStatusManager.characterMaxHealth * 0.01f;
                    character.characterDamageReceiverManager.CharacterFullBuildupDamageReceiver(poisonDamage);
                    buildupDamagesTimeCount = 0f;
                }
            }
            else
            {
                isPoisoned = false;
                maximumBuildupValue = defaultBuildupValue;
            }
        }

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