using UnityEngine;
using UnityEngine.TextCore.Text;

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

            ProcessAICharacterRotateTowardsTargetViaAnimation(aiCharacter);

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

        private void ProcessAICharacterRotateTowardsTargetViaAnimation(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.aiCharaterAnimationManager.ProcessCharacterMovementAnimation(0f, 0f, false);

                //  WHEN TURNING TO FIND A TARGET CHARACTER,
                //  WE WANT TO RESET ALL COMBAT FLAG FOR A NEW ATTACK AFTER THIS
                ResetStateFlagsBeforeChangesState();
                return;
            }

            Vector3 targetsDirection = 
                aiCharacter.characterCombatManager.currentTargetCharacter.transform.position - 
                aiCharacter.transform.position;

            float viewableSignedAngles = Vector3.SignedAngle
                (targetsDirection, aiCharacter.transform.forward, Vector3.up);

            if (viewableSignedAngles >= 100 && viewableSignedAngles <= 180)
            {
                aiCharacter.aiCharaterAnimationManager.CharacterPlayAnimation("Protector_TurnL_180_Root", true);
            }
            else if (viewableSignedAngles <= -100 && viewableSignedAngles >= -180)
            {
                aiCharacter.aiCharaterAnimationManager.CharacterPlayAnimation("Protector_TurnR_180_Root", true);
            }
            else if (viewableSignedAngles >= 45 && viewableSignedAngles <= 100)
            {
                aiCharacter.aiCharaterAnimationManager.CharacterPlayAnimation("Protector_TurnL_90_Root", true);
            }
            else if (viewableSignedAngles <= -45 && viewableSignedAngles >= 100)
            {
                aiCharacter.aiCharaterAnimationManager.CharacterPlayAnimation("Protector_TurnR_90_Root", true);
            }

            //  ROTATE MANNUALY, WITH SIMPLE CODE
            aiCharacter.DEBUG_EnemyManuallyRotateTowardsTarget();
        }

        public override void ResetStateFlagsBeforeChangesState()
        {
            base.ResetStateFlagsBeforeChangesState();

            isStrafes = false;
        }
    }
}