/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Arcade.Menu
{
    public class ExitMicroArcade : MonoBehaviour
    {
        [SerializeField] private bool shouldOpenQuitGame;
        [SerializeField] private GameObject[] quitOptions;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool isPopupOpen;
        [SerializeField] private Color[] colors;


        private void Start()
        {
        }


        private void Update()
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

            }
        }


        public void OpenQuitPopup()
        {
            isPopupOpen = true;
        }


        private void CloseQuitPopup()
        {
            isPopupOpen = false;
        }
    }
}