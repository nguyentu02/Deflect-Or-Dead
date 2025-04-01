using UnityEngine;

namespace NT
{
    public class AICharacterCombatManager : CharacterCombatManager
    {
        private AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public override void DrainStaminaBasedOnCharacterAttackType()
        {

        }

        public override void PendingCriticalDamageViaVictimCriticalAnimation()
        {
            base.PendingCriticalDamageViaVictimCriticalAnimation();
        }
    }
}