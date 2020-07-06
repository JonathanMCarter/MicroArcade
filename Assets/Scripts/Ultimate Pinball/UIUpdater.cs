using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Arcade.Saving;
using Arcade.UserInput;

namespace Pinball
{
    public class UIUpdater : MonoBehaviour
    {
        [SerializeField] private OnScreenKeyboard screenKeyboard;

        public enum Ellments
        {
            None,
            WhitePlayerName,
            WhitePlayerScore,
            WhitePlayerHealth,
            BlackPlayerName,
            BlackPlayerScore,
            BlackPlayerHealth,
            WhitePlayerScoreNumberOnly,
            BlackPlayerScoreNumberOnly,
            WhitePlayerHealthNumberOnly,
            BlackPlayerHealthNumberOnly,
            Timer,
        };

        public Ellments Elly;
        public GameObject Hearts;
       

        GameManager BGM;
        UltimatePinballSessionData SessionData;

        private void Awake()
        {
            BGM = FindObjectOfType<GameManager>();
            SessionData = SaveManager.LoadLastUltimatePinballSession();
        }

        private void Update()
        {
            switch (Elly)
            {
                case Ellments.None:
                    break;
                case Ellments.WhitePlayerName:
                    GetComponent<Text>().text = "Player 1 - " + screenKeyboard.InputtedValue;
                    break;
                case Ellments.BlackPlayerName:
                    GetComponent<Text>().text = "Player 2 - " + screenKeyboard.InputtedValue;
                    break;
                case Ellments.WhitePlayerScore:
                    GetComponent<Text>().text = "Score: " + SessionData.Player1Score.ToString();
                    break;
                case Ellments.BlackPlayerScore:
                    GetComponent<Text>().text = "Score: " + SessionData.Player2Score.ToString();
                    break;
                case Ellments.WhitePlayerHealth:
                    GetComponent<Text>().text = "Lives: " + SessionData.Player1Health.ToString();
                    break;
                case Ellments.BlackPlayerHealth:
                    GetComponent<Text>().text = "Lives: " + SessionData.Player2Health.ToString();
                    break;
                case Ellments.WhitePlayerScoreNumberOnly:
                    GetComponent<Text>().text = BGM.Player1Stats.Score.ToString();
                    break;
                case Ellments.BlackPlayerScoreNumberOnly:
                    GetComponent<Text>().text = BGM.Player2Stats.Score.ToString();
                    break;
                case Ellments.WhitePlayerHealthNumberOnly:

                    if (BGM.Game_Type == GameManager.GameTypes.Lives)
                    {
                        if (Hearts)
                        {
                            Hearts.SetActive(true);
                        }

                        GetComponent<Text>().text = BGM.Player1Stats.Health.ToString();
                    }
                    else
                    {
                        if (Hearts)
                        {
                            Hearts.SetActive(false);
                        }

                        GetComponent<Text>().text = "";
                    }

                    break;
                case Ellments.BlackPlayerHealthNumberOnly:

                    if (BGM.Game_Type == GameManager.GameTypes.Lives)
                    {
                        if (Hearts)
                        {
                            Hearts.SetActive(true);
                        }

                        GetComponent<Text>().text = BGM.Player2Stats.Health.ToString();
                    }
                    else
                    {
                        if (Hearts)
                        {
                            Hearts.SetActive(false);
                        }

                        GetComponent<Text>().text = "";
                    }

                    break;
                case Ellments.Timer:

                    if (BGM.Game_Type == GameManager.GameTypes.Timer)
                    {
                        string Mins = (Mathf.FloorToInt(BGM.Timer / 60)).ToString("00");
                        string Sec = (Mathf.FloorToInt(BGM.Timer % 60)).ToString("00");

                        GetComponent<Text>().text = Mins + ":" + Sec;
                    }
                    else
                    {
                        GetComponent<Text>().text = "";
                    }

                    break;
                default:
                    break;
            }
        }
    }
}