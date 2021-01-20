using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Settings
{
    public class ToggleSetting : MonoBehaviour
    {
        public Settings settingToEdit;

        public ColoursIcons buttonIcons;

        private Image[] images;
        private GetSettings getSettings;


        private void Start()
        {
            images = GetComponentsInChildren<Image>();
            getSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<GetSettings>();
            SetButtonValue();
        }


        public void BoolToggleSetting()
        {
            if (getSettings.ReadBoolValue(settingToEdit).Equals(false))
            {
                getSettings.WriteValue(settingToEdit, true);
                SetButtonValue();
                getSettings.SaveSettings();
            }
            else
            {
                getSettings.WriteValue(settingToEdit, false);
                SetButtonValue();
                getSettings.SaveSettings();
            }
        }


        private void SetButtonValue()
        {
            if (getSettings.ReadBoolValue(settingToEdit))
            {
                images[1].color = buttonIcons.colours[1];
                images[2].sprite = buttonIcons.icons[1];
            }
            else
            {
                images[1].color = buttonIcons.colours[0];
                images[2].sprite = buttonIcons.icons[0];
            }
        }
    }
}