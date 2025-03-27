using UnityEngine;

namespace NT
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        private CharacterManager character;

        [Header("Character Base Animator")]
        [SerializeField] RuntimeAnimatorController characterAnimator;

        private int horizontalValue;
        private int verticalValue;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            horizontalValue = Animator.StringToHash("Horizontal");
            verticalValue = Animator.StringToHash("Vertical");
        }

        public virtual void ProcessCharacterMovementAnimation
            (float horizontal_Value, float vertical_Value, bool isSprinting)
        {
            float vert = 0f;
            float hori = 0f;

            //  VERTICAL VALUES
            if (vertical_Value > 0f && vertical_Value < 0.55f)
                vert = 0.5f;
            else if (vertical_Value > 0.55f)
                vert = 1f;
            else if (vertical_Value < 0f && vertical_Value > -0.55f)
                vert = -0.5f;
            else if (vertical_Value < -0.55f)
                vert = -1f;
            else
                vert = 0f;

            //  HORIZONTAL VALUES
            if (horizontal_Value > 0f && horizontal_Value < 0.55f)
                hori = 0.5f;
            else if (horizontal_Value > 0.55f)
                hori = 1f;
            else if (horizontal_Value < 0f && horizontal_Value > -0.55f)
                hori = -0.5f;
            else if (horizontal_Value < -0.55f)
                hori = -1f;
            else
                hori = 0f;

            if (isSprinting)
            {
                vert = 2f;
                hori = horizontal_Value;
            }

            //  APPLY VERT/HORI VALUES TO ANIMATOR VALUES
            character.characterAnimator.SetFloat(horizontalValue, hori, 0.2f, Time.deltaTime);
            character.characterAnimator.SetFloat(verticalValue, vert, 0.2f, Time.deltaTime);
        }

        public virtual void CharacterPlayAnimation
            (string animationName, bool isPerformingAction, 
            bool canMove = false, bool canRotate = false)
        {
            character.characterAnimator.CrossFade(animationName, 0.2f);
            character.characterAnimator.applyRootMotion = isPerformingAction;
            character.isPerformingAction = isPerformingAction;
            character.canMove = canMove;
            character.canRotate = canRotate;
        }

        public virtual void CharacterPlayAttackAnimation
            (string animationName, WeaponItem_SO weapon, CharacterAttackType attackType, bool isAttacking, 
            bool isPerformingAction, bool canMove = false, bool canRotate = false)
        {
            character.characterAnimator.CrossFade(animationName, 0.2f);
            character.characterAnimator.applyRootMotion = isPerformingAction;
            character.characterCombatManager.currentWeaponCharacterUsingForAttack = weapon;
            character.characterCombatManager.attackType = attackType;
            character.isAttacking = isAttacking;
            character.isPerformingAction = isPerformingAction;
            character.canMove = canMove;
            character.canRotate = canRotate;
        }

        public virtual void TrackingCharacterAnimatorParameters()
        {
            //  BOOLS
            character.characterAnimator.SetBool("isDead", character.isDead);
            character.characterAnimator.SetBool("isGrounded", character.isGrounded);
            character.characterAnimator.SetBool("isPerformingAction", character.isPerformingAction);
            character.characterAnimator.SetBool("isChargingAttack", character.isChargingAttack);
            character.characterAnimator.SetBool("canDoComboAttack", character.canDoComboAttack);
            character.characterAnimator.SetBool("isTwoHandingWeapon", character.isTwoHanding);

            //  FLOATS
            character.characterAnimator.SetFloat("inAirTimer", character.characterMovementManager.inAirTimer);
        }

        //  DEBUG FOR ANIMATOR OVERRIDE BASE ON WEAPON HOLD IN WHICH HANDS
        //  CHECK FOR RIGHT HAND FIRST, IF RIGHT HAND != NULL, ALWAYS USE ANIMATOR OF RIGHT WEAPON
        public virtual void UpdateOverrideAnimatorBasedOnWeaponCharacterHoldInHand()
        {
            if (character.characterEquipmentManager.currentWeaponHoldInMainHand != null)
                character.characterAnimator.runtimeAnimatorController =
                    character.characterEquipmentManager.currentWeaponHoldInMainHand.weaponAnimator;
            else if (character.characterEquipmentManager.currentWeaponHoldInMainHand == null &&
                character.characterEquipmentManager.currentWeaponHoldInOffHand != null)
                character.characterAnimator.runtimeAnimatorController =
                    character.characterEquipmentManager.currentWeaponHoldInOffHand.weaponAnimator;
            else
                character.characterAnimator.runtimeAnimatorController = characterAnimator;
        }

        public virtual void OnAnimatorMove()
        {
            if (!character.isPerformingAction)
                return;

            Vector3 velocity = character.characterAnimator.deltaPosition;
            character.characterController.Move(velocity);
            character.transform.rotation *= character.characterAnimator.deltaRotation;
        }
    }
}