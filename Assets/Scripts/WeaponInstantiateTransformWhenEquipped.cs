using UnityEngine;

namespace NT
{
    public class WeaponInstantiateTransformWhenEquipped : MonoBehaviour
    {
        [Header("What Hand?")]
        public bool isMainHand;

        [Header("Weapon Prefab Instantiated")]
        public GameObject weaponPrefabInstantiatedInThisHand;

        public virtual void LoadWeaponPrefabModelInCharacterHand(WeaponItem_SO weapon)
        {
            UnloadWeaponPrefab();

            if (weapon == null)
            {
                UnloadWeaponPrefab();
                return;
            }

            GameObject weaponGameObject = Instantiate(weapon.itemModelPrefab) as GameObject;

            if (weaponGameObject != null)
            {
                weaponPrefabInstantiatedInThisHand = weaponGameObject;
                weaponGameObject.transform.parent = transform;
                weaponGameObject.transform.localPosition = Vector3.zero;
                weaponGameObject.transform.localRotation = Quaternion.identity;
                weaponGameObject.transform.localScale = Vector3.one;
            }
        }

        public virtual void UnloadWeaponPrefab()
        {
            if (weaponPrefabInstantiatedInThisHand != null)
                Destroy(weaponPrefabInstantiatedInThisHand);
        }
    }
}