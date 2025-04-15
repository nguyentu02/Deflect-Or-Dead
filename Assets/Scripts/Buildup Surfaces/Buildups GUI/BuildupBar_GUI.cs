using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class BuildupBar_GUI : MonoBehaviour
    {
        [SerializeField] Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        protected virtual void Start()
        {
            slider.maxValue = 100f;
            slider.value = 0f;
        }

        public virtual void SetCurrentBuildupValue(int currentBuildupValue)
        {
            slider.value = currentBuildupValue;
        }
    }
}