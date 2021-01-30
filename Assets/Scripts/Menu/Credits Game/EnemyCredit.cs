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
        private Text text;


        private void Awake()
        {
            // Set name and title
            text = GetComponentInChildren<Text>();
        }


        public override void Update()
        {
            if (text.text.Equals(""))
            {
                text.text = string.Format("{0}\n{1}", title, desc);
            }

            if (base.health <= 0)
                base.score.IncrementScore(50);

            base.Update();
        }
    }
}