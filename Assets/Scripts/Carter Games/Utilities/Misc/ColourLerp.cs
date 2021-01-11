using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class ColourLerp : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float speed;

        private float hue;
        private float H;
        private float S;
        private float V;


        // Start is called before the first frame update
        private void Start()
        {
            Color.RGBToHSV(_image.color, out H, out S, out V);
            hue = H;
        }

        // Update is called once per frame
        private void Update()
        {
            if (hue >= 1)
            {
                hue = 0;
            }
            else
            {
                hue += Time.deltaTime / speed;
            }

            _image.color = Color.HSVToRGB(hue, S, V);
        }
    }
}