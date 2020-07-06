/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using System.Collections;
using UnityEngine;

namespace Pinball.BallCtrl
{
    public class CurveTriggerPoint : MonoBehaviour
    {
        [SerializeField]
        private CurveCtrl curveController;
        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartCoroutine(WaitBeforeChange(collision.GetComponent<BallMoveScript>()));
        }

        private IEnumerator WaitBeforeChange(BallMoveScript script)
        {
            switch (script.LastHit)
            {
                case Arcade.Joysticks.White:
                    gameManager.Player1Stats.Score += 250;
                    break;
                case Arcade.Joysticks.Black:
                    gameManager.Player2Stats.Score += 250;
                    break;
                case Arcade.Joysticks.None:
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(1f);

            if (curveController.catchBox.enabled)
            {
                curveController.catchBox.enabled = false;
            }
        }
    }
}