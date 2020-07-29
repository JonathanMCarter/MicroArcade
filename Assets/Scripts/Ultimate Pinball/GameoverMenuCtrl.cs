using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Arcade.UserInput;
using CarterGames.Arcade.Menu;
using CarterGames.Arcade.Saving;

namespace CarterGames.UltimatePinball
{
    public class GameoverMenuCtrl : MenuSystem
    {
        public List<GameObject> ButtonOptions;
        public List<GameObject> ReplayOptions;

        public bool IsReplay;
        public bool ReplayOptionSelected;

        public List<GameObject> LivesUI;
        public List<GameObject> JustScoreUI;

        UltimatePinballData ModeData;

        [SerializeField]
        internal bool ScriptEnabled;

        public CanvasGroup ReplayCanvas;
        public CanvasGroup ReplayValueCanvas;

        [SerializeField]
        bool IsCoR;

        public enum Gamemodes { None, Lives, Timer, SetScore };
        public Gamemodes SelectedGamemode;

        int Value;
        int IncrementAmount;
        int minValue;
        public Text ValueTitleText, ValueText;

        public Animator Trans;

        protected override void Start()
        {
            base.Start();
            maxPos = ButtonOptions.Count - 1;
            UpdateDisplay();
            ModeData = SaveManager.LoadUltimatePinball();
            ShowCorrectStats();
        }


        protected override void Update()
        {
            base.Update();

            if (ScriptEnabled)
            {
                // Root Menu With Buttons
                if (!IsReplay)
                {
                    Debug.Log("hi>");

                    if (Confirm())
                    {
                        switch (pos)
                        {
                            case 0:
                                Rematch();
                                break;
                            case 1:
                                ReplaySelected();
                                break;
                            case 2:
                                ReturnToMenu();
                                break;
                            default:
                                break;
                        }
                    }

                    if (ValueChanged()) { UpdateDisplay(); }

                    MoveLR();
                }
                else if ((IsReplay) && (!ReplayOptionSelected))
                {
                    MoveUD();

                    if (ValueChanged()) { UpdateReplayDisplay(); }
                    Debug.Log("hi>>");

                    if (Confirm() && !IsCoR)
                    {
                        switch (pos)
                        {
                            case 0:
                                SelectedGamemode = Gamemodes.Lives;
                                IncrementAmount = 1;
                                Value = 5;
                                minValue = Value;
                                UpdateTextValue();
                                break;
                            case 1:
                                SelectedGamemode = Gamemodes.Timer;
                                IncrementAmount = 15;
                                Value = 60;
                                minValue = Value;
                                UpdateTextValue();
                                break;
                            case 2:
                                SelectedGamemode = Gamemodes.SetScore;
                                IncrementAmount = 1000;
                                Value = 1000;
                                minValue = Value;
                                UpdateTextValue();
                                break;
                            default:
                                break;
                        }

                        ReplayOptionSelected = true;
                    }
                    else if (Return() && !IsCoR)
                    {
                        pos = 1;
                        IsReplay = false;
                        StartCoroutine(InputDelay());
                    }
                }
                else if ((IsReplay) && (ReplayOptionSelected))
                {

                    Debug.Log("hi>>>");
                    if (Confirm() && !IsCoR)
                    {
                        SaveManager.SaveUltimatePinballGamemode((int)SelectedGamemode, IncrementAmount, Value);
                        StartCoroutine(LoadReplay());
                    }
                    else if (Return() && !IsCoR)
                    {
                        ReplayOptionSelected = false;
                        StartCoroutine(InputDelay());
                    }
                }
                else
                {
                    Debug.Log("hi");
                }
            }





            if (IsReplay && ReplayCanvas.alpha != 1)
            {
                ReplayCanvas.alpha += Time.deltaTime * 2;
            }
            else if (!IsReplay && ReplayCanvas.alpha != 0)
            {
                ReplayCanvas.alpha -= Time.deltaTime * 2;
            }

            // Updates the amount needed text and revials it 
            if (IsReplay && ReplayOptionSelected && SelectedGamemode != Gamemodes.None)
            {
                if (ReplayValueCanvas.alpha != 1)
                {
                    ReplayValueCanvas.alpha += Time.deltaTime * 2;
                }

                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if ((ArcadeControls.JoystickLeft(Joysticks.White) && (!IsCoR))) 
                        {
                            if (Value != minValue)
                            {
                                Value -= IncrementAmount;
                            }
                            StartCoroutine(InputDelay()); 
                            UpdateTextValue(); 
                        }
                        if ((ArcadeControls.JoystickRight(Joysticks.White) && (!IsCoR))) 
                        { 
                            Value += IncrementAmount; 
                            StartCoroutine(InputDelay()); 
                            UpdateTextValue(); 
                        }

                        break;
                    case SupportedControllers.GamePadBoth:

                        if ((ControllerControls.ControllerLeft(Players.P1) && (!IsCoR)))
                        {
                            if (Value != minValue)
                            {
                                Value -= IncrementAmount;
                            }
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }
                        if ((ControllerControls.ControllerRight(Players.P1) && (!IsCoR)))
                        {
                            Value += IncrementAmount;
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }

                        break;
                    case SupportedControllers.KeyboardBoth:

                        if ((KeyboardControls.KeyboardLeft(Players.P1) && (!IsCoR)))
                        {
                            if (Value != minValue)
                            {
                                Value -= IncrementAmount;
                            }
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }
                        if ((KeyboardControls.KeyboardRight(Players.P1) && (!IsCoR)))
                        {
                            Value += IncrementAmount;
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }

                        break;
                    case SupportedControllers.KeyboardP1ControllerP2:

                        if ((KeyboardControls.KeyboardLeft(Players.P1) && (!IsCoR)))
                        {
                            if (Value != minValue)
                            {
                                Value -= IncrementAmount;
                            }
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }
                        if ((KeyboardControls.KeyboardRight(Players.P1) && (!IsCoR)))
                        {
                            Value += IncrementAmount;
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }

                        break;
                    case SupportedControllers.KeyboardP2ControllerP1:

                        if ((ControllerControls.ControllerLeft(Players.P1) && (!IsCoR)))
                        {
                            if (Value != minValue)
                            {
                                Value -= IncrementAmount;
                            }
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }
                        if ((ControllerControls.ControllerRight(Players.P1) && (!IsCoR)))
                        {
                            Value += IncrementAmount;
                            StartCoroutine(InputDelay());
                            UpdateTextValue();
                        }

                        break;
                    default:
                        break;
                }
            }
            else if (IsReplay && !ReplayOptionSelected && ReplayValueCanvas.alpha != 0)
            {
                ReplayValueCanvas.alpha -= Time.deltaTime * 2;
            }
        }


        void Rematch()
        {
            StartCoroutine(LoadRematch());
        }


        IEnumerator LoadRematch()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync("Ultimate-Pinball-Level");
        }


        void ReplaySelected()
        {
            IsReplay = true;
            pos = 0;
            UpdateReplayDisplay();
        }


        IEnumerator LoadReplay()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync("Ultimate-Pinball-Level");
        }


        void UpdateTextValue()
        {
            if (SelectedGamemode == Gamemodes.Timer)
            {
                string Mins = (Value / 60).ToString("00");
                string Sec = (Value % 60).ToString("00");

                ValueText.text = Mins + ":" + Sec;
            }
            else
            {
                ValueText.text = Value.ToString();
            }


            switch (SelectedGamemode)
            {
                case Gamemodes.Lives:
                    ValueTitleText.text = "How Many Lives?";
                    break;
                case Gamemodes.Timer:
                    ValueTitleText.text = "How Long?";
                    break;
                case Gamemodes.SetScore:
                    ValueTitleText.text = "Target Score?";
                    break;
            }
        }



        void ReturnToMenu()
        {
            StartCoroutine(LoadMenu());
        }


        IEnumerator LoadMenu()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync("Ultimate-Pinball-Menu");
        }


        void UpdateDisplay()
        {
            for (int i = 0; i < ButtonOptions.Count; i++)
            {
                if ((i == pos) && (!ButtonOptions[i].GetComponent<Image>().enabled))
                {
                    ButtonOptions[i].GetComponent<Image>().enabled = true;
                }
                else if ((i != pos) && (ButtonOptions[i].GetComponent<Image>().enabled))
                {
                    ButtonOptions[i].GetComponent<Image>().enabled = false;
                }
            }
        }



        void UpdateReplayDisplay()
        {
            for (int i = 0; i < ReplayOptions.Count; i++)
            {
                if ((i == pos) && (ReplayOptions[i].GetComponent<Text>().color != Color.white))
                {
                    ReplayOptions[i].GetComponent<Text>().color = Color.white;
                }
                else if ((i != pos) && (ReplayOptions[i].GetComponent<Text>().color == Color.white))
                {
                    ReplayOptions[i].GetComponent<Text>().color = Color.yellow;
                }
            }
        }


        void ShowCorrectStats()
        {
            if (ModeData.LastGameTypeSelected == 1)
            {
                for (int i = 0; i < LivesUI.Count; i++)
                {
                    if (!LivesUI[i].activeInHierarchy)
                    {
                        LivesUI[i].SetActive(true);
                        JustScoreUI[i].SetActive(false);
                    }
                }
            }
        }


        IEnumerator InputDelay()
        {
            IsCoR = true;
            yield return new WaitForSeconds(.15f);
            IsCoR = false;
        }
    }
}