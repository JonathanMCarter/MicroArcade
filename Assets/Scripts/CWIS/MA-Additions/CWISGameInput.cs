using CarterGames.Arcade.UserInput;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class CWISGameInput : InputSettings
    {
        [SerializeField] private Joysticks player;


        private new void Update()
        {
            base.Update();
        }

        public bool FireButton()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(player, Buttons.B1)) { return true; }
                    else { return false; }

                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(player), ControllerButtons.RB)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }

        public bool Button1()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(player, Buttons.B2)) { return true; }
                    else { return false; }

                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(player) ,ControllerButtons.X)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }

        public bool Button2()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(player, Buttons.B3)) { return true; }
                    else { return false; }

                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(player), ControllerButtons.Y)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }

        public bool Button3()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(player, Buttons.B5)) { return true; }
                    else { return false; }

                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(player), ControllerButtons.B)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }

        public bool Button4()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(player, Buttons.B6)) { return true; }
                    else { return false; }

                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(player), ControllerButtons.A)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }
    }
}