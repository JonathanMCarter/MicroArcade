using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade.Menu;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Starshine
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
            MaxPos = (Buttons.Count - 1);
            UpdateDisplay();
            AM = MAM;
        }


        protected override void Update()
        {
            base.Update();

            MoveLR();

            if (Confirm()) 
            { 
                Trans.SetBool("ChangeScene", true);  
                ChangeScene(SceneNames[Pos]); 
            }

            UpdateDisplay();
        }


        void UpdateDisplay()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if ((Pos == i) && (Buttons[i].GetComponent<Text>().color != Colours[0]))
                {
                    Buttons[i].GetComponent<Text>().color = Colours[0];
                }
                else if ((Pos != i) && (Buttons[i].GetComponent<Text>().color != Colours[1]))
                {
                    Buttons[i].GetComponent<Text>().color = Colours[1];
                }
            }
        }
    }
}