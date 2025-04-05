using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class CharacterModelsChanger : MonoBehaviour
    {
        [SerializeField] List<GameObject> gameObjects;

        private void Awake()
        {
            GetAllChildInThisGameObject();
        }

        private void GetAllChildInThisGameObject()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                gameObjects.Add(transform.GetChild(i).gameObject);
            }
        }

        public void EnableGameObjectWithStringName(string whatNameToEnable)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].name == whatNameToEnable)
                {
                    gameObjects[i].SetActive(true);
                }
            }
        }

        public void DisableAllGameObjectsInList()
        {
            foreach (var @gameObject in gameObjects)
            {
                @gameObject.SetActive(false);
            }
        }
    }
}