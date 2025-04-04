using UnityEngine;

namespace NT
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
    }
}