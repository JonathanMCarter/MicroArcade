using UnityEngine;
using CarterGames.MAExtras;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public interface IMultiLayerMenu
    {
        StringAoA[] SceneMethods { get; set; }
        GameObjectAoA[] SceneObjects { get; set; }
        Animator SceneTransition { get; set; }
        short ActiveTier { get; set; }
        void UpdateDisplay();
    }
}