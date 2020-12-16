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
        [SerializeField] private TextMeshProUGUI _gameTitle;
        [SerializeField] private TextMeshProUGUI _gameAuthor;
        [SerializeField] private Color _green;
        [SerializeField] private Color _red;
        [SerializeField] private Image[] _gameSupport;

        public GameData gameInfo;


        private void Start()
        {
            if (gameInfo)
            {
                UpdateValues();
            }
            else
            {
                _gameTitle.text = "-----";
            }
        }


        private void UpdateValues()
        {
            // Set values
            _gameTitle.text = gameInfo.gameName;
            _gameAuthor.text = gameInfo.gameAuthor;


            if (gameInfo.supportArcade)
                _gameSupport[0].color = _green;
            else
                _gameSupport[0].color = _red;


            if (gameInfo.supportController)
                _gameSupport[1].color = _green;
            else
                _gameSupport[1].color = _red;


            if (gameInfo.supportContKey)
                _gameSupport[2].color = _green;
            else
                _gameSupport[2].color = _red;


            if (gameInfo.supportKeyboard)
                _gameSupport[3].color = _green;
            else
                _gameSupport[3].color = _red;
        }


        /// <summary>
        /// Sets the game data to the input so the popup is correct xD.
        /// </summary>
        /// <param name="_newData">GameData to set to.</param>
        public void SetGameData(GameData _newData)
        {
            gameInfo = _newData;
            UpdateValues();
        }
    }
}