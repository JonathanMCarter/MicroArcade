using CarterGames.Arcade.UserInput;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class CWISPlayPanel : MonoBehaviour
    {
        [SerializeField] private Image[] options;
        [SerializeField] private Image[] whiteOptions;
        [SerializeField] private Image[] blackOptions;

        private Panel panel;
        private ArcadeGameMenuCtrl aGMC;
        private const string sceneName = "CWIS-Game";

        private bool whiteReady, blackReady;
        

        private void Start()
        {
            aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
            panel = new Panel();
            panel.maxPos = 1;
            panel.BaseSetup();
            panel.pos = 0;
            Display();
        }


        private void Update()
        {
            Controls();


            if (whiteReady && blackReady && !panel.isCoR)
            {
                panel.isCoR = true;
                aGMC.transitions.SetBool("ChangeScene", true);
                StartCoroutine(panel.ChangeScene(sceneName));
            }
        }


        private void Controls()
        {
            if (MenuControls.Left() && !panel.isCoR)
            {
                StartCoroutine(panel.MoveAround(-1));
                Display();
            }

            if (MenuControls.Right() && !panel.isCoR)
            {
                StartCoroutine(panel.MoveAround(1));
                Display();
            }


            if (MenuControls.Confirm())
            {
                if (panel.pos == 0)
                {
                    if (!whiteReady)
                    {
                        whiteReady = true;
                        whiteOptions[0].enabled = false;
                        whiteOptions[1].enabled = true;
                    }

                    aGMC.ChangeScene(sceneName);
                }
            }

            if (MenuControls.Return())
            {
                if (whiteReady)
                {
                    whiteReady = false;
                    whiteOptions[1].enabled = false;
                    whiteOptions[0].enabled = true;
                }
                else
                {
                    aGMC.playPanelActive = false;
                    gameObject.SetActive(false);
                }
            }


            if (MenuControls.Confirm(false))
            {
                blackReady = true;
                blackOptions[0].enabled = false;
                blackOptions[1].enabled = true;
            }

            if (MenuControls.Return(false))
            {
                blackReady = false;
                blackOptions[1].enabled = false;
                blackOptions[0].enabled = true;
            }
        }


        private void Display()
        {
            if (panel.pos == 0)
            {
                GetParentParentParent(blackOptions[0].gameObject).SetActive(false);
            }
            else
            {
                GetParentParentParent(blackOptions[0].gameObject).SetActive(true);
            }

            for (int i = 0; i < options.Length; i++)
            {
                if (i == panel.pos && options[i].color != Color.grey)
                {
                    options[i].color = Color.grey;
                }
                else if (i != panel.pos && options[i].color != Color.white)
                {
                    options[i].color = Color.white;
                }
            }
        }


        private GameObject GetParentParentParent(GameObject child)
        {
            return child.transform.parent.transform.parent.transform.parent.gameObject;
        }
    }
}