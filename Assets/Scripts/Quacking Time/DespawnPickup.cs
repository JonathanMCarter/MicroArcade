/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using System.Collections;
using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class DespawnPickup : MonoBehaviour
    {

        private void OnEnable()
        {
            StartCoroutine(DespawnObject());
        }


        private void OnTriggerEnter(Collider other)
        {
            // if hitting water
            if (other.gameObject.tag.Contains("Dead"))
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }


        private IEnumerator DespawnObject()
        {
            yield return new WaitForSeconds(Random.Range(10f, 14f));
            gameObject.SetActive(false);
        }
    }
}