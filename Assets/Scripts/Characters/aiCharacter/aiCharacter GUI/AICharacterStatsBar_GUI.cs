using UnityEngine;

namespace NT
{
    public class AICharacterStatsBar_GUI : CharacterStatsBar_GUI
    {
        [Header("Hidden Timer")]
        public float theBarTimer;
        public float timeUntilBarIsHidden = 3f;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void SetCurrentStatusPointsOfCharacter_GUI(float currentStatusValue)
        {
            base.SetCurrentStatusPointsOfCharacter_GUI(currentStatusValue);

            theBarTimer = timeUntilBarIsHidden;
        }
    }
}