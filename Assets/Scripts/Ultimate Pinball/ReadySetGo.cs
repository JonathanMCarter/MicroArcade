/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using Pinball.BallCtrl;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Pinball
{
    public class ReadySetGo : MonoBehaviour
    {
        public BallSpawnerScript[] ballSpawners;
        public Image[] readyImages;
        GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine(IntroSequence());
        }


        private IEnumerator IntroSequence()
        {
            gameManager.StartTimer = false;
            yield return new WaitForSeconds(.5f);
            readyImages[0].GetComponentInChildren<Text>().text = "Ready...";
            readyImages[1].GetComponentInChildren<Text>().text = "Ready...";
            yield return new WaitForSeconds(2f);
            readyImages[0].GetComponentInChildren<Text>().text = "Go...";
            readyImages[1].GetComponentInChildren<Text>().text = "Go...";
            yield return new WaitForSeconds(1f);
            DisableDisplay();
            StartGame();
        }


        private void StartGame()
        {
            gameManager.StartTimer = true;

            for (int i = 0; i < 2; i++)
            {
                ballSpawners[i].enabled = true;
            }
        }


        private void DisableDisplay()
        {
            for (int i = 0; i < 2; i++)
            {
                readyImages[i].GetComponentInChildren<Text>().enabled = false;
                readyImages[i].enabled = false;
            }
        }
    }
}