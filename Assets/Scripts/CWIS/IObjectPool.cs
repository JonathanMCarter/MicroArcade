using System.Collections.Generic;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public interface IObjectPool<T>
    {
        int objectLimit { get; set; }
        List<T> objectPool { get; set; }
    }
}