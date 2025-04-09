using UnityEngine;

namespace NT
{
    public class WeaponVFX : MonoBehaviour
    {
        [Header("Weapon VFX")]
        public ParticleSystem weaponNormalTrailVFX;

        public virtual void PlayWeaponTrailVFX()
        {
            if (weaponNormalTrailVFX == null)
                return;

            weaponNormalTrailVFX.Stop();
            weaponNormalTrailVFX.Play();
        }
    }
}