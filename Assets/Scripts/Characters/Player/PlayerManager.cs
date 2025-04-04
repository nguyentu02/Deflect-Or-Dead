using UnityEngine;

namespace NT
{
    public class PlayerManager : CharacterManager
    {
        public static PlayerManager instance;

        [HideInInspector] public PlayerMovementManager playerMovementManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerStatusManager playerStatusManager;
        [HideInInspector] public PlayerDamageReceiverManager playerDamageReceiverManager;
        [HideInInspector] public PlayerInteractionManager playerInteractionManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerGUIManager playerGUIManager;
        [HideInInspector] public PlayerEffectsManager playerEffectsManager;

        [Header("Player Souls Collected")]
        public int playerSoulsCollected;

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
            playerGUIManager = GetComponent<PlayerGUIManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();

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

            playerStatusManager.PlayerStaminaRegeneration();
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