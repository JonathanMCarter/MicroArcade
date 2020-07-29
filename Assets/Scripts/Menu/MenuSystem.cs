using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CarterGames.Assets.AudioManager;
using CarterGames.Arcade.UserInput;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class MenuSystem : InputSettings
    {
        private bool isCoR;

        internal int lastPos;
        internal int pos;
        internal int maxPos;
        internal bool inputReady;
        internal AudioManager am;



        protected virtual void Start()
        {
            MenuSystemStart();
        }


        public bool Confirm()
        {
            if (inputReady)
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
            if (inputReady)
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
            if (inputReady)
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
            if (inputReady)
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

                    if ((ArcadeControls.JoystickUp(Joysticks.White)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ArcadeControls.JoystickDown(Joysticks.White)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerUp(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerDown(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardUp(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardDown(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardUp(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardDown(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerUp(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerDown(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

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

                    if ((ArcadeControls.JoystickLeft(Joysticks.White)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ArcadeControls.JoystickRight(Joysticks.White)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(-1)); }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!isCoR)) { StartCoroutine(MoveAround(1)); }

                    break;
                default:
                    break;
            }
        }


        public void MenuSystemStart()
        {
            lastPos = pos;
            isCoR = false;
            inputReady = true;

            if (GetComponent<AudioManager>())
            {
                am = GetComponent<AudioManager>();
                am.UpdateLibrary();
            }
        }


        IEnumerator MoveAround(int Value)
        {
            isCoR = true;

            lastPos = pos;
            pos += Value;

            if (pos == (maxPos + 1))  {  pos = 0;  }
            else if (pos == -1)  {  pos = maxPos;   }

            if (am)
            {
                am.Play("Menu_Click", Random.Range(.65f, .85f), Random.Range(.85f, 1.15f));
            }

            yield return new WaitForSecondsRealtime(.25f);
            isCoR = false;
        }


        IEnumerator InputLag()
        {
            inputReady = false;
            yield return new WaitForSecondsRealtime(.25f);
            inputReady = true;
        }


        public void Reset()
        {
            pos = 0;
        }


        public bool ValueChanged()
        {
            if (lastPos != pos) { return true; }
            else { return false; }
        }


        public void ChangeScene(string Scene, float Delay = 1.25f)
        {
            StartCoroutine(ChangeToScene(Scene, Delay));
        }


        IEnumerator ChangeToScene(string NewScene, float Delay = 1.25f)
        {
            inputReady = false;
            am.Play("Menu-Confirm", Random.Range(.65f, .85f));
            yield return new WaitForSecondsRealtime(Delay);
            AsyncOperation Async = SceneManager.LoadSceneAsync(NewScene);
            Async.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(.1f);
            Async.allowSceneActivation = true;
            yield return new WaitForSecondsRealtime(.1f);
            inputReady = true;
        }


        public int GetLRDir()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if ((ArcadeControls.JoystickLeft(Joysticks.White)) && (!isCoR)) { return -1; }
                    if ((ArcadeControls.JoystickRight(Joysticks.White)) && (!isCoR)) { return 1; }
                    else { return 0; }

                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!isCoR)) { return -1; }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!isCoR)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!isCoR)) { return -1; }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!isCoR)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardLeft(Players.P1)) && (!isCoR)) { return -1; }
                    if ((KeyboardControls.KeyboardRight(Players.P1)) && (!isCoR)) { return 1; }
                    else { return 0; }

                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerLeft(Players.P1)) && (!isCoR)) { return -1; }
                    if ((ControllerControls.ControllerRight(Players.P1)) && (!isCoR)) { return 1; }
                    else { return 0; }

                default:
                    return 0;
            }
        }
    }
}