using UnityEngine;

namespace NT
{
    public class CharacterStatusManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Status")]
        //  VIGOR
        public int characterVigor = 15;
        public int characterMaxHealth;
        public float characterCurrentHealth;

        //  MIND

        //  ENDURANCE
        public int characterEndurance = 12;
        public int characterMaxStamina;
        public float characterCurrentStamina;

        //  STRENGTH
        //  DEXTERITY
        //  INTELLIGENCE
        //  FAITH
        //  ARCANE

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            SetMaximumCharacterStatsWhenStart();
        }

        public virtual void SetMaximumCharacterStatsWhenStart()
        {
            //  CHARACTER HEALTH
            characterMaxHealth = GetMaximumHealthBasedOnVigor();
            characterCurrentHealth = characterMaxHealth;
            PlayerCanvasManager.instance.playerHealthPointsBar.SetMaximumStatusPointsOfCharacter_GUI(characterMaxHealth);

            //  CHARACTER STAMINA
            characterMaxStamina = GetMaximumStaminaBasedOnEndurance();
            characterCurrentStamina = characterMaxStamina;
            PlayerCanvasManager.instance.playerStaminaPointsBar.SetMaximumStatusPointsOfCharacter_GUI(characterMaxStamina);
        }

        private int GetMaximumHealthBasedOnVigor()
        {
            characterMaxHealth = characterVigor * 15;
            return characterMaxHealth;
        }

        private int GetMaximumStaminaBasedOnEndurance()
        {
            characterMaxStamina = characterEndurance * 12;
            return characterMaxStamina;
        }
    }
}