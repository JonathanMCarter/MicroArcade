using CarterGames.Arcade.UserInput;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class LeaderboardPanel : MonoBehaviour
    {
        [SerializeField] private ScrollRect rect;
        [SerializeField] private GameObject parent;

        [SerializeField] private GameObject leaderboardRowPrefab;

        private Panel panel;
        private ArcadeGameMenuCtrl aGMC;

        internal List<string> playerNames;
        internal List<string> playerScores;



        private void Start()
        {
            aGMC = FindObjectOfType<ArcadeGameMenuCtrl>();
            rect = GetComponent<ScrollRect>();
            panel = new Panel();
            panel.BaseSetup();
        }


        private void Update()
        {
            if (MenuControls.Up())
            {
                rect.verticalNormalizedPosition += 2f * Time.deltaTime;
            }

            if (MenuControls.Down())
            {
                rect.verticalNormalizedPosition -= 2f * Time.deltaTime;
            }


            if (MenuControls.Return())
            {
                aGMC.leaderboardPanelActive = false;
                parent.SetActive(false);
            }
        }


        internal void PopulateLeaderboard()
        {
            for (int i = 0; i < playerNames.Count; i++)
            {
                GameObject _go = Instantiate(leaderboardRowPrefab, transform.GetChild(0).transform.GetChild(0));
                _go.GetComponentsInChildren<Text>()[0].text = (i+1).ToString();
                _go.GetComponentsInChildren<Text>()[1].text = playerNames[i];
                _go.GetComponentsInChildren<Text>()[2].text = playerScores[i];
            }
        }


        internal void ClearLeaderboard()
        {
            for (int i = 0; i < transform.GetChild(0).transform.GetChild(0).childCount; i++)
            {
                transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}