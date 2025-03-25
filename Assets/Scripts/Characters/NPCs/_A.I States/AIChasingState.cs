using UnityEngine;
using UnityEngine.AI;

namespace NT
{
    public class AIChasingState : AISate
    {
        [Header("Enemy Chasing Settings")]
        public float distanceToTarget;

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            if (enemy.isPerformingAction)
            {
                enemy.enemyAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            //  CHASE THE TARGET
            HandleEnemyChasingTarget(enemy);

            //  SWITCH TO COMBAT STANCE STATE IF PLAYER IS INRANGE COMBAT
            if (distanceToTarget <= enemy.enemyCombatStanceState.combatStanceRadius)
                return enemy.enemyCombatStanceState;

            return this;
        }

        protected virtual void HandleEnemyChasingTarget(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            Vector3 targetsDirection = enemy.characterCombatManager.currentTargetCharacter.transform.position - transform.position;
            distanceToTarget = Vector3.Distance(enemy.characterCombatManager.currentTargetCharacter.transform.position, transform.position);
            float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

            //  ROTATE WITH NAVMESH AGENT ROTATION
            //HandleEnemyRotateTowardsTarget();

            //  ROTATE MANNUALY, WITH SIMPLE CODE
            enemy.DEBUG_EnemyManuallyRotateTowardsTarget();

            enemy.enemyAnimationManager.ProcessCharacterMovementAnimation(0f, 1f, false);
            enemy.navMeshAgent.enabled = true;

            enemy.navMeshAgent.SetDestination
                (enemy.characterCombatManager.currentTargetCharacter.transform.position);
        }
    }
}