using UnityEngine;
using UnityEngine.TextCore.Text;

namespace NT
{
    public class ResetCharacterParametersBools : StateMachineBehaviour
    {
        private CharacterManager character;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (character == null)
                character = animator.GetComponent<CharacterManager>();

            character.characterCombatManager.isAttacking = false;
            character.isPerformingAction = false;
            character.characterCombatManager.isStanceBreak = false;
            character.canMove = true;
            character.canRotate = true;
            character.isRolling = false;
            character.characterCombatManager.isUsingMainHand = false;
            character.characterCombatManager.isUsingOffHand = false;
            character.characterCombatManager.isBackstabbing = false;
            character.characterCombatManager.isRiposting = false;
            character.characterCombatManager.isBeingBackstabbed = false;
            character.characterCombatManager.isBeingRiposted = false;
            character.characterCombatManager.isCanBeBackstabbed = true;
            character.characterCombatManager.isLightAttack = false;
            character.characterCombatManager.isHeavyAttack = false;
            character.characterCombatManager.DisableIsCanBeRiposte();
            character.characterCombatManager.DisableCanDoComboAttack();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}