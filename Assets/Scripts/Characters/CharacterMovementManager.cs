using UnityEngine;

namespace NT
{
    public class CharacterMovementManager : MonoBehaviour
    {
        private CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }
    }
}