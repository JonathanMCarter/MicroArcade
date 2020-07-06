using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Menu
{
    public class CreditsCtrl : MenuSystem
    {
        private bool StartCreditsSequence;

        public GameObject CreditsHolder;
        [Header("Credits Speed")]
        [Tooltip("How fast the crewdits should scroll up")]
        public float CreditsSpd;
        [Header("Start Delay")]
        [Tooltip("How long should the script wait before starting the credits?")]
        public float StartingDelay;

        public float maxYPosition;

        public Animator FadeToWhite;

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
        }


        void RunCredits()
        {
            // if the credits should be moving..
            if (StartCreditsSequence)
            {
                if (CreditsHolder.transform.localPosition.y < maxYPosition)
                {
                    // Move the credits xD
                    CreditsHolder.transform.localPosition += Vector3.up * CreditsSpd * Time.deltaTime;
                }
                else
                {
                    CreditsHolder.transform.localPosition = new Vector3(CreditsHolder.transform.localPosition.x, maxYPosition, CreditsHolder.transform.localPosition.z);
                }
            }
            else
            {
                // else if they are not at vector 3 zero
                if (transform.localPosition != Vector3.zero)
                {
                    // move 'em to vector3 zero xD
                    ResetCreditsPos();
                }
            }
        }

        IEnumerator CreditsStartDelay()
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


        void ReturnToMainMenu()
        {
            FadeToWhite.SetBool("ChangeScene", true);
            ChangeScene("MainMenu", 1.1f);
        }
    }
}