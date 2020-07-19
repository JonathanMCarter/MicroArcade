using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing.Saving
{
    public static class SaveManager
    {
        private const string savePath = "/savedata.csf";


        /// <summary>
        /// Checks to see if the save file(s) exsits, if not it makes them.
        /// </summary>
        public static void Init()
        {
            string _saveFilePath = Application.persistentDataPath + savePath;

            if (!File.Exists(_saveFilePath))
            {
                BinaryFormatter _formatter = new BinaryFormatter();
                string _savePath = Application.persistentDataPath + savePath;
                FileStream _stream = new FileStream(_savePath, FileMode.Create);

                CrushingData _blankData = new CrushingData();

                _formatter.Serialize(_stream, _blankData);
                _stream.Close();
            }
        }


        /// <summary>
        /// Saves the game data
        /// </summary>
        /// <param name="data">data to save</param>
        public static void SaveGame(CrushingData data)
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            string _savePath = Application.persistentDataPath + savePath;
            FileStream _stream = new FileStream(_savePath, FileMode.Open);
            _stream.Position = 0;

            _formatter.Serialize(_stream, data);
            _stream.Close();
        }


        /// <summary>
        /// Loads the game data and return it for use
        /// </summary>
        /// <returns>The loaded save game data</returns>
        public static CrushingData LoadGame()
        {
            string _savePath = Application.persistentDataPath + savePath;

            if (File.Exists(_savePath))
            {
                BinaryFormatter _formatter = new BinaryFormatter();
                FileStream _stream = new FileStream(_savePath, FileMode.Open);

                _stream.Position = 0;

                CrushingData _dataLoaded = new CrushingData();
                _dataLoaded = _formatter.Deserialize(_stream) as CrushingData;


                _stream.Close();

                return _dataLoaded;
            }
            else
            {
                Debug.LogError("Save file not found!");
                return null;
            }
        }


        /// <summary>
        /// Deletes the current save file, used for editor only really....
        /// </summary>
        public static void DeleteSaveFile()
        {
            string _savePath = Application.persistentDataPath + savePath;

            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
            }
        }
    }
}