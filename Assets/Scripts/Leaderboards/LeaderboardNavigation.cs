using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Leaderboard
{
    public class LeaderboardNavigation : InputSettings
    {
        public ScrollRect scrollRect;

        protected override void Update()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.JoystickUp(Joysticks.White)) { scrollRect.verticalNormalizedPosition += 1f * Time.deltaTime; }
                    else if (ArcadeControls.JoystickDown(Joysticks.White)) { scrollRect.verticalNormalizedPosition -= 1f * Time.deltaTime; }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ControllerUp(Players.P1)) { scrollRect.verticalNormalizedPosition += 1f * Time.deltaTime; }
                    else if (ControllerControls.ControllerDown(Players.P1)) { scrollRect.verticalNormalizedPosition -= 1f * Time.deltaTime; }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.KeyboardUp(Players.P1)) { scrollRect.verticalNormalizedPosition += 1f * Time.deltaTime; }
                    else if (KeyboardControls.KeyboardDown(Players.P1)) { scrollRect.verticalNormalizedPosition -= 1f * Time.deltaTime; }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.KeyboardUp(Players.P1)) { scrollRect.verticalNormalizedPosition += 1f * Time.deltaTime; }
                    else if (KeyboardControls.KeyboardDown(Players.P1)) { scrollRect.verticalNormalizedPosition -= 1f * Time.deltaTime; }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ControllerControls.ControllerUp(Players.P1)) { scrollRect.verticalNormalizedPosition += 1f * Time.deltaTime; }
                    else if (ControllerControls.ControllerDown(Players.P1)) { scrollRect.verticalNormalizedPosition -= 1f * Time.deltaTime; }

                    break;
                default:
                    break;
            }


            base.Update();
        }
    }
}