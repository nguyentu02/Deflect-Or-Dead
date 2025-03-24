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
        public CharacterEquipmentManager characterEquipmentManager;
        public CharacterCombatManager characterCombatManager;
        public CharacterStatusManager characterStatusManager;
        public CharacterDamageReceiverManager characterDamageReceiverManager;

        [Header("Character Status")]
        public bool isGrounded = true;
        public bool canMove = true;
        public bool canRotate = true;
        public bool isPerformingAction = false;
        public bool isRolling = false;
        public bool isSprinting = false;
        public bool isChargingAttack = false;
        public bool canDoComboAttack = false;
        public bool isAttacking = false;
        public bool isLockedOn = false;
        public bool isTwoHanding = false;
        public bool isTwoHanding_MainWeapon = false;
        public bool isTwoHanding_OffWeapon = false;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponent<Animator>();
            characterRigidbody = GetComponent<Rigidbody>();

            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterMovementManager = GetComponent<CharacterMovementManager>();
            characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterStatusManager = GetComponent<CharacterStatusManager>();
            characterDamageReceiverManager = GetComponent<CharacterDamageReceiverManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            characterMovementManager.HandleGroundCheck();
            characterAnimationManager.TrackingCharacterAnimatorParameters();

            //  DEBUG FOR ANIMATION
            characterAnimationManager.UpdateOverrideAnimatorBasedOnWeaponCharacterHoldInHand();
        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void LateUpdate()
        {

        }
    }
}