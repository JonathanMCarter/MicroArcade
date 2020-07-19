using CarterGames.Assets.AudioManager;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing.Menu
{
    public class Transitions : MonoBehaviour
    {
        private Image[] crusherSides;
        private AudioManager audioManager;


        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }

            crusherSides = GetComponentsInChildren<Image>();

            for (int i = 0; i < crusherSides.Length; i++)
            {
                if (!crusherSides[i].enabled)
                {
                    crusherSides[i].enabled = true;
                }
            }
        }


        public void ShutterSound()
        {
            //audioManager.PlayFromTime("Shutter", 5.5f, .85f);
        }
    }
}