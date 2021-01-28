using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Scoring : MonoBehaviour
    {
        [SerializeField] private Text display;
        private const string prefix = "Score:";
        private int playerScore;
        private int lastScore;


        private void Start()
        {
            ResetScore();
        }


        private void Update()
        {
            if (!lastScore.Equals(playerScore))
            {
                display.text = string.Format("{0} {1}", prefix, playerScore.ToString());
                lastScore = playerScore;
            }
        }


        public void IncrementScore(int value)
        {
            lastScore = playerScore;
            playerScore += value;
        }


        public void DecrementScore(int value)
        {
            lastScore = playerScore;
            playerScore -= value;
        }


        public void ResetScore()
        {
            lastScore = -1;
            playerScore = 0;
        }
    }
}