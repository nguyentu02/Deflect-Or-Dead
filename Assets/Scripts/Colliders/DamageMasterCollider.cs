using UnityEngine;

namespace NT
{
    public class DamageMasterCollider : MonoBehaviour
    {
        [SerializeField] protected Collider damageCollider;

        //  DEBUG TEST
        [SerializeField] float weaponDamageTest = 50f;

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
                characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver(weaponDamageTest);
            }
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
    }
}