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
    }
}