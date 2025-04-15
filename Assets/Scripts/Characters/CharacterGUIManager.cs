using UnityEngine;

namespace NT
{
    public class CharacterGUIManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Bars Status GUI")]
        public CharacterStatsBar_GUI characterHealthPointsBar;
        public CharacterStatsBar_GUI characterFocusPointsBar;
        public CharacterStatsBar_GUI characterStaminaPointsBar;

        [Header("Character Buildup Bars GUI")]
        //  POISON
        [SerializeField] BuildupBar_GUI poisonBuildupBar;
        [SerializeField] BuildupAmountBar_GUI poisonAmountBuildupBar;

        //  ROT
        [SerializeField] BuildupBar_GUI rotBuildupBar;
        [SerializeField] BuildupAmountBar_GUI rotAmountBuildupBar;

        //  FROST
        [SerializeField] BuildupBar_GUI frostBuildupBar;
        [SerializeField] BuildupAmountBar_GUI frostAmountBuildupBar;

        //  BLEED
        [SerializeField] BuildupBar_GUI bleedBuildupBar;

        //  DEATH
        //  SLEEP

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void ShowUpPoisonBuildupForPlayer_GUI()
        {
            if (poisonBuildupBar == null)
                return;

            if (character.characterEffectsManager.poisonCurrentBuildup <= 0f)
                poisonBuildupBar.gameObject.SetActive(false);
            else
                poisonBuildupBar.gameObject.SetActive(true);

            poisonBuildupBar.SetCurrentBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.poisonCurrentBuildup));
        }

        public virtual void ShowUpPoisonAmountBuildupIfPlayerIsPoisoned_GUI()
        {
            if (poisonAmountBuildupBar == null)
                return;

            if (!character.characterEffectsManager.isPoisoned)
                poisonAmountBuildupBar.gameObject.SetActive(false);
            else
                poisonAmountBuildupBar.gameObject.SetActive(true);

            poisonAmountBuildupBar.SetCurrentAmountBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.poisonAmountBuildup));
        }

        public virtual void ShowUpRotBuildupForPlayer_GUI()
        {
            if (rotBuildupBar == null)
                return;

            if (character.characterEffectsManager.rotCurrentBuildup <= 0f)
                rotBuildupBar.gameObject.SetActive(false);
            else
                rotBuildupBar.gameObject.SetActive(true);

            rotBuildupBar.SetCurrentBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.rotCurrentBuildup));
        }

        public virtual void ShowUpRotAmountBuildupIfPlayerIsRottened_GUI()
        {
            if (rotAmountBuildupBar == null)
                return;

            if (!character.characterEffectsManager.isRottened)
                rotAmountBuildupBar.gameObject.SetActive(false);
            else
                rotAmountBuildupBar.gameObject.SetActive(true);

            rotAmountBuildupBar.SetCurrentAmountBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.rotAmountBuildup));
        }

        public virtual void ShowUpFrostBuildupForPlayer_GUI()
        {
            if (frostBuildupBar == null)
                return;

            if (character.characterEffectsManager.frostCurrentBuildup <= 0f)
                frostBuildupBar.gameObject.SetActive(false);
            else
                frostBuildupBar.gameObject.SetActive(true);

            frostBuildupBar.SetCurrentBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.frostCurrentBuildup));
        }

        public virtual void ShowUpFrostAmountBuildupIfPlayerIsFrosted_GUI()
        {
            if (frostAmountBuildupBar == null)
                return;

            if (!character.characterEffectsManager.isFrosted)
                frostAmountBuildupBar.gameObject.SetActive(false);
            else
                frostAmountBuildupBar.gameObject.SetActive(true);

            frostAmountBuildupBar.SetCurrentAmountBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.frostAmountBuildup));
        }

        public virtual void ShowUpBleedBuildupForPlayer_GUI()
        {
            if (bleedBuildupBar == null)
                return;

            if (character.characterEffectsManager.bleedCurrentBuildup <= 0f)
                bleedBuildupBar.gameObject.SetActive(false);
            else
                bleedBuildupBar.gameObject.SetActive(true);

            bleedBuildupBar.SetCurrentBuildupValue
                (Mathf.RoundToInt(character.characterEffectsManager.bleedCurrentBuildup));
        }
    }
}