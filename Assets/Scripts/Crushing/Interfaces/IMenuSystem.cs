using UnityEngine;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public interface IMenuSystem
    {
        List<GameObject> menuOptions { get; set; }
        int position { get; set; }
        void ChangePosition(int newPosition);
    }
}