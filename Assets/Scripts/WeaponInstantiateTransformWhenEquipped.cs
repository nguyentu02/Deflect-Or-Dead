using UnityEngine;

namespace NT
{
    public class WeaponInstantiateTransformWhenEquipped : MonoBehaviour
    {
        [Header("What Slot?")]
        public WeaponInstantiateSlot weaponInstantiateSlot;

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

                //  DEBUG FOR SPAWN GAMEOBJECTS MODELS
                switch (weaponInstantiateSlot)
                {
                    case WeaponInstantiateSlot.MainHandSlot:
                        weaponGameObject.transform.parent = transform;
                        weaponGameObject.transform.localPosition = Vector3.zero;
                        weaponGameObject.transform.localRotation = Quaternion.identity;
                        break;
                    case WeaponInstantiateSlot.OffHandSlot:
                        weaponGameObject.transform.parent = transform;
                        weaponGameObject.transform.localPosition = Vector3.zero;
                        weaponGameObject.transform.localRotation = Quaternion.identity;
                        break;
                    case WeaponInstantiateSlot.OffHand_Shield_Slot:
                        weaponGameObject.transform.parent = transform;
                        weaponGameObject.transform.localPosition = Vector3.zero;
                        weaponGameObject.transform.localRotation = Quaternion.identity;
                        break;
                    case WeaponInstantiateSlot.BackSlot:
                        //  DEBUG RIGHT NOW FOR TWO HANDED STYLE
                        if (weapon.weaponType != WeaponType.Shield_Weapon)
                        {
                            //  IF WEAPON IS NOT A SHIELD, JUST SPAWN NORMALY
                            weaponGameObject.transform.parent = transform;
                            weaponGameObject.transform.localPosition = Vector3.zero;
                            weaponGameObject.transform.localRotation = Quaternion.identity;
                        }
                        //  OTHERWISE, IF IS SHIELD, SPAWN WITH THAT VALUES
                        else
                        {
                            weaponGameObject.transform.parent = transform;
                            weaponGameObject.transform.localPosition = new Vector3(0.212543f, -0.1514575f, -0.4658348f);
                            weaponGameObject.transform.localRotation = Quaternion.Euler(-22.719f, 65.157f, 149.634f);
                        }
                        break;
                    default:
                        break;
                }

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