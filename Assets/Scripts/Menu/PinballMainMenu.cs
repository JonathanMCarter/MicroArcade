using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Arcade.Menu
{
    public class PinballMainMenu : InputSettings
    {
        public enum Dir { Hoz, Ver, None };
        public Dir GetDir;
        public bool MultiOptionMenu;
        public bool MultiNoRetrunBtn;
        public bool ValueAlter;
        public bool LoadData;
        public bool BothConfirm;
        public bool HasDesc;
        [HideInInspector]
        public List<GameObject> MenuObjects;
        public List<UnityEvent> MenuEvents;
        int Pos;
        int LastPos;
        int MaxPos;
        public bool CanInput;
        public Color SelectedTextColour;
        public Color DeselectedTextColour;
        bool FadeCanvasGroup;
        bool FadeCanvasGroupAndKeep;
        bool HideThisCanvas;
        CanvasGroup GroupToFadeIn;
        CanvasGroup GroupToFadeOut;
        public int Value;
        public int IncrementAmount;
        public enum Gamemodes { None, Lives, Timer, SetScore };
        public Gamemodes SelectedGamemode;
        public bool WhiteReady;
        public bool BlackReady;

        public Animator Trans;
        public AudioManager AM;
        public AudioManager MAM;

        public Text GamemodeText;
        public string[] GameDescriptions;

        AsyncOperation asyncLoadLevel;

        private void Start()
        {
            if (GetComponent<CanvasGroup>())
            {
                GroupToFadeOut = GetComponent<CanvasGroup>();
            }
            else if (GetComponentInParent<CanvasGroup>())
            {
                GroupToFadeOut = GetComponentInParent<CanvasGroup>();
            }

            if (MultiOptionMenu)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    MenuObjects.Add(transform.GetChild(i).gameObject);
                }

                CanInput = true;

                MaxPos = MenuObjects.Count;
            }
        }


        protected override void Update()
        {
            base.Update();

            if (BothConfirm)
            {
                if (WhiteReady) { GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<Text>().color = SelectedTextColour; }
                else { GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<Text>().color = Color.white; }
                if (BlackReady) { GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<Text>().color = SelectedTextColour; }
                else { GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<Text>().color = Color.black; }
            }



            if (FadeCanvasGroup)
            {
                GroupToFadeIn.alpha += Time.deltaTime * 2;
                GroupToFadeOut.alpha -= 2;

                if ((GroupToFadeOut.alpha == 0) && (GroupToFadeIn.alpha == 1))
                {
                    FadeCanvasGroup = false;
                    DisableScript();
                }
            }


            if (FadeCanvasGroupAndKeep)
            {
                GroupToFadeIn.alpha += Time.deltaTime * 2;

                if (GroupToFadeIn.alpha == 1)
                {
                    FadeCanvasGroupAndKeep = false;
                    DisableScript();
                }
            }


            if (HideThisCanvas)
            {
                GroupToFadeOut.alpha -= Time.deltaTime * 2;

                if (GroupToFadeOut.alpha == 0)
                {
                    HideThisCanvas = false;
                    DisableScript();
                }
            }


            if (CanInput && MultiOptionMenu)
            {
                switch (GetDir)
                {
                    case Dir.Hoz:

                        switch (ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:

                                if (ArcadeControls.JoystickLeft(Joysticks.White)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ArcadeControls.JoystickRight(Joysticks.White)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.GamePadBoth:

                                if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardBoth:

                                if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:

                                if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:

                                if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            default:
                                break;
                        }


                        break;
                    case Dir.Ver:

                        switch (ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:

                                if (ArcadeControls.JoystickUp(Joysticks.White)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ArcadeControls.JoystickDown(Joysticks.White)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.GamePadBoth:

                                if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardBoth:

                                if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:

                                if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:

                                if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(ChangeSelectedOption(-1)); }
                                else if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(ChangeSelectedOption(1)); }

                                break;
                            default:
                                break;
                        }

                        break;
                }
            }



            if (CanInput && ValueAlter)
            {
                switch (GetDir)
                {
                    case Dir.Hoz:

                        switch (ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:

                                if (ArcadeControls.JoystickLeft(Joysticks.White)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ArcadeControls.JoystickRight(Joysticks.White)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.GamePadBoth:

                                if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardBoth:

                                if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:

                                if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:

                                if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            default:
                                break;
                        }

                        break;
                    case Dir.Ver:

                        switch (ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:

                                if (ArcadeControls.JoystickUp(Joysticks.White)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ArcadeControls.JoystickDown(Joysticks.White)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.GamePadBoth:

                                if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardBoth:

                                if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:

                                if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:

                                if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(ChangeValue(-1 * IncrementAmount)); }
                                else if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(ChangeValue(1 * IncrementAmount)); }

                                break;
                            default:
                                break;
                        }

                        break;
                }
            }

            if (!BothConfirm)
            {
                if (MultiOptionMenu)
                {
                    if (Pos != LastPos)
                    {
                        UpdateSelectedObject();
                    }

                    // Confirm
                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { RunEvent(); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm)) { RunEvent(); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { RunEvent(); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { RunEvent(); }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm)) { RunEvent(); }

                            break;
                        default:
                            break;
                    }


                    if (MultiNoRetrunBtn)
                    {
                        // Return
                        switch (ControllerType)
                        {
                            case SupportedControllers.ArcadeBoard:

                                if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { RunEvent(MenuEvents.Count - 1); }

                                break;
                            case SupportedControllers.GamePadBoth:

                                if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return)) { RunEvent(MenuEvents.Count - 1); }

                                break;
                            case SupportedControllers.KeyboardBoth:

                                if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(MenuEvents.Count - 1); }

                                break;
                            case SupportedControllers.KeyboardP1ControllerP2:

                                if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(MenuEvents.Count - 1); }

                                break;
                            case SupportedControllers.KeyboardP2ControllerP1:

                                if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return)) { RunEvent(MenuEvents.Count - 1); }

                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    // return & confirm pt.2
                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { RunEvent(0); }

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                            {
                                if (MenuEvents.Count > 1)
                                {
                                    RunEvent(1);
                                }
                            }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return)) { RunEvent(0); }

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm))
                            {
                                if (MenuEvents.Count > 1)
                                {
                                    RunEvent(1);
                                }
                            }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(0); }

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                            {
                                if (MenuEvents.Count > 1)
                                {
                                    RunEvent(1);
                                }
                            }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(0); }

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                            {
                                if (MenuEvents.Count > 1)
                                {
                                    RunEvent(1);
                                }
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Return)) { RunEvent(0); }

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm))
                            {
                                if (MenuEvents.Count > 1)
                                {
                                    RunEvent(1);
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }



            if (LoadData)
            {
                Saving.UltimatePinballData Data = Saving.SaveManager.LoadUltimatePinball();

                SelectedGamemode = (Gamemodes)Data.LastGameTypeSelected;
                IncrementAmount = Data.LastGameTypeIncrement;

                switch (SelectedGamemode)
                {
                    case Gamemodes.Lives:

                        Value = 5;

                        break;
                    case Gamemodes.Timer:

                        Value = 60;

                        break;
                    case Gamemodes.SetScore:

                        Value = 10000;

                        break;
                    default:
                        break;
                }

                UpdateTextValue();

                LoadData = false;
            }


            if (BothConfirm)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { WhiteReady = !WhiteReady; MAM.Play("Menu_Select"); }
                        if (ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B8)) { BlackReady = !BlackReady; MAM.Play("Menu_Select"); }
                        if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7)) { RunEvent(0); }

                        break;
                    case SupportedControllers.GamePadBoth:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { WhiteReady = !WhiteReady; MAM.Play("Menu_Select"); }
                        if (ControllerControls.ButtonPress(Players.P2, ControllerButtons.A)) { BlackReady = !BlackReady; MAM.Play("Menu_Select"); }
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { RunEvent(0); }

                        break;
                    case SupportedControllers.KeyboardBoth:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { WhiteReady = !WhiteReady; MAM.Play("Menu_Select"); }
                        if (KeyboardControls.ButtonPress(Players.P2, Buttons.B8)) { BlackReady = !BlackReady; MAM.Play("Menu_Select"); }
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(0); }

                        break;
                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { WhiteReady = !WhiteReady; MAM.Play("Menu_Select"); }
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { BlackReady = !BlackReady; MAM.Play("Menu_Select"); }
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7)) { RunEvent(0); }

                        break;
                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { WhiteReady = !WhiteReady; MAM.Play("Menu_Select"); }
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { BlackReady = !BlackReady; MAM.Play("Menu_Select"); }
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { RunEvent(0); }

                        break;
                    default:
                        break;
                }

                if (WhiteReady && BlackReady)
                {
                    RunEvent(1);
                }
            }
        }


        #region Update Visuals - Updates selection / value
        void UpdateSelectedObject()
        {
            for (int i = 0; i < MenuObjects.Count; i++)
            {
                if (i == Pos)
                {
                    MenuObjects[i].GetComponentInChildren<Text>().color = SelectedTextColour;
                }
                else
                {
                    MenuObjects[i].GetComponentInChildren<Text>().color = DeselectedTextColour;
                }
            }
        }


        void UpdateTextValue()
        {
            if (SelectedGamemode == Gamemodes.Timer)
            {
                string Mins = (Value / 60).ToString("00");
                string Sec = (Value % 60).ToString("00");

                GetComponentsInChildren<Text>()[0].text = Mins + ":" + Sec;
            }
            else
            {
                GetComponentsInChildren<Text>()[0].text = Value.ToString();
            }


            switch (SelectedGamemode)
            {
                case Gamemodes.Lives:
                    GetComponentsInChildren<Text>()[1].text = "How Many Lives?";
                    break;
                case Gamemodes.Timer:
                    GetComponentsInChildren<Text>()[1].text = "How Long?";
                    break;
                case Gamemodes.SetScore:
                    GetComponentsInChildren<Text>()[1].text = "Target Score?";
                    break;
            }
        }
        #endregion


        #region Corutines - For editing Selection & Values
        IEnumerator ChangeSelectedOption(int Direction)
        {
            CanInput = false;
            LastPos = Pos;

            Pos += Direction;

            if (Pos <= -1)
            {
                Pos = MaxPos - 1;
            }
            else if (Pos == MaxPos)
            {
                Pos = 0;
            }


            // if desc
            if (HasDesc)
            {
                SetGamemodeString(GameDescriptions[Pos]);
            }

            MAM.Play("Menu_Click", .5f);
            yield return new WaitForSeconds(.25f);
            CanInput = true;
        }

        IEnumerator ChangeValue(int Direction)
        {
            CanInput = false;

            Value += Direction;

            if (Value <= IncrementAmount)
            {
                Value = IncrementAmount;
            }

            switch (SelectedGamemode)
            {
                case Gamemodes.Lives:

                    if (Value < 5)
                    {
                        Value = 5;
                    }

                    break;
                case Gamemodes.Timer:

                    if (Value < 60)
                    {
                        Value = 60;
                    }

                    break;
                case Gamemodes.SetScore:

                    if (Value < 10000)
                    {
                        Value = 10000;
                    }

                    break;
                default:
                    break;
            }

            MAM.Play("Menu_Click", .5f);
            UpdateTextValue();
            yield return new WaitForSeconds(.25f);
            CanInput = true;
        }
#endregion


        #region Functionaility Methods ( The ones that make the menu do things )

        void RunEvent()
        {
            MenuEvents[Pos].Invoke();
        }

        void RunEvent(int I)
        {
            MenuEvents[I].Invoke();
        }


        public void ChangeScene(string SceneName)
        {
            StartCoroutine(ChangeSceneAfter1Second(SceneName));
        }


        IEnumerator ChangeSceneAfter1Second(string SceneName)
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync(SceneName);
        }


        public void ChangeScene()
        {
            StartCoroutine(LoadLevel());
        }


        IEnumerator LoadLevel()
        {
            asyncLoadLevel = SceneManager.LoadSceneAsync("Ultimate-Pinball-Level", LoadSceneMode.Single);

            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }
        }


        public void EnableCanvasGroup_DisableThis(CanvasGroup Group)
        {
            GroupToFadeIn = Group;
            FadeCanvasGroup = true;
        }

        public void EnableCanvasGroup_KeepThis(CanvasGroup Group)
        {
            GroupToFadeIn = Group;
            FadeCanvasGroupAndKeep = true;
        }

        public void HideThisCanvasGroup()
        {
            HideThisCanvas = true;
        }

        void DisableScript()
        {
            enabled = false;
        }

        public void EnableScript_CanvasEdits(PinballMainMenu Script)
        {
            Script.enabled = true;
        }

        public void EnableScript_DisableThis(PinballMainMenu Script)
        {
            Script.enabled = true;
            enabled = false;
        }

        public void DisableThisScript()
        {
            this.enabled = false;
        }

        public void SetValue(int Input)
        {
            Value = Input;
        }

        public void SaveValue()
        {
            Saving.SaveManager.SaveUltimatePinballGamemode((int)SelectedGamemode, IncrementAmount, Value);
        }

        public void SetIncrementOffset(int Input)
        {
            Saving.SaveManager.SaveUltimatePinballGamemode((int)SelectedGamemode, Input);
        }

        public void LoadData_Other(PinballMainMenu Script)
        {
            Script.LoadData = true;
            Script.CanInput = true;
            Script.UpdateTextValue();
        }

        public void SetGamemode(int Mode)
        {
            Saving.SaveManager.SaveUltimatePinballGamemode(Mode, IncrementAmount);
            SelectedGamemode = (Gamemodes)Mode;
        }

        public void PlayClip(string Name)
        {
            AM.Play(Name, .25f);
        }


        public void SetGamemodeString(string input)
        {
            GamemodeText.text = input;
        }


        #endregion
    }
}