using UnityEngine;

namespace NT
{
    public class BossManager : AICharacterManager
    {
        [Header("Boss Character Settings")]
        [SerializeField] string bossName;

        [Header("DEBUG Tesing Bosses")]
        [SerializeField] BossCharacterStatsBar_GUI DEBUG_BossHealthBar_GUI;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}