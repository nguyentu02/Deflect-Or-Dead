using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class PlayerEquipmentSlot_GUI : MonoBehaviour
    {
        [SerializeField] Image itemIcon;
        [SerializeField] Item_SO item;

        public bool rightHandEquipSlot_01;
        public bool rightHandEquipSlot_02;

        public bool leftHandEquipSlot_01;
        public bool leftHandEquipSlot_02;

        //[SerializeField] bool arrowEquipSlot_01;
        //[SerializeField] bool arrowEquipSlot_02;

        //[SerializeField] bool boltEquipSlot_01;
        //[SerializeField] bool boltEquipSlot_02;

        public void AddItemToThisSlot(Item_SO newItem)
        {
            item = newItem;
            itemIcon.sprite = newItem.itemIcon;
            itemIcon.enabled = true;
            gameObject.SetActive(true);
        }

        public void RemoveItemFromThisSlot()
        {
            item = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}