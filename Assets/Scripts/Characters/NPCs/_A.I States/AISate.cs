using UnityEngine;

namespace NT
{
    public abstract class AISate : MonoBehaviour
    {
        public abstract AISate SwitchToState(CharacterManager character);

        public virtual void ResetStateFlagsBeforeChangesState()
        {

        }
    }
}