using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS.Menu
{
    public class UIBSColourChange : MonoBehaviour
    {
        [Header("Colour Change Setting")]
        [SerializeField] private bool shouldChangeColour;
        [SerializeField] private Color defaultColour;
        [SerializeField] private Color hoverColour;

        private UIButtonSwitch uibs;

        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }

        public void ChangeColour()
        {
            if (shouldChangeColour)
            {
                for (int i = 0; i < uibs.buttons.Length; i++)
                {
                    if (!i.Equals(uibs.pos))
                    {
                        uibs.buttons[i].GetComponent<Image>().color = defaultColour;
                    }
                    else
                    {
                        uibs.buttons[i].GetComponent<Image>().color = hoverColour;
                    }
                }
            }
        }


        public void RevertEffects()
        {
            for (int i = 0; i < uibs.buttons.Length; i++)
            {
                uibs.buttons[i].GetComponent<Image>().color = hoverColour;
            }
        }
    }
}