using TMPro;
using UnityEngine;

namespace NT
{
    public class BossCharacterStatsBar_GUI : AICharacterStatsBar_GUI
    {
        [Header("Boss Health Bar Settings")]
        public TextMeshProUGUI bossNameTMPro;

        protected override void Awake()
        {
            base.Awake();
        }

        public void EnableBossHealthBarToPlayer_GUI()
        {
            gameObject.SetActive(true);
        }

        public void SetTheBossName_GUI(string whatName)
        {
            bossNameTMPro.text = whatName;
        }
    }
}