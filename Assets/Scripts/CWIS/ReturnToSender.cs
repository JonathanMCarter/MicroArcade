using CarterGames.Utilities;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class ReturnToSender : MonoBehaviour, IObjectPool<GameObject>
    {
        // Ranks
        public GameManager.Ranks currentRank;
        public int timesUsed;
        private int[] rankUpRequirements;
        private GameManager gm;

        [SerializeField] private CWIS_Controller.Controller thisturret;
        [SerializeField] private CWIS_Controller control;
        [SerializeField] private GameObject missilePrafab;
        [SerializeField] private MissileSpawer ms;

        private ShipController ship;
        private AudioManager am;


        [SerializeField] private ParticleSystem missileFire;
        private bool isCoR;

        public int playerMissiles;
        private float missileDelay = 2f;

        public int objectLimit { get; set; }
        public List<GameObject> objectPool { get; set; }


        private void Start()
        {
            objectLimit = playerMissiles;
            objectPool = new List<GameObject>();

            for (int i = 0; i < objectLimit; i++)
            {
                GameObject _go = Instantiate(missilePrafab);
                _go.SetActive(false);
                objectPool.Add(_go);
            }

            ship = FindObjectOfType<ShipController>();
            gm = FindObjectOfType<GameManager>();

            rankUpRequirements = new int[6]
            {
            Random.Range(2, 3),
            Random.Range(6, 7),
            Random.Range(10, 12),
            Random.Range(14, 18),
            Random.Range(20, 25),
            Random.Range(30, 40)
            };

            am = FindObjectOfType<AudioManager>();
        }


        private void Update()
        {
            if (control.activeTurret == thisturret && Input.GetMouseButton(0))
            {
                if (ship.shipMissiles > 0)
                {
                    ShootMissileBack();
                }
            }
        }


        public void ShootMissileBack()
        {
            if (!isCoR)
            {
                StartCoroutine(ShootMissileCo());
            }
        }


        private IEnumerator ShootMissileCo()
        {
            isCoR = true;

            if (ms.activeMissiles.Count > 0)
            {
               float test = Vector3.Distance(ms.activeMissiles[ms.activeMissiles.Count - 1].transform.position, transform.position);

                if (test > 250f)
                {
                    for (int i = 0; i < objectLimit; i++)
                    {
                        if (!objectPool[i].activeInHierarchy)
                        {
                            timesUsed++;
                            CheckForNewRank();
                            ship.shipMissiles--;
                            missileFire.Play();
                            objectPool[i].transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                            objectPool[i].GetComponent<Homing>().targetPos = ms.activeMissiles[ms.activeMissiles.Count - 1];
                            ms.activeMissiles.RemoveAt(ms.activeMissiles.Count - 1);
                            am.Play("missileSmoke", .75f);
                            am.PlayWithDelay("missileFire", .25f, .5f);
                            objectPool[i].SetActive(true);
                            break;
                        }
                    }
                }
            }

            yield return new WaitForSeconds(missileDelay);
            isCoR = false;
        }



        private void CheckForNewRank()
        {
            if (timesUsed == rankUpRequirements[0])
            {
                currentRank = gm.Rankup(GameManager.Ranks.None);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[1])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev1);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                missileDelay = 1.75f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[2])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev2);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                missileDelay = 1.5f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[3])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev3);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                missileDelay = 1.25f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[4])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Star1);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                missileDelay = 1f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[5])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Star2);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                missileDelay = .5f;
                //flicker.shouldFlicker = true;
            }
        }
    }
}