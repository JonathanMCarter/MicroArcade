using System.Collections;
using UnityEngine;
using CarterGames.Crushing.Saving;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Animator gameOverAnim;
        [SerializeField] private Animator crusherAnim;
        [SerializeField] private CanvasGroup startTutorial;

        private GameTimerScript timerScript;
        private CrusherController crusherController;
        private ISceneChanger sceneChanger;
        private bool showTutorial;

        // optimising code to not call find() outside of start...
        private SpriteRenderer gameBG;
        private PlayerScript playerScript;

        internal bool isPB;

        public CrushingData saveData;
        public float gameSpeed { get; set; }


        private void Awake()
        {
            gameBG = GameObject.FindGameObjectWithTag("GameBG").GetComponent<SpriteRenderer>();
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

            sceneChanger = new SceneChanger();
            sceneChanger.transitionsAnim = crusherAnim;
            timerScript = GetComponent<GameTimerScript>();
            timerScript.ResetTimer();
            crusherController = GetComponent<CrusherController>();
            DataSetup();
            StartCoroutine(DelayStart());
        }


        private void Update()
        {
            if ((showTutorial) && (startTutorial.alpha != 0))
            {
                startTutorial.alpha -= 1 * Time.deltaTime;
            }
        }

        /// <summary>
        /// Runs the gamne over code for the game... (only call once, it is not needed after it has been run once)
        /// </summary>
        public void GameOver()
        {
            timerScript.StopTimer();

            if (isPB)
            {
                saveData.bestNumberOfDodges = crusherController.numberDodged - 1;
                saveData.bestStarsCollected[1] = saveData.starsCollected[1];
            }

            // need to check for PB's on stars and dodges!!!
            saveData.numberOfRoundsPlayedLifetime++;
            saveData.numberOfDodges = (crusherController.numberDodged - 1);
            saveData.numberOfDodgesLifetime += (crusherController.numberDodged - 1);
            saveData.starsCollectedLifetime[0] += saveData.starsCollected[0];
            saveData.starsCollectedLifetime[1] += saveData.starsCollected[1];

            SaveManager.SaveGame(saveData);
            sceneChanger.transitionsAnim = gameOverAnim;
            crusherController.isGameRunning = false;
            ChangeSceneWithFade("GameOver");
        }


        /// <summary>
        /// Runs the delayed start, allowing for the game to show the UI.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayStart()
        {
            crusherController.isGameRunning = false;
            yield return new WaitForSeconds(1);
            showTutorial = true;
            yield return new WaitForSeconds(2);
            // Could do some Ui here to show ready set go etc...
            crusherController.isGameRunning = true;
            timerScript.StartTimer();
        }


        /// <summary>
        /// Changes the scene with a fade effect...
        /// </summary>
        /// <param name="Input">The scene to change to...</param>
        private void ChangeSceneWithFade(string Input)
        {
            sceneChanger.isCrusherTransition = false;
            StartCoroutine(sceneChanger.ChangeScene(Input));
        }


        /// <summary>
        /// Changes the scene with the crusher effect...
        /// </summary>
        /// <param name="Input">The scene to change to...</param>
        public void ChangeSceneWithCrusher(string Input)
        {
            sceneChanger.isCrusherTransition = true;
            StartCoroutine(sceneChanger.ChangeScene(Input));
        }


        /// <summary>
        /// Sets up the game data version for this round of the game...
        /// </summary>
        private void DataSetup()
        {
            saveData = SaveManager.LoadGame();
            saveData.numberOfDodges = 0;

            // changing crusher colours
            for (int i = 0; i < crusherController.crusherObjects.Length; i++)
            {
                crusherController.crusherObjects[i].GetComponentsInChildren<SpriteRenderer>()[1].color = Converters.ConvertFloatArrayToColor(saveData.crusherColour);
                crusherController.crusherObjects[i].GetComponentsInChildren<SpriteRenderer>()[5].color = Converters.ConvertFloatArrayToColor(saveData.crusherColour);
            }

            // editing the game BG to match the crusher BG
            gameBG.color = Converters.ConvertFloatArrayToColor(saveData.crusherColour);

            // change the player stuff xD
            playerScript.SetParticleColour(Converters.ConvertColourToParticleSystemGradient(Converters.ConvertFloatArrayToColor(saveData.playerColour)), saveData.playerShapeChoice - 1);

            saveData.starsCollected[0] = 0;
            saveData.starsCollected[1] = 0;
        }
    }
}