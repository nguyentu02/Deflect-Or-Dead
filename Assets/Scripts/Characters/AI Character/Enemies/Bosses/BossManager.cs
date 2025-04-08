using UnityEngine;

namespace NT
{
    public class BossManager : AICharacterManager
    {
        [HideInInspector] public BossCombatManager bossCombatManager;
        [HideInInspector] public BossStatusManager bossStatusManager;
        [HideInInspector] public BossGUIManager bossGUIManager;

        [Header("The Boss Status Flags")]
        public bool isDefeated = false;
        public bool isSecondPhase = false;
        public bool isRest = false;

        [Header("The Boss States")]
        public BossAttackTargetState bossAttackTargetState;

        [Header("DEBUG The Boss Second Phase")]
        [SerializeField] GameObject DEBUG_bossSecondPhaseVFX;
        [SerializeField] GameObject DEBUG_bossModelFirstPhaseGameObject;
        [SerializeField] GameObject DEBUG_bossModelSecondPhaseGameOobject;

        protected override void Awake()
        {
            base.Awake();

            bossCombatManager = GetComponent<BossCombatManager>();
            bossStatusManager = GetComponent<BossStatusManager>();
            bossGUIManager = GetComponent<BossGUIManager>();

            //  BOSS STATES
            bossAttackTargetState = GetComponentInChildren<BossAttackTargetState>();

            aiCurrentState = aiAmbushState;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (isRest)
                return;

            base.Update();

            ProcessBossSecondPhase();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void LateUpdate()
        {

        }

        public virtual void ProcessBossSecondPhase()
        {
            if (bossStatusManager.characterCurrentHealth <= bossStatusManager.characterMaxHealth / 2f)
            {
                if (!isSecondPhase)
                {
                    isSecondPhase = true;

                    aiCharaterAnimationManager.CharacterPlayAnimation("Protector_Attack14_Buff_Root", true);

                    GameObject bossPhase2VFX = Instantiate(DEBUG_bossSecondPhaseVFX, transform);

                    DEBUG_bossModelFirstPhaseGameObject.SetActive(false);
                    DEBUG_bossModelSecondPhaseGameOobject.SetActive(true);
                }
            }
        }
    }
}