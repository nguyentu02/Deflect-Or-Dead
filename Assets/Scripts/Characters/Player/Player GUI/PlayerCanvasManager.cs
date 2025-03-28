using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class PlayerCanvasManager : MonoBehaviour
    {
        public static PlayerCanvasManager instance;

        public PlayerManager player;

        public PlayerWeaponEquipment_GUI playerWeaponEquipment;

        [SerializeField] GameObject playerMenuOptionGameObject;

        [Header("Player Is Open Any GUI")]
        public bool isPlayerOpenMenuOption = false;

        //[Header("Player Status Bars GUI")]
        //public CharacterStatsBar_GUI playerHealthPointsBar;
        //public CharacterStatsBar_GUI playerFocusPointsBar;
        //public CharacterStatsBar_GUI playerStaminaPointsBar;

        [Header("Player Souls Collected GUI")]
        [SerializeField] TextMeshProUGUI soulsCountTMPro;

        [Header("Player Quick Slots GUI")]
        [SerializeField] Image playerLeftWeaponIconImage;
        [SerializeField] Image playerRightWeaponIconImage;

        [Header("Player Interactions GUI")]
        [SerializeField] GameObject playerNewItemAlertGameObject;
        [SerializeField] GameObject playerItemAlertGameObject;
        [SerializeField] GameObject playerAlertMessageGameObject;
        [SerializeField] Image playerNewItemAlertItemIconImage;
        [SerializeField] Image playerItemAlertItemIconImage;
        [SerializeField] TextMeshProUGUI playerNewItemAlertItemNameText;
        [SerializeField] TextMeshProUGUI playerItemAlertItemNameText;
        [SerializeField] TextMeshProUGUI playerAlertMessageText;
        public bool isNewItemAlert = false;

        [Header("Player Inventory GUI")]
        [SerializeField] GameObject playerInventorySlotPrefab;
        [SerializeField] RectTransform playerWeaponInventorySlotRectTransform;

        //  PLAYER INVENTORY ALL

        [SerializeField] GameObject playerWeaponInventory;

        //  PLAYER EQUIPMENT INVENTORY (HELMET, ARMOR, HIPS, GAUNTLETS)

        //  DEBUG HERE.
        [SerializeField] PlayerInventorySlots_GUI[] playerWeaponInventorySlots;

        [Header("Player Equipment GUI")]
        [SerializeField] GameObject playerEquipmentGameObject;

        public bool playerRightWeaponSlot_01_Selected = false;
        public bool playerRightWeaponSlot_02_Selected = false;

        public bool playerLeftWeaponSlot_01_Selected = false;
        public bool playerLeftWeaponSlot_02_Selected = false;

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

            playerWeaponEquipment = GetComponentInChildren<PlayerWeaponEquipment_GUI>(true);
        }

        private void Start()
        {
            //  WEAPON GET COMPONENTS (OF WEAPON INVENTORIES)
            playerWeaponInventorySlots = playerWeaponInventorySlotRectTransform.
                GetComponentsInChildren<PlayerInventorySlots_GUI>();
        }

        public void UpdatePlayerWeaponInventoryWhenPlayerOpenMenuOptions_GUI()
        {
            for (int i = 0; i < playerWeaponInventorySlots.Length; i++)
            {
                if (i < player.playerInventoryManager.playerWeaponInventories.Count)
                {
                    if (playerWeaponInventorySlots.Length < player.playerInventoryManager.playerWeaponInventories.Count)
                    {
                        GameObject weaponInventorySlot = Instantiate
                            (playerInventorySlotPrefab, playerWeaponInventorySlotRectTransform);
                        playerWeaponInventorySlots = playerWeaponInventorySlotRectTransform.
                            GetComponentsInChildren<PlayerInventorySlots_GUI>();
                    }

                    playerWeaponInventorySlots[i].AddItem(player.playerInventoryManager.playerWeaponInventories[i]);
                }
                else
                {
                    playerWeaponInventorySlots[i].RemoveItem();
                }
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

        public void UpdatePlayerAlertMessageIfPlayerCanInteract_GUI()
        {
            if (player.isPerformingAction)
                return;

            if (player.playerInteractionManager.interactableObjects.Count <= 0)
            {
                DEBUG_TurnOffMessageAlertPopUp();
                return;
            }

            if (isNewItemAlert)
            {
                DEBUG_TurnOffMessageAlertPopUp();
                return;
            }

            if (isPlayerOpenMenuOption)
            {
                DEBUG_TurnOffMessageAlertPopUp();
                return;
            }

            if (player.playerInteractionManager.interactableObjects[0] != null)
            {
                playerAlertMessageGameObject.SetActive(true);
                playerAlertMessageText.text = player.playerInteractionManager.interactableObjects[0].interactObjectText;
            }
        }

        public void ShowAlertToPlayerWhenPickUpAnWeaponNeverHaveBefore_GUI()
        {
            isNewItemAlert = true;

            playerNewItemAlertGameObject.SetActive(true);

            WeaponItemInteractable weaponPickUp = player.playerInteractionManager.interactableObjects[0] as WeaponItemInteractable;

            playerNewItemAlertItemIconImage.sprite = weaponPickUp.weapon.itemIcon;
            playerNewItemAlertItemNameText.text = weaponPickUp.weapon.itemName;
        }

        public void ShowAlertToPlayerWhenPickUpAnWeaponPlayerAlreadyHave_GUI()
        {
            playerItemAlertGameObject.SetActive(true);

            WeaponItemInteractable weaponPickUp = player.playerInteractionManager.interactableObjects[0] as WeaponItemInteractable;

            playerItemAlertItemIconImage.sprite = weaponPickUp.weapon.itemIcon;
            playerItemAlertItemNameText.text = weaponPickUp.weapon.itemName;
        }

        public void ShowMenuOptionToPlayer_GUI()
        {
            isPlayerOpenMenuOption = true;
            playerMenuOptionGameObject.SetActive(true);
        }

        public void ResetAllGUIGameObject_GUI()
        {
            ResetAllWeaponEquipmentSlotsSelect();

            isNewItemAlert = false;

            playerMenuOptionGameObject.SetActive(false);
            playerItemAlertGameObject.SetActive(false);
            playerAlertMessageGameObject.SetActive(false);
            playerNewItemAlertGameObject.SetActive(false);
            playerWeaponInventory.SetActive(false);
            playerEquipmentGameObject.SetActive(false);
        }

        //  DEBUG SET OF MESSAGE
        public void DEBUG_TurnOffMessageAlertPopUp()
        {
            playerAlertMessageGameObject.SetActive(false);
        }

        public void ResetAllWeaponEquipmentSlotsSelect()
        {
            playerRightWeaponSlot_01_Selected = false;
            playerRightWeaponSlot_02_Selected = false;

            playerLeftWeaponSlot_01_Selected = false;
            playerLeftWeaponSlot_02_Selected = false;
        }

        //  DEBUG SOULS COUNT
        public void UpdateSoulsCollectedOnPlayerGUI()
        {
            soulsCountTMPro.text = player.soulsRewardOnDeath.ToString();
        }
    }
}