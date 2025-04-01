using UnityEngine;

namespace NT
{
    public abstract class AISate : MonoBehaviour
    {
        public abstract AISate SwitchToState(AICharacterManager aiCharacter);

        public virtual void ResetStateFlagsBeforeChangesState()
        {

        }
    }
}