using CarterGames.Arcade.UserInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.UltimatePinball.BallCtrl
{
    /// <summary>
    /// CLASS | Ball Spawner, spawns the balls in the game, duh
    /// </summary>
    public class BallSpawnerScript : MonoBehaviour
    {
        // The prefab that will be spawned
        [Header("The Ball")]
        [SerializeField] private GameObject ballPrefab;

        // How long until the next ball spawns
        [Header("The delay for spawning balls")]
        [SerializeField] private float spawnDelay, autoSpawnDelay;

        // The Object pool variables
        [Header("OBJ-POOL: Ball Prefabs")]
        [SerializeField] private List<GameObject> ballObjectPool;
        [Header("OBJ-POOL: How many are in the pool")]
        [SerializeField] private int ballObjectPoolAmount;


        [SerializeField] private GameObject spawnPoint;


        private bool isSpawningAuto;
        private int numberSpawned;
        private bool canSpawnAuto;
        private AudioManager audioManager;


        internal bool isSpawning;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            // OBJ-POOL setup, spawns the amount of objects and disables then ready for use
            for (int i = 0; i < ballObjectPoolAmount; i++)
            {
                GameObject G = Instantiate(ballPrefab, Vector3.zero, transform.rotation);

                G.gameObject.name = "Ball(OBJ-POOL)";

                G.SetActive(false);
                ballObjectPool.Add(G);
            }

            canSpawnAuto = true;

            audioManager = FindObjectOfType<AudioManager>();
        }


        private void Update()
        {
            if ((!isSpawningAuto) && (canSpawnAuto))
            {
                StartCoroutine(SpawnBallAuto());
            }


            switch (numberSpawned)
            {
                case 1:
                    autoSpawnDelay = 7.5f;
                    break;
                case 2:
                    autoSpawnDelay = 6f;
                    break;
                case 4:
                    autoSpawnDelay = 5f;
                    break;
                case 8:
                    autoSpawnDelay = 4f;
                    break;
                default:
                    break;
            }

        }



        /// <summary>
        /// Spawns a ball every set seconds by the script
        /// </summary>
        private IEnumerator SpawnBallAuto()
        {
            // Sets the is spawning bool to true
            isSpawningAuto = true;

            // goes through the OBJ-POOL and finds the next disabled object
            for (int i = 0; i < ballObjectPoolAmount; i++)
            {
                // Enables the first object it finds that is not enabled and enables / sets it up (also checks if it is the system (in a in/out etc...)
                if (!ballObjectPool[i].activeInHierarchy)
                {
                    if (!ballObjectPool[i].GetComponent<BallMoveScript>().IsIn)
                    {
                        ballObjectPool[i].transform.position = spawnPoint.transform.position;

                        if (ballObjectPool[i].transform.position.y > 10.01f)
                        {
                            ballObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = -1;
                        }
                        else
                        {
                            ballObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                        }

                        ballObjectPool[i].SetActive(true);
                        ballObjectPool[i].GetComponent<BallMoveScript>().BallInit();
                        numberSpawned++;
                        break;
                    }
                }
            }

            audioManager.Play("BallSpawn", .05f);

            // wait for the desired seconds
            yield return new WaitForSeconds(autoSpawnDelay);

            // Sets the is spawning bool to false
            isSpawningAuto = false;
        }


        public void SpawnBallCall(Vector3 pos, Joysticks Side)
        {
            for (int i = 0; i < ballObjectPoolAmount; i++)
            {
                if (!ballObjectPool[i].activeInHierarchy)
                {
                    switch (Side)
                    {
                        case Joysticks.White:
                            ballObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                            break;
                        case Joysticks.Black:
                            ballObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = -1;
                            break;
                        default:
                            break;
                    }

                    ballObjectPool[i].transform.position = pos;
                    ballObjectPool[i].SetActive(true);
                    break;
                }
            }
        }
    }
}