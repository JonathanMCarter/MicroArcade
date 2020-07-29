/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using System.Collections;
using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class PaintCanScript : MonoBehaviour
    {
        private SphereCollider[] sphereColliders;
        private bool hasPlayedSound;

        public DuckPlayers duckToUse;
        public Color32 duckColour;


        private void OnEnable()
        {
            sphereColliders = GetComponents<SphereCollider>();
            hasPlayedSound = false;
        }


        private void OnDisable()
        {
            duckToUse = DuckPlayers.None;
            duckColour = Color.clear;
            StopAllCoroutines();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<DuckScript>())
            {
                if (!hasPlayedSound)
                {
                    other.gameObject.GetComponent<DuckScript>().am.Play("RetroFX2", .5f);
                    hasPlayedSound = true;
                }

                duckToUse = other.gameObject.GetComponent<DuckScript>().Ducks;
                duckColour = other.gameObject.GetComponent<DuckScript>().DuckColour;
                PaintHexagons();
            }
        }


        private void PaintHexagons()
        {
            StartCoroutine(PaintHexagonsCorutine());
        }


        private IEnumerator PaintHexagonsCorutine()
        {
            yield return new WaitForSeconds(.1f);
            sphereColliders[1].enabled = true;
            yield return new WaitForSeconds(1f);
            sphereColliders[1].enabled = false;
            gameObject.SetActive(false);
        }
    }
}