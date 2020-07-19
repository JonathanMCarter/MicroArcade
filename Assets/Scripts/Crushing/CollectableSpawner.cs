using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class CollectableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private int collectableAmount;
        private List<GameObject> collectablePool = new List<GameObject>();
        private bool isCoRunning = false;

        private Vector2 cameraBounds;
        private float objWidth;
        private float objHeight;
        private Camera mainCamera;


        private void Start()
        {
            ObjectPool();
            mainCamera = Camera.main;
            cameraBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            objWidth = 7.8f;
            objHeight = 4f;
        }


        private void Update()
        {
            if (!isCoRunning)
            {
                StartCoroutine(SpawnCollectablesCo());
            }
        }



        private IEnumerator SpawnCollectablesCo()
        {
            isCoRunning = true;

            yield return new WaitForSeconds(Random.Range(5, 21));

            if (ShouldSpawn())
            {
                SpawnCollectable();
            }

            isCoRunning = false;
        }


        private bool ShouldSpawn()
        {
            int _random = Random.Range(0, 101);

            if (_random <= 85)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void ObjectPool()
        {
            collectablePool.Clear();

            for (int i = 0; i < collectableAmount; i++)
            {
                GameObject _go = Instantiate(collectablePrefab);
                _go.SetActive(false);
                collectablePool.Add(_go);
            }
        }


        private void SpawnCollectable()
        {
            GameObject _go = null;

            for (int i = 0; i < collectablePool.Count; i++)
            {
                if (!collectablePool[i].activeInHierarchy)
                {
                    _go = collectablePool[i];
                    break;
                }
            }

            _go.transform.position = ChooseRandomPosition();
            _go.SetActive(true);
        }


        private Vector2 ChooseRandomPosition()
        {
            return new Vector2(Random.Range(-cameraBounds.x + .5f, cameraBounds.x - .5f),
                               Random.Range(-cameraBounds.y + .4f, cameraBounds.y - .4f));
        }
    }
}