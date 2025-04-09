using UnityEngine;

namespace NT
{
    public class FireballDamageCollider : DamageMasterCollider
    {
        [SerializeField] private FireballManager fireballManager;
        private Vector3 fireballImpactNormal;

        protected override void Awake()
        {
            base.Awake();

            fireballManager = GetComponentInParent<FireballManager>();

            damageCollider.enabled = true;
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

                CalculateDamageAfterAddedToCharacterDamaged(characterDamaged, other);
            }

            fireballManager.WaitThenInstantiateSpellDestructionVFX(0.04f);
        }
    }
}