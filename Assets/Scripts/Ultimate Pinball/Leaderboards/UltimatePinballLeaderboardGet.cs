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


        private void OnDisable()
        {
            StopAllCoroutines();
        }



        protected virtual void Update()
        {
            if (GetData)
            {
                StartCoroutine(Call_Ultimate_Pinball_Data_Online_Lives());
                GetData = false;
            }
        }


        private IEnumerator Call_Ultimate_Pinball_Data_Online_Lives()
        {
            List<UltimatePinballLeaderboardData> ListData = new List<UltimatePinballLeaderboardData>(10);

            List<string> ReceivedPlayerName = new List<string>();
            List<string> ReceivedPlayerScore = new List<string>();

            UnityWebRequest Request = UnityWebRequest.Get(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "/getpinballlivestop.php?");

            yield return Request.SendWebRequest();

            if (Request.error == null)
            {
                string[] Values = Request.downloadHandler.text.Split("\r"[0]);

                // only get the top 5 entries
                for (int i = 0; i < Values.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        ReceivedPlayerName.Add(Values[i]);
                    }
                    else if (i % 2 == 1)
                    {
                        ReceivedPlayerScore.Add(Values[i]);
                    }
                    else
                    {
                        Debug.LogError("Value to added to any list!");
                    }
                }


                for (int i = 0; i < ReceivedPlayerName.Count; i++)
                {
                    UltimatePinballLeaderboardData Data = new UltimatePinballLeaderboardData();

                    Data.PlayerName = ReceivedPlayerName[i];
                    Data.PlayerScore = int.Parse(ReceivedPlayerScore[i]);

                    ListData.Add(Data);
                }

                ZeData = ListData;
            }
        }
    }
}