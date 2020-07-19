using System.Collections;
using UnityEngine;
using CarterGames.Assets.AudioManager;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private ParticleSystem explosionParticlesPrefab;

        private Rigidbody2D rigidBody;
        private GameManager gameManager;
        private AudioManager audioManager;
        private Camera mainCamera;
        private CircleCollider2D thisCollider;
        private ParticleSystem explosionParticlesInstance;
        private bool isPlayerActive = true;
        private Vector2[] positions;
        private LineRenderer lR;
        private bool canSetLine;


        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            gameManager = FindObjectOfType<GameManager>();
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            mainCamera = Camera.main;
            thisCollider = GetComponent<CircleCollider2D>();
            positions = new Vector2[2];
            lR = GetComponent<LineRenderer>();

            // sets the player to 0,0,0
            transform.position = Vector3.zero;


            Gradient _grad = new Gradient();
            _grad.colorKeys = new GradientColorKey[] { new GradientColorKey(Converters.ConvertFloatArrayToColor(gameManager.saveData.playerColour), 0f), new GradientColorKey(Converters.ConvertFloatArrayToColor(gameManager.saveData.playerColour), 1f) };
            _grad.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) };
            lR.colorGradient = _grad;
        }


        private void Update()
        {
#if UNITY_EDITOR
            // Debugging movement
            if (isPlayerActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    canSetLine = true;
                    positions[0] = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    lR.enabled = true;
                }

                if (canSetLine)
                {
                    lR.SetPosition(0, transform.position);
                    lR.SetPosition(1, mainCamera.ScreenToWorldPoint(Input.mousePosition));
                }

                if (Input.GetMouseButtonUp(0))
                {
                    positions[1] = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    rigidBody.AddForce((positions[1] - positions[0]).normalized * moveSpeed, ForceMode2D.Impulse);
                    audioManager.Play("PlayerShoot", .75f);
                    lR.enabled = false;
                    canSetLine = false;
                }
            }
#endif

            // Only runs android movement code if on android devices
#if UNITY_ANDROID

            if (isPlayerActive)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        canSetLine = true;
                        lR.enabled = true;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        positions[0] = transform.position;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        positions[1] = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                        rigidBody.AddForce((positions[1] - positions[0]).normalized * moveSpeed, ForceMode2D.Impulse);
                        audioManager.PlayFromTime("PlayerShoot", .3f);
                        canSetLine = false;
                        lR.enabled = false;
                    }
                }

                if (canSetLine)
                {
                    lR.SetPosition(0, transform.position);
                    lR.SetPosition(1, mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position));
                }
            }
#endif
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Crusher"))
            {
                PlayerDeathSequence();
            }
        }


        /// <summary>
        /// Method called when the player has been hit by a crusher to end the round...
        /// </summary>
        private void PlayerDeathSequence()
        {
            transform.GetChild(gameManager.saveData.playerShapeChoice-1).GetComponent<ParticleSystem>().Stop();
            isPlayerActive = false;
            thisCollider.enabled = false;
            explosionParticlesInstance.Play();
            audioManager.Play("Explosion", 1.15f);
            audioManager.Play("Echo");
            gameManager.GameOver();
        }


        /// <summary>
        /// Method that sets the particles of the player to the colour set up in the game settings
        /// </summary>
        /// <param name="grad">Gradient to use.</param>
        /// <param name="choice">The particle system with the shape the player selected.</param>
        public void SetParticleColour(Gradient grad, int choice)
        {
            // Select the particle system for the player based on the settings
            transform.GetChild(choice).gameObject.SetActive(true);
            var _system = transform.GetChild(choice).GetComponent<ParticleSystem>();

            // Object Pooling the particle system for explosions
            explosionParticlesInstance = Instantiate(explosionParticlesPrefab, transform);

            var _col = explosionParticlesInstance.colorOverLifetime;
            _col.color = grad;


            var _playerCol = _system.colorOverLifetime;
            _playerCol.color = grad;
        }


        /// <summary>
        /// Method that returns the touch positions used to determin where the player is moving to...
        /// </summary>
        /// <returns>Vector2 Array of positions (2 elements)</returns>
        public Vector2[] GetTouchPositions()
        {
            return positions;
        }
    }
}