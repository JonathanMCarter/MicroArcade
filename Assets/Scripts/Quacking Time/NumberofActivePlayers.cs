using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.QuackingTime
{
    public class NumberofActivePlayers : MonoBehaviour
    {
        public int Selection;
        public int MaxSelection = 2;

        public List<Image> SelectionBorders;
        public GameObject RndSelect;

        private bool IsCoRunning;

        public AudioManager AM;

        private void Start()
        {
            AM = FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            if (!IsCoRunning)
            {
                StartCoroutine(PlayerSelect());
            }

            if (Input.GetButtonDown("JumpP1"))
            {
                AM.Play("Menu_Select", .75f);
                SetNumberofPlayers(Selection);
            }
        }

        private IEnumerator PlayerSelect()
        {
            IsCoRunning = true;

            if (Input.GetAxis("HorizontalP1") > 0)
            {
                if (Selection < MaxSelection) { Selection++; }
                else { Selection = 0; }
                AM.Play("Menu_Switch", .5f);
            }
            else if (Input.GetAxis("HorizontalP1") < 0)
            {
                if (Selection > 0) { Selection--; }
                else { Selection = MaxSelection; }
                AM.Play("Menu_Switch", .5f);
            }


            SelectionBorders[0].enabled = false;
            SelectionBorders[1].enabled = false;
            SelectionBorders[2].enabled = false;
            SelectionBorders[Selection].enabled = true;

            yield return new WaitForSeconds(.25f);
            IsCoRunning = false;
        }


        public void SetNumberofPlayers(int Ply)
        {
            PlayerPrefs.SetInt("Players", Ply);
        }


    }
}