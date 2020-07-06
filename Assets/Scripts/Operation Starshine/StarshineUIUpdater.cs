using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Arcade;
using Arcade.UserInput;
using Arcade.Saving;


namespace Starshine
{
    public class StarshineUIUpdater : MonoBehaviour
    {
        public enum Ellyments
        {
            WhitePlayerHealth,
            BlackPlayerHealth,
            WhitePlayerShields,
            BlackPlayerShields,
            ShipHealthNumberValue,
            ShipShieldNumberValue,
            Player1ShipSelection,
            Player2ShipSelection,
            PlayerSpeedSliderValue,
            PlayerMainWeaponIcon,
            PlayerAltWeaponIcon,
            Player1Score,
            Player2Score,
            Player1AndPlayer2Score,
            LeaderboardP1Value,
            LeaderboardP2Value,
            Player1Controls,
            Player2Controls,
        };

        public Ellyments Elly;

        [Header("Ship Selection Health/Shield Value")]
        public ShipStats Ship;

        [Header("Ship Selection Options List")]
        public List<GameObject> ShipSelectionOptions;

        [Header("Victory Online Board Names")]
        public OnScreenKeyboard Player1Name;
        public OnScreenKeyboard Player2Name;

        GameManager GM;
        PlayerController White;
        PlayerController Black;
        ShipSelection SS;
        public StarshineLeaderboardsSend SSLBS;
        public OperationStarshineData Data;

        public bool UIUNeedsUpdating;


        void Start()
        {
            GM = FindObjectOfType<GameManager>();

            if ((Elly == Ellyments.BlackPlayerHealth) || (Elly == Ellyments.BlackPlayerShields) || (Elly == Ellyments.WhitePlayerHealth) || (Elly == Ellyments.WhitePlayerShields))
            {
                White = GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<PlayerController>();
                Black = GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<PlayerController>();
            }

            if ((Elly == Ellyments.Player1ShipSelection) || (Elly == Ellyments.Player2ShipSelection))
            {
                SS = FindObjectOfType<ShipSelection>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    ShipSelectionOptions.Add(transform.GetChild(i).gameObject);
                }

                SS.Player1MaxPos = transform.childCount;
                SS.Player2MaxPos = transform.childCount;
            }

            if ((Elly == Ellyments.LeaderboardP1Value) || (Elly == Ellyments.LeaderboardP2Value))
            {
                Data = SaveManager.LoadOperationStarshine();
            }

            SSLBS = FindObjectOfType<StarshineLeaderboardsSend>();

            UIUNeedsUpdating = true;
        }


        void Update()
        {
            switch (Elly)
            {
                case Ellyments.WhitePlayerHealth:

                    if (GetComponent<Slider>().maxValue == 1)
                    {
                        GetComponent<Slider>().maxValue = White.Ship.Health[(int)GM.ActiveStage];
                    }

                    GetComponent<Slider>().value = White.Health;

                    break;
                case Ellyments.BlackPlayerHealth:

                    if (GetComponent<Slider>().maxValue == 1)
                    {
                        GetComponent<Slider>().maxValue = Black.Ship.Health[(int)GM.ActiveStage];
                    }

                    GetComponent<Slider>().value = Black.Health;

                    break;
                case Ellyments.WhitePlayerShields:

                    if (GetComponent<Slider>().maxValue == 1)
                    {
                        GetComponent<Slider>().maxValue = White.Ship.Shield[(int)GM.ActiveStage];
                    }

                    GetComponent<Slider>().value = White.Shield;

                    break;
                case Ellyments.BlackPlayerShields:

                    if (GetComponent<Slider>().maxValue == 1)
                    {
                        GetComponent<Slider>().maxValue = Black.Ship.Shield[(int)GM.ActiveStage];
                    }

                    GetComponent<Slider>().value = Black.Shield;

                    break;
                case Ellyments.ShipHealthNumberValue:

                    GetComponent<Text>().text = Ship.Health[0].ToString();

                    break;
                case Ellyments.ShipShieldNumberValue:

                    GetComponent<Text>().text = Ship.Shield[0].ToString();

                    break;
                case Ellyments.Player1ShipSelection:

                    ChangeActiveShipSelection(Joysticks.White);

                    break;
                case Ellyments.Player2ShipSelection:

                    ChangeActiveShipSelection(Joysticks.Black);

                    break;
                case Ellyments.PlayerSpeedSliderValue:

                    GetComponent<Slider>().value = Ship.ShipSpd;

                    break;
                case Ellyments.Player1Score:

                    GetComponent<Text>().text = "Score: " + SaveManager.LoadOperationStarshine().Player1Score.ToString();

                    break;
                case Ellyments.Player2Score:

                    GetComponent<Text>().text = "Score: " + SaveManager.LoadOperationStarshine().Player2Score.ToString();

                    break;
                case Ellyments.Player1AndPlayer2Score:

                    GetComponent<Text>().text = "Final Score: " + (SaveManager.LoadOperationStarshine().Player1Score + SaveManager.LoadOperationStarshine().Player2Score).ToString();

                    break;
                case Ellyments.LeaderboardP1Value:

                    if (UIUNeedsUpdating)
                    {

                        if (Player1Name.InputtedValue != "")
                        {
                            GetComponent<Text>().text = Player1Name.InputtedValue + "\n" + ConvertToShipName(Data.LastPlayer1ShipSelection) + "\n" + "Score: " + Data.Player1Score;
                        }
                        else
                        {
                            GetComponent<Text>().text = "Player 1" + "\n" + ConvertToShipName(Data.LastPlayer1ShipSelection) + "\n" + "Score: " + Data.Player1Score;
                        }

                        UIUNeedsUpdating = false;
                    }

                    break;
                case Ellyments.LeaderboardP2Value:

                    if (UIUNeedsUpdating)
                    {


                        if (Player2Name.InputtedValue != "")
                        {
                            GetComponent<Text>().text = Player2Name.InputtedValue + "\n" + ConvertToShipName(Data.LastPlayer2ShipSelection) + "\n" + "Score: " + Data.Player2Score;
                        }
                        else
                        {
                            GetComponent<Text>().text = "Player 2" + "\n" + ConvertToShipName(Data.LastPlayer2ShipSelection) + "\n" + "Score: " + Data.Player2Score;
                        }

                        UIUNeedsUpdating = false;
                    }

                    break;
                case Ellyments.PlayerMainWeaponIcon:
                    break;
                case Ellyments.PlayerAltWeaponIcon:
                    break;
                case Ellyments.Player1Controls:
                    break;
                case Ellyments.Player2Controls:
                    break;
                default:
                    break;
            }
        }



        void ChangeActiveShipSelection(Joysticks Player)
        {
            switch (Player)
            {
                case Joysticks.White:

                    for (int i = 0; i < ShipSelectionOptions.Count; i++)
                    {
                        if (i == SS.Player1Pos)
                        {
                            if (!ShipSelectionOptions[i].activeInHierarchy)
                            {
                                ShipSelectionOptions[i].SetActive(true);
                            }
                        }
                        else
                        {
                            if (ShipSelectionOptions[i].activeInHierarchy)
                            {
                                ShipSelectionOptions[i].SetActive(false);
                            }
                        }
                    }

                    break;
                case Joysticks.Black:

                    for (int i = 0; i < ShipSelectionOptions.Count; i++)
                    {
                        if (i == SS.Player2Pos)
                        {
                            if (!ShipSelectionOptions[i].activeInHierarchy)
                            {
                                ShipSelectionOptions[i].SetActive(true);
                            }
                        }
                        else
                        {
                            if (ShipSelectionOptions[i].activeInHierarchy)
                            {
                                ShipSelectionOptions[i].SetActive(false);
                            }
                        }
                    }

                    break;
            }
        }

        string ConvertToShipName(int Value)
        {
            switch (Value)
            {
                case 0:
                    return "Aether Alpha";
                case 1:
                    return "Aether Attack";
                case 2:
                    return "Scarlet Interceptor";
                case 3:
                    return "Scarlet One";
                case 4:
                    return "Unity Gunship";
                default:
                    return "";
            }
        }
    }
}