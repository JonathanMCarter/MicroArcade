using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class EnemyCredit : Enemy
    {
        public string title;
        public string desc;


        private void Awake()
        {
            // Set name and title
            GetComponentInChildren<Text>().text = string.Format("{0}\n{1}", title, desc);
        }
    }
}