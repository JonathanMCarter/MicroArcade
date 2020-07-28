using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private Animator transitions;


        public void PlayGame()
        {
            StartCoroutine(ChangeScene("Game"));
        }


        public void ToMenu()
        {
            StartCoroutine(ChangeScene("Menu"));
        }


        public void Leaderboard()
        {
            StartCoroutine(ChangeScene("Leaderboard"));
        }


        public void Quit()
        {
            Application.Quit();
        }


        public void Credits()
        {
            StartCoroutine(ChangeScene("Credits"));
        }



        private IEnumerator ChangeScene(string scn)
        {
            transitions.SetTrigger("ChangeScene");
            yield return new WaitForSecondsRealtime(.6f);
            SceneManager.LoadSceneAsync(scn);
        }
    }
}