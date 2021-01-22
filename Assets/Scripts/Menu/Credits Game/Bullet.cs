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
        private Rigidbody2D rB;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start Method | Makes the bullet move 'n' all.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            if (rB)
                rB.velocity = Vector2.right * bulletSpd;
            else
            {
                rB = GetComponent<Rigidbody2D>();
                rB.velocity = Vector2.right * bulletSpd;
            }
        }
    }
}