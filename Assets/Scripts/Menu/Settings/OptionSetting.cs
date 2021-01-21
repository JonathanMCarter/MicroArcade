using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Settings
{
    public class OptionSetting : MonoBehaviour
    {
        public Settings settingToEdit;
        public ColoursIcons coloursIcons;

        [SerializeField] private int settingValue;
        [SerializeField] private Image[] ticks;
        [SerializeField] private Canvas popup;

        private GetSettings settings;


        private void Start()
        {
            settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<GetSettings>();
            settingValue = settings.ReadIntValue(settingToEdit);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            for (int i = 1; i < ticks.Length + 1; i++)
            {
                if (i.Equals(settingValue))
                {
                    ticks[i - 1].color = coloursIcons.colours[1];
                    ticks[i - 1].GetComponentsInChildren<Image>()[1].sprite = coloursIcons.icons[1];
                }
                else
                {
                    ticks[i - 1].color = coloursIcons.colours[0];
                    ticks[i - 1].GetComponentsInChildren<Image>()[1].sprite = coloursIcons.icons[0];
                }
            }
        }


        public void EnableOptions()
        {
            popup.enabled = true;
        }


        public void SetOption(int value)
        {
            switch (settingToEdit)
            {
                case Settings.arcade_Visuals_QualityLevel:
                    settingValue = value;
                    settings.WriteValue(settingToEdit, value);
                    break;
                default:
                    break;
            }

            UpdateDisplay();
            settings.SaveSettings();
        }
    }
}