using UnityEngine;
using CarterGames.Arcade.UserInput;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class PinballPlayPanel : MonoBehaviour
    {
        [SerializeField] private Text livesText;
        [SerializeField] private Image[] whiteOptions;
        [SerializeField] private Image[] blackOptions;

        private Panel panel;
        private ArcadeGameMenuCtrl aGMC;
        private const string sceneName = "Ultimate-Pinball-Level";
        public int lives;

        public bool readyStage;
        public bool whiteReady, blackReady;


        private void Start()
        {
            aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
            panel = new Panel();

            panel.BaseSetup();
            lives = 3;
        }


        private void Update()
        {
            livesText.text = lives.ToString();


            if (MenuControls.Left() && !panel.isCoR)
            {
                lives -= 1;
                StartCoroutine(panel.ChangeValueCooldown());
                panel.KeepWithinBounds(lives, 3, 99);
            }

            if (MenuControls.Right() && !panel.isCoR)
            {
                lives += 1;
                StartCoroutine(panel.ChangeValueCooldown());
                panel.KeepWithinBounds(lives, 3, 99);
            }


            if (MenuControls.Return())
            {
                if (!readyStage)
                {
                    aGMC.playPanelActive = false;
                    gameObject.SetActive(false);
                }
                else if (whiteReady)
                {
                    whiteReady = false;
                    whiteOptions[1].enabled = false;
                    whiteOptions[0].enabled = true;
                }
                else
                {
                    readyStage = false;
                }
            }


            if (MenuControls.Confirm())
            {
                if ((readyStage) && (whiteReady) && (blackReady))
                {
                    StartCoroutine(panel.ChangeScene(sceneName));
                }
                else if (readyStage && !whiteReady)
                {
                    whiteReady = true;
                    whiteOptions[0].enabled = false;
                    whiteOptions[1].enabled = true;
                }
                else if (!readyStage)
                {
                    readyStage = true;
                }
            }


            if (MenuControls.Confirm(false) && readyStage)
            {
                blackReady = true;
                blackOptions[0].enabled = false;
                blackOptions[1].enabled = true;
            }

            if (MenuControls.Return(false) && readyStage)
            {
                blackReady = false;
                blackOptions[1].enabled = false;
                blackOptions[0].enabled = true;
            }
        }
    }
}