using UnityEngine;

namespace NT
{
    public class AIAttackTargetState : AISate
    {
        [Header("Enemy Attack Settings")]
        public float enemyAttackRangeRadius = 3f;
        public EnemyAction enemyCurrentAttackAction;
        public EnemyAction[] enemyAttackActions;
        private EnemyAttackAction DEBUG_StoreEnemyComboAttackAction;

        [Header("Enemy Combo Attack")]
        public bool isCanDoCombo = false;
        public float chanceToPerformComboAttack = 25f;

        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            //  USE ENEMY ACTION SRCIPT AS ENEMY ATTACK ACTION SCRIPT
            EnemyAttackAction enemyAttackAction = enemyCurrentAttackAction as EnemyAttackAction;

            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            aiCharacter.DEBUG_EnemyManuallyRotateTowardsTarget();

            if (isCanDoCombo && DEBUG_StoreEnemyComboAttackAction != null)
            {
                DEBUG_HandleEnemyAttackTargetWithCombo(aiCharacter, DEBUG_StoreEnemyComboAttackAction);
                return this;
            }

            if (enemyCurrentAttackAction != null)
            {
                //  IF POSSIBLE, STADING AND ATTACK OUR TARGET
                if (aiCharacter.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                    aiCharacter.viewableAngles >= enemyAttackAction.minimumAttackAngle)
                {
                    DEBUG_HandleEnemyAttackTargetIfPossible(aiCharacter, enemyAttackAction);

                    //  ROLL FOR CANDO COMBO ATTACK, IF CAN COMBO ATTACK, RETURN TOP AND PERFORM IT
                    if (RollForComboAttackChance())
                        return this;
                }
                //  IF SELECTED ATTACK IS NOT ABLE TO USE (BAD ANGLE, BAD DISTANCE) CHOOSE THE NEW ONE
                else
                {
                    enemyCurrentAttackAction = null;
                    return aiCharacter.aiCombatStanceState;
                }
            }
            else
            {
                //  SELECT ONE OF MANY OUR ATTACK BASED ON ATTACK SCORE
                HandleEnemyRandomizeGetAttackAction(aiCharacter);
            }

            if (aiCharacter.distanceToTarget > aiCharacter.navMeshAgent.stoppingDistance)
                //  WHEN WE DONE, RETURN TO COMBAT STANCE IF TARGET RUN AWAY
                return aiCharacter.aiCombatStanceState;
            else
                //  OTHERWISE, RETURN THIS STATE AND MAKE NEW ATTACK
                return this;
        }

        protected virtual void HandleEnemyRandomizeGetAttackAction(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
                return;

            int maxScore = 0;

            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i] as EnemyAttackAction;

                if (aiCharacter.distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    aiCharacter.distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (aiCharacter.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        aiCharacter.viewableAngles >= enemyAttackAction.minimumAttackAngle)
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

                if (aiCharacter.distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    aiCharacter.distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (aiCharacter.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        aiCharacter.viewableAngles >= enemyAttackAction.minimumAttackAngle)
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
        private void DEBUG_HandleEnemyAttackTargetIfPossible
            (AICharacterManager aiCharacter, EnemyAttackAction currentAttack)
        {
            //  DEBUG STORE ACTION OF ENEMY
            if (currentAttack.isHasComboAttackAction)
                DEBUG_StoreEnemyComboAttackAction = currentAttack.comboAttackAction;

            aiCharacter.timeToNextAttack = currentAttack.timeToNextAttack;
            enemyCurrentAttackAction.NPCPerformAnAction(aiCharacter);
            enemyCurrentAttackAction = null;
        }

        private void DEBUG_HandleEnemyAttackTargetWithCombo
            (AICharacterManager aiCharacter, EnemyAttackAction currentAttack)
        {
            aiCharacter.timeToNextAttack = currentAttack.timeToNextAttack;
            enemyCurrentAttackAction = currentAttack;
            enemyCurrentAttackAction.NPCPerformAnAction(aiCharacter);
            enemyCurrentAttackAction = null;
            DEBUG_StoreEnemyComboAttackAction = null;
        }

        //  ROLLS FOR CHANCE FUNC
        private bool RollForComboAttackChance()
        {
            isCanDoCombo = false;

            int randomScore = Random.Range(0, 100);

            if (randomScore < chanceToPerformComboAttack)
                isCanDoCombo = true;

            return isCanDoCombo;
        }
    }
}