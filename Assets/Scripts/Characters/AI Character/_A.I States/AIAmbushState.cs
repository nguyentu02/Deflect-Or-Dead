using UnityEngine;

namespace NT
{
    public class AIAmbushState : AISate
    {
        [Header("Ambush State Settings")]
        [SerializeField] bool isWaiting = false;
        [SerializeField] string ambushAnimation;
        [SerializeField] string detectAnimation;
        [SerializeField] float enemyAmbushRadius = 3f;

        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                aiCharacter.characterAnimationManager.CharacterPlayAnimation(ambushAnimation, true);
            }

            HandleEnemyDetectionPlayer(aiCharacter);

            if (aiCharacter.characterCombatManager.currentTargetCharacter != null)
            {
                ResetStateFlagsBeforeChangesState();
                return aiCharacter.aiChasingState;
            }

            return this;
        }

        protected virtual void HandleEnemyDetectionPlayer(AICharacterManager aiCharacter)
        {
            Collider[] colliders = Physics.OverlapSphere
                (transform.position, enemyAmbushRadius, aiCharacter.aiIdleState.enemyDetectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterDetected = colliders[i].gameObject.GetComponent<CharacterManager>();

                if (characterDetected != null)
                {
                    Vector3 targetsDirection = characterDetected.transform.position - transform.position;
                    float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

                    if (viewableAngles > aiCharacter.aiIdleState.enemyMinimumFieldOfView &&
                        viewableAngles < aiCharacter.aiIdleState.enemyMaximumFieldOfView)
                    {
                        if (characterDetected.characterTeamID == aiCharacter.characterTeamID)
                            continue;

                        if (characterDetected == aiCharacter)
                            continue;

                        aiCharacter.characterCombatManager.currentTargetCharacter = characterDetected;

                        //  IF HAVE TARGET, PLAY DETECT ANIMATION AND FIGHT WITH THEM
                        aiCharacter.characterAnimationManager.CharacterPlayAnimation(detectAnimation, true);
                    }
                }
            }
        }

        public override void ResetStateFlagsBeforeChangesState()
        {
            base.ResetStateFlagsBeforeChangesState();

            isWaiting = false;
        }
    }
}