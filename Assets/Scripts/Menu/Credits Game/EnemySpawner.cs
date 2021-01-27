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
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject[] healthBars;

        private GameObject[] pool;
        private int numberSpawned;

        private float _timer;
        private float _timeLimit;
        

        private void Start()
        {
            pool = new GameObject[amount[0] + amount[1] + amount[2] + amount[3]];
            healthBars = new GameObject[amount[1] + amount[2] + amount[3]];

            for (int j = 0; j < amount.Length; j++)
            {
                for (int i = 0; i < amount[j]; i++)
                {
                    if (prefab[j].name.Contains("Prefab"))
                    {
                        GameObject _go = Instantiate(prefab[j], parent);
                        _go.SetActive(false);
                        pool[i] = _go;
                    }
                    else
                    {
                        GameObject _go = Instantiate(prefab[j]);
                        _go.SetActive(false);
                        pool[i] = _go;
                    }
                }
            }

            for (int i = 0; i < healthBars.Length; i++)
            {
                GameObject _hB = Instantiate(healthBar, parent);
                _hB.SetActive(false);
                healthBars[i] = _hB;
            }

            _timer = 0f;
            _timeLimit = GetRandom.Float(0.5f, 3f);
        }


        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _timeLimit)
            {
                int _choice = GetRandom.Int(0, 3);

                if (_choice.Equals(0))
                    SpawnCreditEnemy();
                else
                    SpawnEnemy();

                _timeLimit = GetRandom.Float(0.5f, 3f);
                _timer = 0f;
            }
        }


        public void SpawnCreditEnemy()
        {
            for (int i = 0; i < amount[0]; i++)
            {
                if (!pool[i].activeSelf && pool[i].GetComponent<EnemyCredit>())
                {
                    pool[i].transform.localPosition = GetRandom.Vector2(115, 125, -32.5f, 32.5f);
                    pool[i].GetComponentInChildren<EnemyCredit>().title = so.roles[numberSpawned];
                    pool[i].GetComponentInChildren<EnemyCredit>().desc = so.names[numberSpawned];
                    pool[i].SetActive(true);
                    ++numberSpawned;
                    break;
                }
            }
        }


        public void SpawnEnemy()
        {
            Vector2 spawnLocation = GetRandom.Vector2(-8f, 3f, 8f, 5f);

            while (Physics.OverlapSphere(new Vector3(spawnLocation.x, spawnLocation.y, 0), 1f).Length > 0)
            {
                spawnLocation = GetRandom.Vector2(-8f, 3f, 8f, 5f);
            }

            for (int i = 0; i < (amount[1] + amount[2] + amount[3]); i++)
            {
                if (!pool[i].activeSelf)
                {
                    pool[i].transform.localPosition = spawnLocation;
                    pool[i].transform.rotation = Quaternion.Euler(0, 0, GetRandom.Float(0, 360));


                    for (int j = 0; j < healthBars.Length; j++)
                    {
                        if (!healthBars[j].activeSelf)
                        {
                            healthBars[j].GetComponent<Follow>().toFollow = pool[i].transform;
                            pool[i].GetComponent<Enemy>().healthBar = healthBars[j].GetComponent<Slider>();
                            healthBars[j].SetActive(true);
                            break;
                        }
                    }
                    pool[i].SetActive(true);

                    ++numberSpawned;
                    break;
                }
            }
        }
    }
}