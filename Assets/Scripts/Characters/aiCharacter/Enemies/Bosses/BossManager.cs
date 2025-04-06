using UnityEngine;

namespace NT
{
    public class BossManager : AICharacterManager
    {
        [HideInInspector] public BossCombatManager bossCombatManager;
        [HideInInspector] public BossStatusManager bossStatusManager;
        [HideInInspector] public BossGUIManager bossGUIManager;

        protected override void Awake()
        {
            base.Awake();

            bossCombatManager = GetComponent<BossCombatManager>();
            bossStatusManager = GetComponent<BossStatusManager>();
            bossGUIManager = GetComponent<BossGUIManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void LateUpdate()
        {

        }
    }
}