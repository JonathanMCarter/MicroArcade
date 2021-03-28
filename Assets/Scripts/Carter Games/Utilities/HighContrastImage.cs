using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class HighContrastImage : HighContrast
    {
        [SerializeField] private Color highContrastCol;
        private Color defaultCol;
        private Image _image;
        private bool isHighCon;

        public override void Start()
        {
            base.Start();

            _image = GetComponent<Image>();
            defaultCol = _image.color;
            
            if (base.settings.ReadBoolValue(Arcade.Settings.Settings.arcade_Access_IsHighContrast))
            {
                _image.color = highContrastCol;
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
                        _image.color = highContrastCol;
                        break;
                    case false:
                        _image.color = defaultCol;
                        break;
                }
            }
        }
    }
}