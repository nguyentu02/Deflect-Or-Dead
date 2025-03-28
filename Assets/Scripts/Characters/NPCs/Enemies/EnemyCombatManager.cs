using UnityEngine;

namespace NT
{
    public class EnemyCombatManager : CharacterCombatManager
    {
        private EnemyManager enemy;

        protected override void Awake()
        {
            base.Awake();

            enemy = GetComponent<EnemyManager>();
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