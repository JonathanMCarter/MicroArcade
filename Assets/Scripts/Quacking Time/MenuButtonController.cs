using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Arcade;   

namespace Quacking
{
    public class MenuButtonController : InputSettings
    {
        [Header(" ----- { Menu Button controller } -----")]
        public UnityEvent ConfirmEvent;
        public UnityEvent ReturnEvent;

        protected override void Update()
        {
            base.Update();

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { ConfirmEvent.Invoke(); }
                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { ReturnEvent.Invoke(); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { ConfirmEvent.Invoke(); }
                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { ReturnEvent.Invoke(); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ConfirmEvent.Invoke(); Debug.Log("I'm being pressed"); }
                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { ReturnEvent.Invoke(); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ConfirmEvent.Invoke(); Debug.Log("I'm being pressed"); }
                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { ReturnEvent.Invoke(); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { ConfirmEvent.Invoke(); }
                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { ReturnEvent.Invoke(); }

                    break;
                default:
                    break;
            }
        }
    }
}