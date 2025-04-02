using UnityEngine;

namespace NT
{
    public class FireballDamageCollider : DamageMasterCollider
    {
        private Rigidbody fireballRigidbody;
        private Vector3 fireballImpactNormal;

        public bool isFullChargeFireball = false;

        [Header("Fireball Impact VFXs")]
        [SerializeField] GameObject fireballImpactSmallVFX;
        [SerializeField] GameObject fireballImpactFullChargeVFX;

        protected override void Awake()
        {
            base.Awake();

            damageCollider.enabled = true;
            fireballRigidbody = GetComponent<Rigidbody>();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            CharacterManager characterDamaged = other.transform.GetComponentInParent<CharacterManager>();

            if (characterDamaged != null)
            {
                //  CHECK FOR DAMAGE MYSELF
                if (characterDamaged == characterCausingDamage)
                    return;

                //  CHECK FOR TEAMMATE
                if (characterCausingDamage.characterTeamID == characterDamaged.characterTeamID)
                    return;

                CalculateDamageAfterAddedToCharacterDamaged(characterDamaged);
            }

            GameObject impactVFX = Instantiate
                (fireballImpactSmallVFX, transform.position,
                Quaternion.FromToRotation(Vector3.up, fireballImpactNormal));
            Destroy(gameObject);
        }
    }
}