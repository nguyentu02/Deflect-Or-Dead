using UnityEngine;

namespace NT
{
    public class AshOfWarItem_SO : Item_SO
    {
        //  JUST DEBUG FOR NOW
        [Header("Ash Of War Animation")]
        public string ashOfWarAnimation;

        public virtual void CharacterPlayingAshOfWar(CharacterManager characterWhoPlay)
        {
            characterWhoPlay.characterAnimationManager.CharacterPlayAnimation(ashOfWarAnimation, true);
        }
    }
}