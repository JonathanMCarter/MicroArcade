using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.Saving;

namespace CarterGames.UltimatePinball.Leaderboard
{
    public class UltimatePinballLeaderboardGet : MonoBehaviour
    {
        public List<UltimatePinballLeaderboardData> ZeData;
        public bool GetData;
        public enum DataTypes { Lives, Timer, SetScore };
        public DataTypes Type;


        protected virtual void Start()
        {
            if (GetData)
            {
                switch (Type)
                {
                    case DataTypes.Lives:
                        StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives());
                        break;
                    case DataTypes.Timer:
                        StartCoroutine(Call_Ultimate_Pinball_Data_Online_Timer());
                        break;
                    case DataTypes.SetScore:
                        StartCoroutine(Call_Ultimate_Pinball_Data_Online_SetScore());
                        break;
                }

                GetData = false;
            }
        }

        IEnumerator Call_Ultimate_Pinball_Data_Online_Lives()
        {
            List<UltimatePinballLeaderboardData> ListData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getpinballlivestop.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length; i++)
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


                for (int i = 0; i < ReceivedPlayerPlatform.Count; i++)
                {
                    UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                    Data.PlayerName = ReceivedPlayerName[i];
                    Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);
                    Data.PlayerPlatform = ReceivedPlayerPlatform[i];
                    Data.PlayerGamemode = int.Parse(ReceivedPlayerGamemode[i]);

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }


        IEnumerator Call_Ultimate_Pinball_Data_Online_Timer()
        {
            List<UltimatePinballLeaderboardData> ListData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "getpinballtimertop.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length; i++)
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


                for (int i = 0; i < ReceivedPlayerPlatform.Count; i++)
                {
                    UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                    Data.PlayerName = ReceivedPlayerName[i];
                    Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);
                    Data.PlayerPlatform = ReceivedPlayerPlatform[i];
                    Data.PlayerGamemode = int.Parse(ReceivedPlayerGamemode[i]);

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }


        IEnumerator Call_Ultimate_Pinball_Data_Online_SetScore()
        {
            List<UltimatePinballLeaderboardData> ListData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();
            List<string> ReceivedPlayerPlatform = new List<string>();
            List<string> ReceivedPlayerGamemode = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "getpinballscoretop.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length; i++)
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


                for (int i = 0; i < ReceivedPlayerPlatform.Count; i++)
                {
                    UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                    Data.PlayerName = ReceivedPlayerName[i];
                    Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);
                    Data.PlayerPlatform = ReceivedPlayerPlatform[i];
                    Data.PlayerGamemode = int.Parse(ReceivedPlayerGamemode[i]);

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }
    }
}