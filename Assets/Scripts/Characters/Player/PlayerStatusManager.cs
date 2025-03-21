using UnityEngine;

namespace NT
{
    public class PlayerStatusManager : CharacterStatusManager
    {
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }
    }
}