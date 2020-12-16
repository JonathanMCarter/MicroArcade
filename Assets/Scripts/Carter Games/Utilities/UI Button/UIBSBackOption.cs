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
    public class UIBSBackOption : MonoBehaviour
    {
        [SerializeField] private UnityEvent backAction;
        private UIButtonSwitch uibs;


        private void Awake()
        {
            uibs = GetComponent<UIButtonSwitch>();
        }


        private void Update()
        {
            if (uibs.enabled && uibs.action != null && !uibs.isCoR)
            {
                if (uibs.action.Menu.Cancel.phase.Equals(InputActionPhase.Performed))
                {
                    backAction.Invoke();
                }
            }
        }
    }
}