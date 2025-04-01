using UnityEngine;

namespace NT
{
    public class PlayerMovementManager : CharacterMovementManager
    {
        private PlayerManager player;

        [Header("Player Movement Settings")]
        public bool isWalking = false;
        [SerializeField] float walkingSpeed = 1.5f;
        [SerializeField] float runningSpeed = 4f;
        [SerializeField] float sprintingSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;

        [Header("Player Actions Costs")]
        [SerializeField] float sprintingStaminaCost = 2f;
        [SerializeField] float rollDodgeStaminaCost = 15f;
        [SerializeField] float backstepDodgeStaminaCost = 12f;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void HandleAllPlayerMovements()
        {
            HandlePlayerMovementWithCharacterController();
            HandlePlayerRotationTowardsCameraLookDirection();
        }

        private void HandlePlayerMovementWithCharacterController()
        {
            if (!player.canMove)
                return;

            characterMoveDirection = Vector3.zero;

            characterMoveDirection = PlayerCameraManager.instance.playerCameraTransform.transform.forward * PlayerInputManager.instance.vertical_Input;
            characterMoveDirection += PlayerCameraManager.instance.playerCameraTransform.transform.right * PlayerInputManager.instance.horizontal_Input;
            characterMoveDirection.Normalize();
            characterMoveDirection.y = 0f;

            if (player.isSprinting)
            {
                if (player.playerStatusManager.characterCurrentStamina <= 0f)
                    player.isSprinting = false;

                player.characterController.Move(characterMoveDirection * sprintingSpeed * Time.deltaTime);

                player.playerStatusManager.characterCurrentStamina -= sprintingStaminaCost * Time.deltaTime;

                //  SET STAMINA ON GUI
                player.playerGUIManager.characterStaminaPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (player.playerStatusManager.characterCurrentStamina);
            }
            else
            {
                if (isWalking || player.playerCombatManager.isDefense && 
                    !player.characterCombatManager.DEBUG_isAlreadyHasOffHandWeapon ||
                    //  DEBUG IN CASE IF PLAYER IS TWO HANDED AND DEFENSE
                    player.playerCombatManager.isTwoHanding && player.playerCombatManager.isDefense)
                {
                    player.characterController.Move(characterMoveDirection * walkingSpeed * Time.deltaTime);
                }
                else
                {
                    player.characterController.Move(characterMoveDirection * runningSpeed * Time.deltaTime);
                }
            }


            if (player.playerCombatManager.isLockedOn && !player.isSprinting)
            {
                player.playerAnimationManager.ProcessCharacterMovementAnimation
                    (PlayerInputManager.instance.horizontal_Input, 
                    PlayerInputManager.instance.vertical_Input, 
                    player.isSprinting);
            }
            else
            {
                player.playerAnimationManager.ProcessCharacterMovementAnimation
                    (0, PlayerInputManager.instance.moveAmount, player.isSprinting);
            }
        }

        private void HandlePlayerRotationTowardsCameraLookDirection()
        {
            if (!player.canRotate)
                return;

            if (player.playerCombatManager.isLockedOn)
            {
                if (player.isSprinting || player.isRolling)
                {
                    Vector3 rotationDirection = Vector3.zero;

                    rotationDirection = PlayerCameraManager.instance.playerCameraTransform.transform.forward * PlayerInputManager.instance.vertical_Input;
                    rotationDirection += PlayerCameraManager.instance.playerCameraTransform.transform.right * PlayerInputManager.instance.horizontal_Input;
                    rotationDirection.Normalize();
                    rotationDirection.y = 0f;

                    if (rotationDirection == Vector3.zero)
                        rotationDirection = player.transform.forward;

                    Quaternion rotateTowardsCamera = Quaternion.LookRotation(rotationDirection);
                    Quaternion finalRotationDirection = Quaternion.Slerp(player.transform.rotation, rotateTowardsCamera, rotationSpeed * Time.deltaTime);
                    player.transform.rotation = finalRotationDirection;
                }
                else
                {
                    Vector3 rotationDirection = characterMoveDirection;
                    rotationDirection = player.playerCombatManager.currentTargetCharacter.transform.position - transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();

                    Quaternion rotation = Quaternion.LookRotation(rotationDirection);
                    Quaternion finalRotation = Quaternion.Slerp
                        (transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                    transform.rotation = finalRotation;
                }
            }
            else
            {
                Vector3 rotationDirection = Vector3.zero;

                rotationDirection = PlayerCameraManager.instance.playerCameraTransform.transform.forward * PlayerInputManager.instance.vertical_Input;
                rotationDirection += PlayerCameraManager.instance.playerCameraTransform.transform.right * PlayerInputManager.instance.horizontal_Input;
                rotationDirection.Normalize();
                rotationDirection.y = 0f;

                if (rotationDirection == Vector3.zero)
                    rotationDirection = player.transform.forward;

                Quaternion rotateTowardsCamera = Quaternion.LookRotation(rotationDirection);
                Quaternion finalRotationDirection = Quaternion.Slerp(player.transform.rotation, rotateTowardsCamera, rotationSpeed * Time.deltaTime);
                player.transform.rotation = finalRotationDirection;
            }
        }

        public void PerformPlayerDodging()
        {
            if (player.isPerformingAction)
                return;

            //  IF WE OUT OF STAMINA, WE CAN'T DO ANYTHING
            if (player.characterStatusManager.characterCurrentStamina <= 0)
                return;

            characterMoveDirection = Vector3.zero;

            if (player.isRolling)
            {
                characterMoveDirection = PlayerCameraManager.instance.playerCameraTransform.transform.forward * PlayerInputManager.instance.vertical_Input;
                characterMoveDirection += PlayerCameraManager.instance.playerCameraTransform.transform.right * PlayerInputManager.instance.horizontal_Input;
                characterMoveDirection.Normalize();
                characterMoveDirection.y = 0f;

                if (PlayerInputManager.instance.moveAmount > 0f)
                {
                    player.playerAnimationManager.CharacterPlayAnimation("Roll (Forward)", true);

                    Quaternion rollRotateDirection = Quaternion.LookRotation(characterMoveDirection);
                    player.transform.rotation = rollRotateDirection;

                    player.playerStatusManager.characterCurrentStamina -= rollDodgeStaminaCost;
                }
                else
                {
                    player.playerAnimationManager.CharacterPlayAnimation("Back_Step_01", true);

                    player.playerStatusManager.characterCurrentStamina -= backstepDodgeStaminaCost;
                }

                //  SET STAMINA ON GUI
                player.playerGUIManager.characterStaminaPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (player.playerStatusManager.characterCurrentStamina);
            }
        }

        //  ANIMTION EVENTS
        public override void EnableCharacterCanRotate()
        {
            player.canRotate = true;
        }

        public override void DisableCharacterCanRotate()
        {
            player.canRotate = false;
        }
    }
}