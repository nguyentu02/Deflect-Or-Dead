using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class DamageMasterCollider : MonoBehaviour
    {
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
            damageCollider = GetComponent<Collider>();

            if (damageCollider != null)
            {
                damageCollider.isTrigger = true;
                damageCollider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager characterDamaged = other.GetComponent<CharacterManager>();

            //  DEBUG DAMAGE COLLIDER
            if (characterDamaged != null)
            {
                //  CHECK FOR TEAMMATE

                //  CHECK FOR BLOCK

                //  CHECK FOR PARRY

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