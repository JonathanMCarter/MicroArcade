using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class GateScript : MonoBehaviour
    {
        public enum GateEnum
        {
            P1Gate,
            P2Gate,
        };

        // Defines which gate this script is gonna use
        [Header("Which gate is this?")]
        public GateEnum Gates;

        GameManager GM;
        AudioManager am;


        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
            am = FindObjectOfType<AudioManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // sets the position to the spawn pos & disables the gameobject
            if (collision.gameObject.GetComponents<CircleCollider2D>().Length > 1)
            {
                for (int i = 0; i < collision.gameObject.GetComponents<CircleCollider2D>().Length; i++)
                {
                    collision.gameObject.GetComponents<CircleCollider2D>()[i].enabled = false;
                }
            }

            collision.gameObject.SetActive(false);


            // Score sorting when a ball gets in into the gates
            if (Gates == GateEnum.P1Gate)
            {
                GM.Player1Stats.Score -= 50;

                if (GM.Game_Type == GameManager.GameTypes.Lives)
                {
                    --GM.Player1Stats.Health;
                }

                am.Play("BallLost", .1f);
            }
            else if (Gates == GateEnum.P2Gate)
            {

                GM.Player2Stats.Score -= 50;

                if (GM.Game_Type == GameManager.GameTypes.Lives)
                {
                    --GM.Player2Stats.Health;
                }

                am.Play("BallLost", .1f);
            }
        }
    }
}