using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arcade.Menu
{
    public class MenuSystem : InputSettings
    {
        [HideInInspector]
        public int LastPos;
        [HideInInspector]
        public int Pos;
        [HideInInspector]
        public int MaxPos;
        bool IsCo;
        public bool InputReady;

        internal AudioManager AM;

        protected virtual void Start()
        {
            MenuSystemStart();
        }


        public bool Confirm()
        {
            if (InputReady)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { StartCoroutine(InputLag()); Debug.Log(">> Confirm Pressed <<");  return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool Confirm(ControllerButtons DesiredButton)
        {
            if (InputReady)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, DesiredButton)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, DesiredButton)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool Return()
        {
            if (InputReady)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }    
        }


        public bool Return(ControllerButtons DesiredButton)
        {
            if (InputReady)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, DesiredButton)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, DesiredButton)) { StartCoroutine(InputLag()); return true; }
                        else { return false; }

                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }


        public void MoveUD()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if ((ArcadeControls.JoystickUp(Joysticks.White)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ArcadeControls.JoystickDown(Joysticks.White)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerUp(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerDown(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardUp(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardDown(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardUp(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardDown(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerUp(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerDown(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                default:
                    break;
            }
        }


        public void MoveLR()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if ((ArcadeControls.JoystickLeft(Joysticks.White)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ArcadeControls.JoystickRight(Joysticks.White)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!IsCo)) { StartCoroutine(MoveAround(1)); }

                    break;
                default:
                    break;
            }
        }


        public void MenuSystemStart()
        {
            LastPos = Pos;
            IsCo = false;
            InputReady = true;

            if (GetComponent<AudioManager>())
            {
                AM = GetComponent<AudioManager>();
                AM.UpdateLibrary();
            }
        }


        IEnumerator MoveAround(int Value)
        {
            IsCo = true;

            LastPos = Pos;
            Pos += Value;

            if (Pos == (MaxPos + 1))  {  Pos = 0;  }
            else if (Pos == -1)  {  Pos = MaxPos;   }

            if (AM)
            {
                AM.Play("Menu_Click", Random.Range(.65f, .85f), Random.Range(.85f, 1.15f));
            }

            yield return new WaitForSecondsRealtime(.25f);
            IsCo = false;
        }


        IEnumerator InputLag()
        {
            InputReady = false;
            yield return new WaitForSecondsRealtime(.25f);
            InputReady = true;
        }


        public void Reset()
        {
            Pos = 0;
        }


        public bool ValueChanged()
        {
            if (LastPos != Pos) { return true; }
            else { return false; }
        }


        public void ChangeScene(string Scene, float Delay = 1.25f)
        {
            StartCoroutine(ChangeToScene(Scene, Delay));
        }


        IEnumerator ChangeToScene(string NewScene, float Delay = 1.25f)
        {
            InputReady = false;
            AM.Play("Menu-Confirm", Random.Range(.65f, .85f));
            yield return new WaitForSecondsRealtime(Delay);
            AsyncOperation Async = SceneManager.LoadSceneAsync(NewScene);
            Async.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(.1f);
            Async.allowSceneActivation = true;
            yield return new WaitForSecondsRealtime(.1f);
            InputReady = true;
        }


        public int GetLRDir()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if ((ArcadeControls.JoystickLeft(Joysticks.White)) && (!IsCo)) { return -1; }
                    if ((ArcadeControls.JoystickRight(Joysticks.White)) && (!IsCo)) { return 1; }
                    else { return 0; }

                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!IsCo)) { return -1; }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!IsCo)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!IsCo)) { return -1; }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!IsCo)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!IsCo)) { return -1; }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!IsCo)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!IsCo)) { return -1; }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!IsCo)) { return 1; }
                    else { return 0; }

                default:
                    return 0;
            }
        }
    }
}