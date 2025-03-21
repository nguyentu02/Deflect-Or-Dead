using UnityEngine;

namespace NT
{
    public class CharacterMovementManager : MonoBehaviour
    {
        private CharacterManager character;

        public Vector3 characterMoveDirection;
        //public Vector3 jumpDirection;
        public LayerMask groundLayer;

        [Header("Gravity Settings")]
        public float inAirTimer = 0;
        [SerializeField] public Vector3 yVelocity;
        [SerializeField] public float groundYVelocity = -20;        //THE FORCE APPLIED TO YOU WHILST GROUND
        [SerializeField] protected float fallStartYVelocity = -7;   //THE FORCE APPLIED TO YOU WHEN YOU BEGIN TO FALL (INCREASES OVER TIME)
        [SerializeField] protected float gravityForce = -25;
        [SerializeField] float groundCheckSphereRadius = 1f;
        protected bool fallingVelocitySet = false;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            yVelocity.y = groundYVelocity;
        }

        public virtual void HandleGroundCheck()
        {
            character.isGrounded = Physics.CheckSphere
                (character.transform.position, groundCheckSphereRadius, groundLayer);

            HandleCharacterFallingDown();
        }

        public virtual void HandleCharacterFallingDown()
        {
            if (character.isGrounded)
            {
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocitySet = false;
                    yVelocity.y = groundYVelocity;
                }
            }
            else
            {
                if (!fallingVelocitySet) //!character.isJumping && 
                {
                    fallingVelocitySet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer = inAirTimer + Time.deltaTime;
                yVelocity.y += gravityForce * Time.deltaTime;
            }

            character.characterController.Move(yVelocity * Time.deltaTime);
        }
    }
}