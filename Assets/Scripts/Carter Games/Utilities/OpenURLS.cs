using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class OpenURLS : MonoBehaviour
    {
        public void OpenCarterGamesWebsite()
        {
            Application.OpenURL("https://carter.games");
        }
    }
}