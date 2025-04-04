using System.Collections;
using UnityEngine;

namespace NT
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        private CharacterManager character;

        //  DEBUG TESTING...
        [Header("DEBUG_Tesing Var")]
        public GameObject DEBUG_FlaskModelInstantiatedInCharacterHand;
        [SerializeField] float DEBUG_delayTimeBeforeRecoveryResources = 0.4f;
        private Coroutine delayFlaskRecoveryCoroutine;
        private Coroutine delayBringBackWeaponCoroutine;

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
    }
}