using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.Menu;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class GameOverScreen : MenuSystem
    {
        public List<GameObject> Buttons;
        public List<string> SceneNames;
        public List<Color> Colours;
        public Animator Trans;
        private AudioManager MAM;

        protected override void Start()
        {
            base.Start();

            MAM = GetComponent<AudioManager>();
            maxPos = (Buttons.Count - 1);
            UpdateDisplay();
            am = MAM;
        }


        protected void Update()
        {
            base.Update();

            MoveLR();

            if (Confirm()) 
            { 
                Trans.SetBool("ChangeScene", true);  
                ChangeScene(SceneNames[pos]); 
            }

            UpdateDisplay();
        }


        void UpdateDisplay()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if ((pos == i) && (Buttons[i].GetComponent<Text>().color != Colours[0]))
                {
                    Buttons[i].GetComponent<Text>().color = Colours[0];
                }
                else if ((pos != i) && (Buttons[i].GetComponent<Text>().color != Colours[1]))
                {
                    Buttons[i].GetComponent<Text>().color = Colours[1];
                }
            }
        }
    }
}