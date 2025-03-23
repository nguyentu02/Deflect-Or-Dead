using UnityEngine;

namespace NT
{
    public class EnemyManager : CharacterManager
    {
        [Header("Debug Testing")]
        public Transform lockOnTransform;
        [SerializeField] Animator animator;
        [SerializeField] int maxHealth = 1000;
        [SerializeField] float currentHealth = 0f;

        protected override void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            currentHealth = maxHealth;
        }

        protected override void Update()
        {
            
        }

        protected override void FixedUpdate()
        {
            
        }

        protected override void LateUpdate()
        {
           
        }

        public void EnemyDamageReceiver(float damage)
        {
            currentHealth -= damage;

            animator.Play("core_main_hit_reaction_medium_f_01");

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;

                animator.Play("straight_sword_main_death_01");
            }
        }
    }
}