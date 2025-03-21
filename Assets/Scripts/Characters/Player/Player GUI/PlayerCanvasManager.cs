using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class PlayerCanvasManager : MonoBehaviour
    {
        public static PlayerCanvasManager instance;

        [Header("Player Status Bars GUI")]
        public PlayerStatsBar_GUI playerHealthPointsBar;
        public PlayerStatsBar_GUI playerStaminaPointsBar;

        [Header("Player Quick Slots GUI")]
        public Image playerLeftWeaponIconImage;
        public Image playerRightWeaponIconImage;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void UpdatePlayerQuickSlotsIconImage_GUI(WeaponItem_SO weapon, bool isMainHand)
        {
            if (isMainHand)
            {
                if (weapon.itemIcon != null)
                {
                    playerRightWeaponIconImage.enabled = true;
                    playerRightWeaponIconImage.sprite = weapon.itemIcon;
                }
                else
                {
                    playerRightWeaponIconImage.sprite = null;
                    playerRightWeaponIconImage.enabled = false;
                    Debug.Log("Weapon Item no have Icon !!!");
                }
            }
            else
            {
                if (weapon.itemIcon != null)
                {
                    playerLeftWeaponIconImage.enabled = true;
                    playerLeftWeaponIconImage.sprite = weapon.itemIcon;
                }
                else
                {
                    playerLeftWeaponIconImage.sprite = null;
                    playerLeftWeaponIconImage.enabled = false;
                    Debug.Log("Weapon Item no have Icon !!!");
                }
            }
        }
    }
}