using UnityEngine;

namespace NT
{
    public class AICharacterAnimationManager : CharacterAnimationManager
    {
        private AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public override void OnAnimatorMove()
        {
            Vector3 velocity = aiCharacter.characterAnimator.deltaPosition;
            aiCharacter.characterController.Move(velocity);

            if (aiCharacter.isPerformingAction)
                aiCharacter.transform.rotation *= aiCharacter.characterAnimator.deltaRotation;
        }
    }
}