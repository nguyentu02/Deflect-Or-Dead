using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class DamageMasterCollider : MonoBehaviour
    {
        public CharacterManager characterCausingDamage;

        [SerializeField] protected Collider damageCollider;

        [Header("Weapon Damages")]
        public float weaponPhysicalDamage;
        public float weaponMagicDamage;
        public float weaponFireDamage;
        public float weaponHolyDamage;
        public float weaponLightningDamage;

        [Header("Final Damage")]
        public float DEBUG_finalDamage;

        private List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        protected virtual void Awake()
        {
            characterCausingDamage = GetComponentInParent<CharacterManager>(true);

            damageCollider = GetComponent<Collider>();

            if (damageCollider != null)
            {
                damageCollider.isTrigger = true;
                damageCollider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager characterDamaged = other.GetComponentInParent<CharacterManager>();

            //  DEBUG DAMAGE COLLIDER
            if (characterDamaged != null)
            {
                //  CHECK FOR DAMAGE MYSELF
                if (characterDamaged == characterCausingDamage)
                    return;

                //  CHECK FOR TEAMMATE
                if (characterCausingDamage.characterTeamID == characterDamaged.characterTeamID)
                    return;

                //  CHECK FOR CHARACTER BEING GET CRITICAL HIT
                if (characterCausingDamage.characterCombatManager.isBeingBackstabbed ||
                    characterCausingDamage.characterCombatManager.isBeingRiposted)
                    return;

                //  CHECK FOR CHARACTER WHO DEALT CRITICAL HIT
                if (characterDamaged.characterCombatManager.isBackstabbing ||
                    characterDamaged.characterCombatManager.isRiposting)
                    return;

                //  CHECK FOR PARRY
                if (characterDamaged.characterCombatManager.isParrying)
                {
                    CheckForParry(characterCausingDamage);
                    return;
                }

                //  CHECK FOR DEFLECT
                if (characterDamaged.characterCombatManager.isDeflect)
                {
                    CheckForDeflect(characterCausingDamage);
                    return;
                }

                //  CHECK FOR BLOCK

                //  CHECK FOR CAN'T DEAL ANY DAMAGE

                CalculateDamageAfterAddedToCharacterDamaged(characterDamaged);
            }
        }

        protected virtual void CalculateDamageAfterAddedToCharacterDamaged(CharacterManager character)
        {
            if (charactersDamaged.Contains(character))
                return;

            charactersDamaged.Add(character);

            DEBUG_finalDamage = weaponPhysicalDamage + weaponMagicDamage +
                weaponFireDamage + weaponHolyDamage + weaponLightningDamage;

            character.characterDamageReceiverManager.CharacterDamageReceiver(DEBUG_finalDamage, true, false);
        }

        protected virtual void CheckForDeflect(CharacterManager characterBeingDeflected)
        {
            characterBeingDeflected.characterAnimationManager.CharacterPlayAnimation
                ("GhostSamurai_DefenseR_Rebound_Root", true);

            //  JUST DEBUG FOR NOW, DEDUCT 40 STANCE POINT PER DEFLECT (VIA ANIMATION EVENT)
            if (characterBeingDeflected.characterStatusManager.characterCurrentStance <= 0)
            {
                //  BREAK STANCE, MAKE WHO CHARACTER IS BEING DEFLECT CAN BE RIPOSTE
                characterBeingDeflected.characterCombatManager.isStanceBreak = true;

                //  RESET CHARACTER STANCE TO MAX AFTER BREAK
                characterBeingDeflected.characterStatusManager.characterCurrentStance = 
                    characterBeingDeflected.characterStatusManager.characterMaxStance;
            }

            DisableDamageCollider();
        }

        protected virtual void CheckForParry(CharacterManager characterBeingParried)
        {
            characterBeingParried.characterAnimationManager.CharacterPlayAnimation
                ("core_main_parry_victim_01", true);
            characterBeingParried.characterCombatManager.EnableIsCanBeRiposted();
        }

        protected virtual void CheckForDefense(CharacterManager characterDamaged)
        {

        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            charactersDamaged.Clear();
            damageCollider.enabled = false;
            DEBUG_finalDamage = 0f;
        }
    }
}