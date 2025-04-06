using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class EventMasterCollider : MonoBehaviour
    {
        //  JUST KEEP DEBUG TEST
        [SerializeField] List<BossManager> bossCharacters;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                for (int i = 0; i < bossCharacters.Count; i++)
                {
                    if (bossCharacters[i] == null)
                        continue;

                    bossCharacters[i].bossGUIManager.ShowUpTheBossHealthBarAfterPlayerPassThroughTheFogWall_GUI();
                }
            }
        }
    }
}