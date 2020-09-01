using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.UserInput
{
    public class MenuControls : InputSettings
    {
        private new void Update()
        {
            base.Update();
        }


        public static bool Left(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickLeft(Joysticks.White)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerLeft(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardLeft(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardLeft(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerLeft(Players.P1)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickLeft(Joysticks.Black)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerLeft(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardLeft(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardLeft(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerLeft(Players.P2)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool Right(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickRight(Joysticks.White)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerRight(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardRight(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardRight(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerRight(Players.P1)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickRight(Joysticks.Black)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerRight(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardRight(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardRight(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerRight(Players.P2)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool Up(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickUp(Joysticks.White)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerUp(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardUp(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardUp(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerUp(Players.P1)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickUp(Joysticks.Black)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerUp(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardUp(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardUp(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerUp(Players.P2)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool Down(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickDown(Joysticks.White)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerDown(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardDown(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardDown(Players.P1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerDown(Players.P1)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.JoystickDown(Joysticks.Black)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ControllerDown(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.KeyboardDown(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.KeyboardDown(Players.P2)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ControllerDown(Players.P2)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool Confirm(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                Debug.Log("got P1");

                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.A)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.A)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool Return(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B7)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.B)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.B)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B7)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }


        public static bool ToggleButton(bool isPlayer1 = true)
        {
            if (isPlayer1)
            {
                Debug.Log("got P1");

                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B1)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.X)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.X)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B1)) { return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.X)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B1)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.X)) { return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B1)) { return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
        }
    }
}