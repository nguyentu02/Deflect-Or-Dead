using UnityEngine;

namespace NT
{
    public class CharacterDamageReceiverManager : MonoBehaviour
    {
        private CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void CharacterDamageReceiver
            (float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float lightningDamage,
            string damageAnimation = "core_main_hit_reaction_medium_f_01",
            bool isHasDamageAnimtion = false,
            bool isHasNewDeadAnimation = false,
            bool isCanMoveWhileGetHit = false)
        {
            if (character.characterCombatManager.isInvulnerable)
                return;

            if (character.isDead)
                return;

            float totalPhysicalDamageAbsorption;
            float totalMagicDamageAbsorption;
            float totalFireDamageAbsorption;
            float totalHolyDamageAbsorption;
            float totalLightningDamageAbsorption;

            totalPhysicalDamageAbsorption = 1 -
                (1 - character.characterStatusManager.characterPhysicalDamageAbsorptionOfHelmet / 100) *
                (1 - character.characterStatusManager.characterPhysicalDamageAbsorptionOfChestplate / 100) *
                (1 - character.characterStatusManager.characterPhysicalDamageAbsorptionOfGauntlets / 100) *
                (1 - character.characterStatusManager.characterPhysicalDamageAbsorptionOfGreaves / 100);

            physicalDamage -= (physicalDamage * totalPhysicalDamageAbsorption);

            totalMagicDamageAbsorption = 1 -
                (1 - character.characterStatusManager.characterMagicDamageAbsorptionOfHelmet / 100) *
                (1 - character.characterStatusManager.characterMagicDamageAbsorptionOfChestplate / 100) *
                (1 - character.characterStatusManager.characterMagicDamageAbsorptionOfGauntlets / 100) *
                (1 - character.characterStatusManager.characterMagicDamageAbsorptionOfGreaves / 100);

            magicDamage -= (magicDamage * totalMagicDamageAbsorption);

            totalFireDamageAbsorption = 1 -
                (1 - character.characterStatusManager.characterFireDamageAbsorptionOfHelmet / 100) *
                (1 - character.characterStatusManager.characterFireDamageAbsorptionOfChestplate / 100) *
                (1 - character.characterStatusManager.characterFireDamageAbsorptionOfGauntlets / 100) *
                (1 - character.characterStatusManager.characterFireDamageAbsorptionOfGreaves / 100);

            fireDamage -= (fireDamage * totalFireDamageAbsorption);

            totalHolyDamageAbsorption = 1 -
                (1 - character.characterStatusManager.characterHolyDamageAbsorptionOfHelmet / 100) *
                (1 - character.characterStatusManager.characterHolyDamageAbsorptionOfChestplate / 100) *
                (1 - character.characterStatusManager.characterHolyDamageAbsorptionOfGauntlets / 100) *
                (1 - character.characterStatusManager.characterHolyDamageAbsorptionOfGreaves / 100);

            holyDamage -= (holyDamage * totalHolyDamageAbsorption);

            totalLightningDamageAbsorption = 1 -
                (1 - character.characterStatusManager.characterLightningDamageAbsorptionOfHelmet / 100) *
                (1 - character.characterStatusManager.characterLightningDamageAbsorptionOfChestplate / 100) *
                (1 - character.characterStatusManager.characterLightningDamageAbsorptionOfGauntlets / 100) *
                (1 - character.characterStatusManager.characterLightningDamageAbsorptionOfGreaves / 100);

            lightningDamage -= (lightningDamage * totalLightningDamageAbsorption);

            float finalDamages = physicalDamage + magicDamage + fireDamage + holyDamage + lightningDamage;

            Debug.Log("Total Damage is: " + finalDamages);

            character.characterStatusManager.characterCurrentHealth -= finalDamages;

            //  JUST DEBUG FOR PLAYTEST NOW, WILL REFACTOR LATER
            if (isHasDamageAnimtion)
            {
                if (isCanMoveWhileGetHit)
                    character.characterAnimationManager.CharacterPlayAnimation(damageAnimation, false, true, true);
                else
                    character.characterAnimationManager.CharacterPlayAnimation(damageAnimation, true);
            }


            if (character.characterStatusManager.characterCurrentHealth <= 0f)
            {
                character.isDead = true;

                character.characterStatusManager.characterCurrentHealth = 0f;

                if (!isHasNewDeadAnimation)
                    character.characterAnimationManager.CharacterPlayAnimation("straight_sword_main_death_01", true);
            }
        }

        public virtual void CharacterGiveAwardedOnDeath(int soulsReward)
        {

        }

        //  ANIMATION EVENTS
        public virtual void CharacterGiveAwardedWithAnimationEvent()
        {
            if (PlayerManager.instance != null)
            {
                PlayerManager.instance.playerDamageReceiverManager.CharacterGiveAwardedOnDeath
                    (character.soulsRewardOnDeath);
                PlayerCanvasManager.instance.UpdateSoulsCollectedOnPlayerGUI();
            }
        }
    }
}