using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class CharacterStatsBar_GUI : MonoBehaviour
    {
        public Slider slider;

        [Header("Player Status Bar Settings")]
        [SerializeField] RectTransform playerStatusBarTransform;
        [SerializeField] float healthWidthScaleMultiplier = 1;
        [SerializeField] bool thisBarHaveWidthScale = false;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public virtual void SetMaximumStatusPointsOfCharacter_GUI(int maxStatusValue)
        {
            slider.maxValue = maxStatusValue;
            slider.value = maxStatusValue;

            if (thisBarHaveWidthScale)
                playerStatusBarTransform.sizeDelta = new Vector2
                    (maxStatusValue * healthWidthScaleMultiplier, playerStatusBarTransform.sizeDelta.y);

            RefreshStatusBar_GUI();
        }

        public virtual void SetCurrentStatusPointsOfCharacter_GUI(float currentStatusValue)
        {
            slider.value = currentStatusValue;
        }

        //  REFRESH MAKE SURE STATUS BAR NOT MOVE TO MIDDLE AFTER MULTIPLIER SCALE
        public void RefreshStatusBar_GUI()
        {
            slider.gameObject.SetActive(false);
            slider.gameObject.SetActive(true);
        }
    }
}