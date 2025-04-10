using UnityEngine;

namespace NT
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator characterAnimator;
        [HideInInspector] public Rigidbody characterRigidbody;

        [HideInInspector] public CharacterMovementManager characterMovementManager;
        [HideInInspector] public CharacterAnimationManager characterAnimationManager;
        [HideInInspector] public CharacterEquipmentManager characterEquipmentManager;
        [HideInInspector] public CharacterCombatManager characterCombatManager;
        [HideInInspector] public CharacterStatusManager characterStatusManager;
        [HideInInspector] public CharacterDamageReceiverManager characterDamageReceiverManager;
        [HideInInspector] public CharacterGUIManager characterGUIManager;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;

        [Header("Character Team I.D")]
        public TeamID characterTeamID;

        //  JUST APPLY WITH BOT RIGHT NOW !!!
        [Header("Character Reward When Death")]
        public int soulsRewardOnDeath = 50;

        [Header("Character Status")]
        public bool isDead = false;
        public bool isPerformingAction = false;

        //  MOVING BOOLS
        public bool isGrounded = true;
        public bool canMove = true;
        public bool canRotate = true;

        //  ACTIONS BOOLS
        public bool isRolling = false;
        public bool isSprinting = false;

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
            characterGUIManager = GetComponent<CharacterGUIManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            characterMovementManager.HandleGroundCheck();
            characterAnimationManager.TrackingCharacterAnimatorParameters();

            //  DEBUG FOR ANIMATION
            characterAnimationManager.DEBUG_UpdateOverrideAnimatorBasedOnWeaponCharacterHoldInHand();

            //  DEBUG KEEP STATUS VALUE ALWAYS POSITIVE
            characterStatusManager.DEBUG_TrackingStatusPointsAndGetThemNeverNegativeValue();
            characterStatusManager.HandleStanceResetAfterTime();

            //  DEBUG FOR DEFENSE SYSTEM
            characterCombatManager.DEBUG_TrackingIfCharacterAlreadyHasWeaponInOffHand();
        }

        protected virtual void FixedUpdate()
        {
            characterEffectsManager.HandleCharacterAllBuildups();
        }

        protected virtual void LateUpdate()
        {

        }
    }
}