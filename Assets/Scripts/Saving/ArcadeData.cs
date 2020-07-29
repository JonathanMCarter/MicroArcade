using CarterGames.Arcade.UserInput;
using System;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Saving
{
    [Serializable]
    public class ArcadeData
    {
        // Audio Settings
        public float CabinetMusicVolume;
        public float CabinetSFXVolume;

        // Online Settings
        public bool UseOnlineFunctionality;

        // Display Settings
        public int[] DisplayResolution;
        public bool UseFullScreen;
        public int QualityLevel;


        public SupportedControllers SetMixedInputConfig;


        public ArcadeData()
        {
            CabinetMusicVolume = -6f;
            CabinetSFXVolume = -2f;
        }


        public ArcadeData (Menu.SettingsScript Settings)
        {
            CabinetMusicVolume = Settings.MusicVolume;
            CabinetSFXVolume = Settings.SFXVolume;
        }


        public ArcadeData (SupportedControllers ControlScheme)
        {
            SetMixedInputConfig = ControlScheme;
        }
    }
}