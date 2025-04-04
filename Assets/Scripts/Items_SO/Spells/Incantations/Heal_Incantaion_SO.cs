using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Spells/Incantations/Heal Incantation")]
    public class Heal_Incantaion_SO : IncantationItem_SO
    {
        [Header("Heal Settings")]
        public float healAmount = 85f;

        public override void TryToPerformCastASpell(CharacterManager character)
        {
            base.TryToPerformCastASpell(character);

            if (character.characterStatusManager.characterCurrentFocusPoints <= 0f)
                return;

            GameObject instantiatedWarmUpVFX = Instantiate(spellBeforeCastVFX, character.transform);
            character.characterAnimationManager.CharacterPlayAnimation(spellAnimationName, true);
            Destroy(instantiatedWarmUpVFX, 1f);
        }

        public override void SuccesfullyCastASpell(CharacterManager character)
        {
            base.SuccesfullyCastASpell(character);

            GameObject instantiatedAlreadyCastVFX = Instantiate(spellAlreadyCastVFX, character.transform);
            character.characterStatusManager.characterCurrentHealth += healAmount;
            Destroy(instantiatedAlreadyCastVFX, 1f);

            if (character.characterGUIManager.characterHealthPointsBar == null)
                return;

            if (character.characterStatusManager.characterCurrentHealth > 
                character.characterStatusManager.characterMaxHealth)
                character.characterStatusManager.characterCurrentHealth = 
                    character.characterStatusManager.characterMaxHealth;

            character.characterGUIManager.characterHealthPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (character.characterStatusManager.characterCurrentHealth);
        }
    }
}