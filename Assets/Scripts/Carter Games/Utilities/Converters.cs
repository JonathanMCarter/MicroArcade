using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public static class Converters
    {
        /// <summary>
        /// Converts a color to its 0-1 values
        /// </summary>
        /// <param name="toConvert">Colour to convert</param>
        /// <returns>result of method</returns>
        public static Color ConvertColor(Color toConvert)
        {
            return new Color(toConvert.r / 255f, toConvert.g / 255f, toConvert.b / 255f, toConvert.a / 255f);
        }

        /// <summary>
        /// Converts an int[] into a colour to use
        /// </summary>
        /// <param name="input">Array to convert</param>
        /// <returns>new colour</returns>
        public static Color ConvertFloatArrayToColor(float[] input)
        {
            if (input.Length == 4)
            {
                return new Color(input[0], input[1], input[2], input[3]);
            }
            else
            {
                Debug.LogWarning("Did not have enough to create new colour: " + input.Length);
                return Color.clear;
            }
        }

        
        public static float[] ConvertColorToFloatArray(Color input)
        {
            return new float[4] { input.r, input.g, input.b, input.a };
        }


        public static Gradient ConvertColourToParticleSystemGradient(Color col)
        {
            Gradient _newGradient = new Gradient();
            _newGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(col, 0.0f), new GradientColorKey(col, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) });
            return _newGradient;
        }
    }
}
