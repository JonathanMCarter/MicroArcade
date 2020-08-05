using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    public class PlayMenuController : MenuSystem, IMenuSceneChanger
    {
        public string[] SceneNames { get { return this.sceneNames; } set { } }
        public GameObject[] SceneOptions { get { return this.sceneOptions; } set { } }
        public Animator SceneTransition { get { return this.sceneTransition; } set { } }

    
        [SerializeField] private string[] sceneNames;
        [SerializeField] private GameObject[] sceneOptions;
        [SerializeField] private Animator sceneTransition;
        [SerializeField] private Image[] pips;
        [SerializeField] private Color SelectedColour;



        protected override void Start()
        {
            base.Start();
            UpdateDisplay();
            maxPos = SceneOptions.Length - 1;
        }


        protected override void Update()
        {
            base.Update();
            MoveLR();
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { GoToGameMenu(); }
            if (Return()) { ReturnToMainMenu(); }
        }


        public void UpdateDisplay()
        {
            for (int i = 0; i < SceneOptions.Length; i++)
            {
                if ((i == pos) && (!SceneOptions[i].activeSelf))
                {
                    Debug.Log(pos);
                    SceneOptions[i].SetActive(true);
                    pips[i].color = SelectedColour;
                }
                else if ((i != pos) && (SceneOptions[i].activeSelf))
                {
                    SceneOptions[i].SetActive(false);
                    pips[i].color = Color.white;
                }
            }
        }


        private void GoToGameMenu()
        {
            SceneTransition.SetBool("ChangeScene", true);
            PlayerPrefs.SetInt("GameSel", pos);
            ChangeScene("Arcade-Game");
        }


        private void ReturnToMainMenu()
        {
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene("Arcade-Menu");
        }
    }
}