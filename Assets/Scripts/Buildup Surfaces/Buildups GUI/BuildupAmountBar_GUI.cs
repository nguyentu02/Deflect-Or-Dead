using UnityEngine;
using UnityEngine.UI;

namespace NT
{
    public class BuildupAmountBar_GUI : MonoBehaviour
    {
        [SerializeField] Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

        protected virtual void Start()
        {
            slider.maxValue = 100f;
            slider.value = 100f;
        }

        public virtual void SetCurrentAmountBuildupValue(int currentAmountBuildupValue)
        {
            slider.value = currentAmountBuildupValue;
        }
    }
}