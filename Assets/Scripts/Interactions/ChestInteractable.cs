using System.Collections;
using UnityEngine;

namespace NT
{
    public class ChestInteractable : Interactable
    {
        private Animator animator;

        [Header("Items Will Spawn After Chest Open")]
        [SerializeField] WeaponItem_SO weaponItemInChest;
        [SerializeField] GameObject itemSpawner;

        [Header("Chest Standing Position When Opening")]
        [SerializeField] Transform chestOpenStandingPosition;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
        }

        public override void InteractWithAnObject(PlayerManager player)
        {
            //  MOVE TO STANDING POSITION
            player.characterController.Move
                (chestOpenStandingPosition.transform.position - player.transform.position);

            //  ROTATE TOWARDS
            Vector3 chestRotate = player.transform.position - transform.position;
            chestRotate.y = 0f;
            chestRotate.Normalize();

            Quaternion rotate = Quaternion.LookRotation(chestRotate);
            transform.rotation = rotate;

            //  ROTATE TOWARDS
            Vector3 playerRotate = transform.position - player.transform.position;
            playerRotate.y = 0f;
            playerRotate.Normalize();

            rotate = Quaternion.LookRotation(playerRotate);
            player.transform.rotation = rotate;

            WeaponItemInteractable itemSpawned = itemSpawner.GetComponent<WeaponItemInteractable>();

            if (itemSpawned != null)
                itemSpawned.weapon = weaponItemInChest;

            StartCoroutine(DelaySpawnItemInChest());

            player.playerAnimationManager.CharacterPlayAnimation("Character_Open_Chest_01", true);
            animator.Play("Chest Open");
        }

        private IEnumerator DelaySpawnItemInChest()
        {
            PlayerCanvasManager.instance.DEBUG_TurnOffMessageAlertPopUp();

            yield return new WaitForSeconds(2f);
            GameObject itemSpawned = Instantiate(itemSpawner, transform);
            Destroy(this);
        }
    }
}