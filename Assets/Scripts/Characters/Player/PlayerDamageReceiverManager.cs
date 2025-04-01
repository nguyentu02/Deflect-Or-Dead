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
        public override void CharacterDamageReceiver
            (float damageReceiver, 
            string damageAnimation = "core_main_hit_reaction_medium_f_01", 
            bool isHasDamageAnimtion = false, bool isHasNewDeadAnimation = false)
        {
            base.CharacterDamageReceiver(damageReceiver, damageAnimation, isHasDamageAnimtion, isHasNewDeadAnimation);

            player.playerGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (player.playerStatusManager.characterCurrentHealth);
        }

        public override void CharacterGiveAwardedOnDeath(int soulsReward)
        {
            player.playerSoulsCollected += soulsReward;
        }
    }
}