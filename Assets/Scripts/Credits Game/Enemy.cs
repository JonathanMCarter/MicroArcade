using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        public int health;

        private void Start()
        {
            healthBar.maxValue = health;
        }


        private void Update()
        {
            if (!healthBar.value.Equals(health))
                healthBar.value = health;

            if (health <= 0)
                gameObject.SetActive(false);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                collision.gameObject.SetActive(false);
                --health;
            }
        }
    }
}