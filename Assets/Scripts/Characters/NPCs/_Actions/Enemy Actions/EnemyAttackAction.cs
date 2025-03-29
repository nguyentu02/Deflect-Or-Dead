using UnityEngine;

namespace NT
{
    [CreateAssetMenu(menuName = "NPCs/Enemies/Enemy Actions/Attack")]
    public class EnemyAttackAction : EnemyAction
    {
        [Header("Enemy Attack Action Settings")]
        public int attackScore = 3;
        public float timeToNextAttack = 2f;
        public float minimumAttackAngle = -35f;
        public float maximumAttackAngle = 35f;
        public float minimumDistanceNeededToAttack = 0f;
        public float maximumDistanceNeededToAttack = 3f;

        public override void NPCPerformAnAction(CharacterManager character)
        {
            base.NPCPerformAnAction(character);


        }
    }
}