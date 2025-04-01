using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Target Object")]
        public PlayerManager player;

        [SerializeField] private float maximumLockOnDistance = 30f;
        [SerializeField] private List<CharacterManager> availableCharactersCanTarget = new List<CharacterManager>();
        public CharacterManager nearestLockOnTarget;
        public CharacterManager leftNearestLockOnTarget;
        public CharacterManager rightNearestLockOnTarget;

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
        public float cameraLockedOnHeight = 2.0f;
        public float cameraDefaultHeight = 1.65f;

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
            if (!player.playerCombatManager.isLockedOn && 
                player.playerCombatManager.currentTargetCharacter == null)
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
            else
            {
                float velocity = 0f;

                Vector3 direction = player.playerCombatManager.currentTargetCharacter.transform.position - transform.position;
                direction.Normalize();
                direction.y = 0f;

                Quaternion cameraRotation = Quaternion.LookRotation(direction);
                transform.rotation = cameraRotation;

                direction = player.playerCombatManager.currentTargetCharacter.transform.position - playerCameraPivotTransform.position;
                direction.Normalize();

                cameraRotation = Quaternion.LookRotation(direction);
                Vector3 eulerAngles = cameraRotation.eulerAngles;
                eulerAngles.y = 0f;
                playerCameraPivotTransform.localEulerAngles = eulerAngles;
            }
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

        //  DEBUG
        public void HandleCameraLockOnTarget()
        {
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = -Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, maximumLockOnDistance);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterTargeted = colliders[i].GetComponent<CharacterManager>();

                if (characterTargeted != null)
                {
                    Vector3 lockOnTargetDirection = characterTargeted.transform.position - player.transform.position;
                    float distanceFromTarget = Vector3.Distance(player.transform.position, characterTargeted.transform.position);
                    float viewableAngle = Vector3.Angle(lockOnTargetDirection, playerCameraTransform.forward);

                    if (characterTargeted.transform.root != player.transform.root &&
                        viewableAngle > -50f && viewableAngle < 50f && 
                        distanceFromTarget <= maximumLockOnDistance)
                    {
                        availableCharactersCanTarget.Add(characterTargeted);
                    }
                }
            }

            for (int j = 0; j < availableCharactersCanTarget.Count; j++)
            {
                float distanceFromTarget = Vector3.Distance
                    (player.transform.position, availableCharactersCanTarget[j].transform.position);

                if (distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableCharactersCanTarget[j];
                }

                if (player.playerCombatManager.isLockedOn)
                {
                    Vector3 relativeEnemyPosition =
                        player.transform.InverseTransformPoint(availableCharactersCanTarget[j].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;

                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget && 
                        availableCharactersCanTarget[j] != player.playerCombatManager.currentTargetCharacter)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftNearestLockOnTarget = availableCharactersCanTarget[j];
                    }

                    if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget && 
                        availableCharactersCanTarget[j] != player.playerCombatManager.currentTargetCharacter)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightNearestLockOnTarget = availableCharactersCanTarget[j];
                    }
                }
            }
        }

        //  DEBUG
        public void ClearAllLockedOnTargets()
        {
            availableCharactersCanTarget.Clear();
            nearestLockOnTarget = null;
            player.playerCombatManager.currentTargetCharacter = null;
        }

        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newCameraHeightLockedOn = new Vector3(0, cameraLockedOnHeight);
            Vector3 defaultCameraHeightUnlocked = new Vector3(0, cameraDefaultHeight);

            if (player.playerCombatManager.currentTargetCharacter != null)
            {
                playerCameraPivotTransform.transform.localPosition = Vector3.SmoothDamp
                    (playerCameraPivotTransform.transform.localPosition, newCameraHeightLockedOn, 
                    ref velocity, Time.deltaTime);
            }
            else
            {
                playerCameraPivotTransform.transform.localPosition = Vector3.SmoothDamp
                    (playerCameraPivotTransform.transform.localPosition, defaultCameraHeightUnlocked, 
                    ref velocity, Time.deltaTime);
            }
        }
    }
}