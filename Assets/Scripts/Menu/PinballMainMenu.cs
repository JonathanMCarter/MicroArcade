using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CarterGames.Arcade.UserInput;
using CarterGames.Assets.AudioManager;
using System;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Menu
{
    [Serializable]
    public class PinballMainMenu : MenuSystem, IMultiLayerMenu
    {
        public string[,] SceneNames { get { return this.sceneNames; } set { } }
        public GameObject[,] SceneObjects { get { return this.sceneObject; } set { } }
        public Animator SceneTransition { get { return this.sceneTransition; } set { } }


        [SerializeField] private string[,] sceneNames;
        [SerializeField] private GameObject[,] sceneObject;
        [SerializeField] private Animator sceneTransition;




        public void UpdateDisplay()
        {

        }
    }
}