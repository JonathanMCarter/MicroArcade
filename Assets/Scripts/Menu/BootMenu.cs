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
        [SerializeField] private Animator BootMenuAnim;
        [SerializeField] private Animator FadeToWhite;
        [SerializeField] private ExitMicroArcade exitMicroArcade;

        public string sceneName = "Arcade-Menu";
        public bool shouldHideCursor;


        private void Awake()
        {
            SaveManager.InitialseFiles();
        }


        protected override void Start()
        {
            if (shouldHideCursor)
            {
                Cursor.visible = false;
            }

            MenuSystemStart();
        }


        protected override void Update()
        {
            base.Update();

            if (Confirm()) 
            { 
                BootScreenToRootScreen(); 
            }

            if (Return()) 
            { 
                exitMicroArcade.enabled = true; 
                exitMicroArcade.OpenQuitPopup(); 
            }
        }


        void BootScreenToRootScreen()
        {
            BootMenuAnim.SetBool("HasPressedStart", true);
            BootMenuAnim.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            FadeToWhite.SetBool("ChangeScene", true);
            ChangeScene(sceneName, 1.1f);
        }
    }
}