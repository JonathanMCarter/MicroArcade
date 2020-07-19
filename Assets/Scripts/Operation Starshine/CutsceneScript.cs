using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class CutsceneScript : MonoBehaviour
    {

        public bool IsInCutscene;

        public CanvasGroup CinematicCanvasGroup;
        public CanvasGroup OverlayCanvasGroup;

        public Rigidbody2D Player1, Player2;

        public Transform Player1Intro, Player2Intro;

        public float IntroMoveSpd;

        // Stage Changing
        public bool FadeIntoGame;
        public bool FadeIntoCutscene;

        public bool Player1InPos, Player2InPos;

        public DialogueScript DS;
        public CanvasGroup DialCanvas;

        public List<DialogueFile> Files;
        public int SceneChangeNumber = 0;

        public StarshineMusicPlayer SMP;

        bool IsCoR;

        private void Start()
        {
            Player1 = GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<Rigidbody2D>();
            Player2 = GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<Rigidbody2D>();

            DS = GetComponent<DialogueScript>();

            IsInCutscene = true;
            CinematicCanvasGroup.alpha = 1;
            OverlayCanvasGroup.alpha = 0;
            StartCoroutine(Stage1Intro());
        }


        private void Update()
        {
            if (FadeIntoGame)
            {
                CinematicCanvasGroup.alpha -= Time.deltaTime;
                OverlayCanvasGroup.alpha += Time.deltaTime;
                DialCanvas.alpha -= Time.deltaTime;
            }

            if (FadeIntoCutscene)
            {
                IsInCutscene = true;
                OverlayCanvasGroup.alpha = -1f * Time.deltaTime;
                DialCanvas.alpha += Time.deltaTime;
                CinematicCanvasGroup.alpha += Time.deltaTime;

                if (!IsCoR)
                {
                    StartCoroutine(StageChange(2.5f));
                }
            }


            if (SceneChangeNumber == 0)
            {

            }


            // if in cutscene
            if (IsInCutscene)
            {
                // Check to see if players in position
                if (((int)Player1.gameObject.transform.position.y) == 0)
                {
                    Player1InPos = true;
                }

                if (((int)Player2.gameObject.transform.position.y) == 0)
                {
                    Player2InPos = true;
                }

                if (!Player1InPos)
                {
                    if (Player1.transform.position.y < Player1Intro.transform.position.y)
                    {
                        //Player1.AddForce(Vector2.up * IntroMoveSpd);
                        Player1.velocity = Vector2.up * IntroMoveSpd;
                    }
                    else if (Player1.transform.position.y > Player1Intro.transform.position.y)
                    {
                        //Player1.AddForce(Vector2.down * IntroMoveSpd);
                        Player1.velocity = Vector2.up * IntroMoveSpd;
                    }
                }
                else
                {
                    Player1.velocity = Vector2.zero;
                }

                if (!Player2InPos)
                {
                    if (Player2.transform.position.y < Player2Intro.transform.position.y)
                    {
                        //Player2.AddForce(Vector2.up * IntroMoveSpd);
                        Player2.velocity = Vector2.up * IntroMoveSpd;
                    }
                    else if (Player2.transform.position.y > Player2Intro.transform.position.y)
                    {
                        //Player2.AddForce(Vector2.down * IntroMoveSpd);
                        Player2.velocity = Vector2.up * IntroMoveSpd;
                    }
                }
                else
                {
                    Player2.velocity = Vector2.zero;
                }
            }
        }


        IEnumerator Stage1Intro()
        {
            SMP.PlayNextTrack();

            Player1.gameObject.GetComponent<PlayerController>().IsEnabled = false;
            Player2.gameObject.GetComponent<PlayerController>().IsEnabled = false;
            Player1.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Player2.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(1f);

            DS.InputPressed = true;

            yield return new WaitForSeconds(4.5f);

            DS.InputPressed = true;

            yield return new WaitForSeconds(4.5f);

            FadeIntoGame = true;

            yield return new WaitForSeconds(1.5f);

            IsInCutscene = false;
            FadeIntoGame = false;

            Player1.gameObject.GetComponent<PlayerController>().IsEnabled = true;
            Player2.gameObject.GetComponent<PlayerController>().IsEnabled = true;
            Player1.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            Player2.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            SceneChangeNumber++;
        }


        IEnumerator StageChange(float Wait)
        {
            SMP.PlayNextTrack();

            IsCoR = true;

            ClearHarmfulItems();

            Player1.gameObject.GetComponent<PlayerController>().IsEnabled = false;
            Player2.gameObject.GetComponent<PlayerController>().IsEnabled = false;
            Player1InPos = false;
            Player2InPos = false;

            DS.ChangeFile(Files[SceneChangeNumber]);
            SceneChangeNumber++;

            yield return new WaitForSeconds(1f);

            DS.InputPressed = true;

            yield return new WaitForSeconds(4.5f);

            DS.InputPressed = true;

            yield return new WaitForSeconds(4.5f);

            yield return new WaitForSeconds(Wait);

            IsInCutscene = false;
            FadeIntoCutscene = false;
            FadeIntoGame = true;
            yield return new WaitForSeconds(1.5f);
            FadeIntoGame = false;
            Player1.gameObject.GetComponent<PlayerController>().IsEnabled = true;
            Player2.gameObject.GetComponent<PlayerController>().IsEnabled = true;

            IsCoR = false;
        }


        void ClearHarmfulItems()
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("OP:SS:Harmful").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("OP:SS:Harmful")[i].activeInHierarchy)
                {
                    GameObject.FindGameObjectsWithTag("OP:SS:Harmful")[i].SetActive(false);
                }
            }
        }
    }
}