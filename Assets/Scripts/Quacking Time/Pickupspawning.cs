using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quacking
{
    public class Pickupspawning : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pickupObjectPool;
        [SerializeField] private int[] numberOfEachPickup;
        [SerializeField] private GameObject[] pickupPrefabs;
        [SerializeField] private List<GameObject> spawnedObjects;



        private BoxCollider boxCollider;
        public MapController mapController;

        [SerializeField] private bool isSpawningPickups;
        [SerializeField] private bool isSpawningScoringBoxes;

        private GameManager gameManager;


        private void Start()
        {
            gameManager = GetComponent<GameManager>();

            boxCollider = GetComponent<BoxCollider>();

            ObjectPoolSetup();
        }



        private void Update()
        {
            if (gameManager.RoundRunning)
            {
                SpawnPickup();
                SpawnScoringBoxes();
            }
        }



        private void SpawnScoringBoxes()
        {
            if (!isSpawningScoringBoxes)
            {
                StartCoroutine(SpawnScoringBoxesCorutine());
            }
        }



        private IEnumerator SpawnScoringBoxesCorutine()
        {
            isSpawningScoringBoxes = true;

            int _numberOfScoringBoxes = numberOfEachPickup[0] + numberOfEachPickup[1] + numberOfEachPickup[2];

            for (int i = 0; i < Random.Range(2, 3); i++)
            {
                for (int j = 0; j < pickupObjectPool.Count; j++)
                {
                    if (pickupObjectPool[j].name.Contains("Scoring"))
                    {
                        int _randomNumber = Random.Range(0, _numberOfScoringBoxes - 1);

                        if (!pickupObjectPool[_randomNumber].activeInHierarchy)
                        {
                            Vector3 _position = Vector3.zero;

                            while (_position == Vector3.zero)
                            {
                                _position = mapController.GetRandomHexagon();
                            }

                            if (_position == Vector3.zero)
                            {
                                Debug.Log("fjdlfdkjhf");
                                break;
                            }
                            else
                            {
                                pickupObjectPool[_randomNumber].transform.position = _position;
                                pickupObjectPool[_randomNumber].SetActive(true);
                                spawnedObjects.Add(pickupObjectPool[_randomNumber]);
                                break;
                            }
                        }
                    }
                }
            }

            yield return new WaitForSeconds(Random.Range(2f, 4f));

            isSpawningScoringBoxes = false;
        }



        private void SpawnPickup()
        {
            if (!isSpawningPickups)
            {
                StartCoroutine(SpawnPickupCorutine());
            }
        }



        private IEnumerator SpawnPickupCorutine()
        {
            isSpawningPickups = true;
            // Spawn Random Pickup....

            for (int i = 0; i < Random.Range(2, 3); i++)
            {
                for (int j = 0; j < pickupObjectPool.Count; j++)
                {
                    if (!pickupObjectPool[j].name.Contains(pickupPrefabs[0].name))
                    {
                        int _randomNumber = Random.Range(numberOfEachPickup[0] + numberOfEachPickup[1] + numberOfEachPickup[2], pickupObjectPool.Count - 1);

                        if (!pickupObjectPool[_randomNumber].activeInHierarchy)
                        {
                            Vector3 _position = Vector3.zero;

                            while (_position == Vector3.zero)
                            {
                                _position = mapController.GetRandomHexagon();
                            }

                            if (_position == Vector3.zero)
                            {
                                Debug.Log("fjdlfdkjhf");
                                break;
                            }
                            else
                            {
                                pickupObjectPool[_randomNumber].transform.position = _position;
                                pickupObjectPool[_randomNumber].SetActive(true);
                                spawnedObjects.Add(pickupObjectPool[_randomNumber]);
                                break;
                            }
                        }
                    }
                }
            }

            yield return new WaitForSeconds(Random.Range(10f, 20f));            

            isSpawningPickups = false;
        }




        /// <summary>
        /// Object Pooling for all the pickups in the game
        /// </summary>
        private void ObjectPoolSetup()
        {
            for (int i = 0; i < numberOfEachPickup.Length; i++)
            {
                for (int j = 0; j < numberOfEachPickup[i]; j++)
                {
                    GameObject _go = Instantiate(pickupPrefabs[i]);
                    _go.SetActive(false);
                    pickupObjectPool.Add(_go);
                }
            }
        }
    }
}