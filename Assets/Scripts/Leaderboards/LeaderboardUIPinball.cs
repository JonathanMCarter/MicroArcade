using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.UltimatePinball.Leaderboard
{
    public class LeaderboardUIPinball : UltimatePinballLeaderboardGet
    {
        public List<GameObject> leaderboardDisplayRows;
        public bool shouldUpdateLeaderboard;


        protected override void Start()
        {
            base.Start();
            shouldUpdateLeaderboard = true;
        }


        private void Update()
        {
            shouldUpdateLeaderboard = true;

            if (shouldUpdateLeaderboard)
            {
                for (int i = 0; i < leaderboardDisplayRows.Count; i++)
                {
                    leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[0].text = "#" + (i + 1);

                    if (ZeData.Count > i)
                    {
                        leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[1].text = ZeData[i].PlayerName.Substring(0, 1) + ZeData[i].PlayerName.Substring(1).ToLower();
                        leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[2].text = ZeData[i].PlayerScore.ToString();
                        leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[3].text = ZeData[i].PlayerPlatform;
                    }
                }

                shouldUpdateLeaderboard = false;
            }
        }
    }
}