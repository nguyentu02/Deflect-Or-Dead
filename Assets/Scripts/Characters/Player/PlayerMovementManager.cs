using UnityEngine;

namespace NT
{
    public class PlayerMovementManager : CharacterMovementManager
    {
        private PlayerManager player;

        [Header("Player Movement Settings")]
        [SerializeField] float walkingSpeed = 1.5f;
        [SerializeField] float runningSpeed = 4f;
        [SerializeField] float sprintingSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;

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

            Vector3 playerMoveDirection = Vector3.zero;

            playerMoveDirection = PlayerCameraManager.instance.playerCameraTransform.transform.forward * PlayerInputManager.instance.vertical_Input;
            playerMoveDirection += PlayerCameraManager.instance.playerCameraTransform.transform.right * PlayerInputManager.instance.horizontal_Input;
            playerMoveDirection.Normalize();
            playerMoveDirection.y = 0f;

            player.characterController.Move(playerMoveDirection * runningSpeed * Time.deltaTime);

            player.playerAnimationManager.ProcessCharacterMovementAnimation(0, PlayerInputManager.instance.moveAmount);
        }

        private void HandlePlayerRotationTowardsCameraLookDirection()
        {
            if (!player.canRotate)
                return;

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
}