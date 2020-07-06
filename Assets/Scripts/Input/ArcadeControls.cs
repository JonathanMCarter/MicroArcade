using UnityEngine;

/// <summary>
/// Helper Namespace, this is purely to save writig "input.get....." a million times.
/// </summary>
namespace Arcade
{
    /// <summary>
    /// Enum for the controllers, both white (P1) and black (P2)
    /// </summary>
    public enum Joysticks
    {
        White,
        Black,
        None,
    };

    /// <summary>
    /// Enum for all the buttons on the board that can be pressed (Buttons 1-8 for both players)
    /// </summary>
    public enum Buttons
    {
        B1, B2, B3, B4, B5, B6, B7, B8
    };

    public class ArcadeControls
    {
        /// <summary>
        /// Checks to see if the inputted joystick has been moved left
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickLeft(Joysticks controller)
        {
            if (Input.GetAxisRaw(controller.ToString() + "LR") < -.15f)
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved right
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickRight(Joysticks controller)
        {
            if (Input.GetAxisRaw(controller.ToString() + "LR") > .15f)
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved up
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickUp(Joysticks controller)
        {
            if (Input.GetAxisRaw(controller.ToString() + "UD") < -.15f) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved down
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickDown(Joysticks controller)
        {
            if (Input.GetAxisRaw(controller.ToString() + "UD") > .15f) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved to the north east position
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickNorthEast(Joysticks controller)
        {
            if ((Input.GetAxisRaw(controller.ToString() + "UD") < -.15f) && (Input.GetAxisRaw(controller.ToString() + "LR") > .15f)) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved to the south east position
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickSouthEast(Joysticks controller)
        {
            if ((Input.GetAxisRaw(controller.ToString() + "UD") > .15f) && (Input.GetAxisRaw(controller.ToString() + "LR") > .15f)) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved to the north west position
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickNorthWest(Joysticks controller)
        {
            if ((Input.GetAxisRaw(controller.ToString() + "UD") < -.15f) && (Input.GetAxisRaw(controller.ToString() + "LR") < -.15f)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Checks to see if the inputted joystick has been moved to the south west position
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickSouthWest(Joysticks controller)
        {
            if ((Input.GetAxisRaw(controller.ToString() + "UD") > .15f) && (Input.GetAxisRaw(controller.ToString() + "LR") < -.15f)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Checks to see if no input is pressed
        /// </summary>
        /// <param name="controller">Joystick to check</param>
        /// <returns>true or false</returns>
        public static bool JoystickNone(Joysticks controller)
        {
            if (Input.GetAxisRaw(controller.ToString() + "LR") < -.1f) { return false; }
            else if (Input.GetAxisRaw(controller.ToString() + "LR") > .1f) { return false; }
            else if (Input.GetAxisRaw(controller.ToString() + "UD") < -.1f) { return false; }
            else if (Input.GetAxisRaw(controller.ToString() + "UD") > .1f) { return false; }
            else { return true; }
        }

        /// <summary>
        /// Checks to see if the button has been pressed on the desired player
        /// </summary>
        /// <param name="controller">controller to check</param>
        /// <param name="button">Button to check</param>
        /// <returns>true or false</returns>
        public static bool ButtonPress(Joysticks controller, Buttons button)
        {
            if (Input.GetButtonDown(controller.ToString() + button.ToString())) { return true; }
            else { return false; }
        }


        /// <summary>
        /// Checks to see if the button is been held down on the desired player
        /// </summary>
        /// <param name="controller">controller to check</param>
        /// <param name="button">Button to check</param>
        /// <returns>true or false</returns>
        public static bool ButtonHeldDown(Joysticks controller, Buttons button)
        {
            if (Input.GetButton(controller.ToString() + button.ToString())) { return true; }
            else { return false; }
        }


        /// <summary>
        /// Checks to see if the button has been released on the desired player
        /// </summary>
        /// <param name="controller">controller to check</param>
        /// <param name="button">Button to check</param>
        /// <returns>true or false</returns>
        public static bool ButtonReleased(Joysticks controller, Buttons button)
        {
            if (Input.GetButtonUp(controller.ToString() + button.ToString())) { return true; }
            else { return false; }
        }
    }
}
