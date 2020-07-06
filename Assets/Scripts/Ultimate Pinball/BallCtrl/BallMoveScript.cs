using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade;

namespace Pinball.BallCtrl
{
    public class BallMoveScript : MonoBehaviour
    {
        /// <summary>
        /// The spped the ball moves at
        /// </summary>
        [Header("The speed added to the ball velocity")]
        public float BallSpd;

        /// <summary>
        /// The starting vector for the ball
        /// </summary>
        [Header("The ball's vector for movement (normalized)")]
        public Vector2 StartVec;

        /// <summary>
        /// Bool's used for the gravity switching
        /// </summary>
        [Header("Boolenas controling gravity switching")]
        public bool HasSwitchGravity, JustSpawned;

        public Joysticks LastHit;


        public bool IsIn;

        float C;

        /// <summary>
        /// Reference to the ball gamemanager, used for scoring 'n' stuff
        /// </summary>
        GameManager GM;
        AudioManager AM;

        // When Spawned into the game
        void Awake()
        {
            // References the ball gamemanger when it is first made, only runs once in theory
            GM = FindObjectOfType<GameManager>();
            AM = FindObjectOfType<AudioManager>();
        }

        // When Ball is enabled (from OBJ-POOL)
        void OnEnable()
        {
            // Stops the gravity switcher from flipping gravity on the ball when its first made
            JustSpawned = true;

            if (!IsIn)
            {
                // runs the ball init function which sets up the starting vector and gravity
                BallInit();
            }
            else
            {
                IsIn = false;
            }

            // starts the cooldown corutine which toggles thr JustSpawned bool to false after 1 second
            StartCoroutine(SpawnCoolDown());
            // sets the last hit no none
            LastHit = Joysticks.None;

            C = 2 * Mathf.PI * 1.3f;
        }



        private void Update()
        {
            Vector3 Axis = Vector3.Cross(GetComponent<Rigidbody2D>().velocity, Vector3.up);
            float Angle = (GetComponent<Rigidbody2D>().velocity.magnitude * 360 / C);
            transform.GetChild(0).transform.Rotate(Axis, Angle * Time.deltaTime / 4, Space.World);
        }


        private void FixedUpdate()
        {
            if (transform.position.y > 10.04f)
            {
                GetComponent<Rigidbody2D>().gravityScale = -1;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<BallMoveScript>())
            {
                switch (collision.gameObject.GetComponent<BallMoveScript>().LastHit)
                {
                    case Joysticks.White:
                        GM.Player1Stats.Score += 100;
                        AM.Play("BumperHit", .05f, 1.2f);
                        break;
                    case Joysticks.Black:
                        GM.Player2Stats.Score += 100;
                        AM.Play("BumperHit", .05f, 1.2f);
                        break;
                    case Joysticks.None:
                        break;
                    default:
                        break;
                }
            }
        }


        // When the ball leaves a trigger box
        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<GravitySwitcher>())
            {
                // Resets the flipping gravity boolean so it can be flipped again if needed
                HasSwitchGravity = false;
            }
        }

        /// <summary>
        /// Ball startup that sets the start vector and gravity up
        /// </summary>
        public void BallInit()
        {
            // Sets the start vector up
            StartVec = new Vector2(0, 0);

            // Sorts out the gravity dependant on the start vector's y axis value
            if (transform.position.y > 10f)
            {
                GetComponent<Rigidbody2D>().gravityScale = -1;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            // sets the velocity to the start vector * the ball speed
            GetComponent<Rigidbody2D>().velocity = StartVec * BallSpd;
        }

        /// <summary>
        /// Corutine which sets the JustSpawned bool to false after 1 second
        /// </summary>
        IEnumerator SpawnCoolDown()
        {
            yield return new WaitForSeconds(1);
            JustSpawned = false;
            StopCoroutine(SpawnCoolDown());
        }


        /// <summary>
        /// Stops the ball from moving by disabling it (Used in BG_GameManager)
        /// </summary>
        internal void StopBallMoving()
        {
            gameObject.SetActive(false);
        }
    }
}