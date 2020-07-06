using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Arcade.Menu
{
    public class OperationStarshineMenuScreen : InputSettings
    {
        // Visible Stuff
        [Header("-- { Screen Controls } --")]
        public bool Script_Active;
        [Tooltip("Does this screen have multiple options to select from?")]
        public bool Multi_Option;
        [Tooltip("Does this screen need a return event? (only needed if thier is no return option on the options)")]
        public bool Use_Return_Event;
        [Tooltip("Does this screen change a value rather than a selected option?")]
        public bool Change_Value_Screen;
        [Tooltip("Does this screen need some saved data to function?")]
        public bool Need_Loaded_Data;
        [Tooltip("Does this screen need both players to confirm before moving on?")]
        public bool Both_Should_Confirm;

        public bool DisableTutorial;

        public AudioManager MAM;


        // Move selections
        public enum Directions { None, Hoz, Ver };
        [Header("-- { Input Direction } --")]
        public Directions InputDir;

        [Header("-- { Colours } --")]
        public Color SelectedCol;
        public Color DeselectedCol;


        [Header("-- { Events } --")]
        public List<UnityEvent> ScreenEvents;


        [Header("Return Event (If Needed)")]
        [Tooltip("Run on B7 Presson White Player if the return event bool is enabled")]
        public UnityEvent ReturnEvent;


        // Internal Stuff
        
        public List<GameObject> ScreenOptions;     // Holds the screen options on a multi screen

        // Fade in/out canvas groups 
        CanvasGroup ThisCG;
        CanvasGroup NewCG;
        bool FadeInNewGroup;

        // Menu movement (internal)
        int CurrentPos;
        int MaxPos;
        int LastPos;
        bool CanInput;

        Animator Trans;

        private void Start()
        {
            if (Multi_Option)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    ScreenOptions.Add(transform.GetChild(i).gameObject);
                }

                if (DisableTutorial)
                {
                    ScreenOptions.RemoveAt(1);
                }

                MaxPos = ScreenOptions.Count;
                CurrentPos = 0;

                UpdateSelectedVisuals();
            }

            // Assigns This CG
            if (GetComponent<CanvasGroup>()) { ThisCG = GetComponent<CanvasGroup>(); }
            else if (GetComponentInParent<CanvasGroup>()) { ThisCG = GetComponentInParent<CanvasGroup>(); }

            CanInput = true;

            Trans = GameObject.FindGameObjectWithTag("MenuFade").GetComponent<Animator>();
        }



        protected override void Update()
        {
            base.Update();

            if (Script_Active)
            {
                // Is a Multi Option
                if (Multi_Option) 
                {
                    // Input
                    if (CanInput)
                    {
                        MenuInput_MultiOption();
                    }

                    // Update Selected Visual - ( if needed )
                    if (UpdateSelectedNeeded()) { UpdateSelectedVisuals(); }
                }
                else
                {
                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B7) && (Use_Return_Event)) { ReturnEvent.Invoke(); }
                            if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8)) { ScreenEvents[0].Invoke(); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B) && (Use_Return_Event)) { ReturnEvent.Invoke(); }
                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { ScreenEvents[0].Invoke(); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7) && (Use_Return_Event)) { ReturnEvent.Invoke(); }
                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ScreenEvents[0].Invoke(); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B7) && (Use_Return_Event)) { ReturnEvent.Invoke(); }
                            if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8)) { ScreenEvents[0].Invoke(); }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B) && (Use_Return_Event)) { ReturnEvent.Invoke(); }
                            if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { ScreenEvents[0].Invoke(); }

                            break;
                        default:
                            break;
                    }
                }

                FadeInCanvasGroup_and_FadeOutThisCanvasGroup();
            }
        }



        #region Functionality

        void FadeInCanvasGroup_and_FadeOutThisCanvasGroup()
        {
            if (FadeInNewGroup)
            {
                ThisCG.alpha -= Time.deltaTime * 2;
                NewCG.alpha += Time.deltaTime * 2;

                if ((NewCG.alpha == 1) && (ThisCG.alpha == 0))
                {
                    FadeInNewGroup = false;
                }
            }
        }

        void RunEvent(int EventNumberToRun)
        {
            ScreenEvents[EventNumberToRun].Invoke();
        }

        void RunEvent()
        {
            if (DisableTutorial)
            {
                switch (CurrentPos)
                {
                    case 0:
                        ScreenEvents[CurrentPos].Invoke();
                        break;
                    case 1:
                        ScreenEvents[CurrentPos+1].Invoke();
                        break;
                    case 2:
                        ScreenEvents[CurrentPos+1].Invoke();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ScreenEvents[CurrentPos].Invoke();
            }

        }

        void MenuInput_MultiOption()
        {
            switch (InputDir)
            {
                case Directions.Hoz:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.JoystickLeft(Joysticks.White)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ArcadeControls.JoystickRight(Joysticks.White)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.KeyboardLeft(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (KeyboardControls.KeyboardRight(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ControllerLeft(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ControllerControls.ControllerRight(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        default:
                            break;
                    }

                    break;
                case Directions.Ver:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.JoystickUp(Joysticks.White)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ArcadeControls.JoystickDown(Joysticks.White)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (KeyboardControls.KeyboardUp(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (KeyboardControls.KeyboardDown(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ControllerControls.ControllerUp(Players.P1)) { StartCoroutine(MoveAroundMenu(-1)); }
                            if (ControllerControls.ControllerDown(Players.P1)) { StartCoroutine(MoveAroundMenu(1)); }

                            break;
                        default:
                            break;
                    }

                    break;
            }

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Joysticks.White, Buttons.B8))
                    {
                        RunEvent();
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm))
                    {
                        RunEvent();
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        RunEvent();
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (KeyboardControls.ButtonPress(Players.P1, Buttons.B8))
                    {
                        RunEvent();
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.Confirm))
                    {
                        RunEvent();
                    }

                    break;
                default:
                    break;
            }
        }

        bool UpdateSelectedNeeded()
        {
            if (LastPos != CurrentPos) { return true; }
            else { return false; }
        }

        void UpdateSelectedVisuals()
        {
            for (int i = 0; i < ScreenOptions.Count; i++)
            {
                if (i == CurrentPos)
                {
                    ScreenOptions[i].GetComponentInChildren<Text>().color = SelectedCol;
                }
                else
                {
                    ScreenOptions[i].GetComponentInChildren<Text>().color = DeselectedCol;
                }
            }
        }

        #endregion



        #region Corutines

        IEnumerator MoveAroundMenu(int PosChange)
        {
            CanInput = false;

            LastPos = CurrentPos;
            CurrentPos += PosChange;

            if (CurrentPos == MaxPos) { CurrentPos = 0; }
            else if (CurrentPos <= -1) { CurrentPos = (MaxPos - 1); }

            MAM.Play("Menu_Click", .25f);

            yield return new WaitForSeconds(.2f);

            CanInput = true;
        }


        #endregion



        #region Event Methods

        public void FadeCGIN_and_FadeThisCGOUT(CanvasGroup NewGroup)
        {
            NewCG = NewGroup;
            FadeInNewGroup = true;
        }

        public void ChangeActiveScript(OperationStarshineMenuScreen Script)
        {
            Script.Script_Active = true;
            Script_Active = false;
        }

        public void ChangeActiveScriptAfter1Sec(OperationStarshineMenuScreen Script)
        {
            StartCoroutine(ChangeActiveScriptWithDelay(Script));
        }

        IEnumerator ChangeActiveScriptWithDelay(OperationStarshineMenuScreen Script)
        {
            MAM.Play("Confirm", .25f);
            yield return new WaitForSeconds(1.01f);
            Script.Script_Active = true;
            Script_Active = false;
        }

        public void ReturnToMenu()
        {
            StartCoroutine(ChangeToMenuAfter1Second());
        }

        IEnumerator ChangeToMenuAfter1Second()
        {
            MAM.Play("Confirm", .25f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync("PlayMenu");
        }

        public void PlaySound()
        {
            MAM.Play("Confirm", .25f);
        }

        #endregion
    }
}