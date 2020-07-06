using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Arcade;
using Arcade.Leaderboard;

namespace Starshine
{
    public class StarshineLeaderboardsSend : InputSettings
    {

        bool HasDataSent;


        private bool CheckFullData(StarshineLeaderboardData dataToCheck)
        {
            if ((dataToCheck.Player1Name != null) && (dataToCheck.Player2Name != null) &&
                (dataToCheck.Player1ShipName != null) && (dataToCheck.Player2ShipName != null)
                )
            { Debug.Log("Success - Passed check");  return true; }
            else { Debug.LogWarning("Failed - Data did not pass check");  return false; }
        }


        public void SendDataToBoard(StarshineLeaderboardData Data)
        {
            if ((CheckFullData(Data)) && (!HasDataSent))
            {
                StartCoroutine(OnlineLeaderboardManager.Send_OPSS_Data_Online(Data));
                HasDataSent = true;
                Debug.Log("Data Send");
            }
            else if ((!CheckFullData(Data)) && (!HasDataSent)) { Debug.LogWarning("Data Not Send - Not all values were filled"); }
        }
    }
}