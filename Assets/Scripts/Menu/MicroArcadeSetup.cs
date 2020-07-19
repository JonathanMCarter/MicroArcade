/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.Audio;
using CarterGames.Arcade.Saving;

namespace CarterGames.Arcade.Menu
{
    public class MicroArcadeSetup : MonoBehaviour
    {
        private int setupID;
        private bool hasLoadedSettings;

        [Header("Audio Maixer for all games")]
        [Tooltip("This is used to set the Music & SFX volumes as the cabinet starts")]
        public AudioMixer cabinetMixer;

        private void Start()
        {
            setupID = FindObjectsOfType<MicroArcadeSetup>().Length;
            DontDestroyOnLoad(this);
            RemoveDuplicates();

            if (!hasLoadedSettings)
            {
                ArcadeData arcadeSettings = SaveManager.LoadArcadeSettings();

                cabinetMixer.SetFloat("MusicVolume", arcadeSettings.CabinetMusicVolume);
                cabinetMixer.SetFloat("SFXVolume", arcadeSettings.CabinetSFXVolume);

                hasLoadedSettings = true;

                SaveManager.SaveOnlineLeaderboardsPath(new ArcadeOnlinePaths());
            }
        }


        private void RemoveDuplicates()
        {
            if (setupID != 1)
            {
                Destroy(gameObject, .1f);
            }
        }
    }
}