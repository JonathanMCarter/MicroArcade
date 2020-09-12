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
        internal bool isCoR;
        internal int lastPos;
        internal int pos;
        internal int maxPos;
        internal bool inputReady;
        internal AudioManager am;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        protected virtual void Start()
        {
            MenuSystemStart();
        }


        public bool Confirm()
        {
            if (MenuControls.Confirm())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Return()
        {
            if (MenuControls.Return())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void MoveUD()
        {
            if (MenuControls.Up() && !isCoR)
            {
                StartCoroutine(MoveAround(-1));
            }

            if (MenuControls.Down() && !isCoR)
            {
                StartCoroutine(MoveAround(1));
            }
        }


        public void MoveLR()
        {
            if (MenuControls.Left() && !isCoR)
            {
                StartCoroutine(MoveAround(-1));
            }

            if (MenuControls.Right() && !isCoR)
            {
                StartCoroutine(MoveAround(1));
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
            }
        }


        internal IEnumerator MoveAround(int Value)
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


        IEnumerator ChangeToScene(string NewScene, float Delay = .5f)
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