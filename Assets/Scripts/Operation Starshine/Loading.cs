using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CarterGames.Starshine
{
    public class Loading : MonoBehaviour
    {
        AsyncOperation Async;
        float Progress;
        bool IsCoRunning;


        public Slider LoadingSlider;



        void Start()
        {
            IsCoRunning = false;

            // Starts Loading the scene
            Async = SceneManager.LoadSceneAsync("Operation-Starshine-Level");

            // Makes it so the scene will not activate until I want it too.
            //Async.allowSceneActivation = false;
        }


        void Update()
        {
            if (!Async.isDone)
            {
                Progress = Mathf.Clamp01(Async.progress / .9f);
                LoadingSlider.value = Progress;
            }
            else if (!IsCoRunning)
            {
                StartCoroutine(WaitToChangeToTheLevel());
            }
        }


        IEnumerator WaitToChangeToTheLevel()
        {
            Progress = 1;
            LoadingSlider.value = Progress;
            yield return new WaitForSeconds(1);
            //Async.allowSceneActivation = true;
        }
    }
}