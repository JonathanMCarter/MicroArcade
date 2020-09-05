using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Starshine;

namespace CarterGames.Arcade.Leaderboard
{
    public class LeaderboardUIStarshine : MonoBehaviour
    {
        private StarshineLeaderboardsGet leaderboardData;

        public List<GameObject> leaderboardDisplayRows;
        public bool shouldUpdateLeaderboard;
        public bool isMenuLeaderboard;
        public GameObject menuBoardRowPrefab;


        void Start()
        {
            leaderboardData = GetComponent<StarshineLeaderboardsGet>();
            shouldUpdateLeaderboard = true;
        }


        void Update()
        {
            if (shouldUpdateLeaderboard && !isMenuLeaderboard)
            {
                for (int i = 0; i < leaderboardDisplayRows.Count; i++)
                {
                    leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[0].text = "#" + (i+1);
                    leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[1].text = leaderboardData.ZeData[i].Player1Name.Substring(0, 1) + leaderboardData.ZeData[i].Player1Name.Substring(1).ToLower() + " & " + leaderboardData.ZeData[i].Player2Name.Substring(0, 1) + leaderboardData.ZeData[i].Player2Name.Substring(1).ToLower();
                    leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[2].text = leaderboardData.ZeData[i].Player1ShipName.Substring(0, 1) + leaderboardData.ZeData[i].Player1ShipName.Substring(1).ToLower() + " & " + leaderboardData.ZeData[i].Player2ShipName.Substring(0, 1) + leaderboardData.ZeData[i].Player2ShipName.Substring(1).ToLower();
                    leaderboardDisplayRows[i].GetComponentsInChildren<Text>()[3].text = ((leaderboardData.ZeData[i].Player1Score) + (leaderboardData.ZeData[i].Player2Score)).ToString();
                }

                shouldUpdateLeaderboard = false;
            }
            else if (shouldUpdateLeaderboard && isMenuLeaderboard && transform.childCount == 0)
            {
                for (int i = 0; i < leaderboardData.ZeData.Count; i++)
                {
                    GameObject Go = Instantiate(menuBoardRowPrefab, transform);
                    Go.GetComponentsInChildren<Text>()[0].text = "#" + (i + 1);
                    Go.GetComponentsInChildren<Text>()[1].text = leaderboardData.ZeData[i].Player1Name.Substring(0, 1) + leaderboardData.ZeData[i].Player1Name.Substring(1).ToLower();
                    Go.GetComponentsInChildren<Text>()[2].text = leaderboardData.ZeData[i].Player1Score.ToString("#,###,###,###");
                    Go.GetComponentsInChildren<Text>()[3].text = leaderboardData.ZeData[i].Player2Name.Substring(0, 1) + leaderboardData.ZeData[i].Player2Name.Substring(1).ToLower();
                    Go.GetComponentsInChildren<Text>()[4].text = leaderboardData.ZeData[i].Player2Score.ToString("#,###,###,###");
                    Go.GetComponentsInChildren<Text>()[5].text = (leaderboardData.ZeData[i].Player1Score + leaderboardData.ZeData[i].Player2Score).ToString("#,###,###,###");
                }

                shouldUpdateLeaderboard = false;
            }
        }


        public void UpdateZeBoard()
        {
            shouldUpdateLeaderboard = true;
        }
    }
}