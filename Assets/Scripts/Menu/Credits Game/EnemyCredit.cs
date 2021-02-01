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

        private bool handleRunning;
        private PowerupSpawner powerupSpawner;


        private void Awake()
        {
            // Set name and title
            text = GetComponentInChildren<Text>();
            powerupSpawner = FindObjectOfType<PowerupSpawner>();
        }


        public override void Update()
        {
            if (text.text.Equals(""))
            {
                text.text = string.Format("{0}\n{1}", title, desc);
            }

            if (healthBar)
                if (!healthBar.value.Equals(health))
                    healthBar.value = health;

            if (base.health <= 0)
            {
                if (!handleRunning)
                {
                    handleRunning = true;
                    base.score.IncrementScore(75);
                    powerupSpawner.SpawnPowerup(this.transform);
                    gameObject.SetActive(false);
                }   
            }
        }
    }
}