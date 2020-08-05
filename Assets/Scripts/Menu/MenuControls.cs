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


        public static bool Left()
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


        public static bool Right()
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


        public static bool Up()
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


        public static bool Down()
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


        public static bool Confirm()
        {
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


        public static bool Return()
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
    }
}