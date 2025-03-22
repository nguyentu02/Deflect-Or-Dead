using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        private PlayerManager player;

        [Header("Player Interaction Layermask")]
        [SerializeField] private LayerMask interactableLayer;

        public List<Interactable> interactableObjects = new List<Interactable>();

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            
        }

        public void CheckForPlayerInteractionProcess()
        {
            RaycastHit hit;
            List<Interactable> objectsToRemove = new List<Interactable>(interactableObjects);

            if (Physics.SphereCast
                (player.transform.position, 0.5f, player.transform.forward, out hit, 0.77f, interactableLayer))
            {
                Interactable interactWithObject = hit.collider.GetComponent<Interactable>();

                if (interactWithObject == null)
                    return;

                if (!interactableObjects.Contains(interactWithObject))
                    interactableObjects.Add(interactWithObject);

                objectsToRemove.Remove(interactWithObject);
            }

            Collider[] colliders = Physics.OverlapSphere
                (player.transform.position + transform.forward * 0.5f, 0.5f, interactableLayer);

            foreach (Collider collider in colliders)
            {
                Interactable objectInPlayerRange = collider.GetComponent<Interactable>();

                if (objectInPlayerRange == null)
                    return;

                if (!interactableObjects.Contains(objectInPlayerRange))
                    interactableObjects.Add(objectInPlayerRange);

                objectsToRemove.Remove(objectInPlayerRange);
            }

            foreach (var @object in objectsToRemove)
            {
                interactableObjects.Remove(@object);
            }
        }
    }
}