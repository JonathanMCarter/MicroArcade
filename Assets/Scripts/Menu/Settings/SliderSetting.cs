using UnityEngine;
using UnityEngine.UI;
using CarterGames.Utilities;
using TMPro;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Settings
{
    public class SliderSetting : MonoBehaviour
    {
        [SerializeField] private UIButtonSwitch uiBS;
        [SerializeField] private float speed = 6f;
        public Settings settingToEdit;

        private float value;
        private GetSettings settings;
        private Slider sliderPrompt;
        private TMP_Text textPrompt;
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


        private void Start()
        {
            settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<GetSettings>();
            sliderPrompt = GetComponentInChildren<Slider>();
            textPrompt = GetComponentsInChildren<TMP_Text>()[1];

            value = settings.ReadFloatValue(settingToEdit);
            sliderPrompt.maxValue = 6.0f;
            textPrompt.text = value.ToString("##.#");
            sliderPrompt.value = value;
        }


        private void Update()
        {
            if (uiBS.buttons[uiBS.pos].Equals(this.gameObject))
            {
                if (actions.Controls.Joystick.ReadValue<Vector2>().x > .1f)
                {
                    value += speed * Time.deltaTime;
                    value = Keep.WithinBounds(value, -30f, 6f);
                    UpdateDisplay();
                }
                else if (actions.Controls.Joystick.ReadValue<Vector2>().x < -.1f)
                {
                    value -= speed * Time.deltaTime;
                    value = Keep.WithinBounds(value, -30f, 6f);
                    UpdateDisplay();
                }
            }
        }


        private void UpdateDisplay()
        {
            textPrompt.text = value.ToString("##.#");
            sliderPrompt.value = value;
            SetOption();
        }


        public void SetOption()
        {
            settings.WriteValue(settingToEdit, value);
        }
    }
}