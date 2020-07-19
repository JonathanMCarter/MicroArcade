/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using System.Collections.Generic;

namespace CarterGames.Starshine
{
    public class EnemyWeapons : MonoBehaviour
    {
        [SerializeField] private GameObject[] weaponPrefabs;
        [SerializeField] private int[] weaponAmounts;
        [SerializeField] private List<GameObject> weaponPool;
        

        void Start()
        {
            for (int i = 0; i < weaponPrefabs.Length; i++)
            {
                for (int j = 0; j < weaponAmounts[i]; j++)
                {
                    GameObject spawnedGameObject = Instantiate(weaponPrefabs[i]);
                    spawnedGameObject.SetActive(false);
                    weaponPool.Add(spawnedGameObject);
                }
            }
        }


        public GameObject GetOrb()
        {
            for (int i = 0; i < weaponPool.Count; i++)
            {
                if (weaponPool[i].name.Contains(weaponPrefabs[0].name) && !weaponPool[i].activeSelf)
                {
                    return weaponPool[i];
                }
            }

            return null;
        }


        public GameObject GetOrbVariant()
        {
            for (int i = 0; i < weaponPool.Count; i++)
            {
                if (weaponPool[i].name.Contains(weaponPrefabs[1].name) && !weaponPool[i].activeSelf)
                {
                    return weaponPool[i];
                }
            }

            return null;
        }


        public GameObject GetMissile()
        {
            for (int i = 0; i < weaponPool.Count; i++)
            {
                if (weaponPool[i].name.Contains(weaponPrefabs[2].name) && !weaponPool[i].activeSelf)
                {
                    if (!weaponPool[i].GetComponent<MissileScript>().enabled)
                    {
                        weaponPool[i].GetComponent<MissileScript>().enabled = true;
                    }

                    return weaponPool[i];
                }
            }

            return null;
        }


        public int GetMissilePollAmount()
        {
            return weaponAmounts[2];
        }


        public GameObject GetBossBolt()
        {
            for (int i = 0; i < weaponPool.Count; i++)
            {
                if (weaponPool[i].name.Contains(weaponPrefabs[3].name) && !weaponPool[i].activeSelf)
                {
                    return weaponPool[i];
                }
            }

            return null;
        }
    }
}