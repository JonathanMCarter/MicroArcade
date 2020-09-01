using CarterGames.Arcade.Saving;
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
    public class StarshinePlayPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] player1Options;
        [SerializeField] private GameObject[] player2Options;

        [SerializeField] private Image[] whiteOptions;
        [SerializeField] private Image[] blackOptions;

        public bool whiteReady;
        public bool blackReady;

        private Panel panel;
        private Panel panel2;
        private ArcadeGameMenuCtrl aGMC;
        private const string sceneName = "Operation-Starshine-Level";


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
            panel = new Panel();
            panel2 = new Panel();

            panel.BaseSetup();
            panel.items = player1Options;
            panel.maxPos = 4;

            panel2.BaseSetup();
            panel2.items = player2Options;
            panel2.maxPos = 4;
        }


        private void Update()
        {
            if (!whiteReady)
            {
                // Player 1
                if (MenuControls.Left() && !panel.isCoR)
                {
                    StartCoroutine(panel.MoveAround(-1));
                    panel.Display();
                }

                if (MenuControls.Right() && !panel.isCoR)
                {
                    StartCoroutine(panel.MoveAround(1));
                    panel.Display();
                }
            }

            if (!blackReady)
            {
                // Player 2
                if (MenuControls.Left(false) && !panel2.isCoR)
                {
                    StartCoroutine(panel2.MoveAround(-1));
                    panel2.Display();
                }

                if (MenuControls.Right(false) && !panel2.isCoR)
                {
                    StartCoroutine(panel2.MoveAround(1));
                    panel2.Display();
                }
            }


            // Confirm & Go
            if (MenuControls.Confirm())
            {
                if ((whiteReady) && (blackReady))
                {
                    StartCoroutine(panel.ChangeScene(sceneName));
                }
                else if (!whiteReady)
                {
                    whiteReady = true;
                    whiteOptions[0].enabled = false;
                    whiteOptions[1].enabled = true;
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


            if (whiteReady && blackReady && !panel.isCoR)
            {
                panel.isCoR = true;
                OperationStarshineData data = new OperationStarshineData();
                data.LastPlayer1ShipSelection = panel.pos;
                data.LastPlayer2ShipSelection = panel2.pos;
                SaveManager.SaveOperationStarshine(data);
                aGMC.transitions.SetBool("ChangeScene", true);
                StartCoroutine(panel.ChangeScene(sceneName));
            }
        }
    }
}