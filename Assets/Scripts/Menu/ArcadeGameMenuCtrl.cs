using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.Saving;
using CarterGames.Arcade.UserInput;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class ArcadeGameMenuCtrl : MonoBehaviour
    {
        [Header("Controls Script")]
        public MenuControls controls;

        [Header("Menu Data")]
        [SerializeField] private GameMenuData[] data;
        [SerializeField] private GameMenuData activeData;

        [Header("Menu Fields")]
        [SerializeField] private Text gameTitle;
        [SerializeField] private Text gameDesc;
        [SerializeField] private Image[] supportedControllers;
        [SerializeField] private GameObject[] topThreeScores;

        private Color32 activeCol = new Color32(90, 200, 130, 255);
        private Color32 inactiveCol = new Color32(200, 95, 90, 255);



        private void Start()
        {
            controls = GetComponent<MenuControls>();


            // Sets the game data for the menu to the one selected
            activeData = data[PlayerPrefs.GetInt("GameSel")];

            // sets the display info up
            gameTitle.text = activeData.GameTitle;
            gameDesc.text = activeData.GameDesc;


            if (activeData.supportedControls[0]) { supportedControllers[0].color = activeCol; }
            else { supportedControllers[0].color = inactiveCol; }
            if (activeData.supportedControls[1]) { supportedControllers[1].color = activeCol; }
            else { supportedControllers[1].color = inactiveCol; }
            if (activeData.supportedControls[2]) { supportedControllers[2].color = activeCol; }
            else { supportedControllers[2].color = inactiveCol; }




            if (activeData.hasLeaderboard)
            {
                if (activeData.GameTitle.Contains("Pinball"))
                {
                    StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives());
                }
                else if (activeData.GameTitle.Contains("Starshine"))
                {
                    StartCoroutine(Call_Starshine_Online());
                }
            }
        }



        private IEnumerator Call_Ultimate_Pinball_Data_Online_Lives()
        {
            List<UltimatePinballLeaderboardData> listData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getpinballlivestop.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                for (int i = 0; i < 12; i++)
                {
                    if (i % 4 == 0)
                    {
                        ReceivedPlayerName.Add(Values[i]);
                    }
                    else if (i % 4 == 1)
                    {
                        ReceivedPlayerScore.Add(Values[i]);
                    }
                    else if (i % 4 == 2)
                    {
                        ReceivedPlayerPlatform.Add(Values[i]);
                    }
                    else if (i % 4 == 3)
                    {
                        ReceivedPlayerGamemode.Add(Values[i]);
                    }
                    else
                    {
                        Debug.LogError("Value to added to any list!");
                    }
                }


                for (int i = 0; i < 3; i++)
                {
                    UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                    Data.PlayerName = ReceivedPlayerName[i];
                    Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);
                    Data.PlayerPlatform = ReceivedPlayerPlatform[i];

                    listData.Add(Data);
                }


                for (int i = 0; i < 3; i++)
                {
                    topThreeScores[i].GetComponentsInChildren<Text>()[1].text = listData[i].PlayerName;
                    topThreeScores[i].GetComponentsInChildren<Text>()[2].text = listData[i].PlayerScore.ToString();
                }
            }
        }


        private IEnumerator Call_Starshine_Online()
        {
            List<StarshineLeaderboardData> listData = new List<StarshineLeaderboardData>(5);

            List<string> ReceivedPlayer1Name = new List<string>();
            List<string> ReceivedPlayer2Name = new List<string>();
            List<string> ReceivedPlayer1ShipName = new List<string>();
            List<string> ReceivedPlayer2ShipName = new List<string>();
            List<string> ReceivedPlayer1Score = new List<string>();
            List<string> ReceivedPlayer2Score = new List<string>();
            List<string> ReceivedTotalScore = new List<string>();
            List<string> ReceivedPlatform = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "getscorestarshineall.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length; i++)
                {
                    if (i % 8 == 0)
                    {
                        ReceivedPlayer1Name.Add(Values[i]);
                    }
                    else if (i % 8 == 1)
                    {
                        ReceivedPlayer2Name.Add(Values[i]);
                    }
                    else if (i % 8 == 2)
                    {
                        ReceivedPlayer1ShipName.Add(Values[i]);
                    }
                    else if (i % 8 == 3)
                    {
                        ReceivedPlayer2ShipName.Add(Values[i]);
                    }
                    else if (i % 8 == 4)
                    {
                        ReceivedPlayer1Score.Add(Values[i]);
                    }
                    else if (i % 8 == 5)
                    {
                        ReceivedPlayer2Score.Add(Values[i]);
                    }
                    else if (i % 8 == 6)
                    {
                        ReceivedTotalScore.Add(Values[i]);
                    }
                    else if (i % 8 == 7)
                    {
                        ReceivedPlatform.Add(Values[i]);
                    }
                    else
                    {
                        Debug.LogError("Value to added to any list!");
                    }
                }


                for (int i = 0; i < ReceivedPlatform.Count; i++)
                {
                    StarshineLeaderboardData Data = new StarshineLeaderboardData();

                    Data.Player1Name = ReceivedPlayer1Name[i];
                    Data.Player2Name = ReceivedPlayer2Name[i];
                    Data.Player1ShipName = ReceivedPlayer1ShipName[i];
                    Data.Player2ShipName = ReceivedPlayer2ShipName[i];
                    Data.Player1Score = int.Parse(ReceivedPlayer1Score[i]);
                    Data.Player2Score = int.Parse(ReceivedPlayer2Score[i]);
                    Data.Platform = ReceivedPlatform[i];

                    listData.Add(Data);
                }


                for (int i = 0; i < 3; i++)
                {
                    if (i < listData.Count)
                    {
                        topThreeScores[i].GetComponentsInChildren<Text>()[1].text = listData[i].Player1Name + " | " + listData[i].Player2Name;
                        topThreeScores[i].GetComponentsInChildren<Text>()[2].text = (listData[i].Player1Score + listData[i].Player2Score).ToString();
                    }
                    else
                    {
                        topThreeScores[i].GetComponentsInChildren<Text>()[1].text = "##### |  #####";
                        topThreeScores[i].GetComponentsInChildren<Text>()[2].text = "000,000,000,000";
                    }
                }
            }
        }
    }
}