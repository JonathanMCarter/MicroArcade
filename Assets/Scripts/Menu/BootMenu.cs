using Arcade.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Menu
{
    public class BootMenu : MenuSystem
    {
        public Animator BootMenuAnim;
        public Animator FadeToWhite;

        public ExitMicroArcade exitMicroArcade;


        protected override void Start()
        {
            Cursor.visible = false;
            SaveManager.InitialseFiles();
            MenuSystemStart();
        }


        protected override void Update()
        {
            base.Update();

            if (Confirm()) { BootScreenToRootScreen(); }
            if (Return()) { exitMicroArcade.enabled = true; exitMicroArcade.OpenQuitPopup(); }
        }


        void BootScreenToRootScreen()
        {
            BootMenuAnim.SetBool("HasPressedStart", true);
            BootMenuAnim.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            FadeToWhite.SetBool("ChangeScene", true);
            ChangeScene("MainMenu", 1.1f);
        }
    }
}