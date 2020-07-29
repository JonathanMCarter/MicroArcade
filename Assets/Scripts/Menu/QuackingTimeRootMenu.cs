/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using CarterGames.Arcade.Menu;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;


namespace CarterGames.QuackingTime
{
    public class QuackingTimeRootMenu : MenuSystem
    {
        [SerializeField] private Text[] optionsText;
        [SerializeField] private Color[] optionColours;
        [SerializeField] private SceneChanger quackingTimeMenuChanger;
        [SerializeField] private AudioManager menuAudioManager;
        [SerializeField] private TutorialMenu tutorialMenu;
        [SerializeField] private CanvasGroup rootCanvasGroups;
        [SerializeField] private CanvasGroup tutorialCanvasGroups;

        [SerializeField] private bool isTutorial;
        [SerializeField] private bool isRoot;

        [SerializeField] private Animator transition;

        internal bool isScriptEnabled = true;

        private new void Start()
        {
            am = menuAudioManager;
            maxPos = optionsText.Length - 1;
            MenuSystemStart();
            UpdateDisplay();
        }


        private new void Update()
        {
            if (isScriptEnabled)
            {
                base.Update();

                MoveLR();

                if (ValueChanged())
                {
                    UpdateDisplay();
                }

                if (Confirm())
                {
                    switch (pos)
                    {
                        case 0:
                            quackingTimeMenuChanger.OpenRoundsmenu();
                            am.Play("Menu-Confirm", .25f);
                            isTutorial = false;
                            isRoot = false;
                            break;
                        case 1:
                            am.Play("Menu-Confirm", .25f);
                            isTutorial = true;
                            isRoot = false;
                            break;
                        case 2:
                            transition.SetBool("ChangeScene", true);
                            ChangeScene("PlayMenu", 1f);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (isTutorial && !isRoot)
            {
                tutorialCanvasGroups.alpha += Time.deltaTime * 4;
                rootCanvasGroups.alpha -= Time.deltaTime * 6;

                if (rootCanvasGroups.alpha == 0 && tutorialCanvasGroups.alpha == 1)
                {
                    isRoot = false;
                    tutorialMenu.enabled = true;
                    isScriptEnabled = false;
                }
            }

            if (isRoot && !isTutorial)
            {
                rootCanvasGroups.alpha += Time.deltaTime * 4;
                tutorialCanvasGroups.alpha -= Time.deltaTime * 6;

                if (rootCanvasGroups.alpha == 1 && tutorialCanvasGroups.alpha == 0)
                {
                    tutorialMenu.enabled = false;
                    isScriptEnabled = true;
                }
            }
        }


        private void UpdateDisplay()
        {
            for (int i = 0; i < optionsText.Length; i++)
            {
                if (pos == i && optionsText[i].color != optionColours[1])
                {
                    optionsText[i].color = optionColours[1];
                }
                else if (pos != i && optionsText[i].color != optionColours[0])
                {
                    optionsText[i].color = optionColours[0];
                }
            }
        }


        public void ChangeToRootMenu()
        {
            isTutorial = false;
            isRoot = true;
        }
    }
}