using UnityEngine;

namespace NT
{
    public class BossGUIManager : AICharacterGUIManager
    {
        private BossManager bossCharacter;

        [Header("Boss Character Settings")]
        [SerializeField] string bossName;

        [Header("DEBUG Tesing Health Bar Of Bosses")]
        public BossCharacterStatsBar_GUI DEBUG_BossHealthBar_GUI;
        [SerializeField] GameObject DEBUG_BossHealthBarGameObject;
        [SerializeField] RectTransform DEBUG_BossHealthBarInstantiateRectTransform;

        protected override void Awake()
        {
            base.Awake();

            bossCharacter = GetComponent<BossManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        public void ShowUpTheBossHealthBarAfterPlayerPassThroughTheFogWall_GUI()
        {
            GameObject bossHealthBar = Instantiate
                (DEBUG_BossHealthBarGameObject, DEBUG_BossHealthBarInstantiateRectTransform);

            DEBUG_BossHealthBar_GUI = bossHealthBar.GetComponent<BossCharacterStatsBar_GUI>();

            aiCharacterHealthPointsBar = DEBUG_BossHealthBar_GUI;

            DEBUG_BossHealthBar_GUI.SetTheBossName_GUI(bossName);

            DEBUG_BossHealthBar_GUI.SetMaximumStatusPointsOfCharacter_GUI
                (bossCharacter.bossStatusManager.characterMaxHealth);
        }
    }
}