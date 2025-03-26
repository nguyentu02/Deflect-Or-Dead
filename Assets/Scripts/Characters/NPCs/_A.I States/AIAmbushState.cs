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

        public override AISate SwitchToState(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            if (!isWaiting)
            {
                isWaiting = true;
                character.characterAnimationManager.CharacterPlayAnimation(ambushAnimation, true);
            }

            HandleEnemyDetectionPlayer(enemy);

            if (character.characterCombatManager.currentTargetCharacter != null)
            {
                ResetStateFlagsBeforeChangesState();
                return enemy.enemyChasingState;
            }

            return this;
        }

        protected virtual void HandleEnemyDetectionPlayer(CharacterManager character)
        {
            EnemyManager enemy = character as EnemyManager;

            Collider[] colliders = Physics.OverlapSphere
                (transform.position, enemyAmbushRadius, enemy.enemyIdleState.enemyDetectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterDetected = colliders[i].gameObject.GetComponent<CharacterManager>();

                if (characterDetected != null)
                {
                    Vector3 targetsDirection = characterDetected.transform.position - transform.position;
                    float viewableAngles = Vector3.Angle(targetsDirection, transform.forward);

                    if (viewableAngles > enemy.enemyIdleState.enemyMinimumFieldOfView &&
                        viewableAngles < enemy.enemyIdleState.enemyMaximumFieldOfView)
                    {
                        if (characterDetected.characterTeamID == character.characterTeamID)
                            continue;

                        if (characterDetected == character)
                            continue;

                        character.characterCombatManager.currentTargetCharacter = characterDetected;

                        //  IF HAVE TARGET, PLAY DETECT ANIMATION AND FIGHT WITH THEM
                        character.characterAnimationManager.CharacterPlayAnimation(detectAnimation, true);
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