using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Crushing.Saving;
using System;
using UnityEngine.Assertions.Must;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class GameOverScript : MonoBehaviour
    {
        [SerializeField] private CrushingData loadedData;
        [SerializeField] private Text timerTxt;
        [SerializeField] private Text starTxt;
        [SerializeField] private Text dodgeTxt;
        [SerializeField] private Text pbTimeTxt;
        [SerializeField] private Text pbStarTxt;
        [SerializeField] private Text pbDodgeTxt;
        [SerializeField] private Animator crusherTransitionAnim;
        [SerializeField] private Animator fadeAnim;

        private ISceneChanger sceneChanger;


        private void Start()
        {
            fadeAnim.SetTrigger("isInScene");

            loadedData = SaveManager.LoadGame();

            // Round Stats
            if (loadedData.lastRoundTime > 0)
            {
                timerTxt.text = FormatTimer(loadedData.lastRoundTime);
            }

            if (loadedData.starsCollected[1] > 0)
            {
                starTxt.text = loadedData.starsCollected[1].ToString();
            }
            else
            {
                starTxt.text = 0.ToString();
            }

            if (loadedData.numberOfDodges > 0)
            {
                dodgeTxt.text = loadedData.numberOfDodges.ToString();
            }
            else
            {
                dodgeTxt.text = 0.ToString();
            }


            // PB Stats
            if (loadedData.longestRoundTime > 0)
            {
                pbTimeTxt.text = FormatTimer(loadedData.longestRoundTime);
            }

            if (loadedData.bestStarsCollected[1] > 0)
            {
                pbStarTxt.text = loadedData.bestStarsCollected[1].ToString();
            }
            else
            {
                pbStarTxt.text = 0.ToString();
            }

            if (loadedData.bestNumberOfDodges > 0)
            {
                pbDodgeTxt.text = loadedData.bestNumberOfDodges.ToString();
            }
            else
            {
                pbDodgeTxt.text = 0.ToString();
            }

            // Check for achievements earned in the last round played...
            //CheckForAchievements();

            // Update Tracking Info
            ++loadedData.numberOfRoundsPlayedLifetime;
            loadedData.numberOfDodgesLifetime += loadedData.numberOfDodges;

            sceneChanger = new SceneChanger();
            sceneChanger.transitionsAnim = crusherTransitionAnim;
        }


        /// <summary>
        /// Converts a float value to a string value formatted in the following 00:00:000 format
        /// </summary>
        /// <param name="input">float value to format</param>
        /// <returns>returns the float formatted to 00:00:000</returns>
        private string FormatTimer(float input)
        {
            string _mins;
            string _seconds;

            _mins = Mathf.FloorToInt(input / 60).ToString("00");
            _seconds = Mathf.FloorToInt((input % 60)).ToString("00");

            int _pos = input.ToString().IndexOf('.');

            return _mins + ":" + _seconds + ":" + input.ToString().Substring(_pos + 1, 3);
        }


        public void ChangeScene(string scene)
        {
            StartCoroutine(sceneChanger.ChangeScene(scene));
        }


        //private void CheckForAchievements()
        //{
        //    if (loadedData.lastRoundTime < 4f)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_how_does_one_game);
        //    }

        //    if (loadedData.lastRoundTime >= 120f)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_escapist);
        //    }

        //    if (loadedData.lastRoundTime >= 300f)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_survivor);
        //    }

        //    if (loadedData.starsCollectedLifetime[1] >= 1)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_shiny);
        //    }

        //    if (loadedData.starsCollectedLifetime[1] >= 100)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_raining_stars);
        //    }

        //    if (loadedData.numberOfDodgesLifetime >= 100)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_basic_dodger);
        //    }

        //    if (loadedData.numberOfDodgesLifetime >= 250)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_amateur_dodger);
        //    }

        //    if (loadedData.numberOfDodgesLifetime >= 500)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_professional_dodger);
        //    }

        //    if (loadedData.numberOfDodgesLifetime >= 1000)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_expert_dodger);
        //    }

        //    if (loadedData.numberOfRoundsPlayedLifetime >= 10)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_hooked);
        //    }

        //    if (loadedData.numberOfRoundsPlayedLifetime >= 100)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_smashing);
        //    }

        //    if (loadedData.numberOfRoundsPlayedLifetime >= 1000)
        //    {
        //        Awards.AwardAchievement(GPGSIds.achievement_are_you_mad);
        //    }
        //}
    }
}