using CarterGames.Arcade.Saving;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;

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
        public static UltimatePinballData GetUltimatePinballLocal()
        {
            StringBuilder SavePath = new StringBuilder();
            SavePath.Append(Application.persistentDataPath);
            SavePath.Append("/Games/Ultimate Pinball/ultimatepinball.malf");

            if (File.Exists(SavePath.ToString()))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath.ToString(), FileMode.Open);

                UltimatePinballData Data = Formatter.Deserialize(Stream) as UltimatePinballData;

                Stream.Close();

                return Data;
            }
            else
            {
                Debug.LogError("Leaderboard file not found! (Ultimate Pinball Leaderboard - Load)");
                return null;
            }
        }
    }
}