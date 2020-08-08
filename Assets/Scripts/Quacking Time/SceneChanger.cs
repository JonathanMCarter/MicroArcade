using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Assets.AudioManager;

namespace CarterGames.QuackingTime
{
    public class SceneChanger : MonoBehaviour
    {
        public List<GameObject> Menus;
        public List<GameObject> Ticks;
        public int NumberofDucks;

        public bool MoveToRounds, MoveToDucks;

        public List<CanvasGroup> Groups;

        private AudioManager am;
        //public QuackingTimeRootMenu rootMenu;
        public RoundSelectControl RSC;
        public DuckCustomMenu DCM;

        public Animator Trans;
        bool TransCoR;

        private void Start()
        {
            if (TransCoR)
            {
                TransCoR = false;
            }
        }


        private void Update()
        {
            if (am == null)
            {
                am = FindObjectOfType<AudioManager>();
            }


            if (MoveToRounds && !MoveToDucks)
            {
                Groups[0].alpha -= Time.deltaTime * 4;
                Groups[1].alpha += Time.deltaTime * 4;
                Groups[2].alpha -= Time.deltaTime * 4;
            }

            if (MoveToDucks && !MoveToRounds)
            {
                Groups[1].alpha -= Time.deltaTime * 4;
                Groups[2].alpha += Time.deltaTime * 4;

                if ((Groups[1].alpha == 0) && (Groups[2].alpha == 1))
                {
                    RSC.enabled = false;
                    DCM.enabled = true;
                }
            }

            if (!MoveToDucks && !MoveToRounds)
            {
                Groups[0].alpha += Time.deltaTime * 4;
                Groups[1].alpha -= Time.deltaTime * 4;
                Groups[2].alpha -= Time.deltaTime * 4;
            }
        }
        

        public void ChangeToLevel()
        {
            if (!TransCoR)
            {
                StartCoroutine(ChangeToLevelScene());
                TransCoR = true;
            }
        }

        IEnumerator ChangeToLevelScene()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync("QuackingTime-Level");
        }



        public void BackToMenu()
        {
            if (!TransCoR)
            {
                StartCoroutine(ChangeToMenuScene());
                TransCoR = true;
            }
        }

        IEnumerator ChangeToMenuScene()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync("QuackingTime-Menu");
        }


        public void BackToPlayMenu()
        {
            if (!TransCoR)
            {
                StartCoroutine(ChangeToMainMenu());
                TransCoR = true;
            }
        }

        IEnumerator ChangeToMainMenu()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync("PlayMenu");
        }

        public void OpenDuckSelect()
        {
            MoveToRounds = false;
            MoveToDucks = true;
        }

        public void OpenRoundsmenu()
        {
            MoveToRounds = true;
            MoveToDucks = false;
            //rootMenu.isScriptEnabled = false;
            RSC.enabled = true;
        }

        public void OpenRootMenu()
        {
            for (int i = 0; i < Menus.Count; i++)
            {
                if (Menus[i].gameObject.name == "Root")
                {
                    Menus[i].SetActive(true);
                }
                else
                {
                    Menus[i].SetActive(false);
                }
            }
        }


        public void NumberOfPlayers(int Value)
        {
            NumberofDucks = Value;
        }

        public void SetTick(GameObject Tick)
        {
            Tick.GetComponent<Image>().enabled = !Tick.GetComponent<Image>().enabled;

            for (int i = 0; i < Ticks.Count; i++)
            {
                if (Ticks[i] != Tick)
                {
                    Ticks[i].GetComponent<Image>().enabled = false;
                }
            }
        }

        public void PlayGameWithDucks()
        {
            ChangeToLevel();
        }
    }
}