using UnityEngine;

namespace NT
{
    public class PlayerWeaponEquipment_GUI : MonoBehaviour
    {
        [Header("Player Equipment Slots")]
        //  RIGHT HAND SLOTS
        public bool rightHandSlot_01_Selected = false;
        public bool rightHandSlot_02_Selected = false;

        //  LEFT HAND SLOTS
        public bool leftHandSlot_01_Selected = false;
        public bool leftHandSlot_02_Selected = false;

        //  ARROW SLOTS
        //public bool arrowSlot_01_Selected = false;
        //public bool arrowSlot_02_Selected = false;

        //  BOLT SLOTS
        //public bool boltSlot_01_Selected = false;
        //public bool boltSlot_02_Selected = false;

        [SerializeField] private PlayerEquipmentSlot_GUI[] playerEquipmentSlots_GUI;

        private void Awake()
        {
            
        }

        private void Start()
        {
            playerEquipmentSlots_GUI = GetComponentsInChildren<PlayerEquipmentSlot_GUI>(true);

            //  DEBUG
            LoadWeaponsInPlayerHands_GUI(PlayerManager.instance);
        }

        public void LoadWeaponsInPlayerHands_GUI(PlayerManager player)
        {
            for (int i = 0; i < playerEquipmentSlots_GUI.Length; i++)
            {
                if (playerEquipmentSlots_GUI[i].rightHandEquipSlot_01)
                {
                    playerEquipmentSlots_GUI[i].AddItemToThisSlot
                        (player.playerEquipmentManager.weaponsInMainHandQuickSlots[0]);
                }
                else if (playerEquipmentSlots_GUI[i].rightHandEquipSlot_02)
                {
                    playerEquipmentSlots_GUI[i].AddItemToThisSlot
                        (player.playerEquipmentManager.weaponsInMainHandQuickSlots[1]);
                }
                else if (playerEquipmentSlots_GUI[i].leftHandEquipSlot_01)
                {
                    playerEquipmentSlots_GUI[i].AddItemToThisSlot
                        (player.playerEquipmentManager.weaponsInOffHandQuickSlots[0]);
                }
                else if (playerEquipmentSlots_GUI[i].leftHandEquipSlot_02)
                {
                    playerEquipmentSlots_GUI[i].AddItemToThisSlot
                        (player.playerEquipmentManager.weaponsInOffHandQuickSlots[1]);
                }
            }
        }

        public void SelectRightHandSlot_01()
        {
            rightHandSlot_01_Selected = true;
        }

        public void SelectRightHandSlot_02()
        {
            rightHandSlot_01_Selected = true;
        }

        public void SelectLeftHandSlot_01()
        {
            leftHandSlot_01_Selected = true;
        }

        public void SelectLeftHandSlot_02()
        {
            leftHandSlot_02_Selected = true;
        }
    }
}