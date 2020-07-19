using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;

namespace CarterGames.UltimatePinball
{
    public class PlayerCtrl : InputSettings
    {
        // Which joystick player is this?
        [Header("Which player this gameobject is for?")]
        public Joysticks ThisPlayer;

        // The flippers this player controls
        [Header("The players flippers (left first, right second)")]
        public List<Flip_Ctrl> PlayerFlippers;


        protected override void Update()
        {
            base.Update();

            // If the joystick is pressed down
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.JoystickDown(ThisPlayer)) { FlipBoth(); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ControllerDown(ConvertToPlayers())) { FlipBoth(); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.KeyboardDown(ConvertToPlayers())) { FlipBoth(); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (ConvertToPlayers() == Players.P1)
                    {
                        if (KeyboardControls.KeyboardDown(Players.P1)) { FlipBoth(); }
                    }
                    else
                    {
                        if (ControllerControls.ControllerDown(Players.P1)) { FlipBoth(); }
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ConvertToPlayers() == Players.P2)
                    {
                        if (KeyboardControls.KeyboardDown(Players.P1)) { FlipBoth(); }
                    }
                    else
                    {
                        if (ControllerControls.ControllerDown(Players.P1)) { FlipBoth(); }
                    }

                    break;
                default:
                    break;
            }
        }

        void FlipBoth()
        {
            // Flip both flippers up
            PlayerFlippers[0].FlipLeftFlipper();
            PlayerFlippers[1].FlipRightFlipper();
        }

        Players ConvertToPlayers()
        {
            return (Players)ThisPlayer;
        }
    }
}