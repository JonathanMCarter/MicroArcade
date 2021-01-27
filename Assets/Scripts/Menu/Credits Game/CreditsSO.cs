using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    [CreateAssetMenu(fileName = "Credits SO", menuName = "Arcade/Credits SO")]
    public class CreditsSO : ScriptableObject
    {
        public string[] roles;
        public string[] names;
    }
}