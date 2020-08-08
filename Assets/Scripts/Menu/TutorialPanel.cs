using CarterGames.Arcade.UserInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private int pos;
        [SerializeField] private int lastPos;
        [SerializeField] private int maxPos;

        private bool isCoR;
        [SerializeField] private List<GameObject> pages;
        private ArcadeGameMenuCtrl aGMC;

        public bool isUD;


        private void Start()
        {
            pos = 0;
            lastPos = -1;
            maxPos = transform.childCount - 1;

            for (int i = 0; i < maxPos + 1; i++)
            {
                pages.Add(transform.GetChild(i).gameObject);
            }

            aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
        }


        private void Update()
        {
            if (!isUD)
            {
                if (MenuControls.Left() && !isCoR)
                {
                    StartCoroutine(MoveAround(-1));
                }

                if (MenuControls.Right() && !isCoR)
                {
                    StartCoroutine(MoveAround(1));
                }
            }
            else
            {
                if (MenuControls.Up() && !isCoR)
                {
                    StartCoroutine(MoveAround(-1));
                }

                if (MenuControls.Down() && !isCoR)
                {
                    StartCoroutine(MoveAround(1));
                }
            }


            if (MenuControls.Return())
            {
                aGMC.infoPanelActive = false;
                transform.parent.transform.parent.gameObject.SetActive(false);
            }


            Display();
        }


        private void Display()
        {
            for (int i = 0; i < maxPos + 1; i++)
            {
                if ((i == pos) && (!pages[i].activeInHierarchy))
                {
                    pages[i].SetActive(true);
                }
                else if ((i != pos) && (pages[i].activeInHierarchy))
                {
                    pages[i].SetActive(false);
                }
            }
        }


        private IEnumerator MoveAround(int value)
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
    }
}