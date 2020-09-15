using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;
using CarterGames.Utilities;

namespace CarterGames.UltimatePinball.BallCtrl
{
    /// <summary>
    /// CLASS | Controls the ball movement in the scene.
    /// </summary>
    public class BallMoveScript : MonoBehaviour
    {
        // Shown in the inspector
        [Header("The speed added to the ball velocity")]
        [SerializeField] private float BallSpd;

        [Header("Last Hit By")]
        [Tooltip("The last player to hit this ball instance.")]
        [SerializeField] internal Joysticks LastHit;

        // Privates
        private float curve;
        private WaitForSeconds wait = new WaitForSeconds(1);
        private GameManager gameManager;
        private AudioManager audioManager;
        private SpriteRenderer spriteR;

        // Internals
        internal bool IsIn;
        internal bool HasSwitchGravity;
        internal bool JustSpawned;

        // Publics (also showin in inspector unless defined otherwise
        [HideInInspector][Header("The ball's vector for movement (normalized)")]
        public Vector2 StartVec;


        // When Spawned into the game
        private void Awake()
        {
            // References the ball gamemanger when it is first made, only runs once in theory
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
            spriteR = GetComponentInChildren<SpriteRenderer>();
        }


        // When Ball is enabled (from OBJ-POOL)
        private void OnEnable()
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

            curve = 2 * Mathf.PI * 1.3f;
        }


        private void OnDisable()
        {
            StopAllCoroutines();
        }



        private void Update()
        {
            // keeps the ball facing in the right direction and rolling when it is rolling.
            Vector3 Axis = Vector3.Cross(GetComponent<Rigidbody2D>().velocity, Vector3.up);
            float Angle = (GetComponent<Rigidbody2D>().velocity.magnitude * 360 / curve);
            transform.GetChild(0).transform.Rotate(Axis, Angle * Time.deltaTime / 4, Space.World);
        }


        private void FixedUpdate()
        {
            // Flips the gravity of the ball if it has passed the threashold for the gravity to be switched on the Y axis
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
            // When a ball hits another ball instance in the scene
            if (collision.gameObject.GetComponent<BallMoveScript>())
            {
                switch (collision.gameObject.GetComponent<BallMoveScript>().LastHit)
                {
                    case Joysticks.White:
                        gameManager.Player1Stats.Score += 100;
                        audioManager.Play("BumperHit", .05f, 1.2f);
                        break;
                    case Joysticks.Black:
                        gameManager.Player2Stats.Score += 100;
                        audioManager.Play("BumperHit", .05f, 1.2f);
                        break;
                    case Joysticks.None:
                        break;
                    default:
                        break;
                }
            }

            ChangeBallColour();
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

            // Choose a random colour for the ball to be
            ChangeBallColour();
        }

        /// <summary>
        /// Corutine which sets the JustSpawned bool to false after 1 second
        /// </summary>
        IEnumerator SpawnCoolDown()
        {
            yield return wait;
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


        /// <summary>
        /// Changes the colour of the ball for a moment to emphaises it has been hit.
        /// </summary>
        private void ChangeBallColour()
        {
            spriteR.color = GetRandom.Color;
        }
    }
}