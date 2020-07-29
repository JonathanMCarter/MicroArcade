using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public interface IMultiLayerMenu
    {
        string[,] SceneNames { get; set; }
        GameObject[,] SceneObjects { get; set; }
        Animator SceneTransition { get; set; }
        void UpdateDisplay();
    }
}