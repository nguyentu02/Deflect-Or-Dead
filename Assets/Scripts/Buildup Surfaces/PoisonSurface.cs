using UnityEngine;

namespace NT
{
    public class PoisonSurface : BuildupSurfaces
    {
        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            foreach (var characterInPoisonSurface in charactersInBuildupSurface)
            {
                if (characterInPoisonSurface.characterEffectsManager.isPoisoned)
                    return;

                characterInPoisonSurface.characterEffectsManager.poisonCurrentBuildup +=
                    buildupSurfacePerSeconds * Time.deltaTime;
            }
        }
    }
}