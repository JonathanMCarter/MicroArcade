using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade
{
    public class GamePanelPopup : MonoBehaviour
    {
        [Header("Display Fields")]
        [SerializeField] private TMP_Text _gameTitle;
        [SerializeField] private TMP_Text _gameDescription;
        [SerializeField] private TMP_Text _gameAuthor;
        [SerializeField] private Image[] _gameScreenshots;

        private const string devStart = "Developed By:";

        [Header("Active Game Data")]
        public GameData gameInfo;

        [Header("UIBS")]
        [SerializeField] private GameData[] allData;
        [SerializeField] private Utilities.UIButtonSwitch uIButtonSwitch;

        [Header("No Games Display")]
        [SerializeField] private Canvas display;
        [SerializeField] private Canvas noDisplay;


        private void Start()
        {
            if (gameInfo)
                UpdateValues();
            else
                DisableDisplay();
        }


        private void UpdateValues()
        {
            if (gameInfo != null)
            {
                // Set values
                _gameTitle.text = gameInfo.gameName;
                _gameDescription.text = gameInfo.gameDesc;
                _gameAuthor.text = string.Format("{0} {1}", devStart, gameInfo.gameAuthor);

                if (gameInfo.gameScreenshots != null && gameInfo.gameScreenshots.Length > 0 && gameInfo.gameScreenshots.Length.Equals(2))
                {
                    _gameScreenshots[0].sprite = gameInfo.gameScreenshots[0];
                    _gameScreenshots[1].sprite = gameInfo.gameScreenshots[1];
                }
            }
            else
            {
                DisableDisplay();
            }
        }


        private void DisableDisplay()
        {
            noDisplay.enabled = true;
            display.enabled = false;
        }

        /// <summary>
        /// Method | Sets the game data to the input so the popup is correct xD.
        /// </summary>
        public void SetGameData(GameData data)
        {
            gameInfo = data;
            UpdateValues();
        }


        /// <summary>
        /// Method | Sets the game data to the input so the popup is correct xD.
        /// </summary>
        public void SetGameDataViaUIBS()
        {
            if (allData.Length > 0)
            {
                gameInfo = allData[uIButtonSwitch.pos];
                UpdateValues();
            }
        }
    }
}