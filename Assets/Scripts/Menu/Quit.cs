using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class Quit : MonoBehaviour
    {
        public void CloseApplication()
        {
            Application.Quit();
        }
    }
}