using UnityEngine;

namespace NT
{
    public class PlayerGUIManager : CharacterGUIManager
    {
        private PlayerManager player;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}