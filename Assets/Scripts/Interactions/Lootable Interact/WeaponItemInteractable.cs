using UnityEngine;

namespace NT
{
    public class WeaponItemInteractable : Interactable
    {
        private SphereCollider weaponInteractCollider;

        [Header("Weapon Items Choosen For Pick Up")]
        public WeaponItem_SO weapon;
        //[SerializeField] private WeaponItem_SO[] weapons;

        protected override void Awake()
        {
            weaponInteractCollider = GetComponent<SphereCollider>();
        }

        protected override void Start()
        {
            base.Start();

            weaponInteractCollider.radius = interactRadius * 10f;
        }

        public override void InteractWithAnObject(PlayerManager player)
        {
            base.InteractWithAnObject(player);

            player.characterController.Move(Vector3.zero);
            player.playerInventoryManager.playerWeaponInventories.Add(weapon);
            player.playerAnimationManager.CharacterPlayAnimation("Pick Up Item", true);

            Destroy(gameObject);
        }
    }
}