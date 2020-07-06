using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Menu
{
    [RequireComponent(typeof(MenuSystem))]
    public class MainMenuCtrl : MenuSystem
    {
        public List<GameObject> MenuOptions;
        public List<string> SceneNames;
        public Animator Trans;

        private void OnEnable()
        {
            UpdateDisplay();
        }

        protected override void Start()
        {
            Pos = 0;
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


        void UpdateDisplay()
        {
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                if ((i == Pos) && (!MenuOptions[i].activeSelf))
                {
                    Debug.Log(Pos);
                    MenuOptions[i].SetActive(true);
                    MenuOptions[i].GetComponentInParent<Animator>().SetBool("Hover", true);
                }
                else if ((i != Pos) && (MenuOptions[i].activeSelf))
                {
                    MenuOptions[i].SetActive(false);

                    if (MenuOptions[i].GetComponentInParent<Animator>().GetBool("Hover"))
                    {
                        MenuOptions[i].GetComponentInParent<Animator>().SetBool("Hover", false);
                    }
                }
            }
        }


        void MainMenuToAnotherScene()
        {
            Trans.SetBool("ChangeScene", true);
            ChangeScene(SceneNames[Pos]);
        }


        void ReturnToBoot()
        {
            //Trans.SetFloat("Multi", 2f);
            Trans.SetBool("ChangeScene", true);
            ChangeScene("Boot");
        }
    }
}