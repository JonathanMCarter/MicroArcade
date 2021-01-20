using UnityEngine;
using CarterGames.Arcade.Saving;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/
namespace CarterGames.Arcade.Settings
{
    public enum Settings
    {
        arcade_Access_IsHighContrast,
        arcade_Access_IsColourBlind,
        arcade_Access_IsLargeText,
        arcade_Gameplay_InputRebind,
        arcade_Visuals_Resolution,
        arcade_Visuals_QualityLevel,
        arcade_Audio_MasterVolume,
        arcade_Audio_SFXVolume,
        arcade_Audio_MusicVolume,
        arcade_Audio_VoiceVolume,
        arcade_Online_UseOnlineFunctionality,
    };

    public class GetSettings : MonoBehaviour
    {
        private ArcadeData _data;


        private void Awake()
        {
            _data = SaveManager.LoadArcadeSettings();
        }


        public bool ReadBoolValue(Settings setting)
        {
            switch (setting)
            {
                case Settings.arcade_Access_IsHighContrast:
                    return _data.arcade_Access_IsHighContrast;
                case Settings.arcade_Access_IsColourBlind:
                    return _data.arcade_Access_IsColourBlind;
                case Settings.arcade_Access_IsLargeText:
                    return _data.arcade_Access_IsLargeText;
                case Settings.arcade_Gameplay_InputRebind:
                    return _data.arcade_Gameplay_InputRebind;
                case Settings.arcade_Online_UseOnlineFunctionality:
                    return _data.arcade_Online_UseOnlineFunctionality;
                default:
                    return false;
            }
        }


        public int ReadIntValue(Settings setting)
        {
            switch (setting)
            {
                case Settings.arcade_Visuals_QualityLevel:
                    return _data.arcade_Visuals_QualityLevel;
                default:
                    return 0;
            }
        }


        public float ReadFloatValue(Settings setting)
        {
            switch (setting)
            {
                case Settings.arcade_Audio_MasterVolume:
                    return _data.arcade_Audio_MasterVolume;
                case Settings.arcade_Audio_SFXVolume:
                    return _data.arcade_Audio_SFXVolume;
                case Settings.arcade_Audio_MusicVolume:
                    return _data.arcade_Audio_MusicVolume;
                case Settings.arcade_Audio_VoiceVolume:
                    return _data.arcade_Audio_VoiceVolume;
                default:
                    return 0f;
            }
        }


        public int[] ReadIntArrayValue(Settings setting)
        {
            switch (setting)
            {
                case Settings.arcade_Visuals_Resolution:
                    return _data.arcade_Visuals_Resolution;
                default:
                    return new int[0];
            }
        }


        public void WriteValue(Settings setting, bool value)
        {
            switch (setting)
            {
                case Settings.arcade_Access_IsHighContrast:
                    _data.arcade_Access_IsHighContrast = value;
                    break;
                case Settings.arcade_Access_IsColourBlind:
                    _data.arcade_Access_IsColourBlind = value;
                    break;
                case Settings.arcade_Access_IsLargeText:
                    _data.arcade_Access_IsLargeText = value;
                    break;
                case Settings.arcade_Gameplay_InputRebind:
                    _data.arcade_Gameplay_InputRebind = value;
                    break;
                case Settings.arcade_Online_UseOnlineFunctionality:
                    _data.arcade_Online_UseOnlineFunctionality = value;
                    break;
                default:
                    break;
            }
        }


        public void WriteValue(Settings setting, int value)
        {
            switch (setting)
            {
                case Settings.arcade_Visuals_QualityLevel:
                    _data.arcade_Visuals_QualityLevel = value;
                    break;
                default:
                    break;
            }
        }


        public void WriteValue(Settings setting, float value)
        {
            switch (setting)
            {
                case Settings.arcade_Audio_MasterVolume:
                    _data.arcade_Audio_MasterVolume = value;
                    break;
                case Settings.arcade_Audio_SFXVolume:
                    _data.arcade_Audio_SFXVolume = value;
                    break;
                case Settings.arcade_Audio_MusicVolume:
                    _data.arcade_Audio_MusicVolume = value;
                    break;
                case Settings.arcade_Audio_VoiceVolume:
                    _data.arcade_Audio_VoiceVolume = value;
                    break;
                default:
                    break;
            }
        }


        public void WriteValue(Settings setting, int[] value)
        {
            switch (setting)
            {
                case Settings.arcade_Visuals_Resolution:
                    _data.arcade_Visuals_Resolution = value;
                    break;
                default:
                    break;
            }
        }


        public void SaveSettings()
        {
            SaveManager.SaveArcadeSettings(_data);
        }
    }
}