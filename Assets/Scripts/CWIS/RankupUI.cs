using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class RankupUI : MonoBehaviour
    {
        public CWIS_Turret turret;

        [SerializeField] internal int rate;
        [SerializeField] internal int ammo;
        [SerializeField] internal int cool;

        [SerializeField] private GameObject[] rateButtons;
        [SerializeField] private GameObject[] ammoButtons;
        [SerializeField] private GameObject[] coolButtons;

        private int option;
        [SerializeField] private bool updateLocks;
        private GameManager gm;


        internal void Setup()
        {
            updateLocks = true;

            if (!gm)
            {
                gm = FindObjectOfType<GameManager>();
            }
        }


        private void Update()
        {
            if (updateLocks)
            {
                SortLocks();
            }
        }


        private void SortLocks()
        {
            for (int i = 0; i < rateButtons.Length; i++)
            {
                if (i >= (rate + 1))
                {
                    // lock option
                    rateButtons[i].GetComponent<Button>().interactable = false;
                    rateButtons[i].GetComponentsInChildren<Image>()[3].enabled = true;
                }
                else if (i <= rate)
                {
                    // unlock option
                    rateButtons[i].GetComponent<Button>().enabled = true;
                    rateButtons[i].GetComponent<Button>().interactable = true;
                    rateButtons[i].GetComponentsInChildren<Image>()[3].enabled = false;
                }
                else
                {
                    rateButtons[i].GetComponent<Button>().enabled = false;
                    rateButtons[i].GetComponentsInChildren<Image>()[2].color = Color.red;
                    rateButtons[i].GetComponentsInChildren<Image>()[2].enabled = true;
                }
            }

            for (int i = 0; i < ammoButtons.Length; i++)
            {
                if (i >= (ammo + 1))
                {
                    // lock option
                    ammoButtons[i].GetComponent<Button>().interactable = false;
                    ammoButtons[i].GetComponentsInChildren<Image>()[3].enabled = true;
                }
                else if (i <= ammo)
                {
                    // unlock option
                    ammoButtons[i].GetComponent<Button>().enabled = true;
                    ammoButtons[i].GetComponent<Button>().interactable = true;
                    ammoButtons[i].GetComponentsInChildren<Image>()[3].enabled = false;
                }
                else
                {
                    ammoButtons[i].GetComponent<Button>().enabled = false;
                    ammoButtons[i].GetComponentsInChildren<Image>()[2].color = Color.yellow;
                    ammoButtons[i].GetComponentsInChildren<Image>()[2].enabled = true;
                }
            }

            for (int i = 0; i < coolButtons.Length; i++)
            {
                if (i >= (cool + 1))
                {
                    // lock option
                    coolButtons[i].GetComponent<Button>().interactable = false;
                    coolButtons[i].GetComponentsInChildren<Image>()[3].enabled = true;
                }
                else if (i <= cool)
                {
                    // unlock option
                    coolButtons[i].GetComponent<Button>().enabled = true;
                    coolButtons[i].GetComponent<Button>().interactable = true;
                    coolButtons[i].GetComponentsInChildren<Image>()[3].enabled = false;
                }
                else
                {
                    coolButtons[i].GetComponent<Button>().enabled = false;
                    coolButtons[i].GetComponentsInChildren<Image>()[2].color = Color.green;
                    coolButtons[i].GetComponentsInChildren<Image>()[2].enabled = true;
                }
            }

            updateLocks = false;
        }


        public void SelectOption(int selOption)
        {
            option = selOption;
        }



        public void ConfirmOption()
        {
            switch (option)
            {
                case 1:
                    turret.rateOfFire = 1;
                    break;                
                case 2:
                    turret.rateOfFire = 2;
                    break;                
                case 3:
                    turret.rateOfFire = 3;
                    break;               
                case 4:
                    turret.ammoCap = 1;
                    break;                
                case 5:
                    turret.ammoCap = 2;
                    break;                
                case 6:
                    turret.ammoCap = 3;
                    break;                
                case 7:
                    turret.coolerEff = 1;
                    break;                
                case 8:
                    turret.coolerEff = 2;
                    break;                
                case 9:
                    turret.coolerEff = 3;
                    break;
                default:
                    break;
            }

            // close UI & resume time
            gm.openRankupUI = false;
        }
    }
}