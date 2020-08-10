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
        [SerializeField] private List<CWIS.LeaderboardData> data;

        private int CompareData(object a, object b)
        {
            CWIS.LeaderboardData c1 = (CWIS.LeaderboardData)a;
            CWIS.LeaderboardData c2 = (CWIS.LeaderboardData)b;
            return String.Compare(c2.score.ToString(), c1.score.ToString());
        }
    }
}