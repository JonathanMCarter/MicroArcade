using UnityEngine;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Asteroid : Enemy
    {
        private void Awake()
        {
            transform.localScale = Vector3.one;
            transform.localScale = Rand.Vector3MatchXY(.1f, .75f);
        }
    }
}