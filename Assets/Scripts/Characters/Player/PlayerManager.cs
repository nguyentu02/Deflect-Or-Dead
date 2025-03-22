using UnityEngine;

namespace NT
{
    public class PlayerManager : CharacterManager
    {
        public static PlayerManager instance;

        public PlayerMovementManager playerMovementManager;
        public PlayerAnimationManager playerAnimationManager;
        public PlayerEquipmentManager playerEquipmentManager;
        public PlayerCombatManager playerCombatManager;
        public PlayerStatusManager playerStatusManager;
        public PlayerDamageReceiverManager playerDamageReceiverManager;
        public PlayerInteractionManager playerInteractionManager;
        public PlayerInventoryManager playerInventoryManager;

        protected override void Awake()
        {
            base.Awake();

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerStatusManager = GetComponent<PlayerStatusManager>();
            playerDamageReceiverManager = GetComponent<PlayerDamageReceiverManager>();
            playerInteractionManager = GetComponent<PlayerInteractionManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();

            PlayerCameraManager.instance.player = this;
            PlayerInputManager.instance.player = this;
            PlayerCanvasManager.instance.player = this;
        }

        protected override void Update()
        {
            base.Update();

            PlayerInputManager.instance.HandleUpdateAllPlayerInput();

            playerMovementManager.HandleAllPlayerMovements();

            playerInteractionManager.CheckForPlayerInteractionProcess();

            PlayerCanvasManager.instance.UpdatePlayerAlertMessageIfPlayerCanInteract_GUI();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            PlayerCameraManager.instance.HandleAllCameraActions();
        }
    }
}