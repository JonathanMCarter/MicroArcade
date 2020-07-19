using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CarterGames.Arcade.Saving;

namespace CarterGames.Arcade.UserInput
{
    public class InputAssignment : MonoBehaviour
    {
        [SerializeField] private bool isPlayer1Input;

        private const string askUserToInputString = "Please Press Confirm " + "\n" + "(2 or A)";
        private const string keyboardSelected = "Keyboard Selected" + "\n";
        private const string controllerSelected = "Controller Selected" + "\n";
        private SupportedControllers newScheme;

        public Text toolTip;
        public List<Image> controlIcons;

        public enum CheckState { Player1Needed, Player2Needed }
        public CheckState currentState;



        public void ClosePopup()
        {
            Time.timeScale = 1;

            if (SceneManager.GetSceneByName("InputCheck").isLoaded)
            {
                SceneManager.UnloadSceneAsync("InputCheck");
            }
        }


        private void Start()
        {
            currentState = CheckState.Player1Needed;
        }


        private void Update()
        {
            if (!isPlayer1Input && currentState == CheckState.Player1Needed)
            {
                toolTip.text = askUserToInputString;

                if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                {
                    isPlayer1Input = true;
                    controlIcons[0].enabled = true;
                    currentState = CheckState.Player2Needed;
                    SaveManager.SaveArcadeControlScheme(SupportedControllers.KeyboardP2ControllerP1);
                    newScheme = SupportedControllers.KeyboardP2ControllerP1;
                    toolTip.text = controllerSelected;
                }
                else if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                {
                    isPlayer1Input = true;
                    controlIcons[1].enabled = true;
                    currentState = CheckState.Player2Needed;
                    SaveManager.SaveArcadeControlScheme(SupportedControllers.KeyboardP1ControllerP2);
                    newScheme = SupportedControllers.KeyboardP1ControllerP2;
                    toolTip.text = keyboardSelected;
                }
            }

            if ((isPlayer1Input ) && (currentState == CheckState.Player2Needed))
            {
                ClosePopup();
            }
        }
    }
}