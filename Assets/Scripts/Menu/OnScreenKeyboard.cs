using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.UserInput
{
    public class OnScreenKeyboard : InputSettings
    {
        private const int maxLetters = 16;

        //The value the player inputs with this system
        [Header("The Entered Value")]
        public string InputtedValue;

        //The text element that is used to show what the player has inputted
        [Header("The Text Ely To Display The Input")]
        public Text InputtedValueDisplay;

        // The player who can input the value
        [Header("The Player That Can Use The System")]
        public Joysticks PlayerUsing;


        [Header("Lists Of The Images In Their Rows")]
        public List<Image> Row0Keys;
        public List<Image> Row1Keys;
        public List<Image> Row2Keys;

        public GameObject CurrentSelection;
        public int CurrentPos;
        public int CurrentRow;
        public List<int> RowMaxValues;
        bool IsCooldownOver;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        void Start()
        {
            RowMaxValues = new List<int>();

            // Inits the rows as ther are pre-defined
            RowMaxValues.Add(10);
            RowMaxValues.Add(9);
            RowMaxValues.Add(7);

            // Sets the row and pos to the default position
            CurrentRow = 0;
            CurrentPos = 1;

            // Sets up the cool down to be allowed to start with
            IsCooldownOver = true;

            CurrentSelection = Row0Keys[0].gameObject;

            UpdateCurrentSelection();
        }


        protected override void Update()
        {
            base.Update();

            // Controls for the player and more...
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if ((ArcadeControls.JoystickLeft(PlayerUsing)) && IsCooldownOver) { MoveLR(-1); }
                    if ((ArcadeControls.JoystickRight(PlayerUsing)) && IsCooldownOver) { MoveLR(1); }
                    if ((ArcadeControls.JoystickUp(PlayerUsing)) && IsCooldownOver) { MoveUD(-1); }
                    if ((ArcadeControls.JoystickDown(PlayerUsing)) && IsCooldownOver) { MoveUD(1); }

                    if (ArcadeControls.ButtonPress(PlayerUsing, Buttons.B1)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                    if (ArcadeControls.ButtonPress(PlayerUsing, Buttons.B2)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if ((ControllerControls.ControllerLeft(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveLR(-1); }
                    if ((ControllerControls.ControllerRight(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveLR(1); }
                    if ((ControllerControls.ControllerUp(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveUD(-1); }
                    if ((ControllerControls.ControllerDown(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveUD(1); }

                    if (ControllerControls.ButtonPress(ConvertToPlayers(PlayerUsing), ControllerButtons.A)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                    if (ControllerControls.ButtonPress(ConvertToPlayers(PlayerUsing), ControllerButtons.B)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if ((KeyboardControls.KeyboardLeft(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveLR(-1); }
                    if ((KeyboardControls.KeyboardRight(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveLR(1); }
                    if ((KeyboardControls.KeyboardUp(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveUD(-1); }
                    if ((KeyboardControls.KeyboardDown(ConvertToPlayers(PlayerUsing))) && IsCooldownOver) { MoveUD(1); }

                    if (KeyboardControls.ButtonPress(ConvertToPlayers(PlayerUsing), Buttons.B1)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                    if (KeyboardControls.ButtonPress(ConvertToPlayers(PlayerUsing), Buttons.B2)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (ConvertToPlayers(PlayerUsing) == Players.P1)
                    {
                        if ((KeyboardControls.KeyboardLeft(Players.P1)) && IsCooldownOver) { MoveLR(-1); }
                        if ((KeyboardControls.KeyboardRight(Players.P1)) && IsCooldownOver) { MoveLR(1); }
                        if ((KeyboardControls.KeyboardUp(Players.P1)) && IsCooldownOver) { MoveUD(-1); }
                        if ((KeyboardControls.KeyboardDown(Players.P1)) && IsCooldownOver) { MoveUD(1); }

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B2)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }
                    }
                    else
                    {
                        if ((ControllerControls.ControllerLeft(Players.P1)) && IsCooldownOver) { MoveLR(-1); }
                        if ((ControllerControls.ControllerRight(Players.P1)) && IsCooldownOver) { MoveLR(1); }
                        if ((ControllerControls.ControllerUp(Players.P1)) && IsCooldownOver) { MoveUD(-1); }
                        if ((ControllerControls.ControllerDown(Players.P1)) && IsCooldownOver) { MoveUD(1); }

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ConvertToPlayers(PlayerUsing) == Players.P2)
                    {
                        if ((KeyboardControls.KeyboardLeft(Players.P1)) && IsCooldownOver) { MoveLR(-1); }
                        if ((KeyboardControls.KeyboardRight(Players.P1)) && IsCooldownOver) { MoveLR(1); }
                        if ((KeyboardControls.KeyboardUp(Players.P1)) && IsCooldownOver) { MoveUD(-1); }
                        if ((KeyboardControls.KeyboardDown(Players.P1)) && IsCooldownOver) { MoveUD(1); }

                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B2)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }
                    }
                    else
                    {
                        if ((ControllerControls.ControllerLeft(Players.P1)) && IsCooldownOver) { MoveLR(-1); }
                        if ((ControllerControls.ControllerRight(Players.P1)) && IsCooldownOver) { MoveLR(1); }
                        if ((ControllerControls.ControllerUp(Players.P1)) && IsCooldownOver) { MoveUD(-1); }
                        if ((ControllerControls.ControllerDown(Players.P1)) && IsCooldownOver) { MoveUD(1); }

                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { if (InputtedValue.Length < maxLetters) { InputtedValue += GetChar(); UpdateInputtedText(); } }
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) { InputtedValue = RemoveLastChar(); UpdateInputtedText(); }
                    }

                    break;
                default:
                    break;
            }
        }

        void MoveLR(int Value)
        {
            IsCooldownOver = false;
            ClearSelected();
            CurrentPos += Value;
            CheckRow();
            UpdateCurrentSelection();
        }

        void MoveUD(int Value)
        {
            IsCooldownOver = false;
            ClearSelected();
            CurrentRow += Value;
            CheckDownRow();
            UpdateCurrentSelection();
        }


        /// <summary>
        /// Checks the row movement to make sure it stays in the bounds of the keyboard when going left or right
        /// </summary>
        void CheckRow()
        {
            if (CurrentPos > RowMaxValues[CurrentRow])
            {
                CurrentPos = 1;
            }
            else if (CurrentPos == 0)
            {
                CurrentPos = RowMaxValues[CurrentRow];
            }

            if (!IsCooldownOver)
            {
                StartCoroutine(Cooldown());
            }
        }

        /// <summary>
        /// Checks the row movement to make sure it stays in the bounds of the keyboard when going down
        /// It also runs the row check as well, just in case.
        /// </summary>
        void CheckDownRow()
        {
            if (CurrentRow > 2)
            {
                CurrentRow = 0;
            }
            else if (CurrentRow == -1)
            {
                CurrentRow = 2;
            }

            if (CurrentPos > RowMaxValues[CurrentRow])
            {
                CurrentPos = RowMaxValues[CurrentRow];
            }

            CheckRow();
        }

        /// <summary>
        /// Runs a cooldown on the controls, stopping the cycling from happening too fast for the user to actually use.
        /// </summary>
        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(.2f);
            IsCooldownOver = true;
        }

        /// <summary>
        /// Clears the border image from the last selected image
        /// </summary>
        void ClearSelected()
        {
            CurrentSelection.GetComponentsInChildren<Image>()[1].enabled = false;
        }

        /// <summary>
        /// Updates the border image shown to be on the new selected image
        /// </summary>
        public void ShowSelected()
        {
            CurrentSelection.GetComponentsInChildren<Image>()[1].enabled = true;
        }

        /// <summary>
        /// Updates the current selection gameobject
        /// </summary>
        void UpdateCurrentSelection()
        {
            switch (CurrentRow)
            {
                case 0:
                    CurrentSelection = Row0Keys[CurrentPos - 1].gameObject;
                    break;
                case 1:
                    CurrentSelection = Row1Keys[CurrentPos - 1].gameObject;
                    break;
                case 2:
                    CurrentSelection = Row2Keys[CurrentPos - 1].gameObject;
                    break;
                default:
                    break;
            }

            ShowSelected();
        }


        public void HideSelected()
        {
            CurrentSelection.GetComponentsInChildren<Image>()[1].enabled = false;
        }



        /// <summary>
        /// Gets the char on the currently selected image
        /// </summary>
        /// <returns>The char on the object selected only</returns>
        char GetChar()
        {
            return CurrentSelection.GetComponentInChildren<Text>().text.ToCharArray()[0];
        }

        /// <summary>
        /// Removes the last character in the inputted, if there is none left it will do nothing.
        /// </summary>
        /// <returns>The string value without the last character</returns>
        string RemoveLastChar()
        {
            if (InputtedValue.Length > 0)
            {
                return InputtedValue.Remove(InputtedValue.Length - 1);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Updates the display text when called
        /// </summary>
        void UpdateInputtedText()
        {
            InputtedValueDisplay.text = InputtedValue;
        }

        /// <summary>
        /// Returns the current inputted value string
        /// </summary>
        /// <returns>The Inputted string value</returns>
        public string GetFinalValue()
        {
            return InputtedValue;
        }


        Players ConvertToPlayers(Joysticks Input)
        {
            return (Players)Input;
        }
    }
}
