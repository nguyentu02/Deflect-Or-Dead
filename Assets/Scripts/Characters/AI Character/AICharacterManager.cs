using UnityEngine;
using UnityEngine.AI;

namespace NT
{
    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;

        [HideInInspector] public AICharacterAnimationManager aiCharaterAnimationManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
        [HideInInspector] public AICharacterGUIManager aiCharacterGUIManager;
        [HideInInspector] public AICharacterDamageReceiverManager aiCharacterDamageReceiverManager;
        [HideInInspector] public AICharacterStatusManager aiCharacterStatusManager;

        [Header("AI Enemy Character Type")]
        public EnemyType enemyType;

        [Header("Enemy Current State")]
        public AISate aiCurrentState;

        [Header("Enemy Finite State Machine")]
        [HideInInspector] public AIAmbushState aiAmbushState;
        [HideInInspector] public AIIdleState aiIdleState;
        [HideInInspector] public AIChasingState aiChasingState;
        [HideInInspector] public AICombatStanceState aiCombatStanceState;
        [HideInInspector] public AIAttackTargetState aiAttackTargetState;

        [Header("Enemy Target Tracking Values")]
        public Vector3 targetsDirection;
        public float viewableAngles;
        public float distanceToTarget;

        //  DEBUG COOL DOWN AFTER ATTACK (USED TO TRACK WHEN WE CAN ATTACK AGAIN)
        public float timeToNextAttack;

        //  DEBUG ROTATION SPEED
        public float DEBUG_enemyRotationSpeed = 15f;

        protected override void Awake()
        {
            base.Awake();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            aiCharaterAnimationManager = GetComponent<AICharacterAnimationManager>();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterGUIManager = GetComponent<AICharacterGUIManager>();
            aiCharacterDamageReceiverManager = GetComponent<AICharacterDamageReceiverManager>();
            aiCharacterStatusManager = GetComponent<AICharacterStatusManager>();

            //  ENEMY STATE MACHINE COMPONENTS
            aiAmbushState = GetComponentInChildren<AIAmbushState>();
            aiIdleState = GetComponentInChildren<AIIdleState>();
            aiChasingState = GetComponentInChildren<AIChasingState>();
            aiCombatStanceState = GetComponentInChildren<AICombatStanceState>();
            aiAttackTargetState = GetComponentInChildren<AIAttackTargetState>();

            aiCurrentState = aiIdleState;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            HandleEnemyCharacterStates();

            DEBUG_HandleCoolDownUntilEnemyCanAttackTargetAgain();

            if (enemyType == EnemyType.TheBoss)
                return;

            aiCharacterGUIManager.DEBUG_ShowUpAICharacterHealthBarOnHeadForPlayerSee_GUI();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            DEBUG_StitchNavmeshIntoEnemyGameObject();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            aiCharacterGUIManager.DEBUG_RotateAICharacterHealthBarToPlayerMainCamera_GUI();
        }

        protected virtual void HandleEnemyCharacterStates()
        {
            if (aiCurrentState != null)
            {
                AISate nextState = aiCurrentState.SwitchToState(this);

                if (nextState != null)
                    aiCurrentState = nextState;
            }

            DEBUG_AITrackingValuesOfTargetCharacter();
        }

        private void DEBUG_HandleCoolDownUntilEnemyCanAttackTargetAgain()
        {
            if (timeToNextAttack > 0)
                timeToNextAttack -= Time.deltaTime;
        }

        //  DEBUG NAVMESH AGENT
        public virtual void DEBUG_EnemyNavmeshRotateTowardsTarget()
        {
            transform.rotation = navMeshAgent.transform.rotation;
        }

        protected void DEBUG_StitchNavmeshIntoEnemyGameObject()
        {
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        public void DEBUG_EnemyManuallyRotateTowardsTarget()
        {
            Vector3 direction = characterCombatManager.currentTargetCharacter.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
                direction = transform.forward;

            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, DEBUG_enemyRotationSpeed * Time.deltaTime);
        }

        private void DEBUG_AITrackingValuesOfTargetCharacter()
        {
            if (characterCombatManager.currentTargetCharacter != null)
            {
                targetsDirection = characterCombatManager.currentTargetCharacter.transform.position - transform.position;
                distanceToTarget = Vector3.Distance(characterCombatManager.currentTargetCharacter.transform.position, transform.position);
                viewableAngles = Vector3.SignedAngle(targetsDirection, transform.forward, Vector3.up);
            }
        }
    }
}