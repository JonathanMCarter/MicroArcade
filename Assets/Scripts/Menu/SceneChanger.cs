using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class SceneChanger : MonoBehaviour
    {
        private Animator anim;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            anim = GetComponent<Animator>();
        }


        public void ChangeScene(string sceneName)
        {
            StartCoroutine(SceneChangerCO(sceneName));
        }


        private IEnumerator SceneChangerCO(string sceneName)
        {
            anim.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadScene(sceneName);
        }
    }
}