using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Quacking
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] internal Pickupspawning Pickup;
        [SerializeField] private Canvas GameCan;
        [SerializeField] private Canvas EndCan;
        [SerializeField] private ReadySetGo RSG;
        [SerializeField] private MapController mapController;

        public bool StartGame;                          // Start the game?
        public int Duck1Score, Duck2Score;              // Duck Scoring
        public int FinalDuck2Score;
        public int FinalDuck1Score;                     // Duck Saved Scores
        public int RoundsCompleted;                     // Number of rounds one per duck
        public int TargetRounds;

        public float GameTimer;
        public float RoundLength;
        public bool RoundRunning;

        public bool CantCrossTheStreams = false;
        public MenuButtonController EndScreenButton;

        public Animator Anim;


        private void Start()
        {
            Pickup = FindObjectOfType<Pickupspawning>();

            TargetRounds = PlayerPrefs.GetInt("Rounds");

            PlayerPrefs.SetInt("P1-Rounds", 0);
            PlayerPrefs.SetInt("P2-Rounds", 0);

            EndScreenButton.enabled = false;

            RSG = FindObjectOfType<ReadySetGo>();
        }


        private void Update()
        {
            if (RoundRunning)
            {
                bool isSet = false;
                GameTimer += Time.deltaTime;


                if (GameTimer > RoundLength)
                {
                    DuckPlayers RoundWinner = GetWinningDuck();

                    EndScreenButton.enabled = true;

                    Rigidbody[] _duckRigidbodies = RSG.GetDuckRigidbodies();

                    for (int i = 0; i < _duckRigidbodies.Length; i++)
                    {
                        // Duck Wins
                        if (_duckRigidbodies[i].gameObject.GetComponent<DuckScript>().Ducks == RoundWinner)
                        {
                            RSG.GlobalWarming.SetBool("IsWarming", false);
                            GameCan.enabled = false;
                            EndCan.enabled = true;
                            _duckRigidbodies[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            _duckRigidbodies[i].gameObject.GetComponent<DuckScript>().enabled = false;
                            FindObjectOfType<RotateScript>().transform.position = _duckRigidbodies[i].transform.position;
                            FindObjectOfType<RotateScript>().Rotate = true;
                            RoundRunning = false;
                            SetRounds(RoundWinner);
                            isSet = true;
                        }
                        // its a tie
                        if (!isSet)
                        {
                            GameCan.enabled = false;
                            EndCan.enabled = true;
                            FindObjectOfType<RotateScript>().transform.position = GameObject.Find("EndCube").transform.position;
                            FindObjectOfType<RotateScript>().Rotate = true;
                            RoundRunning = false;
                        }
                    }
                }
            }
        }


        public void StartGameEvent()
        {
            StartGame = true;
        }


        public void SetDuckScoreToFinal(DuckPlayers Duck, int multiplier)
        {
            switch (Duck)
            {
                case DuckPlayers.P1:
                    FinalDuck1Score += Duck1Score * multiplier;
                    Duck1Score = 0;
                    mapController.RemoveAllOfOneColour(DuckPlayers.P1);
                    mapController.ResetLockedHexagons();
                    break;
                case DuckPlayers.P2:
                    FinalDuck2Score += Duck2Score * multiplier;
                    Duck2Score = 0;
                    mapController.RemoveAllOfOneColour(DuckPlayers.P2);
                    mapController.ResetLockedHexagons();
                    break;
                default:
                    break;
            }
        }


        private void SetRounds(DuckPlayers Winner)
        {
            switch (Winner)
            {
                case DuckPlayers.P1:
                    PlayerPrefs.SetInt("P1-Rounds", PlayerPrefs.GetInt("P1-Rounds") + 1);
                    break;
                case DuckPlayers.P2:
                    PlayerPrefs.SetInt("P2-Rounds", PlayerPrefs.GetInt("P2-Rounds") + 1);
                    break;
                case DuckPlayers.None:
                    break;
                default:
                    break;
            }
        }

        private bool CheckRounds()
        {
            if ((PlayerPrefs.GetInt("P1-Rounds") | PlayerPrefs.GetInt("P2-Rounds")) < TargetRounds)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void ResetDuckScores()
        {
            Duck1Score = 0;
            Duck2Score = 0;
        }

        private void ResetDuckFinalScores()
        {
            FinalDuck1Score = 0;
            FinalDuck2Score = 0;
        }


        internal void RespawnDuck(DuckPlayers ZeDuck)
        {
            Rigidbody[] _duckRigidbodies = RSG.GetDuckRigidbodies();
            GameObject[] _duckSpawnPoints = RSG.GetDuckRespawnPoints();

            switch (ZeDuck)
            {
                case DuckPlayers.P1:
                    _duckRigidbodies[0].gameObject.transform.position = _duckSpawnPoints[0].transform.GetChild(0).transform.GetChild(0).position;
                    _duckRigidbodies[0].gameObject.transform.rotation = Quaternion.Euler(0, -45, 0);
                    _duckRigidbodies[0].velocity = Vector3.zero;
                    _duckRigidbodies[0].mass = 1;
                    Duck1Score = 0;
                    mapController.RemoveAllOfOneColour(DuckPlayers.P1);
                    break;
                case DuckPlayers.P2:
                    _duckRigidbodies[1].gameObject.transform.position = _duckSpawnPoints[1].transform.GetChild(0).transform.GetChild(0).position;
                    _duckRigidbodies[1].gameObject.transform.rotation = Quaternion.Euler(0, 140, 0);
                    _duckRigidbodies[1].velocity = Vector3.zero;
                    _duckRigidbodies[1].mass = 1;
                    Duck2Score = 0;
                    mapController.RemoveAllOfOneColour(DuckPlayers.P2);
                    break;
                default:
                    break;
            }
        }


        public DuckPlayers GetWinningDuck()
        {
            if (FinalDuck1Score == FinalDuck2Score)
            {
                return DuckPlayers.None;
            }
            else if (FinalDuck2Score == FinalDuck1Score)
            {
                return DuckPlayers.None;
            }
            else if (FinalDuck1Score > FinalDuck2Score)
            {
                return DuckPlayers.P1;
            }
            else if (FinalDuck2Score > FinalDuck1Score)
            {
                return DuckPlayers.P2;
            }
            else
            {
                return DuckPlayers.None;
            }
        }


        public void NewRound()
        {
            if (CheckRounds())
            {
                Anim.SetBool("ChangeScene", true);
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("QuackingTime-End");
            }
            else
            {
                FindObjectOfType<RotateScript>().Rotate = false;
                GameCan.enabled = true;
                EndCan.enabled = false;
                ++RoundsCompleted;
                mapController.ResetHexagons();
                RSG.NewRoundSetup();
                GameTimer = 0;
                ResetDuckScores();
                ResetDuckFinalScores();
                EndScreenButton.enabled = false;
            }
        }
    }
}