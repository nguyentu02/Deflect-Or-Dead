using UnityEngine;
using UnityEngine.AI;

namespace NT
{
    public class EnemyManager : CharacterManager
    {
        public NavMeshAgent navMeshAgent;

        public EnemyAnimationManager enemyAnimationManager;
        public EnemyCombatManager enemyCombatManager;

        [Header("Enemy Current State")]
        public AISate enemyCurrentState;

        [Header("Enemy Finite State Machine")]
        public AIIdleState enemyIdleState;
        public AIChasingState enemyChasingState;
        public AICombatStanceState enemyCombatStanceState;
        public AIAttackTargetState enemyAttackTargetState;

        //  DEBUG COOL DOWN AFTER ATTACK (USED TO TRACK WHEN WE CAN ATTACK AGAIN)
        public float timeToNextAttack;

        //  DEBUG ROTATION SPEED
        public float DEBUG_enemyRotationSpeed = 15f;

        protected override void Awake()
        {
            base.Awake();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            enemyAnimationManager = GetComponent<EnemyAnimationManager>();
            enemyCombatManager = GetComponent<EnemyCombatManager>();


            enemyIdleState = GetComponentInChildren<AIIdleState>();
            enemyChasingState = GetComponentInChildren<AIChasingState>();
            enemyCombatStanceState = GetComponentInChildren<AICombatStanceState>();
            enemyAttackTargetState = GetComponentInChildren<AIAttackTargetState>();

            enemyCurrentState = enemyIdleState;
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
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            DEBUG_StitchNavmeshIntoEnemyGameObject();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }

        protected virtual void HandleEnemyCharacterStates()
        {
            if (enemyCurrentState != null)
            {
                AISate nextState = enemyCurrentState.SwitchToState(this);

                if (nextState != null)
                    enemyCurrentState = nextState;
            }
        }

        private void DEBUG_HandleCoolDownUntilEnemyCanAttackTargetAgain()
        {
            if (timeToNextAttack > 0)
                timeToNextAttack -= Time.deltaTime;
        }

        //  DEBUG NAVMESH AGENT
        protected virtual void DEBUG_EnemyNavmeshRotateTowardsTarget()
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
    }
}