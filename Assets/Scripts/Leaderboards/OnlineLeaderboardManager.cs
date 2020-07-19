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
        /// <param name="Data">Data to be sent</param>
        public static IEnumerator Send_UltimatePinball_Data_Online(UltimatePinballLeaderboardData Data)
        {
            // Local Leaderboard Save...
            SaveManager.SaveUltimatePinballToLocal(Data);

            // Online Leaderboard Save...
            WWWForm Form = new WWWForm();

            Form.AddField("Name", Data.PlayerName);
            Form.AddField("Score", Data.PlayerScore);
            Form.AddField("Platform", Data.PlayerPlatform);
            Form.AddField("Gamemode", Data.PlayerGamemode);
            Form.AddField("Date", System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "addscorepinball.php?", Form);
            yield return W.SendWebRequest();
        }


        /// <summary>
        /// Sends the inputted data to the operation starshine leaderboard
        /// </summary>
        /// <param name="Data">Data to be sent</param>
        public static IEnumerator Send_OPSS_Data_Online(StarshineLeaderboardData Data)
        {
            // Local Leaderboard Save...
            SaveManager.SaveOperationStarshineToLocal(Data);


            // Online Leaderboard Save...
            WWWForm Form = new WWWForm();

            Form.AddField("Player1Name", Data.Player1Name);
            Form.AddField("Player2Name", Data.Player2Name);
            Form.AddField("Player1Ship", Data.Player1ShipName);
            Form.AddField("Player2Ship", Data.Player2ShipName);
            Form.AddField("Player1Score", Data.Player1Score);
            Form.AddField("Player2Score", Data.Player2Score);
            Form.AddField("TotalScore", (Data.Player1Score + Data.Player2Score));
            Form.AddField("Platform", Data.Platform);
            Form.AddField("Date", System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post(SaveManager.LoadOnlineBoardPath().onlineLeaderboardsBasePath + "addscorestarshine.php?", Form);
            yield return W.SendWebRequest();
        }
    }

}
