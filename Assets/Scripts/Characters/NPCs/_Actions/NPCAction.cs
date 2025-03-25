using UnityEngine;

namespace NT
{
    public class NPCAction : ScriptableObject
    {
        public string nameActionAnimation;

        public virtual void NPCPerformAnAction(CharacterManager character)
        {
            character.characterAnimationManager.CharacterPlayAnimation(nameActionAnimation, true);
        }
    }
}