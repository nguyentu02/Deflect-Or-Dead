using UnityEngine;

namespace NT
{
    public class DestroyThisObjectAfterTime : MonoBehaviour
    {
        [SerializeField] float destroyAfterTime = 2f;

        private void Start()
        {
            Destroy(gameObject, destroyAfterTime);
        }
    }
}