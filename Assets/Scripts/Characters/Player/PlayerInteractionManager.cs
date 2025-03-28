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

            // USE RAYCAST FOR CHECK INTERACTABLE OBJECTS
            if (Physics.SphereCast
                (player.transform.position, 0.5f, player.transform.forward, out hit, 0.77f, interactableLayer))
            {
                // IF COLLIDER OF OBJECT NOT NULL, CONTINUE =>
                if (hit.collider != null)
                {
                    Interactable interactWithObject = hit.collider.GetComponent<Interactable>();

                    // IF INTERACTABLE SCRIPT IS NOT NULL, DO IT CODE
                    if (interactWithObject != null)
                    {
                        if (!interactableObjects.Contains(interactWithObject))
                            interactableObjects.Add(interactWithObject);

                        objectsToRemove.Remove(interactWithObject);
                    }
                }
            }

            // USE OVERLAPSPHERE FOR CHECK ITEM IS INRANGE OF PLAYER, PLAYER CAN INTERACT
            Collider[] colliders = Physics.OverlapSphere
                (player.transform.position + transform.forward * 0.5f, 0.5f, interactableLayer);

            foreach (Collider collider in colliders)
            {
                // CHECK COLLIDER NULL
                if (collider != null) 
                {
                    Interactable objectInPlayerRange = collider.GetComponent<Interactable>();

                    // IF INTERACTABLE SCRIPT IS NOT NULL, DO IT CODE
                    if (objectInPlayerRange != null)
                    {
                        if (!interactableObjects.Contains(objectInPlayerRange))
                            interactableObjects.Add(objectInPlayerRange);

                        objectsToRemove.Remove(objectInPlayerRange);
                    }
                }
            }

            // IF OBJECT IS DESTROY, REMOVE THEM TO MAKE SURE IT'S DOES'NT MAKE WE NULL SLOT
            interactableObjects.RemoveAll(obj => obj == null);

            foreach (var @object in objectsToRemove)
            {
                interactableObjects.Remove(@object);
            }
        }
    }
}