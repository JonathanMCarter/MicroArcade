using UnityEngine;
using UnityEngine.UI;
using CarterGames.Utilities;

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
        [SerializeField] private GameObject[] prefab;
        [SerializeField] private Transform parent;
        [SerializeField] private int[] amount;

        private GameObject[] credits;
        private int lastCreditSpawned;

        private GameObject[] asteroids;

        [SerializeField] private float _timer;
        [SerializeField] private float _timeLimit;
        

        private void Start()
        {
            credits = new GameObject[amount[0]];
            asteroids = new GameObject[amount[1] + amount[2] + amount[3]];

            for (int i = 0; i < amount[0]; i++)
            {
                GameObject _go = Instantiate(prefab[0], parent);
                _go.GetComponentInChildren<EnemyCredit>().title = so.roles[i];
                _go.GetComponentInChildren<EnemyCredit>().desc = so.names[i];
                _go.SetActive(false);
                credits[i] = _go;
            }


            for (int i = 0; i < asteroids.Length; i++)
            {
                GameObject _go = Instantiate(prefab[GetRandom.Int(1, 3)]);
                _go.SetActive(false);
                asteroids[i] = _go;
            }

            _timer = 0f;
            _timeLimit = GetRandom.Float(0.5f, 4f);

            lastCreditSpawned = -1;
        }


        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _timeLimit)
            {
                int _choice = GetRandom.Int(0, 3);

                if (_choice <= 0)
                    SpawnCreditEnemy();
                else
                    SpawnEnemy();

                _timeLimit = GetRandom.Float(0.5f, 4f);
                _timer = 0f;
            }
        }


        public void SpawnCreditEnemy()
        {
            Vector2 spawnLocation = GetRandom.Vector2(-67f, 67f, 67f, 100f);

            while (Physics.OverlapSphere(new Vector3(spawnLocation.x, spawnLocation.y, 0), 5f).Length > 0)
            {
                spawnLocation = GetRandom.Vector2(-67f, 67f, 67f, 100f);
            }

            for (int i = 0; i < credits.Length; i++)
            {
                if (lastCreditSpawned.Equals(credits.Length - 1))
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
            Vector2 spawnLocation = GetRandom.Vector2(-8f, 3f, 8f, 5f);

            while (Physics.OverlapSphere(new Vector3(spawnLocation.x, spawnLocation.y, 0), 2f).Length > 0)
            {
                spawnLocation = GetRandom.Vector2(-8f, 3f, 8f, 5f);
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                if (!asteroids[i].activeInHierarchy)
                {
                    asteroids[i].transform.localPosition = spawnLocation;
                    asteroids[i].transform.rotation = Quaternion.Euler(0, 0, GetRandom.Float(0, 360));
                    asteroids[i].SetActive(true);
                    break;
                }
            }
        }
    }
}