using UnityEngine;

namespace NT
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Debug Testing")]
        [SerializeField] Animator animator;
        [SerializeField] int maxHealth = 1000;
        [SerializeField] float currentHealth = 0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
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