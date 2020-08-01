using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;
using System;
using CarterGames.MAExtras;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    [Serializable]
    public class PinballMainMenu : MenuSystem, IMultiLayerMenu
    {
        public StringAoA[] SceneMethods { get { return sceneMethods; } set { } }
        public GameObjectAoA[] SceneObjects { get { return sceneObjects; } set { } }
        public Animator SceneTransition { get { return sceneTransition; } set { } }
        public short ActiveTier { get { return activeTier; } set { } }



        [SerializeField] StringAoA[] sceneMethods;
        [SerializeField] GameObjectAoA[] sceneObjects;
        [SerializeField] Animator sceneTransition;
        [SerializeField] short activeTier;

        [SerializeField] CanvasGroup[] cg;
        [SerializeField] Text promptText;
        [SerializeField] short selectionGM;

        private bool shouldDisable = false;
        private bool shouldUsePos = true;
        private short returnTier;
        private short returnPos;


        protected override void Start()
        {
            am = FindObjectOfType<AudioManager>();
            pos = 0;
            maxPos = sceneMethods[activeTier].array.Length - 1;
            base.Start();
            UpdateDisplay();
        }

        
        protected override void Update()
        {
            UpdateDirection();
            base.Update();
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { InvokeMethod(sceneMethods[activeTier].array[pos]); }
            if (shouldUsePos)
            {
                if (Return() && activeTier >= 0) { InvokeMethod(sceneMethods[returnTier].array[pos] + "Back"); }
            }
            else
            {
                if (Return() && activeTier >= 0) { InvokeMethod(sceneMethods[returnTier].array[returnPos] + "Back"); }
            }
        }


        /// <summary>
        /// Calls a method on the string inputted
        /// </summary>
        /// <param name="method">string for the method name to call</param>
        private void InvokeMethod(string method)
        {
            Invoke(method, .1f);
        }

        /// <summary>
        /// Edits canvas groups
        /// </summary>
        /// <param name="fadeIn">is a fade in?</param>
        /// <param name="groupToEdit">cg to edit</param>
        private void EditCanvasGroup(bool fadeIn, CanvasGroup groupToEdit)
        {
            if (fadeIn)
            {
                if (groupToEdit.alpha != 1)
                {
                    groupToEdit.alpha = 1;
                }
            }
            else
            {
                if (groupToEdit.alpha != 0)
                {
                    groupToEdit.alpha = 0;
                }
            }
        }

        /// <summary>
        /// Changes the tier to the desired tier
        /// </summary>
        /// <param name="tier">tier to change to</param>
        /// <param name="updateDisplay">should the update display be called</param>
        private void ChangeTier(short tier, bool updateDisplay)
        {
            activeTier = tier;
            maxPos = sceneObjects[activeTier].array.Length - 1;
            pos = 0;

            if (updateDisplay)
            {
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Changes the direction the controls work in based on the active tier
        /// </summary>
        private void UpdateDirection()
        {
            switch (activeTier)
            {
                case 0:
                    MoveUD();
                    break;
                case 1:
                    MoveLR();
                    break;
                case 2:
                    MoveLR();
                    break;
                case 4:
                    MoveLR();
                    break;
                case 5:
                    MoveUD();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates the display to show the text values with a highlighted colour
        /// </summary>
        public void UpdateDisplay()
        {
            if (!shouldDisable)
            {
                for (int i = 0; i < sceneObjects[activeTier].array.Length; i++)
                {
                    if ((i == pos) && (sceneObjects[activeTier].array[i].GetComponentInChildren<Text>().color != Color.yellow))
                    {
                        sceneObjects[activeTier].array[i].GetComponentInChildren<Text>().color = Color.yellow;
                    }
                    else if ((i != pos) && (sceneObjects[activeTier].array[i].GetComponentInChildren<Text>().color == Color.yellow))
                    {
                        sceneObjects[activeTier].array[i].GetComponentInChildren<Text>().color = Color.grey;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sceneObjects[activeTier].array.Length; i++)
                {
                    if ((i == pos) && (!sceneObjects[activeTier].array[i].activeInHierarchy))
                    {
                        sceneObjects[activeTier].array[i].SetActive(true);
                    }
                    else if ((i != pos) && (sceneObjects[activeTier].array[i].activeInHierarchy))
                    {
                        sceneObjects[activeTier].array[i].SetActive(false);
                    }
                }
            }
        }


/*  ======================================================
* 
*                       Root Menu
*          
* =======================================================
*/ 

        private void Play()
        {
            EditCanvasGroup(true, cg[1]);
            EditCanvasGroup(true, cg[2]);
            ChangeTier(1, true);
        }

        private void PlayBack()
        {
            EditCanvasGroup(false, cg[1]);
            EditCanvasGroup(false, cg[2]);
            ChangeTier(0, true);
        }

        private void Tutorial()
        {
            EditCanvasGroup(false, cg[0]);
            EditCanvasGroup(true, cg[5]);
            ChangeTier(4, false);
            shouldDisable = true;
            shouldUsePos = false;
            returnTier = 0;
            returnPos = 1;
        }

        private void TutorialBack()
        {
            shouldDisable = false;
            EditCanvasGroup(true, cg[0]);
            EditCanvasGroup(false, cg[5]);
            ChangeTier(0, true);
            shouldUsePos = true;
        }

        private void Leaderboard()
        {
            EditCanvasGroup(false, cg[0]);
            EditCanvasGroup(true, cg[6]);
            ChangeTier(5, true);
            shouldUsePos = false;
            returnTier = 0;
            returnPos = 2;
        }

        private void LeaderboardBack()
        {
            EditCanvasGroup(true, cg[0]);
            EditCanvasGroup(false, cg[6]);
            ChangeTier(0, true);
            shouldUsePos = true;
        }

        private void Menu()
        {
            ChangeScene("Arcade-Play");
        }


        /*  ======================================================
         * 
         *                      Gamemode Options
         *          
         * =======================================================
        */

        private void Lives()
        {
            promptText.enabled = false;
            EditCanvasGroup(true, cg[3]);
            ChangeTier(2, false);
        }
        private void LivesBack()
        {
            promptText.enabled = true;
            EditCanvasGroup(false, cg[3]);
            ChangeTier(1, true);
        }
    }
}