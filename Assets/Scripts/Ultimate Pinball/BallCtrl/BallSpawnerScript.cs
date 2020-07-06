using Arcade;
using Arcade.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pinball.BallCtrl
{
    public class BallSpawnerScript : MonoBehaviour
    {
        public Joysticks ThisPlayer;

        // The prefab that will be spawned
        [Header("The Ball")]
        public GameObject BallPrefab;

        // How long until the next ball spawns
        [Header("The delay for spawning balls")]
        public float SpawnDelay, AutoSpawnDelay;

        // The Object pool variables
        [Header("OBJ-POOL: Ball Prefabs")]
        public List<GameObject> BallObjectPool;
        [Header("OBJ-POOL: How many are in the pool")]
        public int BallObjectPoolAmount;


        public GameObject SpawnPoint;

        // bool for if something is spawning
        public bool IsSpawning;
        public bool IsSpawningAuto;

        public bool CanSpawnAuto;

        public AudioManager AM;

        int NumberSpawned;

        void Start()
        {
            // OBJ-POOL setup, spawns the amount of objects and disables then ready for use
            for (int i = 0; i < BallObjectPoolAmount; i++)
            {
                GameObject G = Instantiate(BallPrefab, Vector3.zero, transform.rotation);

                G.gameObject.name = "Ball(OBJ-POOL)";

                G.SetActive(false);
                BallObjectPool.Add(G);
            }
        }


        void Update()
        {
            if (KeyboardControls.ButtonPress((Players)ThisPlayer, Buttons.B3))
            {
                // if the script is not spawning a ball
                if (!IsSpawning)
                {
                    // run the ball spawning corutine
                    StartCoroutine(SpawnBall());
                }
            }

            if ((!IsSpawningAuto) && (CanSpawnAuto))
            {
                StartCoroutine(SpawnBallAuto());
            }


            switch (NumberSpawned)
            {
                case 1:
                    AutoSpawnDelay = 7.5f;
                    break;
                case 2:
                    AutoSpawnDelay = 6f;
                    break;
                case 4:
                    AutoSpawnDelay = 5f;
                    break;
                case 8:
                    AutoSpawnDelay = 4f;
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// Spawns a ball every set seconds by the script
        /// </summary>
        IEnumerator SpawnBall()
        {
            // Sets the is spawning bool to true
            IsSpawning = true;

            // goes through the OBJ-POOL and finds the next disabled object
            for (int i = 0; i < BallObjectPoolAmount; i++)
            {
                // Enables the first object it finds that is not enabled and enables / sets it up (also checks if it is the system (in a in/out etc...)
                if (!BallObjectPool[i].activeInHierarchy)
                {
                    if (!BallObjectPool[i].GetComponent<BallMoveScript>().IsIn)
                    {
                        BallObjectPool[i].transform.position = SpawnPoint.transform.position;

                        if (BallObjectPool[i].transform.position.y > 10.01f)
                        {
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = -1;
                            Debug.LogError("----1");
                        }
                        else
                        {
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                            Debug.LogError("1");
                        }

                        BallObjectPool[i].SetActive(true);
                        BallObjectPool[i].GetComponent<BallMoveScript>().BallInit();
                        NumberSpawned++;
                        break;
                    }
                }
            }

            AM.Play("BallSpawn", .05f);

            // wait for the desired seconds
            yield return new WaitForSeconds(SpawnDelay);

            // Sets the is spawning bool to false
            IsSpawning = false;
        }


        /// <summary>
        /// Spawns a ball every set seconds by the script
        /// </summary>
        IEnumerator SpawnBallAuto()
        {
            // Sets the is spawning bool to true
            IsSpawningAuto = true;

            // goes through the OBJ-POOL and finds the next disabled object
            for (int i = 0; i < BallObjectPoolAmount; i++)
            {
                // Enables the first object it finds that is not enabled and enables / sets it up (also checks if it is the system (in a in/out etc...)
                if (!BallObjectPool[i].activeInHierarchy)
                {
                    if (!BallObjectPool[i].GetComponent<BallMoveScript>().IsIn)
                    {
                        BallObjectPool[i].transform.position = SpawnPoint.transform.position;

                        if (BallObjectPool[i].transform.position.y > 10.01f)
                        {
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = -1;
                            //Debug.LogError("----1");
                        }
                        else
                        {
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                            //Debug.LogError("1");
                        }

                        BallObjectPool[i].SetActive(true);
                        BallObjectPool[i].GetComponent<BallMoveScript>().BallInit();
                        NumberSpawned++;
                        break;
                    }
                }
            }

            AM.Play("BallSpawn", .05f);

            // wait for the desired seconds
            yield return new WaitForSeconds(AutoSpawnDelay);

            // Sets the is spawning bool to false
            IsSpawningAuto = false;
        }


        public void SpawnBallCall(Vector3 Pos, Arcade.Joysticks Side)
        {
            for (int i = 0; i < BallObjectPoolAmount; i++)
            {
                if (!BallObjectPool[i].activeInHierarchy)
                {
                    switch (Side)
                    {
                        case Arcade.Joysticks.White:
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                            Debug.LogWarning("1");
                            break;
                        case Arcade.Joysticks.Black:
                            BallObjectPool[i].GetComponent<Rigidbody2D>().gravityScale = -1;
                            Debug.LogWarning("-----1");
                            break;
                        default:
                            break;
                    }

                    BallObjectPool[i].transform.position = Pos;
                    BallObjectPool[i].SetActive(true);
                    break;
                }
            }
        }
    }
}