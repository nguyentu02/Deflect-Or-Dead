using UnityEngine;

namespace NT
{
    public class AIAttackTargetState : AISate
    {
        [Header("Enemy Attack Settings")]
        public float aiCharacterAttackRangeRadius = 3f;
        public EnemyAttackAction aiCharacterCurrentAttackAction;
        public EnemyAttackAction[] aiCharacterAttackActions;
        private EnemyAttackAction DEBUG_StoreAICharacterComboAttackAction;

        [Header("Enemy Combo Attack")]
        public bool isCanDoCombo = false;
        public float chanceToPerformComboAttack = 25f;

        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);
                return this;
            }

            aiCharacter.DEBUG_EnemyManuallyRotateTowardsTarget();

            if (isCanDoCombo && DEBUG_StoreAICharacterComboAttackAction != null)
            {
                DEBUG_HandleEnemyAttackTargetWithCombo(aiCharacter, DEBUG_StoreAICharacterComboAttackAction);
                return this;
            }

            if (aiCharacterCurrentAttackAction != null)
            {
                //  IF POSSIBLE, STADING AND ATTACK OUR TARGET
                if (aiCharacter.viewableAngles <= aiCharacterCurrentAttackAction.maximumAttackAngle &&
                    aiCharacter.viewableAngles >= aiCharacterCurrentAttackAction.minimumAttackAngle)
                {
                    DEBUG_HandleEnemyAttackTargetIfPossible(aiCharacter, aiCharacterCurrentAttackAction);

                    //  ROLL FOR CANDO COMBO ATTACK, IF CAN COMBO ATTACK, RETURN TOP AND PERFORM IT
                    if (RollForComboAttackChance())
                        return this;
                }
                //  IF SELECTED ATTACK IS NOT ABLE TO USE (BAD ANGLE, BAD DISTANCE) CHOOSE THE NEW ONE
                else
                {
                    aiCharacterCurrentAttackAction = null;
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

            for (int i = 0; i < aiCharacterAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = aiCharacterAttackActions[i];

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

            for (int i = 0; i < aiCharacterAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = aiCharacterAttackActions[i];

                if (aiCharacter.distanceToTarget <= enemyAttackAction.maximumDistanceNeededToAttack &&
                    aiCharacter.distanceToTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (aiCharacter.viewableAngles <= enemyAttackAction.maximumAttackAngle &&
                        aiCharacter.viewableAngles >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (aiCharacterCurrentAttackAction != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomScore)
                            aiCharacterCurrentAttackAction = enemyAttackAction;
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
                DEBUG_StoreAICharacterComboAttackAction = currentAttack.comboAttackAction;

            aiCharacter.characterEffectsManager.CharacterPlayWeaponVFX(true);
            aiCharacter.timeToNextAttack = currentAttack.timeToNextAttack;
            aiCharacterCurrentAttackAction.NPCPerformAnAction(aiCharacter);
            aiCharacterCurrentAttackAction = null;
        }

        private void DEBUG_HandleEnemyAttackTargetWithCombo
            (AICharacterManager aiCharacter, EnemyAttackAction currentAttack)
        {
            aiCharacter.characterEffectsManager.CharacterPlayWeaponVFX(true);
            aiCharacter.timeToNextAttack = currentAttack.timeToNextAttack;
            aiCharacterCurrentAttackAction = currentAttack;
            aiCharacterCurrentAttackAction.NPCPerformAnAction(aiCharacter);
            aiCharacterCurrentAttackAction = null;
            DEBUG_StoreAICharacterComboAttackAction = null;
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