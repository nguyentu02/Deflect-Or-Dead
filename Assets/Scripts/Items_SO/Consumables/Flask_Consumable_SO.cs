using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class Flask_Consumable_SO : ConsumableItem_SO
    {
        [Header("Flask Type")]
        //  IF THIS IS NOT ESTUS FLASK, IT WILL BE ASHEN FLASK
        public bool isEstusFlask = true;

        [Header("Flask Recovery Amount")]
        public int hpRecoveryAmounts = 250;
        public int fpRecoveryAmounts = 250;

        [Header("Flask Recovery VFXs")]
        public GameObject hpRecoveryVFX;
        public GameObject fpRecoveryVFX;

        public override void AttemptToUseConsumableItem(CharacterManager character)
        {
            base.AttemptToUseConsumableItem(character);

            character.characterEffectsManager.DEBUG_FlaskModelInstantiatedInCharacterHand = Instantiate
                (itemModelPrefab, character.characterEquipmentManager.characterMainHand.transform);
            character.characterEffectsManager.CharacterRecoveryResourcesFromFlask
                (character, isEstusFlask, hpRecoveryAmounts, fpRecoveryAmounts, hpRecoveryVFX, fpRecoveryVFX);
            character.characterEquipmentManager.characterMainHand.UnloadWeaponPrefab();
        }
    }
}