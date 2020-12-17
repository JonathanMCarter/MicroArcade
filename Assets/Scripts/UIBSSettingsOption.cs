using UnityEngine;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade
{
    public class UIBSSettingsOption : MonoBehaviour
    {
        [SerializeField] private UIButtonSwitch uibs;

        [SerializeField] private bool isToggle;
        [SerializeField] private bool isSlider;
        private int posInArray;
        private Actions actions;


        private void OnEnable()
        {
            actions = new Actions();
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }


        public void SettingAction()
        {
            if (isSlider && uibs.pos.Equals(posInArray))
            {
                if (actions.Menu.Movement.ReadValue<Vector2>().x > .1f)
                {

                }
                else if (actions.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                {

                }
            }
        }


        private int GetPos()
        {
            for (int i = 0; i < uibs.buttons.Length; i++)
            {
                if (uibs.buttons[i].Equals(this.gameObject))
                {
                    return i;
                }
            }

            return 0;
        }


        private void Start()
        {
            posInArray = GetPos();
        }
    }
}