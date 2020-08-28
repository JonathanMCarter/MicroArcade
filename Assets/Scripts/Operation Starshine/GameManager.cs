using CarterGames.Arcade.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarterGames.Starshine
{
    public enum Diffuculty
    {
        Easy,
        Normal,
        Hard,
        Ludicous,
    };

    public enum Ships
    {
        AetherAlpha,
        AetherAttack,
        ScarletInter,
        ScarletOne,
        UnityGunship,
        CelestialYari,
        CelestialStandard,
        CelestialRocket,
        CelestialDrone,
        CelestialRocketDrone,
        None,
    };

    public enum Stages
    {
        Stage1,
        Stage2,
        Stage3
    };

    public class GameManager : MonoBehaviour
    {
        public Diffuculty GameDiffuculty;

        public List<ShipStats> PlayerShips;

        public List<GameObject> Player1ShipPrefabs;
        public List<GameObject> Player2ShipPrefabs;

        public Stages ActiveStage;

        public GameObject HealthBarUIPrefab;

        public int PlayersAlive;
        Starshine_SaveScript SSS;

        public int Player1Score;
        public int Player2Score;

        public CanvasGroup OverlayCG;

        public bool IsGameVictory;

        public PlayerStats _Player1Stats, _Player2Stats;

        OperationStarshineData _data;
        public Animator animator;


        private void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Awake()
        {
            PlayersAlive = 2;

            _data = SaveManager.LoadOperationStarshine();
            Player1ShipPrefabs[_data.LastPlayer1ShipSelection].SetActive(true);
            Player2ShipPrefabs[_data.LastPlayer2ShipSelection].SetActive(true);
        }



        private void Update()
        {
            if (IsGameOver())
            {
                // need the get both players stuff here somehow....
                _data = SaveManager.LoadOperationStarshine();
                _Player1Stats = Player1ShipPrefabs[_data.LastPlayer1ShipSelection].GetComponent<ShipManagement>().PlayerStats;
                _Player2Stats = Player2ShipPrefabs[_data.LastPlayer2ShipSelection].GetComponent<ShipManagement>().PlayerStats;
                _data.player1Stats = _Player1Stats;
                _data.player2Stats = _Player2Stats;
                _data.Player1Score = Player1Score;
                _data.Player2Score = Player2Score;
                SaveManager.SaveStarshinePlayerStats(_data);

                if (OverlayCG.alpha > 0)
                {
                    OverlayCG.alpha = -1f * Time.deltaTime;
                }

                StartCoroutine(ChangeToLoseScene());
            }


            if (IsGameVictory)
            {
                _data = SaveManager.LoadOperationStarshine();
                _Player1Stats = Player1ShipPrefabs[_data.LastPlayer1ShipSelection].GetComponent<ShipManagement>().PlayerStats;
                _Player2Stats = Player2ShipPrefabs[_data.LastPlayer2ShipSelection].GetComponent<ShipManagement>().PlayerStats;
                _data.player1Stats = _Player1Stats;
                _data.player2Stats = _Player2Stats;
                _data.Player1Score = Player1Score;
                _data.Player2Score = Player2Score;
                SaveManager.SaveStarshinePlayerStats(_data);

                for (int i = 0; i < FindObjectsOfType<PlayerController>().Length; i++)
                {
                    FindObjectsOfType<PlayerController>()[i].enabled = false;
                }

                StartCoroutine(ChangeToVictoryScene());
            }
        }


        bool IsGameOver()
        {
            if (PlayersAlive == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void SetVictory(bool Input = true)
        {
            IsGameVictory = Input;
        }

        public void SetStats(PlayerStats stats, int player)
        {
            if (player == 1)
            {
                _Player1Stats = stats;
                Debug.Log(">> Alt fired: " + stats.altShotsFired);
            }
            else
            {
                _Player2Stats = stats;
            }
        }


        private IEnumerator ChangeToVictoryScene()
        {
            animator.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1.1f);
            SceneManager.LoadSceneAsync("Operation-Starshine-Win");
        }


        private IEnumerator ChangeToLoseScene()
        {
            animator.SetBool("ChangeScene", true);
            yield return new WaitForSeconds(1.1f);
            SceneManager.LoadSceneAsync("Operation-Starshine-Lose");
        }
    }
}
