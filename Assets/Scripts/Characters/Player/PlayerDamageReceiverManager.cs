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

        public override void CharacterDamageReceiver
            (float physicalDamage, 
            float magicDamage, 
            float fireDamage, 
            float holyDamage, 
            float lightningDamage, 
            string damageAnimation = "core_main_hit_reaction_medium_f_01", 
            bool isHasDamageAnimtion = false, bool isHasNewDeadAnimation = false)
        {
            base.CharacterDamageReceiver
                (physicalDamage, magicDamage, fireDamage, holyDamage, lightningDamage, 
                damageAnimation, isHasDamageAnimtion, isHasNewDeadAnimation);

            if (player.playerGUIManager.characterHealthPointsBar != null)
                player.playerGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (player.playerStatusManager.characterCurrentHealth);
        }

        public override void CharacterGiveAwardedOnDeath(int soulsReward)
        {
            player.playerSoulsCollected += soulsReward;
        }
    }
}