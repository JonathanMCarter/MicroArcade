using UnityEngine;
using UnityEngine.AddressableAssets;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Powerups : MonoBehaviour
    {
        [SerializeField] private float moveSpd = default;
        private Rigidbody2D rb;
        internal PowerupSpawner powerupSpawner;

        public enum Powerup { HeavyBullet, Heal, Speed };
        public Powerup powerup;


        private void OnDisable()
        {
            rb.velocity = Vector2.zero;
        }


        private void OnEnable()
        {
            if (rb)
                rb.velocity = Vector2.down * moveSpd * Time.deltaTime;
        }


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * moveSpd * Time.deltaTime;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                switch (powerup)
                {
                    case Powerup.HeavyBullet:
                        collision.GetComponent<Player>().EnableHeavyBullets();
                        break;
                    case Powerup.Heal:
                        collision.GetComponent<Player>().AddToHealth(1);
                        break;
                    case Powerup.Speed:
                        break;
                    default:
                        break;
                }

                powerupSpawner.DespawnPowerup(this.gameObject);
            }
        }
    }
}