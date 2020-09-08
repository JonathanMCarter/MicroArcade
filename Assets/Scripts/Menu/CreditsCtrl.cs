using CarterGames.Arcade.UserInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Arcade.Menu
{
    public class CreditsCtrl : MenuSystem
    {
        [Header("Credits Screens")][Tooltip("All the gameobjects to cycle through in the credits.")]
        [SerializeField] private GameObject[] screens;

        [SerializeField] private GameObject[] controlScreens;

        private float timer;
        private float timeLimit = 4f;

        private bool StartCreditsSequence;
        private bool updateScreen;

        [Header("Start Delay")]
        [Tooltip("How long should the script wait before starting the credits?")]
        public float StartingDelay;

        public Animator FadeToWhite;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        protected override void Start()
        {
            maxPos = screens.Length;

            base.Start();

            // Wait before starting the credits (helps show the title a little)
            StartCoroutine(CreditsStartDelay());


            screens[pos].SetActive(true);



            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:
                    controlScreens[0].SetActive(true);
                    controlScreens[1].SetActive(false);
                    controlScreens[2].SetActive(false);
                    break;
                case SupportedControllers.GamePadBoth:
                    controlScreens[1].SetActive(true);
                    controlScreens[0].SetActive(false);
                    controlScreens[2].SetActive(false);
                    break;
                case SupportedControllers.KeyboardBoth:
                    controlScreens[2].SetActive(true);
                    controlScreens[0].SetActive(false);
                    controlScreens[1].SetActive(false);
                    break;
                case SupportedControllers.KeyboardP1ControllerP2:
                    controlScreens[2].SetActive(true);
                    controlScreens[0].SetActive(false);
                    controlScreens[1].SetActive(false);
                    break;
                case SupportedControllers.KeyboardP2ControllerP1:
                    controlScreens[1].SetActive(true);
                    controlScreens[0].SetActive(false);
                    controlScreens[2].SetActive(false);
                    break;
                default:
                    break;
            }
        }


        protected override void Update()
        {
            base.Update();


            if (Return()) { ReturnToMainMenu(); }


            if (StartCreditsSequence)
            {
                timer += Time.deltaTime;

                if (timer >= timeLimit)
                {
                    MoveToNextScreen();
                }

                if (timer >= timeLimit - 1)
                {
                    updateScreen = true;
                }


                if (ControllerType == SupportedControllers.ArcadeBoard && ArcadeControls.ButtonPress(Joysticks.White, Buttons.B1))
                {
                    timer = 2.9f;
                }
                else if (ControllerType == SupportedControllers.GamePadBoth && ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                {
                    timer = 2.9f;
                }
                else if (ControllerType == SupportedControllers.KeyboardP2ControllerP1 && ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                {
                    timer = 2.9f;
                }
                else if (ControllerType == SupportedControllers.KeyboardP1ControllerP2 && KeyboardControls.ButtonPress(Players.P1, Buttons.B1))
                {
                    timer = 2.9f;
                }
                else if (ControllerType == SupportedControllers.KeyboardBoth && KeyboardControls.ButtonPress(Players.P1, Buttons.B1))
                {
                    timer = 2.9f;
                }
            }


            if (updateScreen)
            {
                ScreenTrans();
            }
        }



        private IEnumerator CreditsStartDelay()
        {
            // wait for the amount inputted in the inspector
            yield return new WaitForSeconds(StartingDelay);

            // the start 'em credits
            StartCreditsSequence = true;
        }



        /// <summary>
        /// Resets the credits position to the default positon of vector3 zero to be played
        /// </summary>
        public void ResetCreditsPos()
        {
            transform.localPosition = Vector3.zero;
        }



        private void ReturnToMainMenu()
        {
            FadeToWhite.SetBool("ChangeScene", true);
            ChangeScene("Arcade-Menu", 1.1f);
        }


        private void ResetTimer()
        {
            timer = 0;
            updateScreen = false;
        }


        private void MoveToNextScreen()
        {
            lastPos = pos;

            if (pos +1 != maxPos)
            {
                pos++;
            }
            else
            {
                pos = 0;
            }

            ResetTimer();
        }


        private void ScreenTrans()
        {
            screens[lastPos].GetComponent<CanvasGroup>().alpha -= 1 * Time.deltaTime;
            screens[pos].GetComponent<CanvasGroup>().alpha += 1 * Time.deltaTime;
        }
    }
}