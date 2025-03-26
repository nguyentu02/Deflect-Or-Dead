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

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            //  LOOKING FOR A TARGET
            HandleEnemyDetectionPlayer(enemy);

            //  SWITCH TO PERSUE TARGET STATE IF HAVE A TARGET
            if (enemy.characterCombatManager.currentTargetCharacter != null)
                return enemy.enemyChasingState;
            else
                return this;
        }

        protected virtual void HandleEnemyDetectionPlayer(CharacterManager character)
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
                        if (characterDetected.characterTeamID == character.characterTeamID)
                            continue;

                        if (characterDetected == character)
                            continue;

                        character.characterCombatManager.currentTargetCharacter = characterDetected;
                    }
                }
            }
        }
    }
}