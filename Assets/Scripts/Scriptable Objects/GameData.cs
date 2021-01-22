using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Arcade/Game Data")]
    public class GameData : ScriptableObject
    {
        public string gameName;
        [TextArea]
        public string gameDesc;
        public string gameAuthor;
        public Sprite[] gameScreenshots;
    }
}