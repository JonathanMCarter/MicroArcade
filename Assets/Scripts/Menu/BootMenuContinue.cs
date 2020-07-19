using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class BootMenuContinue : InputSettings
    {
        [HideInInspector]
        public List<GameObject> DisplayOptions;


        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                DisplayOptions.Add(transform.GetChild(i).gameObject);
            }
        }


        protected override void Update()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    ShowOption(2);

                    break;
                case SupportedControllers.GamePadBoth:

                    ShowOption(1);

                    break;
                case SupportedControllers.KeyboardBoth:

                    ShowOption(0);

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    ShowOption(0);

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    ShowOption(1);

                    break;
                default:
                    break;
            }
        }


        void ShowOption(int Element)
        {
            for (int i = 0; i < DisplayOptions.Count; i++)
            {
                if ((i != Element) && (DisplayOptions[i].activeInHierarchy))
                {
                    DisplayOptions[i].SetActive(false);
                }
                else
                {
                    DisplayOptions[Element].SetActive(true);
                }
            }
        }
    }
}
