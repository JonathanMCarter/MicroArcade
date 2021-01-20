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
    public class GamePanel : MonoBehaviour
    {
        private TextMeshProUGUI _gameTitle;
        private Image _gameBanner;
        public GameData gameInfo;


        private void Start()
        {
            // Set UI
            _gameTitle = GetComponentInChildren<TextMeshProUGUI>();
            _gameBanner = GetComponentsInChildren<Image>()[3];


            if (gameInfo)
            {
                // Set values
                _gameTitle.text = gameInfo.gameName;
                _gameBanner.sprite = gameInfo.gameBanner;
            }
            else
                _gameTitle.text = string.Empty;
        }
    }
}