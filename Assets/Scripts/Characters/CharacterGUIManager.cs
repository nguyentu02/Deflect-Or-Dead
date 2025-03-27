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

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }
    }
}