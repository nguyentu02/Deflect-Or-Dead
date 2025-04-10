using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class BuildupSurfaces : MonoBehaviour
    {
        [Header("Buildup Surface Settings")]
        [SerializeField] protected float buildupSurfacePerSeconds;
        [SerializeField] protected List<CharacterManager> charactersInBuildupSurface;

        protected virtual void OnTriggerEnter(Collider other)
        {
            CharacterManager characterInSurface = other.GetComponent<CharacterManager>();

            if (characterInSurface != null)
            {
                if (charactersInBuildupSurface.Contains(characterInSurface))
                    return;

                charactersInBuildupSurface.Add(characterInSurface);
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {

        }

        protected virtual void OnTriggerExit(Collider other)
        {
            CharacterManager characterInSurface = other.GetComponent<CharacterManager>();

            if (characterInSurface != null)
                charactersInBuildupSurface.Remove(characterInSurface);
        }
    }
}