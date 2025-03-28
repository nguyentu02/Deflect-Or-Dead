using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class DamageMasterCollider : MonoBehaviour
    {
        public CharacterManager characterCausingDamage;

        [SerializeField] protected Collider damageCollider;

        [Header("Weapon Damages")]
        public float weaponPhysicalDamage;
        public float weaponMagicDamage;
        public float weaponFireDamage;
        public float weaponHolyDamage;
        public float weaponLightningDamage;

        [Header("Final Damage")]
        public float DEBUG_finalDamage;

        private List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        protected virtual void Awake()
        {
            characterCausingDamage = GetComponentInParent<CharacterManager>(true);

            damageCollider = GetComponent<Collider>();

            if (damageCollider != null)
            {
                damageCollider.isTrigger = true;
                damageCollider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager characterDamaged = other.GetComponentInParent<CharacterManager>();

            //  DEBUG DAMAGE COLLIDER
            if (characterDamaged != null)
            {
                //  CHECK FOR DAMAGE MYSELF
                if (characterDamaged == characterCausingDamage)
                    return;

                //  CHECK FOR TEAMMATE

                //  CHECK FOR BLOCK

                //  CHECK FOR PARRY
                if (characterDamaged.characterCombatManager.isDeflect)
                {
                    CheckForDeflect(characterDamaged);
                    return;
                }

                //  CHECK FOR CAN'T DEAL ANY DAMAGE

                CalculateDamageAfterAddedToCharacterDamaged(characterDamaged);
            }
        }

        protected virtual void CalculateDamageAfterAddedToCharacterDamaged(CharacterManager character)
        {
            if (charactersDamaged.Contains(character))
                return;

            charactersDamaged.Add(character);

            DEBUG_finalDamage = weaponPhysicalDamage + weaponMagicDamage +
                weaponFireDamage + weaponHolyDamage + weaponLightningDamage;

            character.characterDamageReceiverManager.CharacterDamageReceiver(DEBUG_finalDamage, true, false);
        }

        protected virtual void CheckForDeflect(CharacterManager characterDamaged)
        {
            characterCausingDamage.characterAnimationManager.CharacterPlayAnimation("GhostSamurai_DefenseR_Rebound_Root", true);
            characterCausingDamage.characterCombatManager.isStanceBreak = true;
        }

        protected virtual void CheckForDefense(CharacterManager characterDamaged)
        {

        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            charactersDamaged.Clear();
            damageCollider.enabled = false;
            DEBUG_finalDamage = 0f;
        }
    }
}