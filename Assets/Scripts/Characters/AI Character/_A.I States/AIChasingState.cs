using UnityEngine;
using UnityEngine.AI;

namespace NT
{
    public class AIChasingState : AISate
    {
        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            //  CHASE THE TARGET
            HandleEnemyChasingTarget(aiCharacter);

            //  SWITCH TO COMBAT STANCE STATE IF PLAYER IS INRANGE COMBAT
            if (aiCharacter.distanceToTarget <= aiCharacter.aiCombatStanceState.combatStanceRadius)
                return aiCharacter.aiCombatStanceState;

            return this;
        }

        protected virtual void HandleEnemyChasingTarget(AICharacterManager aiCharacter)
        {
            //  ROTATE WITH NAVMESH AGENT ROTATION
            //HandleEnemyRotateTowardsTarget();

            //  ROTATE MANNUALY, WITH SIMPLE CODE
            aiCharacter.DEBUG_EnemyManuallyRotateTowardsTarget();

            aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 1f, false);
            aiCharacter.navMeshAgent.enabled = true;

            aiCharacter.navMeshAgent.SetDestination
                (aiCharacter.characterCombatManager.currentTargetCharacter.transform.position);
        }
    }
}