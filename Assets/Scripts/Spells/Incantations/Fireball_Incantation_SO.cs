using System.Collections;
using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Spells/Incantations/Fireball Incantation")]
    public class Fireball_Incantation_SO : IncantationItem_SO
    {
        [Header("Fireball Gravities")]
        [SerializeField] float fireballForwardVelocity;
        [SerializeField] float fireballUpwardVelocity;

        [Header("Fireball Damages")]
        public float fireballPhysicalDamage;
        public float fireballMagicDamage;
        public float fireballFireDamage;
        public float fireballHolyDamage;
        public float fireballLightningDamage;

        // DEBUG
        public float DEBUG_fireballTimeCastDelay;

        public override void TryToPerformCastASpell(CharacterManager character)
        {
            base.TryToPerformCastASpell(character);
        }

        public override void SuccesfullyCastASpell(CharacterManager character)
        {
            base.SuccesfullyCastASpell(character);

            GameObject fireballAlreadyCastVFX = Instantiate
                (spellAlreadyCastVFX, 
                character.characterEquipmentManager.characterMainHand.transform.position, 
                PlayerCameraManager.instance.playerCameraPivotTransform.rotation);

            Rigidbody fireballRigidbody = fireballAlreadyCastVFX.GetComponent<Rigidbody>();

            FireballDamageCollider fireballDamageCollider = fireballAlreadyCastVFX.
                GetComponent<FireballDamageCollider>();
            fireballDamageCollider.characterCausingDamage = character;

            fireballAlreadyCastVFX.transform.parent = null;

            if (character.characterCombatManager.isLockedOn)
            {
                fireballAlreadyCastVFX.transform.LookAt
                    (character.characterCombatManager.
                    currentTargetCharacter.characterCombatManager.lockOnTransform.position);
            }
            else
            {
                Vector3 forwardDirection = character.transform.forward;
                fireballAlreadyCastVFX.transform.forward = forwardDirection;
            }

            Vector3 upwardVelocity = fireballAlreadyCastVFX.transform.up * fireballUpwardVelocity;
            Vector3 forwardVelocity = fireballAlreadyCastVFX.transform.forward * fireballForwardVelocity;
            Vector3 totalVelocity = upwardVelocity + forwardVelocity;
            fireballRigidbody.linearVelocity = totalVelocity;
        }

        //  JUST DEBUG FOR INSTANTIATE CASTING VFX, BEFORE WE THROWING TO TARGETS
        public IEnumerator DEBUG_DelayInstantateVFXWhenCharacterTryToCast(CharacterManager character)
        {
            character.characterAnimationManager.CharacterPlayAnimation(spellAnimationName, true);

            yield return new WaitForSeconds(DEBUG_fireballTimeCastDelay);

            GameObject fireballBeforeCastVFX = Instantiate
                (spellBeforeCastVFX, character.characterEquipmentManager.characterMainHand.transform);
        }
    }
}