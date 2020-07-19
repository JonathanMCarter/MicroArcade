using UnityEngine;
using CarterGames.Crushing.Saving;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class Init : MonoBehaviour
    {
        private void Awake()
        {
            SaveManager.Init();
        }
    }
}