using UnityEngine;
using CarterGames.Arcade.Settings;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class HighContrast : MonoBehaviour
    {
        internal GetSettings settings;

        public virtual void Start()
        {
            settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<GetSettings>();
        }
    }
}