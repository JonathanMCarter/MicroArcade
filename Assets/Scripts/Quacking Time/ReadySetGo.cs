using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quacking
{
    public class ReadySetGo : MonoBehaviour
    {
        public MapController mapController;

        public bool RunTimer = true;
        public float StartTimer = 7;
        public List<Text> StartTxt;
        [SerializeField] private List<Rigidbody> DucksRBs;
        public List<GameObject> DuckSpawnPoints;

        private GameManager GM;
        public Animator GlobalWarming;

        private AudioManager AM;
        private bool IsPlayingAudio;


        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
            AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            for (int i = 0; i < DucksRBs.Count; i++)
            {
                DucksRBs[i].gameObject.transform.position = DuckSpawnPoints[i].transform.GetChild(0).transform.GetChild(0).position;
            }

            mapController.ResetHexagons();

            Debug.Log(AM.GetNumberOfClips());
        }


        private void Update()
        {
            if ((GM.StartGame) && (RunTimer))
            {
                Controller();
            }
        }


        private void Controller()
        {
            StartTimer -= Time.deltaTime;

            switch (Mathf.FloorToInt(StartTimer))
            {
                case 6:
                    for (int i = 0; i < StartTxt.Count; i++)
                    {
                        StartTxt[i].text = "Ready";
                    }

                    if (!IsPlayingAudio)
                    {
                        AM.Play("Ready");
                        IsPlayingAudio = true;
                    }

                    for (int i = 0; i < DucksRBs.Count; i++)
                    {
                        DucksRBs[i].gameObject.GetComponent<DuckScript>().enabled = false;
                    }

                    break;
                case 4:
                    IsPlayingAudio = false;
                    break;
                case 3:
                    for (int i = 0; i < StartTxt.Count; i++)
                    {
                        StartTxt[i].text = "Set";
                    }

                    if (!IsPlayingAudio)
                    {
                        AM.Play("Set");
                        IsPlayingAudio = true;
                    }

                    break;
                case 1:
                    IsPlayingAudio = false;
                    break;
                case 0:

                    for (int i = 0; i < StartTxt.Count; i++)
                    {
                        StartTxt[i].text = "Go";
                    }

                    if (!IsPlayingAudio)
                    {
                        AM.Play("Go");
                        IsPlayingAudio = true;
                    }

                    for (int i = 0; i < DucksRBs.Count; i++)
                    {
                        DucksRBs[i].GetComponent<DuckScript>().enabled = true;
                    }

                    GM.StartGameEvent();
                    GlobalWarming.SetBool("IsWarming", true);

                    break;
                case -1:
                    for (int i = 0; i < StartTxt.Count; i++)
                    {
                        StartTxt[i].text = "";
                        GM.RoundRunning = true;
                        RunTimer = false;
                        StartTimer = 7;
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Sets up the game for a new round...
        /// </summary>
        public void NewRoundSetup()
        {
            GM.RespawnDuck(DuckPlayers.P1);
            GM.RespawnDuck(DuckPlayers.P2);
            RunTimer = true;
        }


        /// <summary>
        /// Returns the duck rigidbodies...
        /// </summary>
        /// <returns>an array of the duck rigidbodies...</returns>
        public Rigidbody[] GetDuckRigidbodies()
        {
            return DucksRBs.ToArray();
        }


        public GameObject[] GetDuckRespawnPoints()
        {
            return DuckSpawnPoints.ToArray();
        }
    }
}