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
    public class ArcadeGameMenuCtrl : MonoBehaviour
    {
        [Header("Controls Script")]
        public MenuControls controls;

        [Header("Menu Data")]
        [SerializeField] private GameMenuData[] data;
        [SerializeField] private GameMenuData activeData;

        [Header("Menu Fields")]
        [SerializeField] private Text gameTitle;
        [SerializeField] private Text gameDesc;
        [SerializeField] private Image[] supportedControllers;
        [SerializeField] private GameObject[] topThreeScores;

        private Color32 activeCol = new Color32(90, 200, 130, 255);
        private Color32 inactiveCol = new Color32(200, 95, 90, 255);


        private void Start()
        {
            controls = GetComponent<MenuControls>();

            activeData = data[PlayerPrefs.GetInt("GameSel")];

            gameTitle.text = activeData.GameTitle;
            gameDesc.text = activeData.GameDesc;

            if (activeData.supportedControls[0]) { supportedControllers[0].color = activeCol; }
            else { supportedControllers[0].color = inactiveCol; }
            if (activeData.supportedControls[1]) { supportedControllers[1].color = activeCol; }
            else { supportedControllers[1].color = inactiveCol; }
            if (activeData.supportedControls[2]) { supportedControllers[2].color = activeCol; }
            else { supportedControllers[2].color = inactiveCol; }
        }


        private void Update()
        {

        }
    }
}