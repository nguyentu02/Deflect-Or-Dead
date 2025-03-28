using UnityEngine;

namespace NT
{
    public class CharacterDamageReceiverManager : MonoBehaviour
    {
        private CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void CharacterDamageReceiver
            (float damageReceiver, bool isHasDamageAnimtion, bool isHasNewDeadAnimation)
        {
            if (character.isInvulnerable)
                return;

            if (character.isDead)
                return;

            character.characterStatusManager.characterCurrentHealth -= damageReceiver;

            //  JUST DEBUG FOR PLAYTEST NOW, WILL REFACTOR LATER
            if (isHasDamageAnimtion)
                character.characterAnimationManager.CharacterPlayAnimation("core_main_hit_reaction_medium_f_01", true);

            if (character.characterStatusManager.characterCurrentHealth <= 0f)
            {
                character.isDead = true;

                character.characterStatusManager.characterCurrentHealth = 0f;

                if (!isHasNewDeadAnimation)
                    character.characterAnimationManager.CharacterPlayAnimation("straight_sword_main_death_01", true);
            }
        }

        public virtual void CharacterGiveAwardedOnDeath(int soulsReward)
        {

        }

        //  ANIMATION EVENTS
        public virtual void CharacterGiveAwardedWithAnimationEvent()
        {
            if (PlayerManager.instance != null)
            {
                PlayerManager.instance.playerDamageReceiverManager.CharacterGiveAwardedOnDeath
                    (character.soulsRewardOnDeath);
                PlayerCanvasManager.instance.UpdateSoulsCollectedOnPlayerGUI();
            }
        }
    }
}