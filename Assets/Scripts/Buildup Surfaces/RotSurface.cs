using UnityEngine;

namespace NT
{
    public class RotSurface : BuildupSurfaces
    {
        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            foreach (var characterInPoisonSurface in charactersInBuildupSurface)
            {
                if (characterInPoisonSurface.characterEffectsManager.isRottened)
                    return;

                characterInPoisonSurface.characterEffectsManager.rotCurrentBuildup +=
                    buildupSurfacePerSeconds * Time.deltaTime;
            }
        }
    }
}