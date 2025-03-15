using UnityEngine;

namespace NT
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        private CharacterManager character;

        private int horizontalValue;
        private int verticalValue;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            horizontalValue = Animator.StringToHash("Horizontal");
            verticalValue = Animator.StringToHash("Vertical");
        }

        public virtual void ProcessCharacterMovementAnimation(float horizontal_Value, float vertical_Value)
        {
            float vert = 0f;
            float hori = 0f;

            //  VERTICAL VALUES
            if (vertical_Value > 0f && vertical_Value < 0.55f)
                vert = 0.5f;
            else if (vertical_Value > 0.55f)
                vert = 1f;
            else if (vertical_Value < 0f && vertical_Value > -0.55f)
                vert = -0.5f;
            else if (vertical_Value < -0.55f)
                vert = -1f;
            else
                vert = 0f;

            //  HORIZONTAL VALUES
            if (horizontal_Value > 0f && horizontal_Value < 0.55f)
                hori = 0.5f;
            else if (horizontal_Value > 0.55f)
                hori = 1f;
            else if (horizontal_Value < 0f && horizontal_Value > -0.55f)
                hori = -0.5f;
            else if (horizontal_Value < -0.55f)
                hori = -1f;
            else
                hori = 0f;

            //  APPLY VERT/HORI VALUES TO ANIMATOR VALUES
            character.characterAnimator.SetFloat(horizontalValue, hori, 0.2f, Time.deltaTime);
            character.characterAnimator.SetFloat(verticalValue, vert, 0.2f, Time.deltaTime);
        }
    }
}