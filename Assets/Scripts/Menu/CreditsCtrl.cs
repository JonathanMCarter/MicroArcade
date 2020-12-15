using CarterGames.Arcade.UserInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Arcade.Menu
{
    public class CreditsCtrl : MonoBehaviour
    {
        [Header("Credits Screens")][Tooltip("All the gameobjects to cycle through in the credits.")]
        [SerializeField] private GameObject[] screens;

        [SerializeField] private GameObject[] controlScreens;

        private float timer;
        private float timeLimit = 4f;

        private bool StartCreditsSequence;
        private bool updateScreen;

        [Header("Start Delay")]
        [Tooltip("How long should the script wait before starting the credits?")]
        public float StartingDelay;

        public Animator FadeToWhite;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        protected void Start()
        {

        }


        private IEnumerator CreditsStartDelay()
        {
            // wait for the amount inputted in the inspector
            yield return new WaitForSeconds(StartingDelay);

            // the start 'em credits
            StartCreditsSequence = true;
        }



        /// <summary>
        /// Resets the credits position to the default positon of vector3 zero to be played
        /// </summary>
        public void ResetCreditsPos()
        {
            transform.localPosition = Vector3.zero;
        }



        private void ReturnToMainMenu()
        {
            FadeToWhite.SetBool("ChangeScene", true);
        }


        private void ResetTimer()
        {
            timer = 0;
            updateScreen = false;
        }

    }
}