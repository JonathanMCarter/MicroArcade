using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public interface IMenuSystem
    {
        int lastPos { get; set; }
        int currentPos { get; set; }
        int maxPos { get; set; }
        bool inputReady { get; set; }
        bool isCoR { get; set; }
        bool Confirm();
        bool Return();
    }
}