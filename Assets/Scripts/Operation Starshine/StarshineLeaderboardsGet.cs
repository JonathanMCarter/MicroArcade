using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.Saving;

namespace CarterGames.Starshine
{
    public class StarshineLeaderboardsGet : MonoBehaviour
    {
        public List<StarshineLeaderboardData> ZeData;
        public bool GetData;
        public bool GetAll;

        void Start()
        {
            if ((GetData) && (!GetAll))
            {
                StartCoroutine(GetTopFive());
                GetData = false;
            }
            else
            {
                StartCoroutine(GetAllEntries());
                GetData = false;
            }
        }


        IEnumerator GetTopFive()
        {
            List<StarshineLeaderboardData> ListData = new List<StarshineLeaderboardData>(5);

            List<string> ReceivedPlayer1Name = new List<string>();
            List<string> ReceivedPlayer2Name = new List<string>();
            List<string> ReceivedPlayer1ShipName = new List<string>();
            List<string> ReceivedPlayer2ShipName = new List<string>();
            List<string> ReceivedPlayer1Score = new List<string>();
            List<string> ReceivedPlayer2Score = new List<string>();
            List<string> ReceivedTotalScore = new List<string>();
            List<string> ReceivedPlatform = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "getscorestarshinetopfive.php?");

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

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }


        IEnumerator GetAllEntries()
        {
            List<StarshineLeaderboardData> ListData = new List<StarshineLeaderboardData>(5);

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

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }
    }
}