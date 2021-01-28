using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Utilities;

namespace CarterGames.Arcade.OnScreenKeyboard
{
    public class OnScreenKeyboard : MonoBehaviour
    {
        [Header("Key Rows")]
        [Tooltip("The rows that the keys are under.")]
        [SerializeField] private GameObject[] rows;

        //The text element that is used to show what the player has inputted
        [Header("The Output Display")]
        [Tooltip("The Text Ely To Display The Input")]
        [SerializeField] private Text inputtedValueDisplay;

        private const int maxLetters = 16;
        private bool isCoR;
        private GameObject currentSelection;
        private int currentPos;
        private int currentRow;
        private int[] rowMaxValues;

        private Actions actions;

        //The value the player inputs with this system
        [Header("The Entered Value")]
        public string inputtedValue;


        private void OnEnable()
        {
            actions = new Actions();
            actions.Enable();
        }


        private void OnDisable()
        {
            actions.Disable();
        }


        private void Start()
        {
            rowMaxValues = new int[3];

            // Inits the rows as ther are pre-defined
            rowMaxValues[0] = 10;
            rowMaxValues[1] = 9;
            rowMaxValues[2] = 7;

            // Sets the row and pos to the default position
            currentRow = 0;
            currentPos = 1;

            currentSelection = rows[0].transform.GetChild(0).gameObject;

            UpdateCurrentSelection();
        }


        protected void Update()
        {
            if (actions.Controls.Joystick.ReadValue<Vector2>().x < -.1f && !isCoR)
                MoveLR(-1);
            else if (actions.Controls.Joystick.ReadValue<Vector2>().x > .1f && !isCoR)
                MoveLR(1);
            else if (actions.Controls.Joystick.ReadValue<Vector2>().y < -.1f && !isCoR)
                MoveUD(1);
            else if (actions.Controls.Joystick.ReadValue<Vector2>().x > .1f && !isCoR)
                MoveUD(-1);


            if (NewInputSystemHelper.ButtonPressed(actions.Controls.Button1))
            {
                if (inputtedValue.Length < maxLetters)
                {
                    inputtedValue += GetChar();
                    UpdateInputtedText();
                }
            }

            if (NewInputSystemHelper.ButtonPressed(actions.Controls.Button2))
            {
                inputtedValue = RemoveLastChar();
                UpdateInputtedText();
            }
        }



        void MoveLR(int value)
        {
            ClearSelected();
            currentPos += value;
            CheckRow();
            UpdateCurrentSelection();
        }

        void MoveUD(int value)
        {
            ClearSelected();
            currentRow += value;
            CheckDownRow();
            UpdateCurrentSelection();
        }


        /// <summary>
        /// Checks the row movement to make sure it stays in the bounds of the keyboard when going left or right
        /// </summary>
        void CheckRow()
        {
            if (currentPos > rowMaxValues[currentRow])
            {
                currentPos = 1;
            }
            else if (currentPos == 0)
            {
                currentPos = rowMaxValues[currentRow];
            }

            StartCoroutine(Cooldown());
        }

        /// <summary>
        /// Checks the row movement to make sure it stays in the bounds of the keyboard when going down
        /// It also runs the row check as well, just in case.
        /// </summary>
        void CheckDownRow()
        {
            if (currentRow > 2)
            {
                currentRow = 0;
            }
            else if (currentRow == -1)
            {
                currentRow = 2;
            }

            if (currentPos > rowMaxValues[currentRow])
            {
                currentPos = rowMaxValues[currentRow];
            }

            CheckRow();
        }

        /// <summary>
        /// Runs a cooldown on the controls, stopping the cycling from happening too fast for the user to actually use.
        /// </summary>
        IEnumerator Cooldown()
        {
            isCoR = true;
            yield return new WaitForSecondsRealtime(.2f);
            isCoR = false;
        }

        /// <summary>
        /// Clears the border image from the last selected image
        /// </summary>
        void ClearSelected()
        {
            currentSelection.GetComponentsInChildren<Image>()[1].enabled = false;
        }

        /// <summary>
        /// Updates the border image shown to be on the new selected image
        /// </summary>
        public void ShowSelected()
        {
            currentSelection.GetComponentsInChildren<Image>()[1].enabled = true;
        }

        /// <summary>
        /// Updates the current selection gameobject
        /// </summary>
        void UpdateCurrentSelection()
        {
            currentSelection = rows[currentRow].transform.GetChild(currentPos - 1).gameObject;
            ShowSelected();
        }


        public void HideSelected()
        {
            currentSelection.GetComponentsInChildren<Image>()[1].enabled = false;
        }



        /// <summary>
        /// Gets the char on the currently selected image
        /// </summary>
        /// <returns>The char on the object selected only</returns>
        char GetChar()
        {
            return currentSelection.GetComponentInChildren<Text>().text.ToCharArray()[0];
        }

        /// <summary>
        /// Removes the last character in the inputted, if there is none left it will do nothing.
        /// </summary>
        /// <returns>The string value without the last character</returns>
        string RemoveLastChar()
        {
            if (inputtedValue.Length > 0)
            {
                return inputtedValue.Remove(inputtedValue.Length - 1);
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
            inputtedValueDisplay.text = inputtedValue;
        }

        /// <summary>
        /// Returns the current inputted value string
        /// </summary>
        /// <returns>The Inputted string value</returns>
        public string GetFinalValue()
        {
            return inputtedValue;
        }
    }
}
