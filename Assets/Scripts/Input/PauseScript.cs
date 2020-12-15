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
    public class PauseScript : MonoBehaviour
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

        private void Start()
        {
            pauseAnim = GetComponent<Animator>();
        }


        private void Update()
        {

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