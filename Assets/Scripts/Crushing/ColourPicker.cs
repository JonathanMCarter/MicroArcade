using CarterGames.Assets.AudioManager;
using CarterGames.Crushing.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class ColourPicker : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SettingsScript settingsScript;
        [SerializeField] private bool isPlayerPicker;
        [SerializeField] private Image previewImage;
        [SerializeField] private Image colourTexture;

        private Image point;
        private AudioManager audioManager;
        private Color newColour;


        private void Start()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            point = transform.GetChild(0).GetComponent<Image>();
            settingsScript = GameObject.FindGameObjectWithTag("SettingsCtrl").GetComponent<SettingsScript>();


            if (isPlayerPicker)
            {
                newColour = Converters.ConvertFloatArrayToColor(settingsScript.gameData.playerColour);
            }
            else
            {
                newColour = Converters.ConvertFloatArrayToColor(settingsScript.gameData.crusherColour);
            }


            previewImage.color = newColour;


            if (isPlayerPicker)
            {
                point.rectTransform.localPosition = ExtraSerialize.Vector3DeSerialize(settingsScript.gameData.playerPipPosition);
                point.enabled = true;
            }
            else
            {
                point.rectTransform.localPosition = ExtraSerialize.Vector3DeSerialize(settingsScript.gameData.crusherPipPosition);
                point.enabled = true;
            }
        }


        /// <summary>
        /// Controls the colour picker location
        /// </summary>
        public void OnPointerClick(PointerEventData ED)
        {
            Vector2 localCursor;
            var rect1 = GetComponent<RectTransform>();
            var pos1 = ED.position;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1, null, out localCursor))
            {
                return;
            }

            int xpos = (int)(localCursor.x);
            int ypos = (int)(localCursor.y);

            if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
            else xpos += (int)rect1.rect.width / 2;

            if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
            else ypos += (int)rect1.rect.height / 2;

            Debug.Log("Correct Cursor pos: " + xpos + " " + ypos);

            Color CheckColour = colourTexture.sprite.texture.GetPixel(xpos, ypos);

            if (CheckColour.a == 1)
            {
                newColour = CheckColour;
                point.rectTransform.localPosition = new Vector3(xpos - (rect1.rect.width / 2), ypos - (rect1.rect.height / 2), 0);
                previewImage.color = newColour;
                audioManager.Play("MenuButton", .5f);
            }
        }


        /// <summary>
        /// Get the current colour that is selected by the pip...
        /// </summary>
        /// <returns>The colour that is selected.</returns>
        public Color GetColourPicked()
        {
            if (newColour.a != 0)
            {
                return newColour;
            }
            else
            {
                if (isPlayerPicker)
                {
                    return Converters.ConvertFloatArrayToColor(settingsScript.gameData.playerColour);
                }
                else
                {
                    return Converters.ConvertFloatArrayToColor(settingsScript.gameData.crusherColour);
                }
            }
        }


        /// <summary>
        /// Get the current position of the pip...
        /// </summary>
        /// <returns>The position of the pip as a Vector3.</returns>
        public Vector3 GetPipPosition()
        {
            if (point)
            {
                return point.rectTransform.localPosition;
            }
            else
            {
                if (isPlayerPicker)
                {
                     return ExtraSerialize.Vector3DeSerialize(settingsScript.gameData.playerPipPosition);
                }
                else
                {
                     return ExtraSerialize.Vector3DeSerialize(settingsScript.gameData.crusherPipPosition);
                }
            }
        }
    }
}