using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS.Menu
{
    public class UIBSScalingEffect : MonoBehaviour
    {
        [Header("Scaling Settings")]
        [SerializeField] private bool shouldScaleOnHover;
        [SerializeField] private float scaleFactor;

        private UIButtonSwitch uibs;


        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }

        /// <summary>
        /// Controls the hover factor.
        /// </summary>
        public void HoverButton()
        {
            if (shouldScaleOnHover)
            {
                for (int i = 0; i < uibs.buttons.Length; i++)
                {
                    if (!i.Equals(uibs.pos))
                    {
                        uibs.buttons[i].transform.localScale = Vector3.one;
                    }
                    else
                    {
                        uibs.buttons[i].transform.localScale = Vector3.one * scaleFactor;
                    }
                }
            }
        }


        public void RevertEffect()
        {
            for (int i = 0; i < uibs.buttons.Length; i++)
            {
                uibs.buttons[i].transform.localScale = Vector3.one;
            }
        }
    }
}