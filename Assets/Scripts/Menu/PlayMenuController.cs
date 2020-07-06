using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Menu
{
    [RequireComponent(typeof(MenuSystem))]
    public class PlayMenuController : MenuSystem
    {
        public List<GameObject> Games;
        public List<Image> Pips;
        public List<string> GameSceneNames;
        public Animator Trans;
        public Color32 SelectedColour;

        protected override void Start()
        {
            base.Start();
            UpdateDisplay();
            MaxPos = Games.Count - 1;
        }

        protected override void Update()
        {
            base.Update();
            MoveLR();
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { GoToGameMenu(); }
            if (Return()) { ReturnToMainMenu(); }
        }

        void UpdateDisplay()
        {
            for (int i = 0; i < Games.Count; i++)
            {
                if ((i == Pos) && (!Games[i].activeSelf))
                {
                    Debug.Log(Pos);
                    Games[i].SetActive(true);
                    Pips[i].color = SelectedColour;
                }
                else if ((i != Pos) && (Games[i].activeSelf))
                {
                    Games[i].SetActive(false);
                    Pips[i].color = Color.white;
                }
            }
        }


        void GoToGameMenu()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            ChangeScene(GameSceneNames[Pos]);
        }


        void ReturnToMainMenu()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            ChangeScene("MainMenu");
        }
    }
}