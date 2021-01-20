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
        // Accessability Settings
        public bool arcade_Access_IsHighContrast;
        public bool arcade_Access_IsColourBlind;
        public bool arcade_Access_IsLargeText;

        // Gameplay Settings
        public bool arcade_Gameplay_InputRebind;

        // Visual Settings
        public int[] arcade_Visuals_Resolution;
        public int arcade_Visuals_QualityLevel;

        // Audio Settings
        public float arcade_Audio_MasterVolume;
        public float arcade_Audio_SFXVolume;
        public float arcade_Audio_MusicVolume;
        public float arcade_Audio_VoiceVolume;

        // Online Settings
        public bool arcade_Online_UseOnlineFunctionality;
    }
}