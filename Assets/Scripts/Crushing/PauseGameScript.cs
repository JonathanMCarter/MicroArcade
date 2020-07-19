using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class PauseGameScript : MonoBehaviour
    {
        [SerializeField] private CanvasGroup pauseMenuCanvasGroup;
        [SerializeField] private Text resumeText;

        private bool isPaused = false;
        private bool canPause = false;
        private int numberOfTimesPressed = 0;
        private const int numberOfPressesNeeded = 3;


        private void Start()
        {
            // stops the user pausing the game until it has started...
            StartCoroutine(WaitToAllow());
        }


        private void Update()
        {
            if (canPause)
            {
                // check to see if the required amount of presses have been made
                if (numberOfTimesPressed >= numberOfPressesNeeded)
                {
                    PauseGame();
                }

                if (numberOfTimesPressed == 0 && pauseMenuCanvasGroup.alpha != 0)
                {
                    pauseMenuCanvasGroup.alpha -= (2 * Time.unscaledDeltaTime);
                }

                // increment on user input
                if (Input.touchCount >= 2)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        ++numberOfTimesPressed;
                    }
                }

                // for debugging only
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ++numberOfTimesPressed;
                }
            }
        }


        /// <summary>
        /// pauses the game instantly on call
        /// </summary>
        public void PauseGame()
        {
            // Does the pausing
            Time.timeScale = 0;
            isPaused = true;
            pauseMenuCanvasGroup.alpha += 2 * Time.unscaledDeltaTime;
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }


        /// <summary>
        /// Calls the corutine that resumes the game
        /// </summary>
        public void ResumeGame()
        {
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
            // calls the method to resume the game...
            StartCoroutine(ResumeGameCorutine());
        }


        /// <summary>
        /// Resumes the game after showing the display counting dwon from 3 to 0
        /// </summary>
        private IEnumerator ResumeGameCorutine()
        {
            numberOfTimesPressed = 0;
            resumeText.enabled = true;
            resumeText.gameObject.GetComponent<Animator>().SetBool("isResume", true);
            resumeText.text = "3";
            yield return new WaitForSecondsRealtime(1f);
            resumeText.text = "2";
            yield return new WaitForSecondsRealtime(1f);
            resumeText.text = "1";
            yield return new WaitForSecondsRealtime(1f);
            resumeText.text = "Go.";
            isPaused = false;
            Time.timeScale = 1;
            yield return new WaitForSecondsRealtime(.95f);
            resumeText.gameObject.GetComponent<Animator>().SetBool("isResume", false);
            resumeText.enabled = false;
        }


        /// <summary>
        /// Allows the user to pause the games after 3 seconds...
        /// </summary>
        private IEnumerator WaitToAllow()
        {
            yield return new WaitForSeconds(3f);
            canPause = true;
        }
    }
}