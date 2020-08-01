using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;
using System;
using CarterGames.MAExtras;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    [Serializable]
    public class PinballMainMenu : MenuSystem, IMenuSceneChanger
    {
        public string[] SceneNames { get { return this.sceneNames; } set { } }
        public GameObject[] SceneOptions { get { return sceneOptions; } set { } }
        public Animator SceneTransition { get { return this.sceneTransitions; } set { } }


        [SerializeField] private string[] sceneNames;
        [SerializeField] private GameObject[] sceneOptions;
        [SerializeField] private Animator sceneTransitions;


        private void OnEnable()
        {
            UpdateDisplay();
        }


        protected override void Start()
        {
            pos = 0;
            maxPos = sceneOptions.Length - 1;
            base.Start();
            UpdateDisplay();
        }


        protected override void Update()
        {
            base.Update();
            MoveLR();
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { MainMenuToAnotherScene(); }
            if (Return()) { ReturnToBoot(); }
        }


        public void UpdateDisplay()
        {
            for (int i = 0; i < SceneOptions.Length; i++)
            {
                if ((i == pos) && (!SceneOptions[i].activeSelf))
                {
                    SceneOptions[i].SetActive(true);
                    SceneOptions[i].GetComponentInParent<Animator>().SetBool("Hover", true);
                }
                else if ((i != pos) && (SceneOptions[i].activeSelf))
                {
                    SceneOptions[i].SetActive(false);

                    if (SceneOptions[i].GetComponentInParent<Animator>().GetBool("Hover"))
                    {
                        SceneOptions[i].GetComponentInParent<Animator>().SetBool("Hover", false);
                    }
                }
            }
        }

        void MainMenuToAnotherScene()
        {
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene(sceneNames[pos]);
        }


        void ReturnToBoot()
        {
            //SceneTransition.SetFloat("Multi", 2f);
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene("Boot");
        }
    }
}