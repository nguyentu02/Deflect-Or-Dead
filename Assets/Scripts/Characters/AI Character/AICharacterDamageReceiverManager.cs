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
            (float physicalDamage, float magicDamage, float fireDamage, float holyDamage, float lightningDamage, 
            string damageAnimation = "core_main_hit_reaction_medium_f_01", 
            bool isHasDamageAnimtion = false, bool isHasNewDeadAnimation = false, bool isCanMoveWhileGetHit = false)
        {
            base.CharacterDamageReceiver(physicalDamage, magicDamage, fireDamage, holyDamage, lightningDamage, 
                damageAnimation, isHasDamageAnimtion, isHasNewDeadAnimation, isCanMoveWhileGetHit);

            if (aiCharacter.aiCharacterGUIManager.aiCharacterHealthPointsBar != null)
                aiCharacter.aiCharacterGUIManager.aiCharacterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                    (aiCharacter.characterStatusManager.characterCurrentHealth);
        }
    }
}