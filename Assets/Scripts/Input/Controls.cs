using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.UserInput
{
    /// <summary>
    /// CLASS | Universal Controls, get user input from this class as it will work on all platforms from this class, only use specific platform classes when needed.
    /// </summary>
    public class Controls
    {
        /// <summary>
        /// Gets whether or not the user has inputted left on any control scheme
        /// </summary>
        /// <param name="isPlayer1">should get player 1? default = true</param>
        /// <returns>whether the input has been pressed.</returns>
        public static bool Left(bool isPlayer1 = true)
        {
            switch (InputSettings.ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (isPlayer1) { return ArcadeControls.JoystickLeft(Joysticks.White); }
                    else { return ArcadeControls.JoystickLeft(Joysticks.Black); }

                case SupportedControllers.GamePadBoth:

                    if (isPlayer1) { return ControllerControls.ControllerLeft(Players.P1); }
                    else { return ControllerControls.ControllerLeft(Players.P2); }

                case SupportedControllers.KeyboardBoth:

                    if (isPlayer1) { return KeyboardControls.KeyboardLeft(Players.P1); }
                    else { return KeyboardControls.KeyboardLeft(Players.P2); }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if (isPlayer1) { return KeyboardControls.KeyboardLeft(Players.P1); }
                    else { return ControllerControls.ControllerLeft(Players.P2); }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if (isPlayer1) { return ControllerControls.ControllerLeft(Players.P1); }
                    else { return KeyboardControls.KeyboardLeft(Players.P2); }

                default:
                    return false;
            }
        }


        /// <summary>
        /// Gets whether or not the user has inputted right on any control scheme
        /// </summary>
        /// <param name="isPlayer1">should get player 1? default = true</param>
        /// <returns>whether the input has been pressed.</returns>
        public static bool Right(bool isPlayer1 = true)
        {
            switch (InputSettings.ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (isPlayer1) { return ArcadeControls.JoystickRight(Joysticks.White); }
                    else { return ArcadeControls.JoystickRight(Joysticks.Black); }

                case SupportedControllers.GamePadBoth:

                    if (isPlayer1) { return ControllerControls.ControllerRight(Players.P1); }
                    else { return ControllerControls.ControllerRight(Players.P2); }

                case SupportedControllers.KeyboardBoth:

                    if (isPlayer1) { return KeyboardControls.KeyboardRight(Players.P1); }
                    else { return KeyboardControls.KeyboardRight(Players.P2); }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if (isPlayer1) { return KeyboardControls.KeyboardRight(Players.P1); }
                    else { return ControllerControls.ControllerRight(Players.P2); }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if (isPlayer1) { return ControllerControls.ControllerRight(Players.P1); }
                    else { return KeyboardControls.KeyboardRight(Players.P2); }

                default:
                    return false;
            }
        }


        /// <summary>
        /// Gets whether or not the user has inputted down on any control scheme
        /// </summary>
        /// <param name="isPlayer1">should get player 1? default = true</param>
        /// <returns>whether the input has been pressed.</returns>
        public static bool Down(bool isPlayer1 = true)
        {
            switch (InputSettings.ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (isPlayer1) { return ArcadeControls.JoystickDown(Joysticks.White); }
                    else { return ArcadeControls.JoystickDown(Joysticks.Black); }

                case SupportedControllers.GamePadBoth:

                    if (isPlayer1) { return ControllerControls.ControllerDown(Players.P1); }
                    else { return ControllerControls.ControllerDown(Players.P2); }

                case SupportedControllers.KeyboardBoth:

                    if (isPlayer1) { return KeyboardControls.KeyboardDown(Players.P1); }
                    else { return KeyboardControls.KeyboardDown(Players.P2); }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if (isPlayer1) { return KeyboardControls.KeyboardDown(Players.P1); }
                    else { return ControllerControls.ControllerDown(Players.P2); }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if (isPlayer1) { return ControllerControls.ControllerDown(Players.P1); }
                    else { return KeyboardControls.KeyboardDown(Players.P2); }

                default:
                    return false;
            }
        }


        /// <summary>
        /// Gets whether or not the user has inputted right on any control scheme
        /// </summary>
        /// <param name="isPlayer1">should get player 1? default = true</param>
        /// <returns>whether the input has been pressed.</returns>
        public static bool None(bool isPlayer1 = true)
        {
            switch (InputSettings.ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (isPlayer1) { return ArcadeControls.JoystickNone(Joysticks.White); }
                    else { return ArcadeControls.JoystickNone(Joysticks.Black); }

                case SupportedControllers.GamePadBoth:

                    if (isPlayer1) { return ControllerControls.ControllerNone(Players.P1); }
                    else { return ControllerControls.ControllerNone(Players.P2); }

                case SupportedControllers.KeyboardBoth:

                    if (isPlayer1) { return KeyboardControls.KeyboardNone(Players.P1); }
                    else { return KeyboardControls.KeyboardNone(Players.P2); }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if (isPlayer1) { return KeyboardControls.KeyboardNone(Players.P1); }
                    else { return ControllerControls.ControllerNone(Players.P2); }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if (isPlayer1) { return ControllerControls.ControllerNone(Players.P1); }
                    else { return KeyboardControls.KeyboardNone(Players.P2); }

                default:
                    return false;
            }
        }
    }
}