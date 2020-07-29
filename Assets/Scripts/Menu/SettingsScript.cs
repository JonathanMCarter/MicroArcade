using CarterGames.Arcade.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CarterGames.Arcade.UserInput;

namespace CarterGames.Arcade.Menu
{
    public class SettingsScript : MenuSystem
    {
        public enum SettingMenuOption { Info, Audio };
        public enum Stage { ChangeScreen, ChooseSetting, EditSetting, };

        public enum SettingTypes { Toggle, Slider, Dropdown, }

        [Header("----- { Settings Navigation } -----")]
        public SettingMenuOption Option;
        public Stage CurrentStage;

        public List<GameObject> Screens;
        public List<Image> Pips;

        public List<GameObject> AudioOptions;
        public List<GameObject> GameplayOptions;

        [Header("Setting Type")]
        public SettingTypes TypeToEdit;

        [Header("Pip Colours")]
        public Color32 ActiveCol;
        public Color32 InactiveCol;

        [Header("----- { Menu Transitions } -----")]
        public Animator Trans;

        public GameObject ConfirmResetText;

        public AudioMixer Mixer;
        ArcadeData Data;
        internal float MusicVolume;
        internal float SFXVolume;

        public Text SettingText;


        protected new void Start()
        {
            maxPos = Screens.Count - 1;

            lastPos = 5;

            // Update Pips
            UpdatePips();

            // Update Display
            UpdateDisplay();

            // Load Data
            Data = SaveManager.LoadArcadeSettings();

            MusicVolume = Data.CabinetMusicVolume;
            SFXVolume = Data.CabinetSFXVolume;

            AudioOptions[0].GetComponent<Slider>().value = MusicVolume;
            AudioOptions[1].GetComponent<Slider>().value = SFXVolume;

            Mixer.SetFloat("MusicVolume", MusicVolume);
            Mixer.SetFloat("SFXVolume", SFXVolume);

            inputReady = true;

            base.Start();
         }



        protected new void Update()
        {
            base.Update();


            // Movement
            if (CurrentStage == Stage.ChangeScreen)
            {
                MoveLR();
            }
            else if (CurrentStage == Stage.ChooseSetting) 
            {
                MoveUD(); 
            }



            // Pips Update
            if (CurrentStage == Stage.ChangeScreen) 
            { 
                UpdatePips(); 
                UpdateDisplay(); 
            }



            // Controls 'n' stuff
            switch (CurrentStage)
            {
                case Stage.ChangeScreen:
                    Root();
                    break;
                case Stage.ChooseSetting:
                    ChooseSetting();
                    break;
                case Stage.EditSetting:
                    EditSetting();
                    break;
                default:
                    break;
            }
        }


        void ReturnToMainMenu()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            ChangeScene("MainMenu");
        }


        void Root()
        {
            if (Confirm())
            {
                Option = (SettingMenuOption)pos;
                Screens[pos].GetComponent<CanvasGroup>().alpha = 1;
                pos = 0;
                lastPos = -1;
                CurrentStage = Stage.ChooseSetting;
            }
            else if (Return())
            {
                ReturnToMainMenu();
            }

            SettingText.text = "";
        }


        void ChooseSetting()
        {
            switch (Option)
            {
                case SettingMenuOption.Audio:

                    maxPos = AudioOptions.Count - 1;
                    UpdateAudioDisplay();

                    // Audio Controls
                    if (Confirm())
                    {
                        AudioOptions[0].GetComponent<Slider>().value = MusicVolume;
                        AudioOptions[1].GetComponent<Slider>().value = SFXVolume;

                        CurrentStage = Stage.EditSetting;
                        AudioOptions[pos].transform.parent.GetComponentInChildren<Text>().color = Color.green;

                        if (pos == 0)
                        {
                            SettingText.text = "Editing Music Volume...";
                        }
                        else
                        {
                            SettingText.text = "Editing SFX Volume...";
                        }

                        TypeToEdit = DetectSettingType(AudioOptions);
                    }

                    Pips[1].color = ActiveCol;
                    Pips[0].color = InactiveCol;

                    break;
                case SettingMenuOption.Info:

                    maxPos = GameplayOptions.Count - 1;
                    UpdateInfoDisplay();

                    Pips[0].color = ActiveCol;
                    Pips[1].color = InactiveCol;

                    if (pos == 0)
                    {
                        if (Confirm())
                        {
                            if (ControllerType == (SupportedControllers.KeyboardP1ControllerP2) || (ControllerType == SupportedControllers.KeyboardP2ControllerP1))
                            {
                                SceneManager.LoadSceneAsync("InputCheck", LoadSceneMode.Additive);
                            }
                        }
                        else if (Confirm())
                        {
                            if (ControllerType == SupportedControllers.ArcadeBoard || ControllerType == SupportedControllers.GamePadBoth || ControllerType == SupportedControllers.KeyboardBoth)
                            {
                                GameplayOptions[pos].GetComponent<Text>().color = Color.red;
                                SettingText.text = "You can't do this... your controllers are not mixed...";
                            }
                        }
                    }
                    else if (pos == 1)
                    {
                        if (Confirm())
                        {
                            SettingText.text = "Reset Save Data?";
                            GameplayOptions[pos].GetComponent<Text>().color = Color.green;
                            ConfirmResetText.SetActive(true);
                            CurrentStage = Stage.EditSetting;
                        }
                    }

                    break;
                default:
                    break;
            }

            if (Return())
            {
                Screens[(int)Option].GetComponent<CanvasGroup>().alpha = .5f;
                pos = (int)Option;

                if (pos == 0)
                {
                    Pips[0].color = ActiveCol;
                    Pips[1].color = InactiveCol;
                }
                else
                {
                    Pips[0].color = InactiveCol;
                    Pips[1].color = ActiveCol;
                }

                maxPos = Screens.Count - 1;
                UpdateDisplay();
                UpdatePips();
                AudioOptions[0].transform.parent.GetComponentInChildren<Text>().color = Color.white;
                AudioOptions[1].transform.parent.GetComponentInChildren<Text>().color = Color.white;
                GameplayOptions[0].GetComponent<Text>().color = Color.white;
                GameplayOptions[1].GetComponent<Text>().color = Color.white;

                CurrentStage = Stage.ChangeScreen;
            }
        }


        void EditSetting()
        {
            switch (Option)
            {
                case SettingMenuOption.Audio:

                    ChangeValue(AudioOptions);

                    if (Confirm())
                    {
                        MusicVolume = AudioOptions[0].GetComponent<Slider>().value;
                        SFXVolume = AudioOptions[1].GetComponent<Slider>().value;

                        if (pos == 0)
                        {
                            Mixer.SetFloat("MusicVolume", MusicVolume);
                            SettingText.text = "Music Volume Set To: " + MusicVolume.ToString("00");
                        }
                        else
                        {
                            Mixer.SetFloat("SFXVolume", SFXVolume);
                            SettingText.text = "SFX Volume Set To: " + SFXVolume.ToString("00");
                        }

                        SaveManager.SaveArcadeSettings(this);
                        CurrentStage = Stage.ChooseSetting;
                    }

                    if (Return())
                    {
                        CurrentStage = Stage.ChooseSetting;
                    }

                    break;
                case SettingMenuOption.Info:

                    if (Confirm())
                    {
                        SaveManager.Reset();
                        SaveManager.InitialseFiles();
                        SettingText.text = "Save Data Reset...";
                        ConfirmResetText.SetActive(false);
                        CurrentStage = Stage.ChooseSetting;
                    }
                    
                    if (Return())
                    {
                        SettingText.text = "No Changes Made...";
                        ConfirmResetText.SetActive(false);
                        CurrentStage = Stage.ChooseSetting;
                    }

                    break;
                default:
                    break;
            }
        }


        void UpdatePips()
        {
            if (lastPos != pos)
            {
                for (int i = 0; i < Pips.Count; i++)
                {
                    if ((i == pos) && (Pips[i].color != ActiveCol))
                    {
                        Pips[i].color = ActiveCol;
                    }
                    else if ((i != pos) && (Pips[i].color == ActiveCol))
                    {
                        Pips[i].color = InactiveCol;
                    }
                }
            }
        }


        void UpdateDisplay()
        {
            if (lastPos != pos)
            {
                for (int i = 0; i < Screens.Count; i++)
                {
                    if ((i == pos) && (Screens[i].GetComponent<CanvasGroup>().alpha != .5f))
                    {
                        Screens[i].GetComponent<CanvasGroup>().alpha = .5f;
                    }
                    else if ((i != pos) && (Screens[i].GetComponent<CanvasGroup>().alpha == .5f))
                    {
                        Screens[i].GetComponent<CanvasGroup>().alpha = 0;
                    }
                }
            }
        }


        SettingTypes DetectSettingType(List<GameObject> ToCheck)
        {
            if (ToCheck[pos].GetComponent<Toggle>())
            {
                return SettingTypes.Toggle;
            }
            else if (ToCheck[pos].GetComponent<Slider>())
            {
                return SettingTypes.Slider;
            }
            else
            {
                return SettingTypes.Dropdown;
            }
        }


        void UpdateInfoDisplay()
        {
            if (lastPos != pos)
            {
                for (int i = 0; i < GameplayOptions.Count; i++)
                {
                    if ((i == pos) && (GameplayOptions[i].GetComponent<Text>().color != Color.yellow))
                    {
                        GameplayOptions[i].GetComponent<Text>().color = Color.yellow;
                    }
                    else if ((i != pos) && (GameplayOptions[i].GetComponent<Text>().color == Color.yellow))
                    {
                        GameplayOptions[i].GetComponent<Text>().color = Color.white;
                    }
                }
            }
        }

        void UpdateAudioDisplay()
        {
            if (lastPos != pos)
            {
                for (int i = 0; i < AudioOptions.Count; i++)
                {
                    if ((i == pos) && (AudioOptions[i].transform.parent.GetComponentInChildren<Text>().color != Color.yellow))
                    {
                        AudioOptions[i].transform.parent.GetComponentInChildren<Text>().color = Color.yellow;
                    }
                    else if ((i != pos) && (AudioOptions[i].transform.parent.GetComponentInChildren<Text>().color == Color.yellow))
                    {
                        AudioOptions[i].transform.parent.GetComponentInChildren<Text>().color = Color.white;
                    }
                }
            }
        }


        void ChangeValue(List<GameObject> ToEdit)
        {
            if (GetLRDir() != 0)
            {
                ToEdit[pos].GetComponent<Slider>().value += .1f * GetLRDir();

                if (pos == 0)
                {
                    Mixer.SetFloat("MusicVolume", MusicVolume);
                }
                else
                {
                    Mixer.SetFloat("SFXVolume", SFXVolume);
                }
            }
        }
    }
}