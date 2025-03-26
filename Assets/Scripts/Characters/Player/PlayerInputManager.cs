using UnityEngine;
using static TreeEditor.TreeGroup;

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

        [SerializeField] private bool q_Input = false;
        [SerializeField] private bool number1_Input = false;
        [SerializeField] private bool number2_Input = false;

        [SerializeField] private bool twoHanding_Input = false;
        [SerializeField] private bool twoHanding_MainWeapon_Input = false;
        [SerializeField] private bool twoHanding_OffWeapon_Input = false;

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
                inputActions.Player.LockOnTarget.performed += i => q_Input = true;
                inputActions.Player.SwitchLeftLockedOnTarget.performed += i => number1_Input = true;
                inputActions.Player.SwitchRightLockedOnTarget.performed += i => number2_Input = true;

                inputActions.Player.TwoHanding.performed += i => twoHanding_Input = true;
                inputActions.Player.TwoHanding.canceled += i => twoHanding_Input = false;
                inputActions.Player.TwoHandingMainWeapon.performed += i => twoHanding_MainWeapon_Input = true;
                inputActions.Player.TwoHandingOffWeapon.performed += i => twoHanding_OffWeapon_Input = true;

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
            HandlePlayerLockOnTargetInput();
            HandlePlayerTwoHandingInput();
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
            //  DEBUG ATTACK INPUT
            if (twoHanding_Input)
            {
                leftMouse_Input = false;
                return;
            }

            if (leftMouse_Input)
            {
                leftMouse_Input = false;

                if (PlayerCanvasManager.instance.isPlayerOpenMenuOption)
                    return;

                if (player.canDoComboAttack)
                {
                    player.playerCombatManager.CharacterPerformComboAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
                else
                {
                    if (player.isPerformingAction)
                        return;

                    switch (player.playerEquipmentManager.currentWeaponHoldInMainHand.weaponType)
                    {
                        case WeaponType.Melee_Weapon:

                            player.playerCombatManager.CharacterPerformLightAttack
                                (player.playerEquipmentManager.currentWeaponHoldInMainHand);

                            break;
                        case WeaponType.Ranged_Weapon:
                            break;
                        case WeaponType.Seal:
                            break;
                        case WeaponType.Staff:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void HandlePlayerHeavyAttackInput()
        {
            //  DEBUG ATTACK INPUT
            if (twoHanding_Input)
            {
                leftMouse_Input = false;
                return;
            }

            if (leftMouse_Hold_Input)
            {
                leftMouse_Hold_Input = false;

                if (PlayerCanvasManager.instance.isPlayerOpenMenuOption)
                    return;

                if (player.canDoComboAttack)
                {
                    player.playerCombatManager.CharacterPerformComboAttack
                        (player.playerEquipmentManager.currentWeaponHoldInMainHand);
                }
                else
                {
                    if (player.isPerformingAction)
                        return;

                    switch (player.playerEquipmentManager.currentWeaponHoldInMainHand.weaponType)
                    {
                        case WeaponType.Melee_Weapon:

                            player.playerCombatManager.CharacterPerformHeavyAttack
                                (player.playerEquipmentManager.currentWeaponHoldInMainHand);

                            break;
                        case WeaponType.Ranged_Weapon:
                            break;
                        case WeaponType.Seal:
                            break;
                        case WeaponType.Staff:
                            break;
                        default:
                            break;
                    }
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

        private void HandlePlayerLockOnTargetInput()
        {
            if (q_Input && !player.isLockedOn)
            {
                q_Input = false;

                PlayerCameraManager.instance.HandleCameraLockOnTarget();

                if (PlayerCameraManager.instance.nearestLockOnTarget != null)
                {
                    player.playerCombatManager.currentTargetCharacter = PlayerCameraManager.instance.nearestLockOnTarget;
                    player.isLockedOn = true;
                }
            }
            else if (q_Input && player.isLockedOn)
            {
                q_Input = false;
                player.isLockedOn = false;

                //  CLEAR LOCK ON TARGETS
                PlayerCameraManager.instance.ClearAllLockedOnTargets();
            }

            if (player.isLockedOn && number1_Input)
            {
                number1_Input = false;

                PlayerCameraManager.instance.HandleCameraLockOnTarget();

                if (PlayerCameraManager.instance.leftNearestLockOnTarget != null)
                {
                    player.playerCombatManager.currentTargetCharacter = PlayerCameraManager.instance.leftNearestLockOnTarget;
                    player.isLockedOn = true;
                }
            }

            if (player.isLockedOn && number2_Input)
            {
                number2_Input = false;

                PlayerCameraManager.instance.HandleCameraLockOnTarget();

                if (PlayerCameraManager.instance.rightNearestLockOnTarget != null)
                {
                    player.playerCombatManager.currentTargetCharacter = PlayerCameraManager.instance.rightNearestLockOnTarget;
                    player.isLockedOn = true;
                }
            }
        }

        //  DEBUG
        private void HandlePlayerTwoHandingInput()
        {
            if (!twoHanding_Input)
                return;

            if (twoHanding_MainWeapon_Input)
            {
                twoHanding_MainWeapon_Input = false;
                leftMouse_Input = false;

                if (player.isTwoHanding)
                {
                    player.isTwoHanding = false;
                    player.playerEquipmentManager.CharacterUnTwoHandingWeapon();
                    return;
                }

                player.isTwoHanding = true;
                player.playerEquipmentManager.CharacterTwoHandingMainWeapon();
            }

            if (twoHanding_OffWeapon_Input)
            {
                twoHanding_OffWeapon_Input = false;
                //  DEBUG RIGHT MOUSE INPUT (IF HAVE) 

                if (player.isTwoHanding)
                {
                    player.isTwoHanding = false;
                    player.playerEquipmentManager.CharacterUnTwoHandingWeapon();
                    return;
                }

                player.isTwoHanding = true;
                player.playerEquipmentManager.CharacterTwoHandingOffWeapon();
            }
        }
    }
}