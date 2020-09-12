using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Leaderboard
{
    /// <summary>
    /// (STATIC) - An Additional Leaderboard Manager with methods to load the exsisting leaderboard data and supports multiple games in this product.
    /// </summary>
    public static class ArcadeLeaderboardManager
    {
        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         Ultimate Pinball Methods
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        /// <summary>
        /// (STATIC) Get Ultimate Pinball Leaderboard Data
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


        /// <summary>
        /// (STATIC) Adds the inputted data to the local leaderboard.
        /// </summary>
        /// <param name="toAdd">Data to add</param>
        public static void AddToUltimatePinballLocal(UltimatePinballLeaderboardData toAdd)
        {

            string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinball.malf";

            FileStream _stream = new FileStream(SavePath, FileMode.Open);

            if (File.Exists(SavePath.ToString()) && _stream.Length > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                List<UltimatePinballLeaderboardData> data = formatter.Deserialize(_stream) as List<UltimatePinballLeaderboardData>;

                if (data.Count > 0)
                {
                    data.Add(toAdd);
                }
                else
                {
                    data = new List<UltimatePinballLeaderboardData>();
                    data.Add(toAdd);
                }

                formatter.Serialize(_stream, data);

                _stream.Close();
            }
            else
            {
                Debug.LogError("Leaderboard file not found! (Ultimate Pinball Leaderboard - Add To Local Board)");
                _stream.Close();
            }
        }



        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         Operation Starshine Methods
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        /// <summary>
        /// (STATIC) Get Ultimate Pinball Leaderboard Data
        /// </summary>
        /// <returns>UltimatePinballData</returns>
        public static StarshineLeaderboardData[] GetOperationStarshineLocal()
        {
            StringBuilder SavePath = new StringBuilder();
            SavePath.Append(Application.persistentDataPath);
            SavePath.Append("/Games/Operation Starshine/operationstarshine.malf");

            FileStream _stream = new FileStream(SavePath.ToString(), FileMode.Open);

            if (File.Exists(SavePath.ToString()) && _stream.Length > 0)
            {
                BinaryFormatter Formatter = new BinaryFormatter();

                StarshineLeaderboardData[] data = Formatter.Deserialize(_stream) as StarshineLeaderboardData[];

                _stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Leaderboard file not found! (Operation Starshine Leaderboard - Load)");
                _stream.Close();
                return null;
            }
        }


        /// <summary>
        /// (STATIC) Adds the inputted data to the local leaderboard.
        /// </summary>
        /// <param name="toAdd">Data to add</param>
        public static void AddToOperationStarshineLocal(StarshineLeaderboardData toAdd)
        {
            StringBuilder SavePath = new StringBuilder();
            SavePath.Append(Application.persistentDataPath);
            SavePath.Append("/Games/Operation Starshine/operationstarshine.malf");

            FileStream _stream = new FileStream(SavePath.ToString(), FileMode.Open);

            if (File.Exists(SavePath.ToString()) && _stream.Length > 0)
            {
                BinaryFormatter Formatter = new BinaryFormatter();

                List<StarshineLeaderboardData> data = Formatter.Deserialize(_stream) as List<StarshineLeaderboardData>;

                data.Add(toAdd);

                Formatter.Serialize(_stream, data);

                _stream.Close();
            }
            else
            {
                Debug.LogError("Leaderboard file not found! (Operation Starshine - Add To Local Board)");
                _stream.Close();
            }
        }
    }
}