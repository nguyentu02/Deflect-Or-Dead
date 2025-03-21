using UnityEngine;

namespace NT
{
    public class CharacterDamageReceiverManager : MonoBehaviour
    {
        private CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public virtual void CharacterDamageReceiver(float damageReceiver)
        {
            character.characterStatusManager.characterCurrentHealth -= damageReceiver;
        }
    }
}