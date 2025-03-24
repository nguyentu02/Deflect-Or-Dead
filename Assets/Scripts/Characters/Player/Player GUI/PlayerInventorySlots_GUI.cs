using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class PlayerInventorySlots_GUI : MonoBehaviour
    {
        [Header("Item Info")]
        [SerializeField] private Item_SO item;
        [SerializeField] private Image itemIcon;

        public virtual void AddItem(Item_SO newItem)
        {
            item = newItem;
            itemIcon.sprite = newItem.itemIcon;
            itemIcon.enabled = true;
            gameObject.SetActive(true);
        }

        public virtual void RemoveItem()
        {
            item = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            gameObject.SetActive(false);
        }

        public void PlayerEquipThisItem()
        {
            if (PlayerCanvasManager.instance.playerRightWeaponSlot_01_Selected)
            {
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Add
                    (PlayerManager.instance.playerEquipmentManager.weaponsInMainHandQuickSlots[0]);

                WeaponItem_SO weaponItem = item as WeaponItem_SO;

                PlayerManager.instance.playerEquipmentManager.weaponsInMainHandQuickSlots[0] = weaponItem;
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Remove(weaponItem);

                PlayerManager.instance.playerEquipmentManager.WhichCharacterHandWeWantToLoadWeaponIn(weaponItem, true);
            }
            else if (PlayerCanvasManager.instance.playerRightWeaponSlot_02_Selected)
            {
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Add
                    (PlayerManager.instance.playerEquipmentManager.weaponsInMainHandQuickSlots[1]);

                WeaponItem_SO weaponItem = item as WeaponItem_SO;

                PlayerManager.instance.playerEquipmentManager.weaponsInMainHandQuickSlots[1] = weaponItem;
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Remove(weaponItem);

                PlayerManager.instance.playerEquipmentManager.WhichCharacterHandWeWantToLoadWeaponIn(weaponItem, true);
            }
            else if (PlayerCanvasManager.instance.playerLeftWeaponSlot_01_Selected)
            {
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Add
                    (PlayerManager.instance.playerEquipmentManager.weaponsInOffHandQuickSlots[1]);

                WeaponItem_SO weaponItem = item as WeaponItem_SO;

                PlayerManager.instance.playerEquipmentManager.weaponsInOffHandQuickSlots[1] = weaponItem;
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Remove(weaponItem);

                PlayerManager.instance.playerEquipmentManager.WhichCharacterHandWeWantToLoadWeaponIn(weaponItem, false);
            }
            else if (PlayerCanvasManager.instance.playerLeftWeaponSlot_02_Selected)
            {
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Add
                    (PlayerManager.instance.playerEquipmentManager.weaponsInOffHandQuickSlots[1]);

                WeaponItem_SO weaponItem = item as WeaponItem_SO;

                PlayerManager.instance.playerEquipmentManager.weaponsInOffHandQuickSlots[1] = weaponItem;
                PlayerManager.instance.playerInventoryManager.playerWeaponInventories.Remove(weaponItem);

                PlayerManager.instance.playerEquipmentManager.WhichCharacterHandWeWantToLoadWeaponIn(weaponItem, false);
            }
            else
            {
                return;
            }

            PlayerManager.instance.playerEquipmentManager.currentWeaponHoldInMainHand =
                PlayerManager.instance.playerEquipmentManager.weaponsInMainHandQuickSlots
                [PlayerManager.instance.playerEquipmentManager.currentWeaponInMainHandIndex];

            PlayerManager.instance.playerEquipmentManager.currentWeaponHoldInOffHand =
                PlayerManager.instance.playerEquipmentManager.weaponsInOffHandQuickSlots
                [PlayerManager.instance.playerEquipmentManager.currentWeaponInOffHandIndex];

            PlayerCanvasManager.instance.playerWeaponEquipment.LoadWeaponsInPlayerHands_GUI(PlayerManager.instance);
            PlayerCanvasManager.instance.ResetAllWeaponEquipmentSlotsSelect();
        }
    }
}