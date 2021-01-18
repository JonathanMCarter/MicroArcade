using UnityEngine;
using System.Collections;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class Missile : MonoBehaviour
    {
        [SerializeField] private float missileSpd;
        [SerializeField] private float delayBeforeMovement;
        private Rigidbody2D rB;
        private WaitForSeconds wait;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start Method | Makes the missile move 'n' all.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            wait = new WaitForSeconds(delayBeforeMovement);

            if (rB)
            {
                rB.velocity = Vector2.right * missileSpd;
                StartCoroutine(MissileFire());
            }
            else
            {
                rB = GetComponent<Rigidbody2D>();
                StartCoroutine(MissileFire());
            }
        }


        private IEnumerator MissileFire()
        {
            int dir = GetRandom.Int(0, 1);

            if (dir.Equals(0))
                rB.velocity = Vector2.down * missileSpd;
            else
                rB.velocity = Vector2.up * missileSpd;

            yield return wait;

            rB.velocity = Vector2.right * missileSpd;

        }
    }
}