using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class HighContrastText : HighContrast
    {
        [SerializeField] private Color highContrastCol;
        private Color defaultCol;
        private Text _text;
        private bool isHighCon;


        public override void Start()
        {
            base.Start();

            _text = GetComponent<Text>();
            defaultCol = _text.color;

            if (base.settings.ReadBoolValue(Arcade.Settings.Settings.arcade_Access_IsHighContrast))
            {
                _text.color = highContrastCol;
                isHighCon = true;
            }
        }


        private void Update()
        {
            if (base.settings.ReadBoolValue(Arcade.Settings.Settings.arcade_Access_IsHighContrast) != isHighCon)
            {
                isHighCon = base.settings.ReadBoolValue(Arcade.Settings.Settings.arcade_Access_IsHighContrast);

                switch (isHighCon)
                {
                    case true:
                        _text.color = highContrastCol;
                        break;
                    case false:
                        _text.color = defaultCol;
                        break;
                }
            }
        }
    }
}