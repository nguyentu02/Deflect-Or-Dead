using UnityEngine;

namespace NT
{
    public class BossStatusManager : AICharacterStatusManager
    {
        private BossManager bossCharacter;

        protected override void Awake()
        {
            base.Awake();

            bossCharacter = GetComponent<BossManager>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}