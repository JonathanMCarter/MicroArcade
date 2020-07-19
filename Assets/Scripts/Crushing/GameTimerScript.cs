using UnityEngine;
//using CarterGames.Crushing.GooglePlay;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class GameTimerScript : MonoBehaviour
    {
        [SerializeField] private float levelTimer;

        private bool isTimerRunning;
        private GameManager gameManager;


        private void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }


        private void Update()
        {
            if (isTimerRunning)
            {
                levelTimer += Time.deltaTime;
            }
        }


        /// <summary>
        /// Starts the round timer at whatever value the level timer is currently at
        /// </summary>
        public void StartTimer()
        {
            isTimerRunning = true;
        }


        /// <summary>
        /// Stop sthe game timer and saves the last round time
        /// </summary>
        public void StopTimer()
        {
            isTimerRunning = false;

            // saves the last round time
            gameManager.saveData.lastRoundTime = levelTimer;


            CheckForPB();
        }


        /// <summary>
        /// Resets the level timer to it's default value (0) as well as not allowing the timer to run right afterwards
        /// </summary>
        public void ResetTimer()
        {
            isTimerRunning = false;
            levelTimer = 0;
        }


        /// <summary>
        /// Returns the current value of the level timer in it's standard float value
        /// </summary>
        /// <returns>levelTimer Variables as float</returns>
        public float GetLevelTimer()
        {
            return levelTimer;
        }


        /// <summary>
        /// Checks the see if the last round played was a PB...
        /// </summary>
        private void CheckForPB()
        {
            if (gameManager.saveData.lastRoundTime > gameManager.saveData.longestRoundTime)
            {
                gameManager.isPB = true;

                gameManager.saveData.longestRoundTime = gameManager.saveData.lastRoundTime;

                int _input;
                _input = Mathf.FloorToInt(gameManager.saveData.longestRoundTime * 1000);

                //Leaderboards.AddScoreToLeaderbaord(GPGSIds.leaderboard_crushing_leaderboard, (long)_input);
            }
        }
    }
}