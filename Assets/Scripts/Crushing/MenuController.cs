using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Animator crusherTransition;
        private ISceneChanger sceneChangerInterface;

        private void Start()
        {
            sceneChangerInterface = new SceneChanger();
            sceneChangerInterface.transitionsAnim = crusherTransition;
        }

        /// <summary>
        /// Changes to the defined scene
        /// </summary>
        public void ChangeToScene(string scene)
        { 
            StartCoroutine(sceneChangerInterface.ChangeScene(scene));
        }
    }
}