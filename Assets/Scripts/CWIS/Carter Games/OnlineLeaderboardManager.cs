using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Assets.LeaderboardManager
{
    public static class OnlineLeaderboardManager
    {
        /// <summary>
        /// Sends the inputted data to the ultimate pinball leaderboard
        /// </summary>
        /// <param name="Data">Data to be sent</param>
        public static IEnumerator SendDataOnline(LeaderboardData Data)
        {
            // Online Leaderboard Save...
            WWWForm Form = new WWWForm();

            Form.AddField("Name", Data.name);
            Form.AddField("Score", Data.score.ToString());
            Form.AddField("Date", System.DateTime.Now.ToString());

            Debug.Log(Data.name + " : " + Data.score + " : " + System.DateTime.Now.ToString());

            UnityWebRequest W = UnityWebRequest.Post("https://carter.games/leaderboardfiles/addscoress9.php", Form);
            yield return W.SendWebRequest();
        }
    }
}
