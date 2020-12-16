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
        public Sprite gameBanner;
        public string gameAuthor;
        public bool supportArcade;
        public bool supportController;
        public bool supportContKey;
        public bool supportKeyboard;
    }
}