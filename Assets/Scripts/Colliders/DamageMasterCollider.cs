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
            EnemyManager enemyDamaged = other.GetComponent<EnemyManager>();

            if (enemyDamaged != null)
            {
                enemyDamaged.EnemyDamageReceiver(weaponDamageTest);
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