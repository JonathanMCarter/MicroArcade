using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CarterGames.Arcade.Saving;

namespace CarterGames.Arcade.Leaderboard
{
    public static class OnlineLeaderboardManager
    {
        /// <summary>
        /// Sends the inputted data to the ultimate pinball leaderboard
        /// </summary>
        /// <param name="_data">data to pass through</param>
        public static IEnumerator Send_UltimatePinball_Data_Online(UltimatePinballLeaderboardData _data)
        {
            // Local Leaderboard Save...
            SaveManager.SaveUltimatePinballToLocal(_data);

            // Online Leaderboard Save...
            WWWForm Form = new WWWForm();

            Form.AddField("Name", _data.PlayerName);
            Form.AddField("Score", _data.PlayerScore);
            Form.AddField("Date", System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "addscorestandard.php?", Form);
            yield return W.SendWebRequest();
        }


        /// <summary>
        /// Sends the inputted data to the operation starshine leaderboard
        /// </summary>
        /// <param name="_data">data to pass through</param>
        public static IEnumerator Send_OPSS_Data_Online(StarshineLeaderboardData _data)
        {
            // Local Leaderboard Save...
            SaveManager.SaveOperationStarshineToLocal(_data);

            // Online Leaderboard Save...
            WWWForm Form = new WWWForm();

            Form.AddField("Player1Name", _data.Player1Name);
            Form.AddField("Player2Name", _data.Player2Name);
            Form.AddField("Player1Ship", _data.Player1ShipName);
            Form.AddField("Player2Ship", _data.Player2ShipName);
            Form.AddField("Player1Score", _data.Player1Score);
            Form.AddField("Player2Score", _data.Player2Score);
            Form.AddField("TotalScore", (_data.Player1Score + _data.Player2Score));
            Form.AddField("Date", System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "addscorestarshine.php?", Form);
            yield return W.SendWebRequest();
        }


        /// <summary>
        /// Sends the inputted data to the cwis leaderboard
        /// </summary>
        /// <param name="_data">data to pass through</param>
        /// <returns></returns>
        private static IEnumerator Send_To_CWIS(CWIS.LeaderboardData _data)
        {
            // Save to local leaderboard


            // Online Leaderboard
            WWWForm Form = new WWWForm();

            Form.AddField("Name", _data.name);
            Form.AddField("Score", _data.score.ToString());
            Form.AddField("Date", System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "addscorestandard.php?", Form);
            yield return W.SendWebRequest();
        }
    }
}
