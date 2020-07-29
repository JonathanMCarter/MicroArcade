using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Arcade.UserInput;
using CarterGames.Arcade.Saving;
using CarterGames.Assets.AudioManager;

namespace CarterGames.QuackingTime
{
    public class DuckCustomMenu : InputSettings
    {
        [Header(" ----- { Duck Custom Menu } -----")]
        public int Duck1Pos;
        public int Duck2Pos, Duck3Pos, Duck4Pos;
        public int MaxSelection = 6;

        public List<Sprite> Hats;
        public List<Image> Ready;
        public List<Image> HatSelected;

        private bool IsCoR_P1, IsCoR_P2, IscoRunningP3, IscoRunningP4;
        public bool ReadyP1, ReadyP2, ReadyP3, ReadyP4;

        private SceneChanger ChangeScene;

        private AudioManager am;
        public SceneChanger SC;
        public RoundSelectControl RSC;

        private void Start()
        {
            am = FindObjectOfType<AudioManager>();
            ChangeScene = FindObjectOfType<SceneChanger>();
        }

        protected override void Update()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    // Player 1
                    if ((ArcadeControls.JoystickLeft(Joysticks.White) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(1)); }
                    if ((ArcadeControls.JoystickRight(Joysticks.White) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(-1)); }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP1 = !ReadyP1;
                        Ready[0].enabled = ReadyP1;
                    }

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7))
                    {
                        am.Play("Confirm", .25f);
                        SC.MoveToDucks = false;
                        SC.MoveToRounds = true;
                        RSC.enabled = true;
                        this.enabled = false;
                    }

                    // Player 2
                    if ((ArcadeControls.JoystickLeft(Joysticks.Black) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(1)); }
                    if ((ArcadeControls.JoystickRight(Joysticks.Black) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(-1)); }

                    if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP2 = !ReadyP2;
                        Ready[1].enabled = ReadyP2;
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    // Player 1
                    if ((ControllerControls.ControllerLeft(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(1)); }
                    if ((ControllerControls.ControllerRight(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(-1)); }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP1 = !ReadyP1;
                        Ready[0].enabled = ReadyP1;
                    }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B))
                    {
                        am.Play("Confirm", .25f);
                        SC.MoveToDucks = false;
                        SC.MoveToRounds = true;
                        RSC.enabled = true;
                        this.enabled = false;
                    }

                    // Player 2
                    if ((ControllerControls.ControllerLeft(Players.P2) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(1)); }
                    if ((ControllerControls.ControllerRight(Players.P2) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(-1)); }

                    if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.Confirm))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP2 = !ReadyP2;
                        Ready[1].enabled = ReadyP2;
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(-1)); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP1 = !ReadyP1;
                        Ready[0].enabled = ReadyP1;
                    }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        am.Play("Confirm", .25f);
                        SC.MoveToDucks = false;
                        SC.MoveToRounds = true;
                        RSC.enabled = true;
                        this.enabled = false;
                    }

                    // Player 2
                    if ((KeyboardControls.KeyboardLeft(Players.P2) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P2) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(-1)); }

                    if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP2 = !ReadyP2;
                        Ready[1].enabled = ReadyP2;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if ((KeyboardControls.KeyboardLeft(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(-1)); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP1 = !ReadyP1;
                        Ready[0].enabled = ReadyP1;
                    }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7))
                    {
                        am.Play("Confirm", .25f);
                        SC.MoveToDucks = false;
                        SC.MoveToRounds = true;
                        RSC.enabled = true;
                        this.enabled = false;
                    }

                    if ((ControllerControls.ControllerLeft(Players.P1) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(1)); }
                    if ((ControllerControls.ControllerRight(Players.P1) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(-1)); }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP2 = !ReadyP2;
                        Ready[1].enabled = ReadyP2;
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if ((ControllerControls.ControllerLeft(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(1)); }
                    if ((ControllerControls.ControllerRight(Players.P1) && (!IsCoR_P1) && (!ReadyP1))) { StartCoroutine(ToggleSelP1(-1)); }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP1 = !ReadyP1;
                        Ready[0].enabled = ReadyP1;
                    }

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B))
                    {
                        am.Play("Confirm", .25f);
                        SC.MoveToDucks = false;
                        SC.MoveToRounds = true;
                        RSC.enabled = true;
                        this.enabled = false;
                    }

                    if ((KeyboardControls.KeyboardLeft(Players.P1) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(1)); }
                    if ((KeyboardControls.KeyboardRight(Players.P1) && (!IsCoR_P2) && (!ReadyP2))) { StartCoroutine(ToggleSelP2(-1)); }

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        am.Play("Menu_Select", .75f);
                        ReadyP2 = !ReadyP2;
                        Ready[1].enabled = ReadyP2;
                    }

                    break;
                default:
                    break;
            }

            if (ReadyP1 && ReadyP2)
            {
                QuackingTimeData _data = new QuackingTimeData(Duck1Pos, Duck2Pos);
                SaveManager.SaveQuackingTime(_data);
                ChangeScene.ChangeToLevel();
            }
        }

        private IEnumerator ToggleSelP1(int Value)
        {
            IsCoR_P1 = true;

            if (Value == 1)
            {
                if (Duck1Pos < MaxSelection) { Duck1Pos++; }
                else { Duck1Pos = 0; }
            }
            else if (Value == -1)
            {
                if (Duck1Pos > 0) { Duck1Pos--; }
                else { Duck1Pos = MaxSelection; }
            }

            UpdateDisplayP1();

            am.Play("Menu_Click", .25f);

            yield return new WaitForSeconds(.25f);
            IsCoR_P1 = false;
        }

        private IEnumerator ToggleSelP2(int Value)
        {
            IsCoR_P2 = true;

            if (Value == 1)
            {
                if (Duck2Pos < MaxSelection) { Duck2Pos++; }
                else { Duck2Pos = 0; }
            }
            else if (Value == -1)
            {
                if (Duck2Pos > 0) { Duck2Pos--; }
                else { Duck2Pos = MaxSelection; }
            }

            UpdateDisplayP2();

            am.Play("Menu_Click", .25f);

            yield return new WaitForSeconds(.25f);
            IsCoR_P2 = false;
        }


        public void UpdateDisplayP1()
        {
            switch (Duck1Pos)
            {
                case 0:
                    HatSelected[0].sprite = Hats[0];
                    break;
                case 1:
                    HatSelected[0].sprite = Hats[1];
                    break;
                case 2:
                    HatSelected[0].sprite = Hats[2];
                    break;
                case 3:
                    HatSelected[0].sprite = Hats[3];
                    break;
                case 4:
                    HatSelected[0].sprite = Hats[4];
                    break;
                case 5:
                    HatSelected[0].sprite = Hats[5];
                    break;
                case 6:
                    HatSelected[0].sprite = Hats[6];
                    break;
                default:
                    break;
            }
        }

        public void UpdateDisplayP2()
        {
            switch (Duck2Pos)
            {
                case 0:
                    HatSelected[1].sprite = Hats[0];
                    break;
                case 1:
                    HatSelected[1].sprite = Hats[1];
                    break;
                case 2:
                    HatSelected[1].sprite = Hats[2];
                    break;
                case 3:
                    HatSelected[1].sprite = Hats[3];
                    break;
                case 4:
                    HatSelected[1].sprite = Hats[4];
                    break;
                case 5:
                    HatSelected[1].sprite = Hats[5];
                    break;
                case 6:
                    HatSelected[1].sprite = Hats[6];
                    break;
                default:
                    break;
            }
        }
    }
}