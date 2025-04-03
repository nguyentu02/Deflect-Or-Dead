using System.Collections;
using UnityEngine;

namespace NT
{
    public class FireballManager : SpellMasterManager
    {
        [Header("Fireball Impact VFXs")]
        public GameObject fireballImpactSmallVFX;
        public GameObject fireballImpactFullChargeVFX;

        [Header("Fireball Damage Collider")]
        [SerializeField] DamageMasterCollider fireballDamageCollider;

        private Rigidbody fireballRigidbody;
        private bool hasCollided = false;
        private Coroutine destructionVFXCoroutine;

        protected override void Awake()
        {
            base.Awake();

            fireballRigidbody = GetComponent<Rigidbody>();
        }

        protected override void Update()
        {
            base.Update();

            if (spellTarget != null)
                transform.LookAt(spellTarget.transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 12)
                return;

            if (!hasCollided)
            {
                hasCollided = true;

                InstantiateSpellDestructionVFX();
            }
        }

        public void InitializeFireball
            (CharacterManager spellCaster,
            float fireballFireDamage,
            float fireballFullChargeMultiplier)
        {
            fireballDamageCollider.characterCausingDamage = spellCaster;

            fireballDamageCollider.weaponFireDamage = fireballFireDamage * fireballFullChargeMultiplier;
        }

        public void InstantiateSpellDestructionVFX()
        {
            GameObject impactVFX = Instantiate
                (fireballImpactSmallVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void WaitThenInstantiateSpellDestructionVFX(float timeToWait)
        {
            if (destructionVFXCoroutine != null)
                StopCoroutine(destructionVFXCoroutine);

            destructionVFXCoroutine = StartCoroutine(WaitThenInstantiateVFX(timeToWait));
            StartCoroutine(WaitThenInstantiateVFX(timeToWait));
        }

        private IEnumerator WaitThenInstantiateVFX(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);

            InstantiateSpellDestructionVFX();
        }
    }
}