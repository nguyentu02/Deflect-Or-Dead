using UnityEngine;

namespace NT
{
    public class EnemyAnimationManager : CharacterAnimationManager
    {
        private EnemyManager enemy;

        protected override void Awake()
        {
            base.Awake();

            enemy = GetComponent<EnemyManager>();
        }

        public override void OnAnimatorMove()
        {
            Vector3 velocity = enemy.characterAnimator.deltaPosition;
            enemy.characterController.Move(velocity);

            if (enemy.isPerformingAction)
                enemy.transform.rotation *= enemy.characterAnimator.deltaRotation;
        }
    }
}