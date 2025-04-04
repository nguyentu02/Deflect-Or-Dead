using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Spells/Incantations/Fireball Incantation")]
    public class Fireball_Incantation_SO : IncantationItem_SO
    {
        [SerializeField] GameObject fireballBeforeCastVFX;

        [Header("Fireball Gravities")]
        [SerializeField] float fireballUpwardVelocity;
        [SerializeField] float fireballForwardVelocity;

        [Header("Fireball Damages")]
        public float fireballPhysicalDamage;
        public float fireballMagicDamage;
        public float fireballFireDamage;
        public float fireballHolyDamage;
        public float fireballLightningDamage;

        //  DEBUG FIREBALL FULL CHARGE DAMAGES
        public float DEBUG_fireballFullChargeMultiplier = 1.5f;

        public override void TryToPerformCastASpell(CharacterManager character)
        {
            base.TryToPerformCastASpell(character);

            fireballBeforeCastVFX = Instantiate
                (spellBeforeCastVFX, character.characterEquipmentManager.characterMainHand.transform);
            character.characterAnimationManager.CharacterPlayAnimation(spellAnimationName, true);

            fireballBeforeCastVFX.transform.localPosition = Vector3.zero;
            fireballBeforeCastVFX.transform.localRotation = Quaternion.identity;
            fireballBeforeCastVFX.transform.localScale = Vector3.one;
        }

        public override void SuccesfullyCastASpell(CharacterManager character)
        {
            base.SuccesfullyCastASpell(character);

            Destroy(fireballBeforeCastVFX);

            //  DEBUG TESTING, WHEN INSTANTIATE, FIREBALL UNDERGROUND, IT'S WEIRD !!!!
            if (character.characterEquipmentManager.characterMainHand == null)
                return;

            GameObject fireballAlreadyCastVFX = Instantiate
                (spellAlreadyCastVFX, character.characterEquipmentManager.characterMainHand.transform);

            Rigidbody fireballRigidbody = fireballAlreadyCastVFX.GetComponent<Rigidbody>();

            FireballManager fireballManager = fireballAlreadyCastVFX.GetComponent<FireballManager>();
            fireballManager.InitializeFireball(character, fireballFireDamage, 1f);

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

            //  DEBUG TESTING, WHEN INSTANTIATE, FIREBALL UNDERGROUND, IT'S WEIRD !!!!
            fireballAlreadyCastVFX.transform.position =
                character.characterEquipmentManager.characterMainHand.transform.position;

            Vector3 upwardVelocity = fireballAlreadyCastVFX.transform.up * fireballUpwardVelocity;
            Vector3 forwardVelocity = fireballAlreadyCastVFX.transform.forward * fireballForwardVelocity;
            Vector3 totalVelocity = upwardVelocity + forwardVelocity;
            fireballRigidbody.linearVelocity = totalVelocity;

            fireballAlreadyCastVFX.transform.parent = null;
        }

        public override void SuccesfullyCastFullChargeASpell(CharacterManager character)
        {
            base.SuccesfullyCastFullChargeASpell(character);

            Destroy(fireballBeforeCastVFX);

            //  DEBUG TESTING, WHEN INSTANTIATE, FIREBALL UNDERGROUND, IT'S WEIRD !!!!
            if (character.characterEquipmentManager.characterMainHand == null)
                return;

            GameObject fireballAlreadyCastVFX = Instantiate
                (spellAlreadyCastVFX, character.characterEquipmentManager.characterMainHand.transform);

            Rigidbody fireballRigidbody = fireballAlreadyCastVFX.GetComponent<Rigidbody>();

            FireballManager fireballManager = fireballAlreadyCastVFX.GetComponent<FireballManager>();
            fireballManager.InitializeFireball(character, fireballFireDamage, DEBUG_fireballFullChargeMultiplier);

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

            //  DEBUG TESTING, WHEN INSTANTIATE, FIREBALL UNDERGROUND, IT'S WEIRD !!!!
            fireballAlreadyCastVFX.transform.position = 
                character.characterEquipmentManager.characterMainHand.transform.position;

            Vector3 upwardVelocity = fireballAlreadyCastVFX.transform.up * fireballUpwardVelocity;
            Vector3 forwardVelocity = fireballAlreadyCastVFX.transform.forward * fireballForwardVelocity;
            Vector3 totalVelocity = upwardVelocity + forwardVelocity;
            fireballRigidbody.linearVelocity = totalVelocity;

            fireballAlreadyCastVFX.transform.parent = null;
        }
    }
}