using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class EditRank : MonoBehaviour
    {
        [SerializeField] private GameObject[] rankImages;

        [SerializeField] private GameManager.Ranks lastRank = GameManager.Ranks.None;

        public CWIS_Turret turret;
        public Shaft_Ability shaft;
        public ReturnToSender missile;


        private void Update()
        {
            if (turret)
            {
                if (turret.thisRank != lastRank)
                {
                    UpdateRankDisplay();
                }
            }
            else if (shaft)
            {
                if (shaft.currentRank != lastRank)
                {
                    UpdateRankDisplay();
                }
            }
            else if (missile)
            {
                if (missile.currentRank != lastRank)
                {
                    UpdateRankDisplay();
                }
            }
        }


        private void UpdateRankDisplay()
        {
            if (turret)
            {
                if (lastRank != GameManager.Ranks.None)
                {
                    rankImages[(int)turret.thisRank - 2].SetActive(false);
                }

                rankImages[(int)turret.thisRank - 1].SetActive(true);
                lastRank = turret.thisRank;
            }
            else if (shaft)
            {
                if (lastRank != GameManager.Ranks.None)
                {
                    rankImages[(int)shaft.currentRank - 2].SetActive(false);
                }

                rankImages[(int)shaft.currentRank - 1].SetActive(true);
                lastRank = shaft.currentRank;
            }
            else if (missile)
            {
                if (lastRank != GameManager.Ranks.None)
                {
                    rankImages[(int)missile.currentRank - 2].SetActive(false);
                }

                rankImages[(int)missile.currentRank - 1].SetActive(true);
                lastRank = missile.currentRank;
            }
        }
    }
}