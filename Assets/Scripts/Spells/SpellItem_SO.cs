using UnityEngine;

namespace NT
{
    public class SpellItem_SO : ScriptableObject
    {
        [Header("Spell Info")]
        public Sprite spellIcon;
        public string spellName;
        public string spellAnimationName;
        public int spellID;

        [Header("Spell Cost")]
        public int spellCostPoints;

        [Header("Spell Types")]
        //  IF TRUE, IT BE INCANTATION, IF NOT, IT WILL BE SORCERY SPELL
        public bool isIncantation;

        [Header("Spell VFX Settings")]
        public GameObject spellBeforeCastVFX;
        public GameObject spellAlreadyCastVFX;

        [Header("Spell Description")]
        [TextArea] public string spellDescription;

        public virtual void TryToPerformCastASpell(CharacterManager character)
        {
            if (character.characterStatusManager.characterCurrentFocusPoints <= 0f)
                character.characterStatusManager.characterCurrentFocusPoints = 0f;

            if (spellCostPoints > character.characterStatusManager.characterCurrentFocusPoints)
                character.characterAnimationManager.CharacterPlayAnimation("Shrug", true);
        }

        public virtual void SuccesfullyCastASpell(CharacterManager character)
        {
            character.characterStatusManager.characterCurrentFocusPoints -= spellCostPoints;

            if (character.characterGUIManager.characterFocusPointsBar == null)
                return;

            character.characterGUIManager.characterFocusPointsBar.SetCurrentStatusPointsOfCharacter_GUI
                (character.characterStatusManager.characterCurrentFocusPoints);
        }
    }
}