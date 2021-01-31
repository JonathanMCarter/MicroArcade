using UnityEngine;
using UnityEngine.UI;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        private CameraShakeScript camshake;
        private Rigidbody2D rb;
        internal Scoring score;
        public int health;
        public float moveSpd;
        public Slider healthBar;


        private void OnEnable()
        {
            health = Rand.Int(20, 40);
            moveSpd = Rand.Float(.75f, 1.5f);


            if (rb)
                rb.velocity = Vector2.down * moveSpd;

            if (healthBar)
            {
                healthBar.maxValue = health;
                healthBar.value = health;
            }
        }


        private void Start()
        {
            health = Rand.Int(20, 40);

            if (healthBar)
                healthBar.maxValue = health;

            camshake = FindObjectOfType<CameraShakeScript>();
            rb = GetComponent<Rigidbody2D>();
            score = FindObjectOfType<Scoring>();
            rb.velocity = Vector2.down * moveSpd;
        }



        public virtual void Update()
        {
            if (healthBar)
                if (!healthBar.value.Equals(health))
                    healthBar.value = health;

            if (health <= 0)
            {
                gameObject.SetActive(false);
                score.IncrementScore(50);
            }
        }


        public void TakeDamage(int dmg)
        {
            health -= dmg;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                camshake.ShakeCamera(true, 0.01f, 0.025f);
            }
        }
    }
}