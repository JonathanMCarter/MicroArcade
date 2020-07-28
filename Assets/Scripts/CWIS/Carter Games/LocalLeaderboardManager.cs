using UnityEngine;
using System.Collections.Generic;
using System;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Assets.LeaderboardManager
{
    public class LocalLeaderboardManager : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardData> data;

        private int CompareData(object a, object b)
        {
            LeaderboardData c1 = (LeaderboardData)a;
            LeaderboardData c2 = (LeaderboardData)b;
            return String.Compare(c2.score.ToString(), c1.score.ToString());
        }
    }
}