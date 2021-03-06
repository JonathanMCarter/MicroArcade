﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;
using CarterGames.Arcade.UserInput;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.UltimatePinball.BallCtrl
{
    public class InOutScript : MonoBehaviour
    {
        // This object which should be the in object for the balls to enter.
        [Header("Object the balls should enter (this)")]
        public GameObject InOBJ;

        // The object the balls shoudl exit from
        [Header("Object where the plays exit from")]
        public GameObject OutOBJ;

        // how long before the next shot can be fired
        [Header("Delay Amount")]
        public float InOutDelay;

        public Joysticks Side;
        BallSpawnerScript BSS;

        AudioManager am;
        GameManager GM;

        private void Start()
        {
            BSS = FindObjectOfType<BallSpawnerScript>();
            am = FindObjectOfType<AudioManager>();
            GM = FindObjectOfType<GameManager>();
            InOBJ = gameObject;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<BallMoveScript>())
            {
                if (collision.gameObject.GetComponent<BallMoveScript>().IsIn) return;
                collision.gameObject.GetComponent<BallMoveScript>().IsIn = true;
                Debug.Log("Check hit");
                collision.gameObject.SetActive(false);
                StartCoroutine(SendBallOutWithDelay(collision.gameObject.GetComponent<BallMoveScript>()));
                am.Play("Gate-In", .15f);
            }
        }


        /// <summary>
        /// Re-enables the ball after the delay amount
        /// </summary>
        /// <param name="GO">Ball gameobject to be activated again</param>
        IEnumerator SendBallOutWithDelay(BallMoveScript Script)
        {
            Debug.Log("been called");
            yield return new WaitForSeconds(InOutDelay);

            switch (Script.LastHit)
            {
                case Joysticks.White:
                    GM.Player1Stats.Score += 500;
                    break;
                case Joysticks.Black:
                    GM.Player2Stats.Score += 500;
                    break;
                case Joysticks.None:
                    break;
                default:
                    break;
            }

            Debug.Log("spawning");
            BSS.SpawnBallCall(OutOBJ.transform.position, Side);
            am.Play("Gate-Out", .15f);
        }
    }
}