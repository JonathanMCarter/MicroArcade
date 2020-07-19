using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class GravitySwitcher : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((collision.gameObject.GetComponent<BallMoveScript>()) && (!collision.gameObject.GetComponent<BallMoveScript>().HasSwitchGravity) && (!collision.gameObject.GetComponent<BallMoveScript>().JustSpawned))
            {
                Debug.Log("flipping gravity" + " : " + collision.gameObject.name);

                // Flips gravity to be inverted ( -1 )
                if (collision.gameObject.GetComponent<Rigidbody2D>().gravityScale > 0)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
                    collision.gameObject.GetComponent<BallMoveScript>().HasSwitchGravity = true;
                }
                // Flips gravity to standard ( 1 )
                else
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    collision.gameObject.GetComponent<BallMoveScript>().HasSwitchGravity = true;
                }
            }
        }

    }
}