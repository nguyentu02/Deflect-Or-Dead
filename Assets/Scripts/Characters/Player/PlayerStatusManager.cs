using UnityEngine;

namespace NT
{
    public class PlayerStatusManager : CharacterStatusManager
    {
        private PlayerManager player;

        [Header("Player Stamina Regeneration Settings")]
        [SerializeField] float staminaRegenAmount = 20f;
        [SerializeField] float staminaRegenDelay;
        [SerializeField] float staminaRegenTimer;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void PlayerStaminaRegeneration()
        {
            if (player.isPerformingAction || player.isSprinting)
            {
                staminaRegenTimer = 0f;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (characterCurrentStamina < characterMaxStamina && staminaRegenTimer > staminaRegenDelay)
                {
                    characterCurrentStamina += staminaRegenAmount * Time.deltaTime;

                    player.playerGUIManager.characterStaminaPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                        (player.playerStatusManager.characterCurrentStamina);
                }
            }
        }
    }
}