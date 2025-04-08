using UnityEngine;

namespace NT
{
    public class BossAttackTargetState : AIAttackTargetState
    {
        [Header("The Boss Attack Settings")]
        public EnemyAttackAction[] bossSeconPhaseAttackActions;

        protected override void HandleEnemyRandomizeGetAttackAction(AICharacterManager aiCharacter)
        {
            BossManager bossCharacter = aiCharacter as BossManager;

            if (bossCharacter.isSecondPhase)
            {
                if (aiCharacter.isPerformingAction)
                    return;

                int maxScore = 0;

                for (int i = 0; i < bossSeconPhaseAttackActions.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = bossSeconPhaseAttackActions[i];

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

                for (int i = 0; i < bossSeconPhaseAttackActions.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = bossSeconPhaseAttackActions[i];

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
            else
            {
                base.HandleEnemyRandomizeGetAttackAction(aiCharacter);
            }
        }
    }
}