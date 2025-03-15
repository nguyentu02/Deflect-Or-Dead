using UnityEngine;

namespace NT
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator characterAnimator;
        public Rigidbody characterRigidbody;

        public CharacterMovementManager characterMovementManager;
        public CharacterAnimationManager characterAnimationManager;

        [Header("Character Status")]
        public bool canMove = true;
        public bool canRotate = true;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponent<Animator>();
            characterRigidbody = GetComponent<Rigidbody>();

            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterMovementManager = GetComponent<CharacterMovementManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void LateUpdate()
        {

        }
    }
}