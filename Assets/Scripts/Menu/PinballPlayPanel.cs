using UnityEngine;
using CarterGames.Arcade.UserInput;
using UnityEngine.UI;
using CarterGames.Arcade.Saving;

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
                KeepWithinBounds(3, 99);
            }

            if (MenuControls.Right() && !panel.isCoR)
            {
                lives += 1;
                StartCoroutine(panel.ChangeValueCooldown());
                KeepWithinBounds(3, 99);
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
                UltimatePinballData _data = new UltimatePinballData();
                _data.LastGameTypeSelected = 1;
                _data.LastGameTypeAmountSelected = lives;
                SaveManager.SaveUltimatePinball(_data);
                aGMC.transitions.SetBool("ChangeScene", true);
                StartCoroutine(panel.ChangeScene(sceneName));
            }
        }

        private void KeepWithinBounds(int min, int max)
        {
            if (lives > max)
            {   
                lives = max;
            }   
                
            if (lives < min)
            {   
                lives = min;
            }
        }
    }
}