using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Arcade;

namespace Pinball
{
    public class GameOverScreen : MonoBehaviour
    {
        // All UI elements used in the health UI game over screen
        [Header("All Health Game Type UI Elly")]
        public List<GameObject> HealthGameUI;

        // All UI elements used in the set score UI game over screen
        [Header("All Score Game Type UI Elly")]
        public List<GameObject> StandardScoreUI;

        public int Hovered;
        public int MaxSelections;

        public List<GameObject> Selections;

        public bool IsAbleToSwitch = true;
        public bool SelectionMade;

        public int SelectionInt;

        public GameObject SelectionPopupMenu;
        public List<Text> ReplayOptions;

        public GameManager.GameTypes NewMode;
        public Text HowManyText;
        public Text HowManyNumberText;

        bool DefaultNumberValue;
        GameManager GM;


        private void Start()
        {
            DisableHealthUI();
            GM = FindObjectOfType<GameManager>();
        }

        private void OnEnable()
        {
            SelectionPopupMenu.SetActive(false);
        }


        private void Update()
        {
            if (!SelectionPopupMenu.activeInHierarchy)
            {
                UpdateDisplay();

                if ((ArcadeControls.JoystickLeft(Joysticks.White)) && (IsAbleToSwitch))
                {
                    if (Hovered == 0)
                    {
                        Hovered = (MaxSelections);
                    }
                    else
                    {
                        --Hovered;
                    }

                    UpdateDisplay();
                    StartCoroutine(DelayToChange());
                }

                if ((ArcadeControls.JoystickRight(Joysticks.White)) && (IsAbleToSwitch))
                {
                    if (Hovered == (MaxSelections))
                    {
                        Hovered = 0;
                    }
                    else
                    {
                        ++Hovered;
                    }

                    UpdateDisplay();
                    StartCoroutine(DelayToChange());
                }

                if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                {
                    RunSelection();
                }

                if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                {
                    SelectionPopupMenu.SetActive(false);
                }
            }
            else
            {
                // Replay with different (Open menu)

                if (!SelectionMade)
                {
                    UpdateTextDisplayOnPopup();

                    if ((ArcadeControls.JoystickDown(Joysticks.White)) && (IsAbleToSwitch))
                    {
                        if (Hovered == (MaxSelections))
                        {
                            Hovered = 0;
                        }
                        else
                        {
                            ++Hovered;
                        }

                        UpdateTextDisplayOnPopup();
                        StartCoroutine(DelayToChange());
                    }

                    if ((ArcadeControls.JoystickUp(Joysticks.White)) && (IsAbleToSwitch))
                    {
                        if (Hovered == 0)
                        {
                            Hovered = (MaxSelections);
                        }
                        else
                        {
                            --Hovered;
                        }

                        UpdateTextDisplayOnPopup();
                        StartCoroutine(DelayToChange());
                    }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                    {
                        NewMode = (GameManager.GameTypes)(Hovered + 1);
                        SelectionMade = true;
                    }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                    {
                        SelectionPopupMenu.SetActive(false);
                    }

                    HowManyText.enabled = false;
                    HowManyNumberText.enabled = false;
                    DefaultNumberValue = false;
                }

                // lives, timer or score selected....
                else
                {
                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B1))
                    {
                        ++SelectionInt;
                    }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B2))
                    {
                        --SelectionInt;
                    }


                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                    {
                        SelectionMade = false;
                    }

                    HowManyText.enabled = true;
                    HowManyNumberText.enabled = true;

                    // Update text & diaply of text while it is still been changed


                    switch (NewMode)
                    {
                        case GameManager.GameTypes.Lives:

                            if (!DefaultNumberValue)
                            {
                                SelectionInt = 3;
                                DefaultNumberValue = true;
                            }
                            

                            HowManyText.text = "How Many Lives?";

                            HowManyNumberText.text = SelectionInt.ToString();

                            break;
                        case GameManager.GameTypes.Timer:

                            if (!DefaultNumberValue)
                            {
                                SelectionInt = 60;
                                DefaultNumberValue = true;
                            }

                            HowManyText.text = "How Long?";

                            string Mins = (SelectionInt / 60).ToString("00");
                            string Sec = (SelectionInt % 60).ToString("00");

                            HowManyNumberText.text = Mins + ":" + Sec;

                            break;
                        case GameManager.GameTypes.SetScore:

                            if (!DefaultNumberValue)
                            {
                                SelectionInt = 1000;
                                DefaultNumberValue = true;
                            }

                            HowManyText.text = "Target Score?";

                            HowManyNumberText.text = SelectionInt.ToString();

                            break;
                        default:
                            break;
                    }



                    // if confirm pressed - send info and reload scene
                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                    {
                        switch (NewMode)
                        {
                            case GameManager.GameTypes.Lives:
                                PlayerPrefs.SetFloat("Pinball-Lives", SelectionInt);
                                SelectionInt = 0;
                                SelectionMade = false;
                                SelectionPopupMenu.SetActive(false);
                                PlayerPrefs.SetInt("Pinball-Gametype", (int)GameManager.GameTypes.Lives);
                                SceneManager.LoadSceneAsync("BallGame");
                                break;
                            case GameManager.GameTypes.Timer:
                                PlayerPrefs.SetFloat("Pinball-Timer", SelectionInt);
                                SelectionInt = 0;
                                SelectionMade = false;
                                SelectionPopupMenu.SetActive(false);
                                PlayerPrefs.SetInt("Pinball-Gametype", (int)GameManager.GameTypes.Timer);
                                SceneManager.LoadSceneAsync("BallGame");
                                break;
                            case GameManager.GameTypes.SetScore:
                                PlayerPrefs.SetFloat("Pinball-SetScore", SelectionInt);
                                SelectionInt = 0;
                                SelectionMade = false;
                                SelectionPopupMenu.SetActive(false);
                                PlayerPrefs.SetInt("Pinball-Gametype", (int)GameManager.GameTypes.SetScore);
                                SceneManager.LoadSceneAsync("BallGame");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }


        IEnumerator DelayToChange()
        {
            IsAbleToSwitch = false;
            yield return new WaitForSeconds(.25f);
            IsAbleToSwitch = true;
        }


        void UpdateDisplay()
        {
            switch (Hovered)
            {
                case 0:
                    Selections[0].GetComponent<Image>().color = Color.yellow;
                    Selections[1].GetComponent<Image>().color = Color.white;
                    Selections[2].GetComponent<Image>().color = Color.white;
                    break;
                case 1:
                    Selections[1].GetComponent<Image>().color = Color.yellow;
                    Selections[0].GetComponent<Image>().color = Color.white;
                    Selections[2].GetComponent<Image>().color = Color.white;
                    break;
                case 2:
                    Selections[2].GetComponent<Image>().color = Color.yellow;
                    Selections[0].GetComponent<Image>().color = Color.white;
                    Selections[1].GetComponent<Image>().color = Color.white;
                    break;
                default:
                    break;
            }
        }


        void UpdateTextDisplayOnPopup()
        {
            switch (Hovered)
            {
                case 0:
                    ReplayOptions[0].color = Color.yellow;
                    ReplayOptions[1].color = Color.white;
                    ReplayOptions[2].color = Color.white;
                    break;
                case 1:
                    ReplayOptions[1].color = Color.yellow;
                    ReplayOptions[0].color = Color.white;
                    ReplayOptions[2].color = Color.white;
                    break;
                case 2:
                    ReplayOptions[2].color = Color.yellow;
                    ReplayOptions[0].color = Color.white;
                    ReplayOptions[1].color = Color.white;
                    break;
                default:
                    break;
            }
        }


        void RunSelection()
        {
            IsAbleToSwitch = true;

            switch (Hovered)
            {
                case 0:
                    SceneManager.LoadSceneAsync("BallGame");
                    break;
                case 1:
                    SelectionMenu();
                    break;
                case 2:
                    SceneManager.LoadSceneAsync("Menu");
                    break;
                default:
                    break;
            }
        }



        void SelectionMenu()
        {
            SelectionPopupMenu.SetActive(true);
            Hovered = 0;
        }



        /// <summary>
        /// Enables the health ui elements
        /// </summary>
        public void EnableHealthUI()
        {
            for (int i = 0; i < HealthGameUI.Count; i++)
            {
                if (!HealthGameUI[i].activeInHierarchy)
                {
                    HealthGameUI[i].SetActive(true);
                }
            }
        }

        /// <summary>
        /// enables the standard score ui elements
        /// </summary>
        public void EnableStandardScoreUI()
        {
            for (int i = 0; i < StandardScoreUI.Count; i++)
            {
                if (!StandardScoreUI[i].activeInHierarchy)
                {
                    StandardScoreUI[i].SetActive(true);
                }
            }
        }

        /// <summary>
        /// disables the health ui elements
        /// </summary>
        public void DisableHealthUI()
        {
            for (int i = 0; i < HealthGameUI.Count; i++)
            {
                if (HealthGameUI[i].activeInHierarchy)
                {
                    HealthGameUI[i].SetActive(false);
                }
            }
        }

        /// <summary>
        /// disables the standard score ui elements
        /// </summary>
        public void DisableStandardScoreUI()
        {
            for (int i = 0; i < StandardScoreUI.Count; i++)
            {
                if (StandardScoreUI[i].activeInHierarchy)
                {
                    StandardScoreUI[i].SetActive(false);
                }
            }
        }
    }
}