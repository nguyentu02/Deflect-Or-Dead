using UnityEngine;
using UnityEngine.TextCore.Text;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Ashes Of War/Transient Moonlight")]
    public class TransientMoonlight_AshOfWar_SO : AshOfWarItem_SO
    {
        [Header("Moonlight Projectile Settings")]
        [SerializeField] float moonlightUpwardVelocity;
        [SerializeField] float moonlightForwardVelocity;

        [Header("Moonlight VFXs")]
        [SerializeField] GameObject moonlightLightVFX;
        [SerializeField] GameObject moonlightHeavyVFX;

        public override void CharacterPlayingAshOfWar(CharacterManager characterWhoPlay)
        {
            characterWhoPlay.characterAnimationManager.CharacterPlayAnimation
                (ashOfWarAnimation, true, true, true, false);
        }

        public void CharacterPlayingLightAttackTransientMoonlight(CharacterManager characterWhoPlay)
        {
            if (!characterWhoPlay.isPerformingAction)
                return;

            characterWhoPlay.characterCombatManager.isLightAttack = true;

            Vector3 spawnVFXPosition = characterWhoPlay.transform.position + Vector3.up * 1f;

            GameObject moonlightVFX = Instantiate
            (moonlightLightVFX, spawnVFXPosition,
                PlayerCameraManager.instance.playerCameraPivotTransform.rotation);
            Rigidbody moonlightRigidbody = moonlightVFX.GetComponentInChildren<Rigidbody>();

            moonlightVFX.transform.parent = null;

            if (characterWhoPlay.characterCombatManager.isLockedOn)
            {
                moonlightVFX.transform.LookAt
                    (characterWhoPlay.characterCombatManager.
                    currentTargetCharacter.characterCombatManager.lockOnTransform.position);
            }
            else
            {
                Vector3 forwardDirection = characterWhoPlay.transform.forward;
                moonlightVFX.transform.forward = forwardDirection;
            }

            Vector3 upwardVelocity = moonlightVFX.transform.up * moonlightUpwardVelocity;
            Vector3 forwardVelocity = moonlightVFX.transform.forward * moonlightForwardVelocity;
            Vector3 totalVelocity = upwardVelocity + forwardVelocity;
            moonlightRigidbody.linearVelocity = totalVelocity;
        }

        public void CharacterPlayingHeavyAttackTransientMoonlight(CharacterManager characterWhoPlay)
        {
            if (!characterWhoPlay.isPerformingAction)
                return;

            characterWhoPlay.characterCombatManager.isHeavyAttack = true;

            Vector3 spawnVFXPosition = characterWhoPlay.transform.position + Vector3.up * 1f;

            GameObject moonlightVFX = Instantiate
            (moonlightHeavyVFX, spawnVFXPosition, Quaternion.identity);
            Rigidbody moonlightRigidbody = moonlightVFX.GetComponentInChildren<Rigidbody>();

            moonlightVFX.transform.parent = null;

            if (characterWhoPlay.characterCombatManager.isLockedOn)
            {
                moonlightVFX.transform.LookAt
                    (characterWhoPlay.characterCombatManager.
                    currentTargetCharacter.characterCombatManager.lockOnTransform.position);
            }
            else
            {
                Vector3 forwardDirection = characterWhoPlay.transform.forward;
                moonlightVFX.transform.forward = forwardDirection;
            }

            Vector3 upwardVelocity = moonlightVFX.transform.up * moonlightUpwardVelocity;
            Vector3 forwardVelocity = moonlightVFX.transform.forward * moonlightForwardVelocity;
            Vector3 totalVelocity = upwardVelocity + forwardVelocity;
            moonlightRigidbody.linearVelocity = totalVelocity;
        }
    }
}