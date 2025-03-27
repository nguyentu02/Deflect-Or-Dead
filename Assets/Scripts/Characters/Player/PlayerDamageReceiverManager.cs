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
            base.CharacterDamageReceiver(damageReceiver);

            player.playerGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (player.playerStatusManager.characterCurrentHealth);
        }
    }
}