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
    public class Panel
    {
        [SerializeField] internal List<GameObject> items;

        internal int pos;
        private int lastPos;
        internal int maxPos;
        internal bool isCoR;
       

        public void BaseSetup()
        {
            pos = 0;
            lastPos = -1;
            isCoR = false;
        }


        public void Display()
        {
            for (int i = 0; i < maxPos + 1; i++)
            {
                if ((i == pos) && (!items[i].activeInHierarchy))
                {
                    items[i].SetActive(true);
                }
                else if ((i != pos) && (items[i].activeInHierarchy))
                {
                    items[i].SetActive(false);
                }
            }
        }


        public IEnumerator MoveAround(int value)
        {
            isCoR = true;

            lastPos = pos;
            pos += value;

            if (pos > maxPos)
            {
                pos -= (maxPos + 1);
            }
            else if (pos < 0)
            {
                pos = maxPos;
            }

            yield return new WaitForSecondsRealtime(.25f);
            isCoR = false;
        }


        public IEnumerator ChangeScene(string newScene, float Delay = 1.25f)
        {
            yield return new WaitForSecondsRealtime(Delay);
            AsyncOperation Async = SceneManager.LoadSceneAsync(newScene);
            Async.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(.1f);
            Async.allowSceneActivation = true;
        }


        public IEnumerator ChangeValueCooldown()
        {
            isCoR = true;
            yield return new WaitForSecondsRealtime(.25f);
            isCoR = false;
        }
    }
}