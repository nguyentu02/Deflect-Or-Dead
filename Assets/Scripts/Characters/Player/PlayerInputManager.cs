using UnityEngine;

namespace NT
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        private InputSystem_Actions inputActions;
        public PlayerManager player;

        [Header("Player Movement Input")]
        private Vector2 movement_Vector2;
        public float horizontal_Input;
        public float vertical_Input;
        public float moveAmount;

        [Header("Player Camera Look Input")]
        private Vector2 cameraLook_Vector2;
        public float mouseX_Input;
        public float mouseY_Input;

        [SerializeField] private bool space_Input = false;
        [SerializeField] private bool space_Hold_Input = false;
        [SerializeField] private bool leftShift_Input = false;

        [SerializeField] private bool leftMouse_Input = false;
        [SerializeField] private bool leftMouse_Hold_Input = false;
        [SerializeField] private bool leftMouse_Hold_For_Charge_Attack = false;

        [SerializeField] private bool rightArrow_Input = false;
        [SerializeField] private bool leftArrow_Input = false;

        [SerializeField] private bool e_Input = false;
        [SerializeField] private bool esc_Input = false;

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
                inputActions.Player.Sprint.performed += i => space_Hold_Input = true;
                inputActions.Player.Sprint.canceled += i => space_Hold_Input = false;

                //  PLAYER ACTIONS
                inputActions.Player.Dodge.performed += i => space_Input = true;
                inputActions.Player.Interact.performed += i => e_Input = true;
                inputActions.Player.OpenOptions.performed += i => esc_Input = true;

                //  PLAYER ATTACKS
                inputActions.Player.Attack.performed += i => leftMouse_Input = true;

                inputActions.Player.HeavyAttack.performed += i => leftMouse_Hold_Input = true;

                inputActions.Player.ChargeAttack.performed += i => leftMouse_Hold_For_Charge_Attack = true;
                inputActions.Player.ChargeAttack.canceled += i => leftMouse_Hold_For_Charge_Attack = false;

                inputActions.Player.SwitchRightWeapon.performed += i => rightArrow_Input = true;
                inputActions.Player.SwitchLeftWeapon.performed += i => leftArrow_Input = true;
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
            HandlePlayerSprintInput();
            HandlePlayerCameraLookInput();
            HandlePlayerDodgeInput();
            HandlePlayerLightAttackInput();
            HandlePlayerHeavyAttackInput();
            HandlePlayerChargeAttackInput();
            HandlePlayerSwitchWeaponInHandsInput();
            HandlePlayerInteractInput();
            HandlePlayerOpenMenuOptionsInput();
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

        private void HandlePlayerDodgeInput()
        {
            if (space_Input)
            {
                space_Input = false;

                player.isRolling = true;

                player.playerMovementManager.PerformPlayerDodging();
            }
        }

        private void HandlePlayerSprintInput()
        {
            if (moveAmount <= 0f)
            {
                player.isSprinting = false;
                return;
            }

            player.isSprinting = space_Hold_Input;
        }

        private void HandlePlayerLightAttackInput()
        {
            if (leftMouse_Input)
            {
                leftMouse_Input = false;

                if (player.canDoComboAttack)
                {
                    player.playerCombatManager.CharacterPerformComboAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
                else
                {
                    if (player.isPerformingAction)
                        return;

                    player.playerCombatManager.CharacterPerformLightAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
            }
        }

        private void HandlePlayerHeavyAttackInput()
        {
            if (leftMouse_Hold_Input)
            {
                leftMouse_Hold_Input = false;

                if (player.canDoComboAttack)
                {
                    player.playerCombatManager.CharacterPerformComboAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
                else
                {
                    if (player.isPerformingAction)
                        return;

                    player.playerCombatManager.CharacterPerformHeavyAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
            }
        }

        private void HandlePlayerChargeAttackInput()
        {
            if (!player.isPerformingAction)
                return;

            player.isChargingAttack = leftMouse_Hold_For_Charge_Attack;
        }

        private void HandlePlayerSwitchWeaponInHandsInput()
        {
            if (rightArrow_Input)
            {
                rightArrow_Input = false;

                player.playerEquipmentManager.CharacterSwitchMainHandWeapon();
            }

            if (leftArrow_Input)
            {
                leftArrow_Input = false;

                player.playerEquipmentManager.CharacterSwitchOffHandWeapon();
            }
        }

        private void HandlePlayerInteractInput()
        {
            if (e_Input)
            {
                e_Input = false;

                if (PlayerCanvasManager.instance.isNewItemAlert)
                {
                    PlayerCanvasManager.instance.ResetAllGUIGameObject_GUI();
                    return;
                }

                if (player.playerInteractionManager.interactableObjects.Count <= 0)
                    return;

                //  IF WE ALREADY HAVE THAT ITEM, JUST SHOW SIMPLE POP UP
                //if (player.playerInventoryManager.playerInventories.Contains())
                //{

                //}
                //  OTHERWISE, WE SHOW LARGE POP UP TO MAKE SURE PLAYER DON'T MISTAKE
                //else
                //{

                //}

                PlayerCanvasManager.instance.ShowAlertToPlayerWhenPickUpAnWeaponNeverHaveBefore_GUI();

                player.playerInteractionManager.interactableObjects[0].InteractWithAnObject(player);
            }
        }

        private void HandlePlayerOpenMenuOptionsInput()
        {
            if (esc_Input)
            {
                esc_Input = false;

                PlayerCanvasManager.instance.isPlayerOpenMenuOption = !PlayerCanvasManager.instance.isPlayerOpenMenuOption;

                if (PlayerCanvasManager.instance.isPlayerOpenMenuOption)
                {
                    PlayerCanvasManager.instance.ShowMenuOptionToPlayer_GUI();
                    PlayerCanvasManager.instance.UpdatePlayerWeaponInventoryWhenPlayerOpenMenuOptions_GUI();
                }
                else
                {
                    PlayerCanvasManager.instance.ResetAllGUIGameObject_GUI();
                }
            }
        }
    }
}