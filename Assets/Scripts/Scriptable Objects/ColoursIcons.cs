using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade
{
    [CreateAssetMenu(fileName = "Colour & Icons", menuName = "Arcade/Colours & Icons")]
    public class ColoursIcons : ScriptableObject
    {
        public Color[] colours;
        public Sprite[] icons;
    }
}