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
        [SerializeField] private Slider healthBar;
        private CameraShakeScript camshake;
        private Rigidbody2D rb;
        public int health;
        public float moveSpd;


        private void Start()
        {
            healthBar.maxValue = health;
            camshake = FindObjectOfType<CameraShakeScript>();
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * moveSpd;
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
                camshake.ShakeCamera(true, 0.05f, 0.05f);
            }
        }
    }
}