/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;
using Arcade;


namespace Arcade.UserInput
{
    public class OnScreenKeyboardHelpText : MonoBehaviour
    {
        [SerializeField] private Text helpTextBox;
        //[SerializeField] private OnScreenKeyboard screenKeyboard;

        private const string arcadeHelpString = "B1 - Input the highlighed letter\nB2 - Remove the last letter inputted\nB8 - Confirm your input\nB7 - Undo input confirmation";
        private const string controllerHelpString = "A - Input the highlighed letter\nB - Remove the last letter inputted\nStart - Confirm your input\nReturn - Undo input confirmation";
        private const string keyboardPlayerOneHelpString = "R - Input the highlighed letter\nT - Remove the last letter inputted\n2 - Confirm your input\n1 - Undo input confirmation";
        private const string keyboardPlayerTwoHelpString = "Numpad 4 - Input the highlighed letter\nNumpad 5 - Remove the last letter inputted\nNumpad 8 - Confirm your input\nNumpad 7 - Undo input confirmation";

        public enum Players { PlayerOne, PlayerTwo };
        public Players thisPlayer;

        private void Start()
        {
            // get the stext component
            helpTextBox = GetComponent<Text>();

            // update the test value
            switch (InputSettings.ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    helpTextBox.text = arcadeHelpString;

                    break;
                case SupportedControllers.GamePadBoth:

                    helpTextBox.text = controllerHelpString;

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (thisPlayer == Players.PlayerOne)
                    {
                        helpTextBox.text = keyboardPlayerOneHelpString;
                    }
                    else
                    {
                        helpTextBox.text = keyboardPlayerTwoHelpString;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (thisPlayer == Players.PlayerOne)
                    {
                        helpTextBox.text = keyboardPlayerOneHelpString;
                    }
                    else
                    {
                        helpTextBox.text = controllerHelpString;
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (thisPlayer == Players.PlayerOne)
                    {
                        helpTextBox.text = controllerHelpString;
                    }
                    else
                    {
                        helpTextBox.text = keyboardPlayerTwoHelpString;
                    }

                    break;
            }
        }
    }
}