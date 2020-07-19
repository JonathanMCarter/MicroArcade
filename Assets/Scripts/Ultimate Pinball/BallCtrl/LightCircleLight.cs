using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class LightCircleLight : MonoBehaviour
    {
        public bool IsActive;

        public enum Colours
        {
            White,
            Black,
        };

        public Colours Col;

        [Header("Auto - From Parent")]
        public LightCircleScript LCS;
        GameManager GM;


        private void OnEnable()
        {
            GM = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (IsActive) {  GetComponent<SpriteRenderer>().color = Color.yellow;  }
            else
            {
                GetComponent<SpriteRenderer>().color = LCS.Col[0];
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsActive)
            {
                switch (collision.gameObject.GetComponent<BallMoveScript>().LastHit)
                {
                    case Joysticks.White:
                        GM.Player1Stats.Score += 25;
                        break;
                    case Joysticks.Black:
                        GM.Player2Stats.Score += 25;
                        break;
                    default:
                        break;
                }

                IsActive = true;
                LCS.NumberActive = LCS.CheckAmount();
            }
        }
    }
}