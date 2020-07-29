using CarterGames.Arcade.Leaderboard;
using System;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Saving
{
    [Serializable]
    public struct PlayerStats
    {
        public int healthLost;
        public int healthGained;
        public int shieldLost;
        public int shieldGained;
        public int shieldSwitches;
        public int mainShotsFired;
        public int altShotsFired;
        public int specialShotsFired;
        public int enemiesKilled;
        public int damageDelt;
    }

    [System.Serializable]
    public class OperationStarshineData
    {
        // Ship Selection
        public int LastPlayer1ShipSelection;
        public int LastPlayer2ShipSelection;


        // Score 
        public int Player1Score;
        public int Player2Score;


        public PlayerStats player1Stats;
        public PlayerStats player2Stats;

        // Leaderboard Data
        public StarshineLeaderboardData LastScore;


        // Saving Ship Selection
        public OperationStarshineData(Starshine.ShipSelection ShipSelection = null)
        {
            if (ShipSelection)
            {
                LastPlayer1ShipSelection = (int)ShipSelection.Player1ShipChoice;
                LastPlayer2ShipSelection = (int)ShipSelection.Player2ShipChoice;
            }
        }

        // Score Saving
        public OperationStarshineData(int Player1, int Player2)
        {
            Player1Score = Player1;
            Player2Score = Player2;
        }


        public OperationStarshineData(PlayerStats Player1Stats, PlayerStats Player2Stats)
        {
            player1Stats = Player1Stats;
            player2Stats = Player2Stats;
        }
    }
}