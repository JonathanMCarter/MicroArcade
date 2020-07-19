using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;
using CarterGames.Arcade.UserInput;

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class LightBarrierOBJ : MonoBehaviour
    {
        public bool Active;

        LightBarrierCtrl LC;
        public AudioManager AM;
        GameManager GM;

        private void Start()
        {
            LC = FindObjectOfType<LightBarrierCtrl>();
            GM = FindObjectOfType<GameManager>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Active = !Active;

            if (Active)
            {
                ++LC.NumberOfActiveBarriers;
                SwitchBetweenColours(collision.GetComponent<BallMoveScript>());
            }
            else
            {
                --LC.NumberOfActiveBarriers;
                ResetColours();
            }
        }


        void SwitchBetweenColours(BallMoveScript Script)
        {
            switch (LC.ScoringStage)
            {
                case LightBarrierCtrl.BarrierStages.Blue:

                    GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, .5f);

                    switch (Script.LastHit)
                    {
                        case Joysticks.White:
                            GM.Player1Stats.Score += 100;
                            break;
                        case Joysticks.Black:
                            GM.Player2Stats.Score += 100;
                            break;
                        case Joysticks.None:
                            break;
                        default:
                            break;
                    }

                    break;
                case LightBarrierCtrl.BarrierStages.Green:

                    GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, .5f);

                    switch (Script.LastHit)
                    {
                        case Joysticks.White:
                            GM.Player1Stats.Score += 250;
                            break;
                        case Joysticks.Black:
                            GM.Player2Stats.Score += 250;
                            break;
                        case Joysticks.None:
                            break;
                        default:
                            break;
                    }

                    break;
                case LightBarrierCtrl.BarrierStages.Yellow:

                    GetComponent<SpriteRenderer>().color = new Color(1, .92f, 0.016f, .5f);

                    switch (Script.LastHit)
                    {
                        case Joysticks.White:
                            GM.Player1Stats.Score += 500;
                            break;
                        case Joysticks.Black:
                            GM.Player2Stats.Score += 500;
                            break;
                        case Joysticks.None:
                            break;
                        default:
                            break;
                    }

                    break;
                case LightBarrierCtrl.BarrierStages.Red:

                    GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, .5f);

                    switch (Script.LastHit)
                    {
                        case Joysticks.White:
                            GM.Player1Stats.Score += 1000;
                            break;
                        case Joysticks.Black:
                            GM.Player2Stats.Score += 1000;
                            break;
                        case Joysticks.None:
                            break;
                        default:
                            break;
                    }

                    break;
                case LightBarrierCtrl.BarrierStages.Purple:

                    GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, .5f);

                    switch (Script.LastHit)
                    {
                        case Joysticks.White:
                            GM.Player1Stats.Score += 5000;
                            break;
                        case Joysticks.Black:
                            GM.Player2Stats.Score += 5000;
                            break;
                        case Joysticks.None:
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }

            AM.Play("LightGateCom", .15f);
        }

        internal void ResetColours()
        {
            Active = false;
            GetComponent<SpriteRenderer>().color = Color.clear;
            Active = false;
        }
    }
}