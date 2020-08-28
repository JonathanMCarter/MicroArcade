using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public static class Comparators
    {
        public static bool Color32Check(Color32 colourA, Color32 colourB)
        {
            if ((colourA.r == colourB.r) && (colourA.g == colourB.g) && (colourA.b == colourB.b) && (colourA.a == colourB.a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool ColorVsFloatArrayCheck(Color col, float[] array)
        {
            if ((col.r == array[0]) && (col.g == array[1]) && (col.b == array[2]) && (col.a == array[3]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}