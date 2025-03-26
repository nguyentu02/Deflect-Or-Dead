using UnityEngine;

namespace NT
{
    public class AIAttackTargetState : AISate
    {
        [Header("Enemy Attack Actions List")]
        public EnemyAction enemyCurrentAttackAction;
        public EnemyAction[] enemyAttackActions;

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            if (enemy.isPerformingAction)
            {
                enemy.enemyAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            if (enemyCurrentAttackAction != null)
            {
                EnemyAttackAction enemyAttackAction = enemyCurrentAttackAction as EnemyAttackAction;

                //  IF SELECTED ATTACK IS NOT ABLE TO USE (BAD ANGLE, BAD DISTANCE) CHOOSE THE NEW ONE
                if (enemy.viewableAngles > enemyAttackAction.maximumAttackAngle &&
                    enemy.viewableAngles < enemyAttackAction.minimumAttackAngle)
                {
                    enemy.DEBUG_EnemyManuallyRotateTowardsTarget();
                    enemyCurrentAttackAction = null;
                    return this;
                }
                //  IF POSSIBLE, STADING AND ATTACK OUR TARGET
                else if (enemy.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                         enemy.viewableAngles >= enemyAttackAction.minimumAttackAngle)
                {
                    DEBUG_HandleEnemyAttackTargetIfPossible(enemy);
                }
            }
            else
            {
                //  SELECT ONE OF MANY OUR ATTACK BASED ON ATTACK SCORE
                HandleEnemyRandomizeGetAttackAction(enemy);
            }

            if (enemy.distanceToTarget > enemy.navMeshAgent.stoppingDistance)
                //  WHEN WE DONE, RETURN TO COMBAT STANCE IF TARGET RUN AWAY
                return enemy.enemyCombatStanceState;
            else
                //  OTHERWISE, RETURN THIS STATE AND MAKE NEW ATTACK
                return this;
        }

        protected virtual void HandleEnemyRandomizeGetAttackAction(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            if (character.isPerformingAction)
                return;

            int maxScore = 0;

            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i] as EnemyAttackAction;

                if (enemy.distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    enemy.distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (enemy.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        enemy.viewableAngles >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomScore = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i] as EnemyAttackAction;

                if (enemy.distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    enemy.distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (enemy.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        enemy.viewableAngles >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (enemyCurrentAttackAction != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomScore)
                            enemyCurrentAttackAction = enemyAttackAction;
                    }
                }
            }
        }

        //  DEBUG FUNC
        private void DEBUG_HandleEnemyAttackTargetIfPossible(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            EnemyAttackAction currentAttackAction = enemyCurrentAttackAction as EnemyAttackAction;
            enemy.timeToNextAttack = currentAttackAction.timeToNextAttack;
            enemyCurrentAttackAction.NPCPerformAnAction(enemy);
            enemyCurrentAttackAction = null;
        }
    }
}