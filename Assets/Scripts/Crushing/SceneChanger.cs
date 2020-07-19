using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class SceneChanger : ISceneChanger
    {
        public Animator transitionsAnim { get; set; }
        public bool isCrusherTransition { get; set; }

        public SceneChanger()
        {
            isCrusherTransition = true;
        }

        public IEnumerator ChangeScene(string sceneName)
        {
            if (isCrusherTransition)
            {
                transitionsAnim.SetTrigger("changeScene");
                yield return new WaitForSecondsRealtime(1.25f);
                SceneManager.LoadSceneAsync(sceneName);
            }
            else
            {
                transitionsAnim.SetTrigger("isGameOver");
                yield return new WaitForSecondsRealtime(1.25f);
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }
}