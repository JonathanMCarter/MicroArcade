using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS.Menu
{
    public class UIBSAnimationToggle : MonoBehaviour
    {
        [Header("Animation Toggle Settings")]
        [SerializeField] private Animator[] anims;

        private UIButtonSwitch uibs;


        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }


        public void AnimationToggle()
        {
            for (int i = 0; i < anims.Length; i++)
            {
                if (i.Equals(uibs.pos))
                {
                    anims[i].ResetTrigger("CloseCard");
                    anims[i].SetTrigger("OpenCard");
                }
                else
                {
                    anims[i].ResetTrigger("OpenCard");
                    anims[i].SetTrigger("CloseCard");
                }
            }
        }


        public void RevertEffects()
        {
            for (int i = 0; i < anims.Length; i++)
            {
                anims[i].ResetTrigger("OpenCard");
                anims[i].SetTrigger("CloseCard");
            }
        }
    }
}