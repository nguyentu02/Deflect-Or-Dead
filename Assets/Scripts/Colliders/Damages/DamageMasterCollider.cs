using System.Collections.Generic;
using UnityEngine;

namespace NT
{
    public class DamageMasterCollider : MonoBehaviour
    {
        public CharacterManager characterCausingDamage;

        [SerializeField] protected Collider damageCollider;

        [Header("Weapon Break Stance/Stance")]
        public float weaponPoiseDamage;
        public float weaponPoiseBonusWhenAttack;

        [Header("Weapon Damages")]
        public float weaponPhysicalDamage;
        public float weaponMagicDamage;
        public float weaponFireDamage;
        public float weaponHolyDamage;
        public float weaponLightningDamage;

        [Header("Weapon Damage Absorptions")]
        public float weaponPhysicalDamageAbsorption;
        public float weaponMagicDamageAbsorption;
        public float weaponFireDamageAbsorption;
        public float weaponHolyDamageAbsorption;
        public float weaponLightningDamageAbsorption;

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

        protected virtual void OnTriggerEnter(Collider other)
        {
            CharacterManager characterDamaged = other.GetComponentInParent<CharacterManager>();

            //  DEBUG DAMAGE COLLIDER
            if (characterDamaged != null)
            {
                //  CALCULATE DOT VALUE FOR PARRY, DEFLECT AND DEFENSE CHECK
                Vector3 directionFromCharacterToTargetCharacter =
                    characterCausingDamage.transform.position - characterDamaged.transform.position;
                float dotValue = Vector3.Dot
                    (directionFromCharacterToTargetCharacter, characterDamaged.transform.forward);

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
                if (characterDamaged.characterCombatManager.isDeflect && dotValue > 0.3f)
                {
                    CheckForDeflect(characterCausingDamage, characterDamaged);
                    return;
                }

                //  CHECK FOR BLOCK
                if (characterDamaged.characterCombatManager.isDefense && dotValue > 0.3f)
                {
                    CheckForDefense(characterDamaged);
                    return;
                }

                //  CHECK FOR CAN'T DEAL ANY DAMAGE

                CalculateDamageAfterAddedToCharacterDamaged(characterDamaged);
            }

            if (other.tag == "Illusionary Wall")
            {
                IllusionaryWallInteract illusionaryWall = other.GetComponent<IllusionaryWallInteract>();

                illusionaryWall.wallHasBeenHit = true;
            }
        }

        protected virtual void CalculateDamageAfterAddedToCharacterDamaged(CharacterManager characterDamaged)
        {
            BossManager bossCharacter = characterDamaged as BossManager;

            if (charactersDamaged.Contains(characterDamaged))
                return;

            charactersDamaged.Add(characterDamaged);

            //  POISE DAMAGES
            characterDamaged.characterStatusManager.resetStanceTimer = characterDamaged.characterStatusManager.stanceResetTime;
            characterDamaged.characterStatusManager.characterCurrentStance -= weaponPoiseDamage;

            if (bossCharacter != null)
            {
                if (characterDamaged.characterStatusManager.characterCurrentStance > weaponPoiseDamage)
                {
                    characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver
                        (weaponPhysicalDamage, weaponMagicDamage, weaponFireDamage, weaponHolyDamage, weaponLightningDamage,
                        "core_main_hit_reaction_medium_f_01");
                }
                else
                {
                    characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver
                        (weaponPhysicalDamage, weaponMagicDamage, weaponFireDamage, weaponHolyDamage, weaponLightningDamage,
                        "core_main_hit_reaction_medium_f_01");
                    //  BREAK STANCE, MAKE WHO CHARACTER IS BEING DEFLECT CAN BE RIPOSTE
                    characterDamaged.characterCombatManager.CharacterStanceBreaking("Break_Stance_01", true);
                }
            }
            else
            {
                if (characterDamaged.characterStatusManager.characterCurrentStance > weaponPoiseDamage)
                {
                    characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver
                        (weaponPhysicalDamage, weaponMagicDamage, weaponFireDamage, weaponHolyDamage, weaponLightningDamage,
                        "core_main_hit_reaction_ping_f_01", true, false, true);
                }
                else
                {
                    characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver
                        (weaponPhysicalDamage, weaponMagicDamage, weaponFireDamage, weaponHolyDamage, weaponLightningDamage,
                        "core_main_hit_reaction_medium_f_01", true);
                }
            }
        }

        protected virtual void CheckForDeflect
            (CharacterManager characterBeingDeflected, CharacterManager characterDamaged)
        {
            characterBeingDeflected.characterAnimationManager.CharacterPlayAnimation
                ("GhostSamurai_DefenseR_Rebound_Root", true);

            //  JUST DEBUG FOR PLAYTEST DEFLECT SYSTEM
            characterBeingDeflected.characterStatusManager.resetStanceTimer = 
                characterBeingDeflected.characterStatusManager.stanceResetTime;
            characterBeingDeflected.characterStatusManager.characterCurrentStance -= (weaponPoiseDamage * 2f);

            //  DEBUG FOR CHOOSE ANIMATION DELFECT OF CHARACTER DEFLECTING
            int randomScore = Random.Range(0, 100);

            if (randomScore < 50)
                characterDamaged.characterAnimationManager.CharacterPlayAnimation
                    ("GhostSamurai_DefenseL_Parry_L2R_Up_Root", true);
            else
                characterDamaged.characterAnimationManager.CharacterPlayAnimation
                    ("GhostSamurai_DefenseR_Parry_R2L_Up_Root", true);

            //  JUST DEBUG FOR NOW, DEDUCT 40 STANCE POINT PER DEFLECT (VIA ANIMATION EVENT)
            if (characterBeingDeflected.characterStatusManager.characterCurrentStance <= 0)
                //  BREAK STANCE, MAKE WHO CHARACTER IS BEING DEFLECT CAN BE RIPOSTE
                characterBeingDeflected.characterCombatManager.CharacterStanceBreaking("", false);

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
            if (charactersDamaged.Contains(characterDamaged))
                return;

            charactersDamaged.Add(characterDamaged);

            float physicalDamageAfterBlocked = weaponPhysicalDamage;
            float magicDamageAfterBlocked = weaponMagicDamage;
            float fireDamageAfterBlocked = weaponFireDamage;
            float holyDamageAfterBlocked = weaponHolyDamage;
            float lightningDamageAfterBlocked = weaponLightningDamage;

            physicalDamageAfterBlocked -= Mathf.RoundToInt(weaponPhysicalDamage *
                (characterDamaged.characterStatusManager.characterPhysicalDamageAbsorption / 100));

            magicDamageAfterBlocked -= Mathf.RoundToInt(weaponMagicDamage *
                (characterDamaged.characterStatusManager.characterMagicDamageAbsorption / 100));

            fireDamageAfterBlocked -= Mathf.RoundToInt(weaponFireDamage *
                (characterDamaged.characterStatusManager.characterFireDamageAbsorption / 100));

            lightningDamageAfterBlocked -= Mathf.RoundToInt(weaponLightningDamage *
                (characterDamaged.characterStatusManager.characterLightningDamageAbsorption / 100));

            holyDamageAfterBlocked -= Mathf.RoundToInt(weaponHolyDamage *
                (characterDamaged.characterStatusManager.characterHolyDamageAbsorption / 100));

            characterDamaged.characterDamageReceiverManager.CharacterDamageReceiver
                (physicalDamageAfterBlocked, 
                magicDamageAfterBlocked, 
                fireDamageAfterBlocked, 
                holyDamageAfterBlocked , 
                lightningDamageAfterBlocked, 
                "shield_off_guard_block_ping_01", true, false, true);
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