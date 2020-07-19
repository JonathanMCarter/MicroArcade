using CarterGames.Utilities;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/


namespace CarterGames.Crushing.Menu
{
    public class SettingsPlayerPreview : MonoBehaviour
    {
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private ColourPicker picker;
        [SerializeField] private bool isPlayerColour;

        private Image playerPreviewImage;
        private Sprite playerShape;
        private SettingsScript settingsScript;


        private void Start()
        {
            settingsScript = GameObject.FindGameObjectWithTag("SettingsCtrl").GetComponent<SettingsScript>();

            playerPreviewImage = GetComponent<Image>();

            if (isPlayerColour)
            {
                playerPreviewImage.color = Converters.ConvertFloatArrayToColor(settingsScript.gameData.playerColour);

                if (settingsScript.gameData.playerShapeChoice > 0)
                {
                    playerShape = ExtraSerialize.SpriteDeSerialize(settingsScript.gameData.playerShapeSprite);
                    playerPreviewImage.sprite = playerShape;
                }
                else
                {
                    playerPreviewImage.sprite = defaultSprite;
                    Debug.LogWarning("fff");
                }
            }
            else
            {
                playerPreviewImage.color = Converters.ConvertFloatArrayToColor(settingsScript.gameData.crusherColour);
            }
        }


        private void Update()
        {
            if ((isPlayerColour) && (playerPreviewImage.sprite != playerShape))
            {
                playerPreviewImage.sprite = playerShape;
            }
        }


        /// <summary>
        /// Sets the shape of that is selected... (this is called via UI buttons, so no references in code).
        /// </summary>
        /// <param name="input">The Sprite that is to be set.</param>
        public void SetShape(Sprite input)
        {
            playerShape = input;
            settingsScript.gameData.playerShapeSprite = ExtraSerialize.SpriteSerialize(input);
        }
    }
}