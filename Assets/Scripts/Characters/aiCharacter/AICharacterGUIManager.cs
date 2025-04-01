using UnityEngine;

namespace NT
{
    public class AICharacterGUIManager : CharacterGUIManager
    {
        private AICharacterManager aiCharacter;

        [Header("AICharacter Bars Status GUI")]
        public AICharacterStatsBar_GUI aiCharacterHealthPointsBar;
        public AICharacterStatsBar_GUI aiCharacterFocusPointsBar;
        public AICharacterStatsBar_GUI aiCharacterStaminaPointsBar;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public void DEBUG_ShowUpAICharacterHealthBarOnHeadForPlayerSee_GUI()
        {
            if (aiCharacterHealthPointsBar.slider == null)
                return;

            aiCharacterHealthPointsBar.theBarTimer -= Time.deltaTime;

            if (aiCharacterHealthPointsBar.theBarTimer <= 0)
            {
                aiCharacterHealthPointsBar.theBarTimer = 0f;
                aiCharacterHealthPointsBar.slider.gameObject.SetActive(false);
            }
            else
            {
                if (!aiCharacterHealthPointsBar.slider.gameObject.activeInHierarchy)
                    aiCharacterHealthPointsBar.slider.gameObject.SetActive(true);
            }

            if (aiCharacterHealthPointsBar.slider.value <= 0f)
                //  DESTROY HEALTH SLIDER AFTER 3 SECOND
                Destroy(aiCharacterHealthPointsBar.gameObject, 3f);
        }

        public void DEBUG_RotateAICharacterHealthBarToPlayerMainCamera_GUI()
        {
            if (aiCharacterHealthPointsBar.slider != null)
                aiCharacterHealthPointsBar.slider.transform.rotation = Quaternion.LookRotation
                    ((aiCharacterHealthPointsBar.slider.transform.position - Camera.main.transform.position).normalized);
        }
    }
}