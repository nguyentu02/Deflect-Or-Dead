using System.Collections;
using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Spells/Incantations/Fireball Incantation")]
    public class Fireball_Incantation_SO : IncantationItem_SO
    {
        [Header("Fireball Gravities")]
        [SerializeField] float fireBallVelocity;
        private Rigidbody fireBallRigidbody;

        [Header("Fireball Impact VFXs")]
        [SerializeField] GameObject fireBallImpactSmallVFX;
        [SerializeField] GameObject fireBallImpactFullChargeVFX;

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
        }

        //  JUST DEBUG FOR INSTANTIATE CASTING VFX, BEFORE WE THROWING TO TARGETS
        public IEnumerator DEBUG_DelayInstantateVFXWhenCharacterTryToCast(CharacterManager character)
        {
            character.characterAnimationManager.CharacterPlayAnimation(spellAnimationName, true);

            yield return new WaitForSeconds(DEBUG_fireballTimeCastDelay);

            GameObject fireBallBeforeCastVFX = Instantiate
                (spellBeforeCastVFX, character.characterEquipmentManager.characterMainHand.transform);
        }
    }
}