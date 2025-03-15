using UnityEngine;

namespace NT
{
    public class PlayerManager : CharacterManager
    {
        public PlayerMovementManager playerMovementManager;
        public PlayerAnimationManager playerAnimationManager;

        protected override void Awake()
        {
            base.Awake();

            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
        }

        protected override void Update()
        {
            base.Update();

            PlayerInputManager.instance.HandleUpdateAllPlayerInput();

            playerMovementManager.HandleAllPlayerMovements();
        }
    }
}