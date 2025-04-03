using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Spells/Incantations/Fireball Incantation")]
    public class Fireball_Incantation_SO : IncantationItem_SO
    {
        [SerializeField] GameObject fireballBeforeCastVFX;

        [Header("Fireball Gravities")]
        [SerializeField] float fireballForwardVelocity;
        [SerializeField] float fireballUpwardVelocity;

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

            fireballBeforeCastVFX.transform.localPosition = new Vector3(0.118f, 0f, 0f);
            fireballBeforeCastVFX.transform.localRotation = Quaternion.identity;
            fireballBeforeCastVFX.transform.localScale = Vector3.one;
        }

        public override void SuccesfullyCastASpell(CharacterManager character)
        {
            base.SuccesfullyCastASpell(character);

            Destroy(fireballBeforeCastVFX);

            GameObject fireballAlreadyCastVFX = Instantiate
                (spellAlreadyCastVFX, 
                character.characterEquipmentManager.characterMainHand.transform.position, 
                PlayerCameraManager.instance.playerCameraPivotTransform.rotation);

            Rigidbody fireballRigidbody = fireballAlreadyCastVFX.GetComponent<Rigidbody>();

            FireballDamageCollider fireballDamageCollider = fireballAlreadyCastVFX.
                GetComponent<FireballDamageCollider>();
            fireballDamageCollider.characterCausingDamage = character;

            //  DEBUG TESTING DAMAGES
            fireballDamageCollider.weaponPhysicalDamage = fireballPhysicalDamage;
            fireballDamageCollider.weaponMagicDamage = fireballMagicDamage;
            fireballDamageCollider.weaponFireDamage = fireballFireDamage;
            fireballDamageCollider.weaponHolyDamage = fireballHolyDamage;
            fireballDamageCollider.weaponLightningDamage = fireballLightningDamage;

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

        public override void SuccesfullyCastFullChargeASpell(CharacterManager character)
        {
            base.SuccesfullyCastFullChargeASpell(character);

            Destroy(fireballBeforeCastVFX);

            GameObject fireballAlreadyCastVFX = Instantiate
                (spellAlreadyCastVFX,
                character.characterEquipmentManager.characterMainHand.transform.position,
                PlayerCameraManager.instance.playerCameraPivotTransform.rotation);

            Rigidbody fireballRigidbody = fireballAlreadyCastVFX.GetComponent<Rigidbody>();

            FireballDamageCollider fireballDamageCollider = fireballAlreadyCastVFX.
                GetComponent<FireballDamageCollider>();
            fireballDamageCollider.characterCausingDamage = character;

            //  DEBUG TESTING DAMAGES
            fireballDamageCollider.weaponPhysicalDamage = fireballPhysicalDamage * DEBUG_fireballFullChargeMultiplier;
            fireballDamageCollider.weaponMagicDamage = fireballMagicDamage * DEBUG_fireballFullChargeMultiplier;
            fireballDamageCollider.weaponFireDamage = fireballFireDamage * DEBUG_fireballFullChargeMultiplier;
            fireballDamageCollider.weaponHolyDamage = fireballHolyDamage * DEBUG_fireballFullChargeMultiplier;
            fireballDamageCollider.weaponLightningDamage = fireballLightningDamage * DEBUG_fireballFullChargeMultiplier;

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
    }
}