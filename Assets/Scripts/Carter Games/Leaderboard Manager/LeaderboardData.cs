using System.Collections.Generic;
using System.Linq;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// (CLASS) The data used on in the local leaderboard system.
    /// </summary>
    [System.Serializable]
    public class LeaderboardData
    {
        /// <summary>
        /// The players name
        /// </summary>
        public string playerName;

        /// <summary>
        /// The players score
        /// </summary>
        public float playerScore;

        /// <summary>
        /// Blank constructor for the leaderboarddata class
        /// </summary>
        public LeaderboardData() { }

        /// <summary>
        /// Constructor that inputs the name and score vales for you
        /// </summary>
        /// <param name="name">name to input</param>
        /// <param name="score">score to input</param>
        public LeaderboardData(string name, float score)
        {
            playerName = name;
            playerScore = score;
        }
    }


    /// <summary>
    /// (CLASS) Class that holds a list of leaderboarddata class so it can be sorted 'n' all
    /// </summary>
    [System.Serializable]
    public class LeaderboardStore
    {
        /// <summary>
        /// A list of the leaderboarddata 
        /// </summary>
        public List<LeaderboardData> leaderboardData;

        /// <summary>
        /// Constructor, creates a new list of leaderboarddata
        /// </summary>
        public LeaderboardStore()
        {
            leaderboardData = new List<LeaderboardData>();
        }

        /// <summary>
        /// IOrderedEnumerable... Sorts the leaderboarddata by player score
        /// </summary>
        public IOrderedEnumerable<LeaderboardData> SortedLBD
        {
            get 
            {
                return leaderboardData.OrderBy(m => m.playerScore);
            }
        }
    }
}