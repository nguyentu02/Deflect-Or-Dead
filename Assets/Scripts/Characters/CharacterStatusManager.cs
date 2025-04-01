using UnityEngine;

namespace NT
{
    public class CharacterStatusManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Stance Points")]
        public int characterMaxStance = 80;
        public int characterCurrentStance = 80;

        [Header("Character Status")]
        //  VIGOR
        public int characterVigor = 15;
        public int characterMaxHealth;
        public float characterCurrentHealth;

        //  MIND
        public int characterMind = 10;
        public int characterMaxFocusPoints;
        public float characterCurrentFocusPoints;

        //  ENDURANCE
        public int characterEndurance = 12;
        public int characterMaxStamina;
        public float characterCurrentStamina;

        //  STRENGTH
        //  DEXTERITY
        //  INTELLIGENCE
        //  FAITH
        //  ARCANE

        [Header("Character Damage Absorptions")]
        public float characterPhysicalDamageAbsorption;
        public float characterMagicDamageAbsorption;
        public float characterFireDamageAbsorption;
        public float characterHolyDamageAbsorption;
        public float characterLightningDamageAbsorption;

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
            //  STANCE
            characterCurrentStance = characterMaxStance;

            //  CHARACTER HEALTH
            characterMaxHealth = GetMaximumHealthBasedOnVigor();
            characterCurrentHealth = characterMaxHealth;

            if (character.characterGUIManager.characterHealthPointsBar != null)
                character.characterGUIManager.characterHealthPointsBar.SetMaximumStatusPointsOfCharacter_GUI
                    (characterMaxHealth);

            //  CHARACTER MANA
            characterMaxFocusPoints = GetMaximumFocusPointsBasedOnMind();
            characterCurrentFocusPoints = characterMaxFocusPoints;

            if (character.characterGUIManager.characterFocusPointsBar != null)
                character.characterGUIManager.characterFocusPointsBar.SetMaximumStatusPointsOfCharacter_GUI
                (characterMaxFocusPoints);

            //  CHARACTER STAMINA
            characterMaxStamina = GetMaximumStaminaBasedOnEndurance();
            characterCurrentStamina = characterMaxStamina;

            if (character.characterGUIManager.characterStaminaPointsBar != null)
                character.characterGUIManager.characterStaminaPointsBar.SetMaximumStatusPointsOfCharacter_GUI
                (characterMaxStamina);
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

        private int GetMaximumFocusPointsBasedOnMind()
        {
            characterMaxFocusPoints = characterMind * 10;
            return characterMaxFocusPoints;
        }

        public virtual void DEBUG_TrackingStatusPointsAndGetThemNeverNegativeValue()
        {
            if (characterCurrentHealth <= 0f)
                characterCurrentHealth = 0f;

            if (characterCurrentStamina <= 0f)
                characterCurrentStamina = 0f;

            if (characterCurrentFocusPoints <= 0f)
                characterCurrentFocusPoints = 0f;
        }
    }
}