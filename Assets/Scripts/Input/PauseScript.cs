/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using CarterGames.Arcade.Menu;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Arcade.UserInput
{
    public class PauseScript : MenuSystem
    {
        [SerializeField] private GameObject[] menuObjects;
        [SerializeField] private AudioManager audioManager;

        private int timesPressed;
        private float timeoutDelay = 3f;
        private float timeoutTimer = 0f;
        private bool gamePaused;
        private Animator pauseAnim;

        public Animator Trans;
        public Animator threeTwoOne;

        private new void Start()
        {
            pauseAnim = GetComponent<Animator>();
            MenuSystemStart();
            maxPos = menuObjects.Length - 1;
            am = audioManager;
        }


        private new void Update()
        {
            Control();

            if (gamePaused)
            {
                MoveUD();
                UpdateHoveringOption();

                if (Confirm())
                {
                    switch (pos)
                    {
                        case 0:
                            ResumeGame();
                            break;
                        case 1:
                            ReturnToMenu();
                            break;
                        default:
                            break;
                    }
                }
            }

            if (timesPressed >= 3 && !gamePaused)
            {
                PauseGame();
            }


            if ((timesPressed > 0) && (timeoutTimer < timeoutDelay))
            {
                timeoutTimer += Time.deltaTime;

                // failed to pause in time
                if (timeoutTimer > timeoutDelay)
                {
                    timesPressed = 0;
                    timeoutTimer = 0;
                }
            }
        }


        private void UpdateHoveringOption()
        {
            for (int i = 0; i < menuObjects.Length; i++)
            {
                if (pos == i && !menuObjects[i].GetComponent<Animator>().GetBool("isHover"))
                {
                    menuObjects[i].GetComponent<Animator>().SetBool("isHover", true);
                }
                else if (pos != i && menuObjects[i].GetComponent<Animator>().GetBool("isHover"))
                {
                    menuObjects[i].GetComponent<Animator>().SetBool("isHover", false);
                }
            }
        }


        private void Control()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                    {
                        ++timesPressed;
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return))
                    {
                        ++timesPressed;
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        ++timesPressed;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        ++timesPressed;
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return))
                    {
                        ++timesPressed;
                    }

                    break;
                default:
                    break;
            }
        }


        private void PauseGame()
        {
            gamePaused = true;
            Time.timeScale = 0;
            pauseAnim.SetBool("isPaused", true);
        }


        private void ResumeGame()
        {
            StartCoroutine(ResumeGameEnumerator());
        }


        private void ReturnToMenu()
        {
            StartCoroutine(GoToMenuEnumerator());
        }


        private IEnumerator GoToMenuEnumerator()
        {
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSecondsRealtime(1f);
            SceneManager.LoadSceneAsync("PlayMenu");
            Time.timeScale = 1;
        }


        private IEnumerator ResumeGameEnumerator()
        {
            timesPressed = 0;
            timeoutTimer = 0;
            pauseAnim.SetBool("isPaused", false);
            gamePaused = false;
            yield return new WaitForSecondsRealtime(1);
            threeTwoOne.SetBool("isRunning", true);
            yield return new WaitForSecondsRealtime(3.5f);
            threeTwoOne.SetBool("isRunning", false);
            Time.timeScale = 1;
        }
    }
}