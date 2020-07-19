using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;

namespace CarterGames.QuackingTime
{
    public class RoundSelectControl : InputSettings
    {
        [Header(" ----- { Rounds Select Control } -----")]
        public int Selection;
        public int MaxSelection = 6;

        public List<Image> SelectionBorders;
        public List<GameObject> Customise;

        private bool IsCoR;
        public AudioManager AM;

        public SceneChanger SC;
        public QuackingTimeRootMenu rootMenu;


        private void Start()
        {
            Selection = 0;
            UpdateDisplay();
        }


       protected override void Update()
       {
            base.Update();

            UpdateDisplay();

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                    {
                        SetNumberofRounds(Selection);
                        AM.Play("Menu-Confirm", .25f);
                        OpenCustomMenu();
                    }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                    {
                        AM.Play("Confirm", .25f);
                        BackToRootMenu();
                    }

                    if (ArcadeControls.JoystickLeft(Joysticks.White) && !IsCoR) { StartCoroutine(RoundsSelect(-1)); }
                    if (ArcadeControls.JoystickRight(Joysticks.White) && !IsCoR) { StartCoroutine(RoundsSelect(1)); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                    {
                        SetNumberofRounds(Selection);
                        AM.Play("Menu-Confirm", .25f);
                        OpenCustomMenu();
                    }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B))
                    {
                        AM.Play("Confirm", .25f);
                        BackToRootMenu();
                    }

                    if (ControllerControls.ControllerLeft(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(-1)); }
                    if (ControllerControls.ControllerRight(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(1)); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        SetNumberofRounds(Selection);
                        AM.Play("Menu-Confirm", .25f);
                        OpenCustomMenu();
                    }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        AM.Play("Confirm", .25f);
                        BackToRootMenu();
                    }

                    if (KeyboardControls.KeyboardLeft(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(-1)); }
                    if (KeyboardControls.KeyboardRight(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(1)); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        SetNumberofRounds(Selection);
                        AM.Play("Menu-Confirm", .25f);
                        OpenCustomMenu();
                    }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        AM.Play("Confirm", .25f);
                        BackToRootMenu();
                    }

                    if (KeyboardControls.KeyboardLeft(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(-1)); }
                    if (KeyboardControls.KeyboardRight(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(1)); }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                    {
                        SetNumberofRounds(Selection);
                        AM.Play("Menu-Confirm", .25f);
                        OpenCustomMenu();
                    }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B))
                    {
                        AM.Play("Confirm", .25f);
                        BackToRootMenu();
                    }

                    if (ControllerControls.ControllerLeft(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(-1)); }
                    if (ControllerControls.ControllerRight(Players.P1) && !IsCoR) { StartCoroutine(RoundsSelect(1)); }

                    break;
                default:
                    break;
            }

        }

        IEnumerator RoundsSelect(int Dir)
        {
            IsCoR = true;

            if (Dir == -1)
            {
                if (Selection > 0) { Selection--; }
                else { Selection = MaxSelection; }
            }
            else
            {
                if (Selection < MaxSelection) { Selection++; }
                else { Selection = 0; }
            }

            AM.Play("Menu_Click", .25f);

            yield return new WaitForSeconds(.25f);
            IsCoR = false;
        }


        void UpdateDisplay()
        {
            for (int i = 0; i < SelectionBorders.Count; i++)
            {
                if (Selection == i)
                {
                    SelectionBorders[i].enabled = true;
                }
                else
                {
                    if (SelectionBorders[i].enabled)
                    {
                        SelectionBorders[i].enabled = false;
                    }
                }
            }
        }


        public void SetNumberofRounds(int Ply)
        {
            PlayerPrefs.SetInt("Rounds", Ply + 1);
        }


        public void OpenCustomMenu()
        {
            SC.OpenDuckSelect();
        }

        private void BackToRootMenu()
        {
            SC.MoveToRounds = false;
            SC.MoveToDucks = false;
            rootMenu.isScriptEnabled = true;
            enabled = false;
        }
    }
}