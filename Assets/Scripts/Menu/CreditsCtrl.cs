using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Arcade.Menu
{
    public class CreditsCtrl : MenuSystem
    {
        [Header("Credits Screens")][Tooltip("All the gameobjects to cycle through in the credits.")]
        [SerializeField] private GameObject[] screens;

        [SerializeField] private GameObject[] controlScreens;

        private bool StartCreditsSequence;


        [Header("Start Delay")]
        [Tooltip("How long should the script wait before starting the credits?")]
        public float StartingDelay;

        public Animator FadeToWhite;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        protected override void Start()
        {
            base.Start();

            // Wait before starting the credits (helps show the title a little)
            StartCoroutine(CreditsStartDelay());
        }


        protected override void Update()
        {
            base.Update();
            RunCredits();

            if (Return()) { ReturnToMainMenu(); }

            if (ControllerType == UserInput.SupportedControllers.ArcadeBoard)
            {

            }
        }


        void RunCredits()
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
            ChangeScene("MainMenu", 1.1f);
        }
    }
}