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
        [SerializeField] private GameObject[] covers;

        [SerializeField] private bool isUpDown;
        [SerializeField] private bool useTrans;

        private Vector3 checkVector = new Vector3(1.1f, 1.1f, 1f);

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
            if (!shouldDisable)
            {
                for (int i = 0; i < SceneOptions.Length; i++)
                {
                    if ((i == pos) && (SceneOptions[i].transform.localScale != checkVector))
                    {
                        SceneOptions[i].transform.localScale = checkVector;
                        covers[i].SetActive(true);

                    }
                    else if ((i != pos) && (SceneOptions[i].transform.localScale == checkVector))
                    {
                        SceneOptions[i].transform.localScale = Vector3.one;
                        covers[i].SetActive(false);
                    }
                }
            }
            else
            {

            }
        }


        private void ConfirmOption()
        {
            if (useTrans)
            {
                SceneTransition.SetBool("ChangeScene", true);
                ChangeScene(sceneNames[pos]);
            }
            else
            {
                if (pos == maxPos)
                {
                    SceneTransition.SetBool("ChangeScene", true);
                    ChangeScene(sceneNames[pos]);
                }
                else
                {
                    ChangeScene(sceneNames[pos]);
                }
            }
        }


        private void ReturnOption()
        {
            SceneTransition.SetBool("ChangeScene", true);
            ChangeScene(returnScene);
        }
    }
}