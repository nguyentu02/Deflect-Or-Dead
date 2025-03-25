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

        public virtual void CharacterDamageReceiver(float damageReceiver)
        {
            character.characterStatusManager.characterCurrentHealth -= damageReceiver;

            //  JUST DEBUG FOR PLAYTEST NOW, WILL REFACTOR LATER
            character.characterAnimationManager.CharacterPlayAnimation("core_main_hit_reaction_medium_f_01", true);

            if (character.characterStatusManager.characterCurrentHealth <= 0f)
            {
                character.characterStatusManager.characterCurrentHealth = 0f;

                character.characterAnimationManager.CharacterPlayAnimation("straight_sword_main_death_01", true);
            }
        }
    }
}