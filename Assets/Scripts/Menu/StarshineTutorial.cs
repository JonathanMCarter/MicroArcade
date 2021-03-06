﻿/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Arcade.Menu
{
    public class StarshineTutorial : MenuSystem
    {
        [SerializeField] private Image[] tutorialPips;
        [SerializeField] private GameObject[] tutorialPages;
        [SerializeField] private CanvasGroup[] menuCanvasGroups;

        [SerializeField] private bool isReturning;

        public OperationStarshineMenuScreen scriptToEnable;
        public AudioManager audioManager;
        public AudioManager menuAudioManager;


        private void OnEnable()
        {
            inputReady = true;
        }

        private void OnDisable()
        {
            inputReady = false;
        }

        private new void Start()
        {
            maxPos = tutorialPips.Length - 1;
            am = menuAudioManager;
        }

        private new void Update()
        {
            MoveLR();

            if (ValueChanged())
            {
                ChangePage();
            }

            if (Return())
            {
                ChangeBackToMenu();
                menuAudioManager.Play("Confirm", .25f);
            }

            if (isReturning)
            {
                if ((menuCanvasGroups[0].alpha != 0) && (menuCanvasGroups[1].alpha != 1))
                {
                    menuCanvasGroups[0].alpha -= 2 * Time.deltaTime;
                    menuCanvasGroups[1].alpha += 2 * Time.deltaTime;

                    if (menuCanvasGroups[0].alpha == 0 && menuCanvasGroups[1].alpha == 1)
                    {
                        isReturning = false;
                        enabled = false;
                    }
                }
            }
        }


        private void ChangePage()
        {
            for (int i = 0; i < tutorialPips.Length; i++)
            {
                if ((pos == i) && (!tutorialPages[i].activeSelf))
                {
                    tutorialPages[i].SetActive(true);
                    tutorialPips[i].color = Color.green;
                }
                else if ((pos != i) && (tutorialPages[i].activeSelf))
                {
                    tutorialPages[i].SetActive(false);
                    tutorialPips[i].color = Color.grey;
                }
                else if ((pos != i) && (!tutorialPages[i].activeSelf))
                {
                    tutorialPips[i].color = Color.grey;
                }
            }
        }


        private void ChangeBackToMenu()
        {
            scriptToEnable.enabled = true;
            isReturning = true;
        }
    }
}