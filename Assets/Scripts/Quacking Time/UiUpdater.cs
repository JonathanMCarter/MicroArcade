using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.QuackingTime
{
    public class UiUpdater : MonoBehaviour
    {
        public enum Elly
        {
            None,
            Player1Current,
            Player1Stored,
            Player2Current,
            Player2Stored,
            Player3Current,
            Player3Stored,
            Player4Current,
            Player4Stored,
            Player1RoundsWon,
            Player2RoundsWon,
            Player3RoundsWon,
            Player4RoundsWon,
            RoundCounter,
            WinningDuck,
            GameTimer,
            Player1RoundsWithX,
            Player2RoundsWithX,
            Player3RoundsWithX,
            Player4RoundsWithX,
        }

        public Elly Ellements;
        private GameManager GM;
        private AudioManager AM;
        private bool HasPlayedBeep;

        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
            AM = FindObjectOfType<DuckScript>().AM;
        }

        private void Update()
        {
            switch (Ellements)
            {
                case Elly.None:
                    break;
                case Elly.Player1Current:
                    GetComponent<Text>().text = GM.Duck1Score.ToString();
                    break;
                case Elly.Player1Stored:
                    GetComponent<Text>().text = GM.FinalDuck1Score.ToString();
                    break;
                case Elly.Player2Current:
                    GetComponent<Text>().text = GM.Duck2Score.ToString();
                    break;
                case Elly.Player2Stored:
                    GetComponent<Text>().text = GM.FinalDuck2Score.ToString();
                    break;
                //case Elly.Player3Current:
                //    GetComponent<Text>().text = GM.Duck3Score.ToString();
                //    break;
                //case Elly.Player3Stored:
                //    GetComponent<Text>().text = GM.FinalDuck3Score.ToString();
                //    break;
                //case Elly.Player4Current:
                //    GetComponent<Text>().text = GM.Duck4Score.ToString();
                //    break;
                //case Elly.Player4Stored:
                //    GetComponent<Text>().text = GM.FinalDuck4Score.ToString();
                //    break;
                case Elly.Player1RoundsWon:
                    GetComponent<Text>().text = PlayerPrefs.GetInt("P1-Rounds").ToString();
                    break;
                case Elly.Player2RoundsWon:
                    GetComponent<Text>().text = PlayerPrefs.GetInt("P2-Rounds").ToString();
                    break;
                //case Elly.Player3RoundsWon:
                //    GetComponent<Text>().text = PlayerPrefs.GetInt("P3-Rounds").ToString();
                //    break;
                //case Elly.Player4RoundsWon:
                //    GetComponent<Text>().text = PlayerPrefs.GetInt("P4-Rounds").ToString();
                //    break;
                case Elly.RoundCounter:
                    GetComponent<Text>().text = "Round: " + (GM.RoundsCompleted + 1).ToString();
                    break;
                case Elly.WinningDuck:

                    switch (GM.GetWinningDuck())
                    {
                        case DuckPlayers.P1:
                            GetComponent<Text>().text = "Yellow Duck Wins!!!";
                            break;
                        case DuckPlayers.P2:
                            GetComponent<Text>().text = "Purple Duck Wins!!!";
                            break;
                        //case DuckPlayers.P3:
                        //    GetComponent<Text>().text = "Blue Duck Wins!!!";
                        //    break;
                        //case DuckPlayers.P4:
                        //    GetComponent<Text>().text = "Green Duck Wins!!!";
                        //    break;
                        case DuckPlayers.None:
                            GetComponent<Text>().text = "Its a Tie!!!";
                            break;
                        default:
                            break;
                    }

                    break;
                case Elly.GameTimer:

                    string Mins = Mathf.Floor(((GM.RoundLength - GM.GameTimer) % 3600) / 60).ToString("00");
                    string Sec = Mathf.Floor((GM.RoundLength - GM.GameTimer) % 60).ToString("00");

                    if ((Sec == "59") && (!HasPlayedBeep))
                    {
                        AM.Play("Game_Timer_Beep_01");
                        HasPlayedBeep = true;
                    }

                    GetComponent<Text>().text = Mins + ":" + Sec;

                    break;
                case Elly.Player1RoundsWithX:
                    GetComponent<Text>().text = "x " + (PlayerPrefs.GetInt("P1-Rounds")).ToString();
                    break;
                case Elly.Player2RoundsWithX:
                    GetComponent<Text>().text = "x " + (PlayerPrefs.GetInt("P2-Rounds")).ToString();
                    break;
                case Elly.Player3RoundsWithX:
                    GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("P3-Rounds").ToString();
                    break;
                case Elly.Player4RoundsWithX:
                    GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("P4-Rounds").ToString();
                    break;
                default:
                    break;
            }
        }
    }
}