using UnityEngine;

namespace NT
{
    public class AICharacterDamageReceiverManager : CharacterDamageReceiverManager
    {
        private AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public override void CharacterDamageReceiver
            (float damageReceiver, string damageAnimation = "core_main_hit_reaction_medium_f_01", 
            bool isHasDamageAnimtion = false, bool isHasNewDeadAnimation = false)
        {
            base.CharacterDamageReceiver
                (damageReceiver, damageAnimation, isHasDamageAnimtion, isHasNewDeadAnimation);

            if (aiCharacter.aiCharacterGUIManager.aiCharacterHealthPointsBar != null)
                aiCharacter.aiCharacterGUIManager.aiCharacterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (aiCharacter.characterStatusManager.characterCurrentHealth);
        }
    }
}