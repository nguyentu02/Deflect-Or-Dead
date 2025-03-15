using UnityEngine;

namespace NT
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        public Camera playerCamera;
        public Transform playerCameraTransform;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            playerCameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            
        }
    }
}