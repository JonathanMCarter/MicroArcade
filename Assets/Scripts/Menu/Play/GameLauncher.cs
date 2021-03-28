﻿using UnityEngine;
using System.Diagnostics;
/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public static class GameLauncher
    {
        public static void Launch_UltimatePinball()
        {
            string path = Application.dataPath + "/../Builds/Ultimate Pinball/Ultimate Pinball.exe";
            Process.Start(path);
        }
    }
}