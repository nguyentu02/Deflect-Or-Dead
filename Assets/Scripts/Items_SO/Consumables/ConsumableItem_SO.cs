using UnityEngine;

namespace NT
{
    public class ConsumableItem_SO : Item_SO
    {
        [Header("Consumables Quantity")]
        public int maxConsumablesAmount = 5;
        public int currentConsumablesAmount = 0;

        [Header("Consumables Using Animation")]
        public string usingConsumableAnimation;
        public bool canMoveWhenUsing = false;

        protected override void Awake()
        {
            base.Awake();

            currentConsumablesAmount = maxConsumablesAmount;
        }

        public virtual void AttemptToUseConsumableItem(CharacterManager character)
        {
            if (currentConsumablesAmount > 0)
            {
                character.characterAnimationManager.CharacterPlayAnimation
                    (usingConsumableAnimation, true, canMoveWhenUsing, canMoveWhenUsing);
            }
            else
            {
                character.characterAnimationManager.CharacterPlayAnimation("Shrug", true);
            }
        }
    }
}