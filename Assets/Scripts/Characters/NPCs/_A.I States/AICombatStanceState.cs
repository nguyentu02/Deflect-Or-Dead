using UnityEngine;

namespace NT
{
    public class AICombatStanceState : AISate
    {
        [Header("Enemy Combat Stance Settings")]
        [SerializeField] bool isStrafes = false;
        [SerializeField] float randomizeStrafesValue;
        public float combatStanceRadius = 6f;

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            if (enemy.isPerformingAction)
            {
                enemy.enemyAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            HandleEnemyStrafesTargetCharacter(enemy);

            //  IF WE ARE COOL DOWN AFTER ATTACKING, KEEP RETURN THIS STATE (IF PLAYER ALREADY IN COMBAT STANCE RANGE)
            if (enemy.timeToNextAttack > 0)
                return this;

            //  CHECK IF IN RANGE ATTACK, SWITCH TO STATE ATTACK
            if (enemy.distanceToTarget <= enemy.enemyAttackTargetState.enemyAttackRangeRadius)
            {
                ResetStateFlagsBeforeChangesState();
                return enemy.enemyAttackTargetState;
            }

            //  IF OUT OF RANGE COMBAT, SWITCH TO CHASING STATE
            if (enemy.distanceToTarget > combatStanceRadius)
            {
                ResetStateFlagsBeforeChangesState();
                return enemy.enemyChasingState;
            }

            return this;
        }

        protected virtual void HandleEnemyStrafesTargetCharacter(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            //  ROTATE WITH NAVMESH AGENT ROTATION
            //HandleEnemyRotateTowardsTarget();

            //  ROTATE MANNUALY, WITH SIMPLE CODE
            enemy.DEBUG_EnemyManuallyRotateTowardsTarget();

            DecideCirclingBehaviorOfEnemyWhenStrafesTargetCharacter();

            enemy.enemyAnimationManager.ProcessCharacterMovementAnimation(randomizeStrafesValue, 0.5f, false);
            enemy.navMeshAgent.enabled = true;

            enemy.navMeshAgent.SetDestination
                (enemy.characterCombatManager.currentTargetCharacter.transform.position);
        }

        private void DecideCirclingBehaviorOfEnemyWhenStrafesTargetCharacter()
        {
            if (!isStrafes)
            {
                isStrafes = true;

                float randomizeHorizontalValue = Random.Range(-0.55f, 0.55f);

                randomizeStrafesValue = randomizeHorizontalValue;
            }
        }

        public override void ResetStateFlagsBeforeChangesState()
        {
            base.ResetStateFlagsBeforeChangesState();

            isStrafes = false;
        }
    }
}