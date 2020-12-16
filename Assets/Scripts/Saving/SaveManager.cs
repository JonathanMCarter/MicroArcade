using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Carter Games Namespaces
using CarterGames.Arcade.Leaderboard;
using CarterGames.Arcade.UserInput;
//using CarterGames.UltimatePinball;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Saving
{
    public static class SaveManager
    {
        /* 
         * Format Types
         * 
         * masf = Micro Arcade Save File
         * malf = Micro Arcade Leaderboard File
         *       
        */
        /// <summary>
        /// Creates all the save files with default vlaues if they do not exist on the system (helps avoid errors later on)
        /// </summary>
        public static void InitialseFiles()
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            string baseSavePath = Application.persistentDataPath;

            FileStream _stream;


            if (!Directory.Exists(baseSavePath + "/Arcade"))
            {
                Directory.CreateDirectory(baseSavePath + "/Arcade");
            }

            // Create Save Files
            // /settings.masf
            if (!File.Exists(baseSavePath + "/Arcade/settings.masf"))
            {
                _stream = new FileStream(baseSavePath + "/Arcade/settings.masf", FileMode.Create);
                ArcadeData _settingsData = new ArcadeData();
                _formatter.Serialize(_stream, _settingsData);
                _stream.Close();
            }

            // /controlconfig.masf -- is not made here as it works when you first run the project with a multi input
            //if (!File.Exists(baseSavePath + "/controlconfig.masf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/controlconfig.masf", FileMode.Create);
            //    ArcadeData _controlData = new ArcadeData();
            //    _formatter.Serialize(_stream, _controlData);
            //    _stream.Close();
            //}

            // /arcadeonline.masf
            if (!File.Exists(baseSavePath + "/Arcade/arcadeonline.masf"))
            {
                _stream = new FileStream(baseSavePath + "/Arcade/arcadeonline.masf", FileMode.Create);
                ArcadeOnlinePaths _onlineData = new ArcadeOnlinePaths();
                _formatter.Serialize(_stream, _onlineData);
                _stream.Close();
            }



            /// =========================================================================================================================
            /// =========================================================================================================================
            /// 
            ///         Ultimeate Pinball File Creation
            /// 
            /// =========================================================================================================================
            /// =========================================================================================================================

            //if (!Directory.Exists(baseSavePath + "/Games"))
            //{
            //    Directory.CreateDirectory(baseSavePath + "/Games");
            //}

            //if (!Directory.Exists(baseSavePath + "/Games/Ultimate Pinball"))
            //{
            //    Directory.CreateDirectory(baseSavePath + "/Games/Ultimate Pinball");
            //}

            //// /ultimatepinball.masf
            //if (!File.Exists(baseSavePath + "/Games/Ultimate Pinball/ultimatepinball.masf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/Games/Ultimate Pinball/ultimatepinball.masf", FileMode.Create);
            //    UltimatePinballData _pinballData = new UltimatePinballData();
            //    _formatter.Serialize(_stream, _pinballData);
            //    _stream.Close();
            //}

            //// /ultimatepinballsession.masf
            //if (!File.Exists(baseSavePath + "/Games/Ultimate Pinball/ultimatepinballsession.masf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/Games/Ultimate Pinball/ultimatepinballsession.masf", FileMode.Create);
            //    UltimatePinballSessionData _pinballSessionData = new UltimatePinballSessionData();
            //    _formatter.Serialize(_stream, _pinballSessionData);
            //    _stream.Close();
            //}

            //// /ultimatepinballsession.malf
            //if (!File.Exists(baseSavePath + "/Games/Ultimate Pinball/ultimatepinball.malf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/Games/Ultimate Pinball/ultimatepinball.malf", FileMode.Create);
            //    _formatter.Serialize(_stream, null);
            //    _stream.Close();
            //}


            ///// =========================================================================================================================
            ///// =========================================================================================================================
            ///// 
            /////         Operation Starshine File Creation
            ///// 
            ///// =========================================================================================================================
            ///// =========================================================================================================================

            //if (!Directory.Exists(baseSavePath + "/Games/Operation Starshine"))
            //{
            //    Directory.CreateDirectory(baseSavePath + "/Games/Operation Starshine");
            //}

            //// /operationstarshine.masf
            ////if (!File.Exists(baseSavePath + "/Games/Operation Starshine/operationstarshine.masf"))
            ////{
            ////    _stream = new FileStream(baseSavePath + "/Games/Operation Starshine/operationstarshine.masf", FileMode.Create);
            ////    OperationStarshineData _operationStarshineData = new OperationStarshineData();
            ////    _formatter.Serialize(_stream, _operationStarshineData);
            ////    _stream.Close();
            ////}

            //// /operationstarshine.malf
            //if (!File.Exists(baseSavePath + "/Games/Operation Starshine/operationstarshine.malf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/Games/Operation Starshine/operationstarshine.malf", FileMode.Create);
            //    StarshineLeaderboardData _operationStarshineLeaderboardData = new StarshineLeaderboardData();
            //    _formatter.Serialize(_stream, _operationStarshineLeaderboardData);
            //    _stream.Close();
            //}



            /// =========================================================================================================================
            /// =========================================================================================================================
            /// 
            ///         Quacking Time File Creation
            /// 
            /// =========================================================================================================================
            /// =========================================================================================================================


            //if (!Directory.Exists(baseSavePath + "/Games/Quacking Time"))
            //{
            //    Directory.CreateDirectory(baseSavePath + "/Games/Quacking Time");
            //}


            //// /quackingtime.masf
            //if (!File.Exists(baseSavePath + "/Games/Quacking Time/quackingtime.masf"))
            //{
            //    _stream = new FileStream(baseSavePath + "/Games/Quacking Time/quackingtime.masf", FileMode.Create);
            //    QuackingTimeData _quackingTimeData = new QuackingTimeData();
            //    _formatter.Serialize(_stream, _quackingTimeData);
            //    _stream.Close();
            //}



            /// =========================================================================================================================
            /// =========================================================================================================================
            /// 
            ///         C.W.I.S File Creation
            /// 
            /// =========================================================================================================================
            /// =========================================================================================================================


            //if (!Directory.Exists(baseSavePath + "/Games/C.W.I.S"))
            //{
            //    Directory.CreateDirectory(baseSavePath + "/Games/C.W.I.S");
            //}
        }


        /// <summary>
        /// Resets all the save files for every game at once xD
        /// </summary>
        public static void Reset()
        {
            string PinballDataSavePath = Application.persistentDataPath + "/ultimatepinballsession.masf";
            string PinballSavePath = Application.persistentDataPath + "/ultimatepinball.masf";
            string StarshineDataSavePath = Application.persistentDataPath + "/operationstarshine.masf";
            string QuackingDataSavePath = Application.persistentDataPath + "/quackingtime.masf";
            string ControllerConfigPath = Application.persistentDataPath + "/controlconfig.masf";
            string SettingsSavePath = Application.persistentDataPath + "/settings.masf";


            if (File.Exists(PinballDataSavePath))
            {
                File.Delete(PinballDataSavePath);
            }

            if (File.Exists(PinballSavePath))
            {
                File.Delete(PinballSavePath);
            }

            if (File.Exists(StarshineDataSavePath))
            {
                File.Delete(StarshineDataSavePath);
            }

            if (File.Exists(QuackingDataSavePath))
            {
                File.Delete(QuackingDataSavePath);
            }

            if (File.Exists(ControllerConfigPath))
            {
                File.Delete(ControllerConfigPath);
            }

            if (File.Exists(SettingsSavePath))
            {
                File.Delete(SettingsSavePath);
            }
        }


        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         General Arcade  ( Saving / Loading )
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        /// <summary>
        /// Saves Settings Data
        /// </summary>
        /// <param name="Settings">Settings Script With the data to save</param>
        //public static void SaveArcadeSettings(Menu.SettingsScript Settings)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Arcade/settings.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    ArcadeData Data = new ArcadeData(Settings);

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        /// <summary>
        /// Saves Control Scheme
        /// </summary>
        /// <param name="Scheme">The supported controller setting to save</param>
        //public static void SaveArcadeControlScheme(SupportedControllers Scheme)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Arcade/controlconfig.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    ArcadeData Data = new ArcadeData(Scheme);

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        /// <summary>
        /// Saves the online leaderboard base path which all online leaderboard php files are found...
        /// </summary>
        /// <param name="paths"></param>
        public static void SaveOnlineLeaderboardsPath(ArcadeOnlinePaths paths)
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            string SavePath = Application.persistentDataPath + "/Arcade/arcadeonline.masf";
            FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

            ArcadeOnlinePaths Data = paths;

            Formatter.Serialize(Stream, Data);
            Stream.Close();
        }


        /// <summary>
        /// Loads the Arcade settings data and returns it
        /// </summary>
        /// <returns>ArcadeData with Settings</returns>
        public static ArcadeData LoadArcadeSettings()
        {
            string SavePath = Application.persistentDataPath + "/Arcade/settings.masf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);

                ArcadeData Data = Formatter.Deserialize(Stream) as ArcadeData;

                Stream.Close();

                return Data;
            }
            else
            {
                Debug.LogError("Save file not found! (Arcade Settings - Load)");
                return null;
            }
        }


        /// <summary>
        /// Loads the Arcade control scheme
        /// </summary>
        /// <returns>ArcadeData with Control Scheme</returns>
        public static ArcadeData LoadArcadeControlScheme()
        {
            string SavePath = Application.persistentDataPath + "/Arcade/controlconfig.masf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);

                ArcadeData Data = Formatter.Deserialize(Stream) as ArcadeData;

                Stream.Close();

                return Data;
            }
            else
            {
                Debug.LogError("Save file not found! (Arcade Control Scheme - Load)");
                return null;
            }
        }


        /// <summary>
        /// Loads the online leaderboard files url
        /// </summary>
        /// <returns>The URL for the leaderboards on the Carter SceneOptions Server</returns>
        public static ArcadeOnlinePaths LoadOnlineBoardPath()
        {
            string SavePath = Application.persistentDataPath + "/Arcade/arcadeonline.masf";

            if (File.Exists(SavePath))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(SavePath, FileMode.Open);

                ArcadeOnlinePaths _data = Formatter.Deserialize(Stream) as ArcadeOnlinePaths;

                Stream.Close();

                return _data;
            }
            else
            {
                Debug.LogError("Save file not found! (Arcade Online Paths - Load)");
                return null;
            }
        }


        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         Ultimate Pinball    ( Saving / Loading )
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        ///// <summary>
        ///// Saves the ultimate pinball gamemode, so the game knows what gameode to play when the level scene is loaded...
        ///// </summary>
        ///// <param name="Gamemode">gamemode as an int value</param>
        ///// <param name="Increment">how much the value should increment when in the menu</param>
        ///// <param name="LastValue">the last value for the gamemode</param>
        //public static void SaveUltimatePinballGamemode(int Gamemode = 0, int Increment = 0, int LastValue = 0)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinball.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    UltimatePinballData Data = new UltimatePinballData();

        //    Data.LastGameTypeSelected = Gamemode;
        //    Data.LastGameTypeIncrement = Increment;
        //    Data.LastGameTypeAmountSelected = LastValue;

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Saves the last pinball game results using values
        ///// </summary>
        ///// <param name="Player1Name">Player 1 Name</param>
        ///// <param name="Player1Score">Player 1 Final Score</param>
        ///// <param name="Player2Name">Player 2 Name</param>
        ///// <param name="Player2Score">Player 2 Final Score</param>
        ///// <param name="Player1Health">Player 1 Health</param>
        ///// <param name="Player2Health">Player 2 Health</param>
        //public static void SaveUltimatePinballSession(string Player1Name, int Player1Score, string Player2Name, int Player2Score, int Player1Health = 0, int Player2Health = 0)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinballsession.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    UltimatePinballSessionData Data = new UltimatePinballSessionData();

        //    Data.Player1Name = Player1Name;
        //    Data.Player1Score = Player1Score;
        //    Data.Player1Health = Player1Health;
        //    Data.Player2Name = Player2Name;
        //    Data.Player2Score = Player2Score;
        //    Data.Player2Health = Player2Health;

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Saves the last pinball game resutls using player stats 
        ///// </summary>
        ///// <param name="Player1Stats">Stats for Player 1</param>
        ///// <param name="Player2Stats">Stats for Player 2</param>
        //public static void SaveUltimatePinballSession(GameManager.BG_PlayerStats Player1Stats, GameManager.BG_PlayerStats Player2Stats)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinballsession.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    UltimatePinballSessionData Data = new UltimatePinballSessionData();

        //    Data.Player1Name = Player1Stats.Name;
        //    Data.Player1Score = Player1Stats.Score;
        //    Data.Player1Health = Player1Stats.Health;
        //    Data.Player2Name = Player2Stats.Name;
        //    Data.Player2Score = Player2Stats.Score;
        //    Data.Player2Health = Player2Stats.Health;

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Saves the last pinball game results using the correct data passed through...
        ///// </summary>
        ///// <param name="Input">data to send</param>
        //public static void SaveUltimatePinballSession(UltimatePinballSessionData Input)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinballsession.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    UltimatePinballSessionData Data = new UltimatePinballSessionData();

        //    Data = Input;

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Saves the game data from the menu through to the game...
        ///// </summary>
        ///// <param name="Input">data to send</param>
        //public static void SaveUltimatePinball(UltimatePinballData Input)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinball.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    UltimatePinballData Data = new UltimatePinballData();

        //    Data = Input;

        //    Formatter.Serialize(Stream, Data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Loads the ultimate pinball game data
        ///// </summary>
        ///// <returns>UltimatePinballData with the loaded data</returns>
        //public static UltimatePinballData LoadUltimatePinball()
        //{
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinball.masf";

        //    if (File.Exists(SavePath))
        //    {
        //        BinaryFormatter Formatter = new BinaryFormatter();
        //        FileStream Stream = new FileStream(SavePath, FileMode.Open);

        //        UltimatePinballData Data = Formatter.Deserialize(Stream) as UltimatePinballData;

        //        Stream.Close();

        //        return Data;
        //    }
        //    else
        //    {
        //        Debug.LogError("Save file not found! (Ultimate Pinball - Load)");
        //        return null;
        //    }
        //}


        ///// <summary>
        ///// Loads the last session played info for use in UI
        ///// </summary>
        ///// <returns>UltimatePinballData with the loaded data</returns>
        //public static UltimatePinballSessionData LoadLastUltimatePinballSession()
        //{
        //    string SavePath = Application.persistentDataPath + "/Games/Ultimate Pinball/ultimatepinballsession.masf";

        //    if (File.Exists(SavePath))
        //    {
        //        BinaryFormatter Formatter = new BinaryFormatter();
        //        FileStream Stream = new FileStream(SavePath, FileMode.Open);

        //        UltimatePinballSessionData Data = Formatter.Deserialize(Stream) as UltimatePinballSessionData;

        //        Stream.Close();

        //        return Data;
        //    }
        //    else
        //    {
        //        Debug.LogError("Save file not found! (Ultimate Pinball Session - Load)");
        //        return null;
        //    }
        //}


        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         Operation Starshine    ( Saving / Loading )
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        ///// <summary>
        ///// Saves the last round to the local leaderboard save file for this game
        ///// </summary>
        ///// <param name="_leaderboardData">The StarshineLeaderboardData to save</param>
        //public static void SaveOperationStarshineToLocal(StarshineLeaderboardData _leaderboardData)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Operation Starshine/operationstarshine.malf";

        //    FileStream Stream = new FileStream(SavePath, FileMode.Append);

        //    Formatter.Serialize(Stream, _leaderboardData);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Save the Starshine game to the save file
        ///// </summary>
        ///// <param name="data">OperationStarshineData to save</param>
        //public static void SaveOperationStarshine(OperationStarshineData data)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Operation Starshine/operationstarshine.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    OperationStarshineData _data = data;

        //    Formatter.Serialize(Stream, _data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Saves the player stats for each round played
        ///// </summary>
        ///// <param name="data">OperationStarshineData to save</param>
        //public static void SaveStarshinePlayerStats(OperationStarshineData data)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Operation Starshine/operationstarshine.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    OperationStarshineData _data = data;

        //    Formatter.Serialize(Stream, _data);
        //    Stream.Close();
        //}


        ///// <summary>
        ///// Loads the data for operation starshine
        ///// </summary>
        ///// <returns>The Loaded OperationStarshineData</returns>
        //public static OperationStarshineData LoadOperationStarshine()
        //{
        //    string SavePath = Application.persistentDataPath + "/Games/Operation Starshine/operationstarshine.masf";

        //    if (File.Exists(SavePath))
        //    {
        //        BinaryFormatter Formatter = new BinaryFormatter();
        //        FileStream Stream = new FileStream(SavePath, FileMode.Open);

        //        OperationStarshineData Data = Formatter.Deserialize(Stream) as OperationStarshineData;

        //        Stream.Close();

        //        return Data;
        //    }
        //    else
        //    {
        //        SaveOperationStarshine(new OperationStarshineData());
        //        Debug.LogError("Save file not found! (OP:SS - Load)");
        //        return null;
        //    }
        //}


        ///// <summary>
        ///// Loads the local leaderboard file for using in a local leaderboard
        ///// </summary>
        ///// <returns>The loaded StarshineLeaderboardData</returns>
        //public static StarshineLeaderboardData LoadOperationStarshineLeaderboard()
        //{
        //    string SavePath = Application.persistentDataPath + "/Games/Operation Starshine/operationstarshine.malf";

        //    if (File.Exists(SavePath))
        //    {
        //        BinaryFormatter Formatter = new BinaryFormatter();
        //        FileStream Stream = new FileStream(SavePath, FileMode.Open);

        //        StarshineLeaderboardData Data = Formatter.Deserialize(Stream) as StarshineLeaderboardData;

        //        Stream.Close();

        //        return Data;
        //    }
        //    else
        //    {
        //        Debug.LogError("Save file not found! (OP:SS - Load)");
        //        return null;
        //    }
        //}


        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         Quacking Time  ( Saving / Loading )
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================


        /// <summary>
        /// Saves the Quacking Time game data
        /// </summary>
        /// <param name="data">QuackingTimeData to save</param>
        //public static void SaveQuackingTime(QuackingTimeData data)
        //{
        //    BinaryFormatter Formatter = new BinaryFormatter();
        //    string SavePath = Application.persistentDataPath + "/Games/Quacking Time/quackingtime.masf";
        //    FileStream Stream = new FileStream(SavePath, FileMode.OpenOrCreate);

        //    QuackingTimeData _data = data;

        //    Formatter.Serialize(Stream, _data);
        //    Stream.Close();
        //}


        /// <summary>
        /// Loads the Quacking Time game data
        /// </summary>
        /// <returns>The Loaded QuackingTimeData</returns>
        //public static QuackingTimeData LoadQuackingTime()
        //{
        //    string SavePath = Application.persistentDataPath + "/Games/Quacking Time/quackingtime.masf";

        //    if (File.Exists(SavePath))
        //    {
        //        BinaryFormatter Formatter = new BinaryFormatter();
        //        FileStream Stream = new FileStream(SavePath, FileMode.Open);

        //        QuackingTimeData _data = Formatter.Deserialize(Stream) as QuackingTimeData;

        //        Stream.Close();

        //        return _data;
        //    }
        //    else
        //    {
        //        Debug.LogError("Save file not found! (Quacking Time - Load)");
        //        return null;
        //    }
        //}


        /// =========================================================================================================================
        /// =========================================================================================================================
        /// 
        ///         C.W.I.S  ( Saving / Loading )
        /// 
        /// =========================================================================================================================
        /// =========================================================================================================================
    }
}