using UnityEngine;
using UnityEngine.UI;
using CarterGames.Utilities;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private CreditsSO so;
        //[SerializeField] private GameObject[] prefab;
        [SerializeField] private AssetReference[] prefab;
        [SerializeField] private Transform parent;
        [SerializeField] private int[] amount;

        [SerializeField] private List<GameObject> credits = new List<GameObject>();
        private int lastCreditSpawned;

        [SerializeField] private List<GameObject> asteroids;

        [SerializeField] private float _timer;
        [SerializeField] private float _timeLimit;
        [SerializeField] private float _maxDelay;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float curvePos = 0f;

        private AsyncOperationHandle<GameObject> asyncOperation;


        private void Start()
        {
            ObjectPoolSetup();

            _timer = 0f;
            _timeLimit = Rand.Float(0.5f, _maxDelay - (curve.Evaluate(curvePos) * _maxDelay));
            curvePos = 0;

            lastCreditSpawned = -1;
        }


        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _timeLimit)
            {
                int _choice = Rand.Int(0, 6);

                if (_choice <= 0)
                {
                    SpawnCreditEnemy();
                    curvePos += .015f;
                }
                else
                    SpawnEnemy();

                _timeLimit = Rand.Float(0.5f, _maxDelay - (curve.Evaluate(curvePos) * _maxDelay));
                _timer = 0f;
                Debug.Log(_maxDelay - (curve.Evaluate(curvePos) * _maxDelay));
            }
        }


        private void ObjectPoolSetup()
        {
            asteroids = new List<GameObject>();
            credits = new List<GameObject>();

            asyncOperation = prefab[0].LoadAssetAsync<GameObject>();
            asyncOperation.Completed += handle =>
            {
                var _prefab = handle.Result;

                for (int i = 0; i < amount[0]; i++)
                {
                    GameObject newGO = Instantiate(_prefab, parent);
                    newGO.GetComponentInChildren<EnemyCredit>().title = so.roles[i];
                    newGO.GetComponentInChildren<EnemyCredit>().desc = so.names[i];
                    newGO.SetActive(false);
                    credits.Add(newGO);
                }
            };


            asyncOperation = prefab[Rand.Int(1,3)].LoadAssetAsync<GameObject>();
            asyncOperation.Completed += handle =>
            {
                var _prefab = handle.Result;

                for (int i = 0; i < amount[1]; i++)
                {
                    GameObject newGO = Instantiate(_prefab);
                    newGO.SetActive(false);
                    asteroids.Add(newGO);
                }
            };
        }


        public void SpawnCreditEnemy()
        {
            Vector2 spawnLocation = Rand.Vector2(-67f, 67f, 67f, 100f);

            while (Physics.OverlapSphere(new Vector3(spawnLocation.x, spawnLocation.y, 0), 5f).Length > 0)
            {
                spawnLocation = Rand.Vector2(-67f, 67f, 67f, 100f);
            }

            for (int i = 0; i < credits.Count; i++)
            {
                if (lastCreditSpawned.Equals(credits.Count - 1))
                {
                    lastCreditSpawned = -1;
                }

                if (!credits[i].activeInHierarchy && i > lastCreditSpawned)
                {
                    credits[i].transform.localPosition = spawnLocation;
                    credits[i].SetActive(true);
                    lastCreditSpawned = i;
                    break;
                }
            }
        }


        public void SpawnEnemy()
        {
            Vector2 spawnLocation = Rand.Vector2(-8f, 3f, 8f, 5f);

            while (Physics.OverlapSphere(new Vector3(spawnLocation.x, spawnLocation.y, 0), 2f).Length > 0)
            {
                spawnLocation = Rand.Vector2(-8f, 3f, 8f, 5f);
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                if (!asteroids[i].activeInHierarchy)
                {
                    asteroids[i].transform.localPosition = spawnLocation;
                    asteroids[i].transform.rotation = Quaternion.Euler(0, 0, Rand.Float(0, 360));
                    asteroids[i].SetActive(true);
                    break;
                }
            }
        }
    }
}