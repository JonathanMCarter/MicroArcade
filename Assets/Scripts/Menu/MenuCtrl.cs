using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Arcade.Menu
{
    public class MenuCtrl : MenuSystem, IMenuSceneChanger
    {
        public string[] SceneNames { get { return this.sceneNames; } set { } }
        public GameObject[] SceneOptions { get { return sceneOptions; } set { } }
        public Animator SceneTransition { get { return this.sceneTransitions; } set { } }


        [SerializeField] private string[] sceneNames;
        [SerializeField] private GameObject[] sceneOptions;
        [SerializeField] private Animator sceneTransitions;

        [SerializeField] private bool shouldDisable;
        [SerializeField] private string returnScene;
        [SerializeField] private Color[] colours;

        [SerializeField] private bool isUpDown;

        private void OnEnable()
        {
            UpdateDisplay();
        }


        protected override void Start()
        {
            am = FindObjectOfType<AudioManager>();
            pos = 0;
            maxPos = sceneOptions.Length - 1;
            base.Start();
            UpdateDisplay();
        }


        protected override void Update()
        {
            base.Update();

            if (isUpDown) { MoveUD(); }
            else { MoveLR(); }
            if (ValueChanged()) { UpdateDisplay(); }
            if (Confirm()) { ConfirmOption(); }
            if (Return()) { ReturnOption(); }
        }


        public void UpdateDisplay()
        {
            if (shouldDisable)
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
            else
            {
                for (int i = 0; i < SceneOptions.Length; i++)
                {
                    if ((i == pos) && (SceneOptions[i].GetComponentInChildren<Text>().color != colours[0]))
                    {
                        SceneOptions[i].GetComponentInChildren<Text>().color = colours[0];
                    }
                    else if ((i != pos) && (SceneOptions[i].GetComponentInChildren<Text>().color == colours[0]))
                    {
                        SceneOptions[i].GetComponentInChildren<Text>().color = colours[1];
                    }
                }
            }
        }


        private void ConfirmOption()
        {
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene(sceneNames[pos]);
        }


        private void ReturnOption()
        {
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene(returnScene);
        }
    }
}