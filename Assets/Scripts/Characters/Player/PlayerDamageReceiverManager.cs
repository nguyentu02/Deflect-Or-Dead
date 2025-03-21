using UnityEngine;

namespace NT
{
    public class PlayerDamageReceiverManager : CharacterDamageReceiverManager
    {
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        //  JUST DEBUG FOR PLAYTEST NOW, WILL REFACTOR LATER
        public override void CharacterDamageReceiver(float damageReceiver)
        {
            player.playerStatusManager.characterCurrentHealth -= damageReceiver;

            PlayerCanvasManager.instance.playerHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (player.playerStatusManager.characterCurrentHealth);

            //  JUST DEBUG FOR PLAYTEST NOW, WILL REFACTOR LATER
            player.playerAnimationManager.CharacterPlayAnimation("core_main_hit_reaction_medium_f_01", true);

            if (player.playerStatusManager.characterCurrentHealth <= 0f)
            {
                player.playerStatusManager.characterCurrentHealth = 0f;

                player.playerAnimationManager.CharacterPlayAnimation("straight_sword_main_death_01", true);
            }
        }
    }
}