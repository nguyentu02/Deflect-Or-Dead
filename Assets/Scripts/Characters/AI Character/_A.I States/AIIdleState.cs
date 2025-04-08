using UnityEngine;

namespace NT
{
    public class AIIdleState : AISate
    {
        [Header("Enemy Detection Settings")]
        public LayerMask enemyDetectionLayer;
        public float enemyDetectionRadius;
        public float enemyMinimumFieldOfView = -35f;
        public float enemyMaximumFieldOfView = 35f;

        public override AISate SwitchToState(AICharacterManager aiCharacter)
        {
            //  LOOKING FOR A TARGET
            HandleEnemyDetectionPlayer(aiCharacter);

            //  SWITCH TO PERSUE TARGET STATE IF HAVE A TARGET
            if (aiCharacter.characterCombatManager.currentTargetCharacter != null)
                return aiCharacter.aiChasingState;
            else
                return this;
        }

        protected virtual void HandleEnemyDetectionPlayer(AICharacterManager aiCharacter)
        {
            Collider[] colliders = Physics.OverlapSphere
                (transform.position, enemyDetectionRadius, enemyDetectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterDetected = colliders[i].gameObject.GetComponent<CharacterManager>();

                if (characterDetected != null)
                {
                    Vector3 targetsDirection = characterDetected.transform.position - transform.position;
                    float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

                    if (viewableAngles > enemyMinimumFieldOfView &&
                        viewableAngles < enemyMaximumFieldOfView)
                    {
                        if (characterDetected.characterTeamID == aiCharacter.characterTeamID)
                            continue;

                        if (characterDetected == aiCharacter)
                            continue;

                        aiCharacter.characterCombatManager.currentTargetCharacter = characterDetected;
                    }
                }
            }
        }
    }
}