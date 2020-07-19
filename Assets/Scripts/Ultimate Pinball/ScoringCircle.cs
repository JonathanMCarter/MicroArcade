using UnityEngine;
using CarterGames.UltimatePinball.BallCtrl;
using CarterGames.Arcade.UserInput;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.UltimatePinball
{
    public class ScoringCircle : MonoBehaviour
    {
        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<BallMoveScript>().LastHit == Joysticks.White)
            {
                gameManager.Player1Stats.Score += 15;
            }
            else if (collision.gameObject.GetComponent<BallMoveScript>().LastHit == Joysticks.Black)
            {
                gameManager.Player2Stats.Score += 15;
            }
        }
    }
}