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
            (float damageReceiver, bool isHasDamageAnimtion, bool isHasNewDeadAnimation)
        {
            base.CharacterDamageReceiver(damageReceiver, isHasDamageAnimtion, isHasNewDeadAnimation);

            player.playerGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (player.playerStatusManager.characterCurrentHealth);
        }

        public override void CharacterGiveAwardedOnDeath(int soulsReward)
        {
            player.playerSoulsCollected += soulsReward;
        }
    }
}