using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public interface IMenuSceneChanger
    {
        string[] SceneNames { get; set; }
        GameObject[] SceneOptions { get; set; }
        Animator SceneTransition { get; set; }
        void UpdateDisplay();
    }
}