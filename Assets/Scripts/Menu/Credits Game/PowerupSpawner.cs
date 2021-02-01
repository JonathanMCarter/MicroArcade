using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    public class PowerupSpawner : MonoBehaviour
    {
        public AssetReference[] powerUp;
        private AsyncOperationHandle<GameObject> asyncOp;
        [SerializeField] private List<GameObject> spawnedObjects;


        private void Start()
        {
            spawnedObjects = new List<GameObject>();
        }


        public void SpawnPowerup(Transform trans)
        {
            asyncOp = powerUp[Rand.Int(0, 2)].InstantiateAsync();
            asyncOp.Completed += handle =>
            {
                GameObject _go = handle.Result;
                _go.transform.position = trans.position;
                _go.GetComponent<Powerups>().powerupSpawner = this;
                _go.SetActive(true);
                spawnedObjects.Add(_go);
            };
        }


        public void DespawnPowerup(GameObject _go)
        {
            GameObject _toDestroy = _go;

            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (_toDestroy.Equals(spawnedObjects[i]))
                {
                    Addressables.ReleaseInstance(spawnedObjects[i]);
                    spawnedObjects.RemoveAt(i);
                }
            }


        }
    }
}