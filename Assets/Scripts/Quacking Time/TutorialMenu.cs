/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using CarterGames.Arcade.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.QuackingTime
{
    public class TutorialMenu : MenuSystem
    {
        [SerializeField] private GameObject[] pages;
        [SerializeField] private Image[] pagePips;
        [SerializeField] private Color[] pipColours;
        [SerializeField] private QuackingTimeRootMenu quackingTimeRootmenu;

        private new void Start()
        {
            MenuSystemStart();
            MaxPos = pages.Length - 1;
            UpdateDisplay();
        }


        private new void Update()
        {
            base.Update();

            MoveLR();

            if (ValueChanged())
            {
                UpdateDisplay();
            }

            if (Return())
            {
                quackingTimeRootmenu.ChangeToRootMenu();
            }
        }


        private void UpdateDisplay()
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (Pos == i)
                {
                    if (!pages[i].activeSelf)
                    {
                        pages[i].SetActive(true);
                    }

                    if (pagePips[i].color != pipColours[1])
                    {
                        pagePips[i].color = pipColours[1];
                    }
                }
                else if (Pos != i)
                {
                    if (pages[i].activeSelf)
                    {
                        pages[i].SetActive(false);
                    }

                    if (pagePips[i].color != pipColours[0])
                    {
                        pagePips[i].color = pipColours[0];
                    }
                }
            }
        }
    }
}