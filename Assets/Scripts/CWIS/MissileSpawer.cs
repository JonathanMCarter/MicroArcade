using System.Collections.Generic;
using UnityEngine;
using CarterGames.Utilities;
using System.Collections;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class MissileSpawer : MonoBehaviour, IObjectPool<GameObject>
    {
        [SerializeField] private int missilesWanted;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;

        private bool isCoR;
        private ShipController ship;

        public GameObject missilePrefab;

        public int objectLimit { get; set; }
        public List<GameObject> objectPool { get; set; }

        public List<GameObject> activeMissiles;

        public float minWait;
        public float maxWait;

        private AudioManager am;
        

        private void Start()
        {
            objectLimit = missilesWanted;

            objectPool = new List<GameObject>();

            for (int i = 0; i < objectLimit; i++)
            {
                GameObject _go = Instantiate(missilePrefab);
                _go.SetActive(false);
                objectPool.Add(_go);
            }

            ship = FindObjectOfType<ShipController>();
            am = FindObjectOfType<AudioManager>();
        }


        private void Update()
        {
            if (!isCoR)
            {
                StartCoroutine(FireMissileCo(Random.Range(minWait, maxWait)));
            }


            switch (ship.gameTimer)
            {
                case 3:
                    minWait = 7f;
                    maxWait = 10f;
                    break;
                case 5:
                    minWait = 6f;
                    maxWait = 9f;
                    break;
                case 10:
                    minWait = 6f;
                    maxWait = 8.5f;
                    break;
                case 15:
                    minWait = 5f;
                    maxWait = 7f;
                    break;
                case 20:
                    minWait = 4f;
                    maxWait = 7f;
                    break;
                case 25:
                    minWait = 3f;
                    maxWait = 7f;
                    break;
                case 40:
                    minWait = 2f;
                    maxWait = 5f;
                    break;
                case 55:
                    minWait = 2f;
                    maxWait = 4f;
                    break;
                case 65:
                    minWait = 1f;
                    maxWait = 4f;
                    break;
                case 80:
                    minWait = 1f;
                    maxWait = 3f;
                    break;
                case 90:
                    minWait = 1f;
                    maxWait = 2f;
                    break;
                case 150:
                    minWait = .5f;
                    maxWait = 1f;
                    break;
                default:
                    break;
            }
        }


        private IEnumerator FireMissileCo(float delay)
        {
            isCoR = true;

            for (int i = 0; i < objectLimit; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    Vector3 newPos = ChooseSpawnLocation();
                    objectPool[i].transform.position = new Vector3(newPos.x, 4, newPos.z);
                    Vector3 dir = new Vector3(ship.mast.transform.position.x - objectPool[i].transform.position.x, 0, ship.mast.transform.position.z - objectPool[i].transform.position.z);
                    objectPool[i].GetComponent<Rigidbody>().velocity = (dir).normalized * 100;
                    objectPool[i].transform.LookAt(dir);
                    objectPool[i].SetActive(true);
                    activeMissiles.Add(objectPool[i]);
                    //am.PlayFromTime("inbound", .8f, .01f, .9f);
                    break;
                }
            }

            yield return new WaitForSeconds(delay);
            isCoR = false;
        }


        private Vector3 ChooseSpawnLocation()
        {
            float innerRad = minDistance;
            float outerRad = maxDistance;

            Vector3 dir = Random.insideUnitSphere;
            float length = innerRad + outerRad * Random.value;

            Vector3 final = dir.normalized * length;

            return final;
        }
    }
}