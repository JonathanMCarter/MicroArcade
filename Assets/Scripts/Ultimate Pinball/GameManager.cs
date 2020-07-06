using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade.Saving;
using UnityEngine.SceneManagement;
using Pinball.BallCtrl;

namespace Pinball
{
    public class GameManager : MonoBehaviour
    {

        public struct BG_PlayerStats
        {
            public int Health;
            public int Score;
            public string Name;
        };

        public BG_PlayerStats Player1Stats;
        public BG_PlayerStats Player2Stats;

        public enum GameTypes
        {
            None,
            Lives,
            Timer,
            SetScore,
        };

        [Header("What game type is been played")]
        public GameTypes Game_Type;

        [Header("Timer Game Variables")]
        public float Timer;
        public float TimeLimit;
        public bool StartTimer;

        public int TargetScore;

        public Animator Trans;
        bool IsCoR;
        BallSpawnerScript BSS;
        UltimatePinballSessionData SessionData;

        private void Awake()
        {
            Player1Stats.Health = 3;
            Player2Stats.Health = 3;
            BSS = FindObjectOfType<BallSpawnerScript>();

            UltimatePinballData Data = SaveManager.LoadUltimatePinball();

            Game_Type = (GameTypes)Data.LastGameTypeSelected;

            switch (Game_Type)
            {
                case GameTypes.Lives:
                    Player1Stats.Health = Data.LastGameTypeAmountSelected;
                    Player2Stats.Health = Data.LastGameTypeAmountSelected;
                    break;
                case GameTypes.Timer:
                    TimeLimit = Data.LastGameTypeAmountSelected;
                    Timer = TimeLimit;
                    break;
                case GameTypes.SetScore:
                    TargetScore = Data.LastGameTypeAmountSelected;
                    break;
                default:
                    break;
            }
        }


        private void Update()
        {

            if (IsGameOver())
            {
                // DO game over stuff
                Debug.Log("true");

                BSS.enabled = false;
                DisableAllActiveBalls();

                SessionData = new UltimatePinballSessionData(Player1Stats, Player2Stats);
                SaveManager.SaveUltimatePinballSession(SessionData);

                GoToEndScene();
            }
            else
            {
                Debug.Log("false");
            }


            // Run the game timer if the game type is set to it...
            if ((Game_Type == GameTypes.Timer) && (!IsGameOver() && (StartTimer)))
            {
                Timer -= Time.deltaTime;
            }
        }


        bool IsGameOver()
        {
            switch (Game_Type)
            {
                case GameTypes.Lives:

                    if ((Player1Stats.Health <= 0) || (Player2Stats.Health <= 0)) { return true; }
                    else { return false; }

                case GameTypes.Timer:

                    if (Timer <= 0) { return true; }
                    else { return false; }

                case GameTypes.SetScore:

                    if ((Player1Stats.Score >= TargetScore) || (Player2Stats.Score >= TargetScore)) { return true; }
                    else { return false; }

                default:
                    return false;
            }
        }

        void DisableAllActiveBalls()
        {
            for (int i = 0; i < FindObjectsOfType<BallMoveScript>().Length; i++)
            {
                FindObjectsOfType<BallMoveScript>()[i].StopBallMoving();
            }
        }


        void GoToEndScene()
        {
            if (!IsCoR)
            {
                StartCoroutine(MoveToEndSceneCo());
                IsCoR = true;
            }
        }

        IEnumerator MoveToEndSceneCo()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadSceneAsync("Ultimate-Pinball-End");
        }
    }
}