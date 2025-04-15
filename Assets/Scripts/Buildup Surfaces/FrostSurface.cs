using UnityEngine;

namespace NT
{
    public class FrostSurface : BuildupSurfaces
    {
        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            foreach (var characterInPoisonSurface in charactersInBuildupSurface)
            {
                if (characterInPoisonSurface.characterEffectsManager.isFrosted)
                    return;

                characterInPoisonSurface.characterEffectsManager.frostCurrentBuildup +=
                    buildupSurfacePerSeconds * Time.deltaTime;
            }
        }
    }
}
