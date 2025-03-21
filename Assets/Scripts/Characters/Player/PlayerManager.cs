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

            PlayerCameraManager.instance.player = this;
            PlayerInputManager.instance.player = this;
        }

        protected override void Update()
        {
            base.Update();

            PlayerInputManager.instance.HandleUpdateAllPlayerInput();

            playerMovementManager.HandleAllPlayerMovements();
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