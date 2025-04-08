using System.Collections;
using UnityEngine;

namespace NT
{
    public class FogWallInteractable : Interactable
    {
        [Header("Fog Wall Collider Settings")]
        [SerializeField] Collider fogWallCollider;

        private Coroutine passThroughTheFogCoroutine;

        [SerializeField] float DEBUG_TimeToBlockAfterPlayerPassFog = 1f;

        protected override void Awake()
        {
            base.Awake();

            fogWallCollider = GetComponent<Collider>();
        }

        public override void InteractWithAnObject(PlayerManager player)
        {
            base.InteractWithAnObject(player);

            Vector3 direction = player.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(direction);
            player.transform.rotation = rotation;

            DelayCheckPlayerPassThroughTheFogWithCoroutine();

            player.playerAnimationManager.CharacterPlayAnimation("core_main_fog_F_01_stub", true);
        }

        private void DelayCheckPlayerPassThroughTheFogWithCoroutine()
        {
            if (passThroughTheFogCoroutine != null)
                StopCoroutine(passThroughTheFogCoroutine);

            passThroughTheFogCoroutine = StartCoroutine(EnableAndDisableFogColliderForBossFightEventCoroutine());
        }

        private IEnumerator EnableAndDisableFogColliderForBossFightEventCoroutine()
        {
            fogWallCollider.isTrigger = true;

            yield return new WaitForSeconds(DEBUG_TimeToBlockAfterPlayerPassFog);

            fogWallCollider.isTrigger = false;
            Destroy(this);
        }
    }
}