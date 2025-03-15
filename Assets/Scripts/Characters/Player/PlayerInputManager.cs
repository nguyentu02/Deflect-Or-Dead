using UnityEngine;

namespace NT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        private InputSystem_Actions inputActions;

        [Header("Player Movement Input")]
        private Vector2 movement_Vector2;
        public float horizontal_Input;
        public float vertical_Input;
        public float moveAmount;

        [Header("Player Camera Look Input")]
        private Vector2 cameraLook_Vector2;
        public float mouseX_Input;
        public float mouseY_Input;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new InputSystem_Actions();

                //  PLAYER MOVE/LOOK AROUND
                inputActions.Player.Move.performed += i => movement_Vector2 = i.ReadValue<Vector2>();
                inputActions.Player.Look.performed += i => cameraLook_Vector2 = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void HandleUpdateAllPlayerInput()
        {
            HandlePlayerMovementInput();
            HandlePlayerCameraLookInput();
        }

        private void HandlePlayerMovementInput()
        {
            horizontal_Input = movement_Vector2.x;
            vertical_Input = movement_Vector2.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal_Input) + Mathf.Abs(vertical_Input));
        }

        private void HandlePlayerCameraLookInput()
        {
            mouseX_Input = cameraLook_Vector2.x;
            mouseY_Input = cameraLook_Vector2.y;
        }
    }
}