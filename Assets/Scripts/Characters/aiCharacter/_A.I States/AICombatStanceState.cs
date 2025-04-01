using UnityEngine;

namespace NT
{
    public class AICombatStanceState : AISate
    {
        [Header("Enemy Combat Stance Settings")]
        [SerializeField] bool isStrafes = false;
        [SerializeField] float randomizeStrafesValue;
        public float combatStanceRadius = 6f;

        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            HandleEnemyStrafesTargetCharacter(aiCharacter);

            //  IF WE ARE COOL DOWN AFTER ATTACKING, KEEP RETURN THIS STATE (IF PLAYER ALREADY IN COMBAT STANCE RANGE)
            if (aiCharacter.timeToNextAttack > 0)
                return this;

            //  CHECK IF IN RANGE ATTACK, SWITCH TO STATE ATTACK
            if (aiCharacter.distanceToTarget <= aiCharacter.aiAttackTargetState.enemyAttackRangeRadius)
            {
                ResetStateFlagsBeforeChangesState();
                return aiCharacter.aiAttackTargetState;
            }

            //  IF OUT OF RANGE COMBAT, SWITCH TO CHASING STATE
            if (aiCharacter.distanceToTarget > combatStanceRadius)
            {
                ResetStateFlagsBeforeChangesState();
                return aiCharacter.aiChasingState;
            }

            return this;
        }

        protected virtual void HandleEnemyStrafesTargetCharacter(AICharacterManager aiCharacter)
        {
            //  ROTATE WITH NAVMESH AGENT ROTATION
            //HandleEnemyRotateTowardsTarget();

            //  ROTATE MANNUALY, WITH SIMPLE CODE
            aiCharacter.DEBUG_EnemyManuallyRotateTowardsTarget();

            DecideCirclingBehaviorOfEnemyWhenStrafesTargetCharacter();

            aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(randomizeStrafesValue, 0.5f, false);
            aiCharacter.navMeshAgent.enabled = true;

            aiCharacter.navMeshAgent.SetDestination
                (aiCharacter.characterCombatManager.currentTargetCharacter.transform.position);
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