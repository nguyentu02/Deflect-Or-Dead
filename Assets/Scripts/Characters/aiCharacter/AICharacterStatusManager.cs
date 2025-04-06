using UnityEngine;

namespace NT
{
    public class AICharacterStatusManager : CharacterStatusManager
    {
        private AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}