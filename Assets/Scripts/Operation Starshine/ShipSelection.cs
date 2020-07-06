using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Arcade;
using Arcade.Saving;
using Arcade.Menu;
using UnityEngine.UI;

namespace Starshine
{
    public class ShipSelection : InputSettings
    {
        public Ships Player1ShipChoice;
        public Ships Player2ShipChoice;

        [Header("Player 1 Selection Options")]
        public int Player1Pos;
        public int Player1MaxPos;
        public bool Player1Inputted;
        public bool Player1Selected;
        public Image Player1Confirmed;

        [Header("Player 2 Selection Options")]
        public int Player2Pos;
        public int Player2MaxPos;
        public bool Player2Inputted;
        public bool Player2Selected;
        public Image Player2Confirmed;

        bool IsCoRunning;

        public Animator Trans;

        public OperationStarshineMenuScreen Screen;
        OperationStarshineData _data;

        public AudioManager MenuAM;


        protected override void Update()
        {
            // Player Selection Controls
            PlayerSelectionMovement(Joysticks.White);
            PlayerSelectionMovement(Joysticks.Black);

            // Confirm Selection
            ConfirmOption(Joysticks.White);
            ConfirmOption(Joysticks.Black);

            // Undo Selection
            if (Player1Selected || Player2Selected)
            {
                Screen.Use_Return_Event = false;
                UndoConfirmOption(Joysticks.White);
                UndoConfirmOption(Joysticks.Black);

                if (Screen.enabled)
                {
                    Screen.enabled = false;
                }
            }
            else
            {
                Screen.Use_Return_Event = true;
            }

            if (Player1Selected && !Player1Confirmed.enabled)
            {
                Player1Confirmed.enabled = true;
            }

            if (Player2Selected && !Player2Confirmed.enabled)
            {
                Player2Confirmed.enabled = true;
            }

            // Change to game scene if the ships have both been selected after a second
            if ((Player1Selected) && (Player2Selected))
            {
                if (!IsCoRunning)
                {
                    StartCoroutine(WaitToChangeScene());
                }
            }

            if ((!Player1Selected) && (!Player2Selected) && (!Screen.enabled))
            {
                Screen.enabled = true;
            }
        }



        IEnumerator WaitToChangeScene()
        {
            IsCoRunning = true;
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1f);
            _data = SaveManager.LoadOperationStarshine();
            _data.LastPlayer1ShipSelection = (int)Player1ShipChoice;
            _data.LastPlayer2ShipSelection = (int)Player2ShipChoice;
            SaveManager.SaveOperationStarshine(_data);
            SceneManager.LoadSceneAsync("Operation-Starshine-Level");
            IsCoRunning = false;
        }


        void ConfirmOption(Joysticks PlayerInput)
        {
            switch (PlayerInput)
            {
                case Joysticks.White:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player1Selected = true;
                            }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                            {
                                MenuAM.Play("Menu_Select");
                                Player1Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player1Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player1Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                            {
                                MenuAM.Play("Menu_Select");
                                Player1Selected = true;
                            }

                            break;
                        default:
                            break;
                    }

                    break;
                case Joysticks.Black:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player2Selected = true;
                            }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.A))
                            {
                                MenuAM.Play("Menu_Select");
                                Player2Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player2Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                            {
                                MenuAM.Play("Menu_Select");
                                Player2Selected = true;
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                            {
                                MenuAM.Play("Menu_Select");
                                Player2Selected = true;
                            }

                            break;
                        default:
                            break;
                    }

                    break;
            }
        }


        void UndoConfirmOption(Joysticks PlayerInput)
        {
            switch (PlayerInput)
            {
                case Joysticks.White:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { Player1Selected = false; }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { Player1Selected = false; }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { Player1Selected = false; }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { Player1Selected = false; }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { Player1Selected = false; }

                            break;
                        default:
                            break;
                    }

                    Player1Confirmed.enabled = false;

                    break;
                case Joysticks.Black:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B7)) { Player2Selected = false; }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.B)) { Player2Selected = false; }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7)) { Player2Selected = false; }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { Player2Selected = false; }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { Player2Selected = false; }

                            break;
                        default:
                            break;
                    }

                    Player2Confirmed.enabled = false;

                    break;
            }
        }


        void PlayerSelectionMovement(Joysticks PlayerInput)
        {
            switch (PlayerInput)
            {
                case Joysticks.White:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if ((ArcadeControls.JoystickLeft(PlayerInput)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player1Pos;
                                if (Player1Pos < 0) { Player1Pos = Player1MaxPos - 1; }
                                StartCoroutine(InputLagP1(.2f));
                            }
                            else if ((ArcadeControls.JoystickRight(PlayerInput)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player1Pos;
                                if (Player1Pos > Player1MaxPos - 1) { Player1Pos = 0; }
                                StartCoroutine(InputLagP1(.2f));
                            }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if ((ControllerControls.ControllerLeft(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player1Pos;
                                if (Player1Pos < 0) { Player1Pos = Player1MaxPos - 1; }
                                StartCoroutine(InputLagP1(.2f));
                            }
                            else if ((ControllerControls.ControllerRight(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player1Pos;
                                if (Player1Pos > Player1MaxPos - 1) { Player1Pos = 0; }
                                StartCoroutine(InputLagP1(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player1Pos;
                                if (Player1Pos < 0) { Player1Pos = Player1MaxPos - 1; }
                                StartCoroutine(InputLagP1(.2f));
                            }
                            else if ((KeyboardControls.KeyboardRight(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player1Pos;
                                if (Player1Pos > Player1MaxPos - 1) { Player1Pos = 0; }
                                StartCoroutine(InputLagP1(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player1Pos;
                                if (Player1Pos < 0) { Player1Pos = Player1MaxPos - 1; }
                                StartCoroutine(InputLagP1(.2f));
                            }
                            else if ((KeyboardControls.KeyboardRight(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player1Pos;
                                if (Player1Pos > Player1MaxPos - 1) { Player1Pos = 0; }
                                StartCoroutine(InputLagP1(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if ((ControllerControls.ControllerLeft(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player1Pos;
                                if (Player1Pos < 0) { Player1Pos = Player1MaxPos - 1; }
                                StartCoroutine(InputLagP1(.2f));
                            }
                            else if ((ControllerControls.ControllerRight(Players.P1)) && (!Player1Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player1Pos;
                                if (Player1Pos > Player1MaxPos - 1) { Player1Pos = 0; }
                                StartCoroutine(InputLagP1(.2f));
                            }

                            break;
                        default:
                            break;
                    }

                    break;
                case Joysticks.Black:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if ((ArcadeControls.JoystickLeft(PlayerInput)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player2Pos;
                                if (Player2Pos < 0) { Player2Pos = Player2MaxPos - 1; }
                                StartCoroutine(InputLagP2(.2f));
                            }
                            else if ((ArcadeControls.JoystickRight(PlayerInput)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player2Pos;
                                if (Player2Pos > Player2MaxPos - 1) { Player2Pos = 0; }
                                StartCoroutine(InputLagP2(.2f));
                            }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if ((ControllerControls.ControllerLeft(Players.P2)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player2Pos;
                                if (Player2Pos < 0) { Player2Pos = Player2MaxPos - 1; }
                                StartCoroutine(InputLagP2(.2f));
                            }
                            else if ((ControllerControls.ControllerRight(Players.P2)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player2Pos;
                                if (Player2Pos > Player2MaxPos - 1) { Player2Pos = 0; }
                                StartCoroutine(InputLagP2(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if ((KeyboardControls.KeyboardLeft(Players.P2)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player2Pos;
                                if (Player2Pos < 0) { Player2Pos = Player2MaxPos - 1; }
                                StartCoroutine(InputLagP2(.2f));
                            }
                            else if ((KeyboardControls.KeyboardRight(Players.P2)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player2Pos;
                                if (Player2Pos > Player2MaxPos - 1) { Player2Pos = 0; }
                                StartCoroutine(InputLagP2(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if ((ControllerControls.ControllerLeft(Players.P1)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player2Pos;
                                if (Player2Pos < 0) { Player2Pos = Player2MaxPos - 1; }
                                StartCoroutine(InputLagP2(.2f));
                            }
                            else if ((ControllerControls.ControllerRight(Players.P1)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player2Pos;
                                if (Player2Pos > Player2MaxPos - 1) { Player2Pos = 0; }
                                StartCoroutine(InputLagP2(.2f));
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                --Player2Pos;
                                if (Player2Pos < 0) { Player2Pos = Player2MaxPos - 1; }
                                StartCoroutine(InputLagP2(.2f));
                            }
                            else if ((KeyboardControls.KeyboardRight(Players.P1)) && (!Player2Inputted))
                            {
                                MenuAM.Play("Menu_Click", .4f);
                                ++Player2Pos;
                                if (Player2Pos > Player2MaxPos - 1) { Player2Pos = 0; }
                                StartCoroutine(InputLagP2(.2f));
                            }

                            break;
                        default:
                            break;
                    }

                    break;
            }
        }


        IEnumerator InputLagP1(float Delay)
        {
            Player1Inputted = true;
            UpdateSelectedShipP1(Player1Pos);
            yield return new WaitForSeconds(Delay);
            Player1Inputted = false;
        }


        IEnumerator InputLagP2(float Delay)
        {
            Player2Inputted = true;
            UpdateSelectedShipP2(Player2Pos);
            yield return new WaitForSeconds(Delay);
            Player2Inputted = false;
        }


        void UpdateSelectedShipP1(int ValueToCheckAgainst)
        {
            Player1ShipChoice = (Ships)ValueToCheckAgainst;
        }


        void UpdateSelectedShipP2(int ValueToCheckAgainst)
        {
            Player2ShipChoice = (Ships)ValueToCheckAgainst;
        }
    }
}