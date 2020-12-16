using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class UIBSButtonActions : MonoBehaviour
    {
        [Header("Actions to perform.")]
        [Tooltip("A grouping of events to run on confirm.")]
        public UnityEvent action;
    }
}