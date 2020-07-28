using CarterGames.Utilities;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class SupplyCrateSpawner : MonoBehaviour, IObjectPool<GameObject>
    {
        [SerializeField] private int cratesNeeded = 5;
        [SerializeField] private GameObject cratePrefab;
        [SerializeField] private int minWait = 20;
        [SerializeField] private int maxWait = 60;

        private bool isCoR;

        public int objectLimit { get; set; }
        public List<GameObject> objectPool { get; set; }


        private void Start()
        {
            objectLimit = cratesNeeded;
            objectPool = new List<GameObject>();

            for (int i = 0; i < objectLimit; i++)
            {
                GameObject _go = Instantiate(cratePrefab);
                _go.SetActive(false);
                objectPool.Add(_go);
            }
        }


        private void Update()
        {
            if (!isCoR)
            {
                StartCoroutine(CrateSpawnerCo());
            }
        }


        private IEnumerator CrateSpawnerCo()
        {
            isCoR = true;

            for (int i = 0; i < objectLimit; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    Vector3 spawnPos = ChooseSpawnLocation();
                    objectPool[i].transform.position = new Vector3(spawnPos.x, 4, spawnPos.z);
                    objectPool[i].SetActive(true);
                    break;
                }
            }

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            isCoR = false;
        }


        private Vector3 ChooseSpawnLocation()
        {
            return Random.onUnitSphere * (Random.Range(500, 2000));
        }
    }
}