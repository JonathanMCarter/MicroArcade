using CarterGames.Arcade.Saving;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;
using CarterGames.Arcade.Leaderboard;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Assets.LeaderboardManager
{
    /// <summary>
    /// (STATIC) - An Additional Leaderboard Manager with methods to load the exsisting leaderboard data and supports multiple 
    /// </summary>
    public static class ArcadeLeaderboardManager
    {
        /// <summary>
        /// (Get Ultimate Pinball Leaderboard Data)
        /// </summary>
        /// <returns>UltimatePinballData</returns>
        public static UltimatePinballLeaderboardData[] GetUltimatePinballLocal()
        {
            StringBuilder SavePath = new StringBuilder();
            SavePath.Append(Application.persistentDataPath);
            SavePath.Append("/Games/Ultimate Pinball/ultimatepinball.malf");

            FileStream _stream = new FileStream(SavePath.ToString(), FileMode.Open);

            if (File.Exists(SavePath.ToString()) && _stream.Length > 0)
            {
                BinaryFormatter Formatter = new BinaryFormatter();

                UltimatePinballLeaderboardData[] data = Formatter.Deserialize(_stream) as UltimatePinballLeaderboardData[];

                _stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Leaderboard file not found! (Ultimate Pinball Leaderboard - Load)");
                _stream.Close();
                return null;
            }
        }
    }
}