﻿/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Arcade.Menu
{
    public class ExitMicroArcade : MenuSystem
    {
        [SerializeField] private bool shouldOpenQuitGame;
        [SerializeField] private GameObject[] quitOptions;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private BootMenu bootMenuController;
        [SerializeField] private Color[] colors;


        private new void Start()
        {
            MenuSystemStart();
            maxPos = quitOptions.Length - 1;
        }


        private new void Update()
        {

            if (!isPopupOpen)
            {
                if (canvasGroup.alpha != 0)
                {
                    canvasGroup.alpha -= Time.deltaTime * 3;
                }
            }


            if (isPopupOpen)
            {
                MoveLR();

                if (ValueChanged())
                {
                    UpdateDisplay();
                    Debug.Log("pos:" + pos);
                }


                switch (pos)
                {
                    case 0:

                        if (Confirm())
                        {
                            Application.Quit();
                        }

                        break;

                    case 1:

                        if (Confirm())
                        {
                            CloseQuitPopup();
                            pos = 0;
                        }

                        break;
                    default:
                        break;
                }


                if (Return())
                {
                    CloseQuitPopup();
                }

                if (canvasGroup.alpha != 1)
                {
                    canvasGroup.alpha += Time.deltaTime * 3;
                }
            }
        }


        public void OpenQuitPopup()
        {
            isPopupOpen = true;
            bootMenuController.enabled = false;
            UpdateDisplay();
        }


        private void CloseQuitPopup()
        {
            isPopupOpen = false;
            bootMenuController.enabled = true;
        }


        /// <summary>
        /// Updates the display on what is selected
        /// </summary>
        private void UpdateDisplay()
        {
            for (int i = 0; i < quitOptions.Length; i++)
            {
                if (pos == i)
                {
                    if ((pos == 0 && quitOptions[i].GetComponent<Image>().color != colors[1]))
                    {
                        quitOptions[i].GetComponent<Image>().color = colors[1];
                    }
                    else if (pos == 1 && quitOptions[i].GetComponent<Image>().color != colors[2])
                    {
                        quitOptions[i].GetComponent<Image>().color = colors[2];
                    }
                }
                else if (pos != i && quitOptions[i].GetComponent<Image>().color != colors[1] || quitOptions[i].GetComponent<Image>().color != colors[2])
                {
                    quitOptions[i].GetComponent<Image>().color = colors[0];
                }
            }
        }
    }
}