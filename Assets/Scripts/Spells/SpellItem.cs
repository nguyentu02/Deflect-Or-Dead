using UnityEngine;

namespace NT
{
    public class SpellItem : MonoBehaviour
    {
        [Header("Spell Info")]
        public Sprite spellIcon;
        public string spellName;
        public int spellID;

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

        }

        public virtual void SuccesfullyCastASpell(CharacterManager character)
        {

        }
    }
}