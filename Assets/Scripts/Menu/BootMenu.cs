using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.Saving;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
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