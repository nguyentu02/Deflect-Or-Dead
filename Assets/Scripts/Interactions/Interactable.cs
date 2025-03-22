using UnityEngine;

namespace NT
{
    public class Interactable : MonoBehaviour
    {
        protected Collider interactCollider;

        [Header("Interact Settings")]
        public string interactObjectText;
        [SerializeField] protected float interactRadius = 0.5f;

        protected virtual void Awake()
        {
            interactCollider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }

        public virtual void InteractWithAnObject(PlayerManager player)
        {

        }
    }
}