using UnityEngine;

namespace NT
{
    public class AIAttackTargetState : AISate
    {
        [Header("Enemy Attack Actions List")]
        public EnemyAction enemyCurrentAttackAction;
        public EnemyAction[] enemyAttackActions;
        public float distanceToTarget;

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            Vector3 targetsDirection = character.characterCombatManager.currentTargetCharacter.transform.position - transform.position;
            distanceToTarget = Vector3.Distance(character.characterCombatManager.currentTargetCharacter.transform.position, transform.position);
            float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

            if (enemy.isPerformingAction)
                return this;

            if (enemyCurrentAttackAction != null)
            {
                EnemyAttackAction enemyAttackAction = enemyCurrentAttackAction as EnemyAttackAction;

                //  IF SELECTED ATTACK IS NOT ABLE TO USE (BAD ANGLE, BAD DISTANCE) CHOOSE THE NEW ONE
                if (viewableAngles > enemyAttackAction.maximumAttackAngle &&
                    viewableAngles < enemyAttackAction.minimumAttackAngle)
                {
                    enemy.DEBUG_EnemyManuallyRotateTowardsTarget();
                    enemyCurrentAttackAction = null;
                    return this;
                }
                //  IF POSSIBLE, STADING AND ATTACK OUR TARGET
                else if (viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                         viewableAngles >= enemyAttackAction.minimumAttackAngle)
                {
                    DEBUG_HandleEnemyAttackTargetIfPossible(enemy);
                }
            }
            else
            {
                //  SELECT ONE OF MANY OUR ATTACK BASED ON ATTACK SCORE
                HandleEnemyRandomizeGetAttackAction(enemy);
            }

            //  WHEN WE DONE, RETURN TO COMBAT STANCE
            return enemy.enemyCombatStanceState;
        }

        protected virtual void HandleEnemyRandomizeGetAttackAction(CharacterManager character)
        {
            if (character.isPerformingAction)
                return;

            Vector3 targetsDirection = character.characterCombatManager.currentTargetCharacter.transform.position - transform.position;
            distanceToTarget = Vector3.Distance(character.characterCombatManager.currentTargetCharacter.transform.position, transform.position);
            float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

            int maxScore = 0;

            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i] as EnemyAttackAction;

                if (distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        viewableAngles >= enemyAttackAction.minimumAttackAngle)
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

                if (distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        viewableAngles >= enemyAttackAction.minimumAttackAngle)
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