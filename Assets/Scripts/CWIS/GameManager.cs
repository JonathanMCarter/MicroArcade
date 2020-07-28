using CarterGames.Assets.AudioManager;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class GameManager : MonoBehaviour
    {
        public enum Ranks { None, Chev1, Chev2, Chev3, Star1, Star2, Star3 }

        [Header("Weapon Ranks")]
        [SerializeField] private int cw1BestRank = 0;
        [SerializeField] private int cw2BestRank = 0;

        [Header("Weapon Rankup UI")]
        [SerializeField] internal bool openRankupUI;
        [SerializeField] private CanvasGroup rankui;
        [SerializeField] private FlickerButton[] buttons;
        [SerializeField] private Text titleTxt;
        [SerializeField] private Text descTxt;


        private int[] rankUpRequirements;
        private AudioManager am;


        [Header("Kill Counts")]
        public int cwis1Hits = 0;
        public int cwis2Hits = 0;

        [Header("Turrets")]
        public CWIS_Turret cwis1Turret;
        public CWIS_Turret cwis2Turret;

        [Header("Game Control")]
        public bool isGameRunning;
        public int score;
        public CanvasGroup gameoverUI;



        private void Start()
        {
            am = FindObjectOfType<AudioManager>();

            rankUpRequirements = new int[6]
            {
            Random.Range(3, 7),
            Random.Range(8, 14),
            Random.Range(20, 25),
            Random.Range(30, 45),
            Random.Range(50, 65),
            Random.Range(75, 100)
            };

            isGameRunning = true;
        }


        private void Update()
        {
            CheckCWForRankup(cwis1Turret, cwis1Hits, 1);
            CheckCWForRankup(cwis2Turret, cwis2Hits, 2);


            if (openRankupUI && rankui.alpha != 1)
            {
                rankui.alpha += Time.unscaledDeltaTime * 3;
                Time.timeScale = 0;

                if (!rankui.blocksRaycasts)
                {
                    rankui.blocksRaycasts = true;
                    rankui.interactable = true;
                }
            }
            else if (!openRankupUI && rankui.alpha != 0)
            {
                rankui.alpha -= Time.unscaledDeltaTime * 3;
                Time.timeScale = 1;

                if (rankui.blocksRaycasts)
                {
                    rankui.blocksRaycasts = false;
                    rankui.interactable = false;
                }
            }
        }


        public Ranks Rankup(Ranks currentRank)
        {
            switch (currentRank)
            {
                case Ranks.None:
                    
                    return Ranks.Chev1;
                case Ranks.Chev1:
                    
                    return Ranks.Chev2;
                case Ranks.Chev2:
                    
                    return Ranks.Chev3;
                case Ranks.Chev3:
                    
                    return Ranks.Star1;
                case Ranks.Star1:
                    
                    return Ranks.Star2;
                case Ranks.Star2:
                    
                    return Ranks.Star3;
                case Ranks.Star3:
                    
                    return Ranks.Star3;
                default:
                    return Ranks.None;
            }
        }

        public void IncrementCWIS1HitCount()
        {
            cwis1Hits++;
        }

        public void IncrementCWIS2HitCount()
        {
            cwis2Hits++;
        }

        private void CheckCWForRankup(CWIS_Turret turret, int hits, int bestRank)
        {
            if (hits >= rankUpRequirements[0])
            {
                turret.thisRank = Rankup(Ranks.None);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 0)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 0)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }

            if (hits >= rankUpRequirements[1])
            {
                turret.thisRank = Rankup(Ranks.Chev1);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 1)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 1)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }

            if (hits >= rankUpRequirements[2])
            {
                turret.thisRank = Rankup(Ranks.Chev2);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 2)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 2)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }

            if (hits >= rankUpRequirements[3])
            {
                turret.thisRank = Rankup(Ranks.Chev3);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 3)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 3)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }

            if (hits >= rankUpRequirements[4])
            {
                turret.thisRank = Rankup(Ranks.Star1);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 4)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 4)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }

            if (hits >= rankUpRequirements[5])
            {
                turret.thisRank = Rankup(Ranks.Star2);

                switch (bestRank)
                {
                    case 1:
                        if (cw1BestRank == 5)
                        {
                            cw1BestRank++;
                            buttons[0].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    case 2:
                        if (cw2BestRank == 5)
                        {
                            cw2BestRank++;
                            buttons[1].shouldFlicker = true;
                            am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void OpenRankupUI(CWIS_Turret whichTurret)
        {
            cwis1Turret.enabled = false;
            cwis2Turret.enabled = false;
            rankui.GetComponentsInChildren<Text>()[0].text = "Upgrading: " + whichTurret.gameObject.name;
            rankui.gameObject.GetComponent<RankupUI>().turret = whichTurret;
            rankui.gameObject.GetComponent<RankupUI>().rate = whichTurret.rateOfFire;
            rankui.gameObject.GetComponent<RankupUI>().ammo = whichTurret.ammoCap;
            rankui.gameObject.GetComponent<RankupUI>().cool = whichTurret.coolerEff;
            rankui.gameObject.GetComponent<RankupUI>().Setup();
            titleTxt.text = "...";
            descTxt.text = "Select an option to see what it does.";
            openRankupUI = true;
        }


        public void AddToScore(int amount)
        {
            score += amount;
        }

        public void ReduceScore(int amount)
        {
            score -= amount;
        }
    }
}