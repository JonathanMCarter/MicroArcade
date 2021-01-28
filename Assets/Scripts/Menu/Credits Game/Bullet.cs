using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    /// <summary>
    /// Class | controls the bullet speed.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpd;
        [SerializeField] private ParticleSystem hitParticles;
        private Rigidbody2D rB;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start Method | Makes the bullet move 'n' all.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            if (rB)
                rB.velocity = Vector2.up * bulletSpd;
            else
            {
                rB = GetComponent<Rigidbody2D>();
                rB.velocity = Vector2.up * bulletSpd;
            }

            if (!hitParticles)
                hitParticles = GetComponentInChildren<ParticleSystem>();

            GetComponentInChildren<BoxCollider2D>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                rB.velocity = Vector2.zero;
                GetComponentInChildren<BoxCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                hitParticles.Play();
            }
        }
    }
}