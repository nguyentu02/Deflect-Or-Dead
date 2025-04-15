using UnityEngine;

namespace NT
{
    public class BleedSurface : BuildupSurfaces
    {
        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            foreach (var characterInPoisonSurface in charactersInBuildupSurface)
            {
                characterInPoisonSurface.characterEffectsManager.bleedCurrentBuildup +=
                    buildupSurfacePerSeconds * Time.deltaTime;
            }
        }
    }
}