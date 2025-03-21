using UnityEngine;

namespace NT
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Target Object")]
        public PlayerManager player;

        [Header("Camera Settings")]
        [SerializeField] private LayerMask cameraCollisionLayer;
        public float cameraFollowTargetSpeed = 10f;
        public float cameraLookUpAndDownSpeed = 15f;
        public float cameraLookLeftAndRightSpeed = 15f;
        public float cameraCollisionWithAnyObjectsSpeed = 15f;
        public float cameraMaximumLookUp = 35f;
        public float cameraMinimumLookDown = -35f;
        public float cameraSphereCheckRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float cameraMinimumCollisionOffset = 0.2f;

        public Transform playerCameraTransform;
        public Transform playerCameraPivotTransform;

        private Vector3 cameraVelocity;
        private Vector3 playerCameraPosition;
        private float cameraTargetPosition;
        private float cameraDefaultPosition;
        private float cameraUpAndDownAngle;
        private float cameraLeftAndRightAngle;

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
            cameraDefaultPosition = playerCameraTransform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleCameraFollowTarget();
                HandleCameraRotateLookAround();
                HandleCameraCollisionWithAnyObjects();
            }
        }

        private void HandleCameraFollowTarget()
        {
            Vector3 followTransform = Vector3.SmoothDamp
                (transform.position, player.transform.position, 
                ref cameraVelocity, cameraFollowTargetSpeed * Time.deltaTime);

            transform.position = followTransform;
        }

        private void HandleCameraRotateLookAround()
        {
            Vector3 rotateAround;
            Quaternion rotateCameraLookAroundBasedOnMouse;

            cameraLeftAndRightAngle += (PlayerInputManager.instance.mouseX_Input * cameraLookLeftAndRightSpeed) * Time.deltaTime;
            cameraUpAndDownAngle -= (PlayerInputManager.instance.mouseY_Input * cameraLookUpAndDownSpeed) * Time.deltaTime;
            cameraUpAndDownAngle = Mathf.Clamp(cameraUpAndDownAngle, cameraMinimumLookDown, cameraMaximumLookUp);

            rotateAround = Vector3.zero;
            rotateAround.y = cameraLeftAndRightAngle;
            rotateCameraLookAroundBasedOnMouse = Quaternion.Euler(rotateAround);
            transform.rotation = rotateCameraLookAroundBasedOnMouse;

            rotateAround = Vector3.zero;
            rotateAround.x = cameraUpAndDownAngle;
            rotateCameraLookAroundBasedOnMouse = Quaternion.Euler(rotateAround);
            playerCameraPivotTransform.localRotation = rotateCameraLookAroundBasedOnMouse;
        }

        private void HandleCameraCollisionWithAnyObjects()
        {
            cameraTargetPosition = cameraDefaultPosition;

            RaycastHit hit;

            Vector3 cameraObjectDirection = playerCameraTransform.position - playerCameraPivotTransform.position;
            cameraObjectDirection.Normalize();

            if (Physics.SphereCast(playerCameraPivotTransform.position, cameraSphereCheckRadius, 
                cameraObjectDirection, out hit, Mathf.Abs(cameraTargetPosition), cameraCollisionLayer))
            {
                float distance = Vector3.Distance(playerCameraPivotTransform.position, hit.point);
                cameraTargetPosition = -(distance - cameraCollisionOffset);
            }

            if (Mathf.Abs(cameraTargetPosition) < cameraMinimumCollisionOffset)
                cameraTargetPosition = -cameraMinimumCollisionOffset;

            playerCameraPosition.z = Mathf.Lerp
                (playerCameraTransform.localPosition.z, cameraTargetPosition, cameraCollisionWithAnyObjectsSpeed * Time.deltaTime);
            playerCameraTransform.localPosition = playerCameraPosition;
        }
    }
}