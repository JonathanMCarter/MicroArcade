using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using CarterGames.Crushing.Saving;
using System.Collections.Generic;
using CarterGames.Utilities;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing.Menu
{
    public class SettingsScript : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private List<GameObject> options;
        [SerializeField] private Color[] buttonColours;
        [SerializeField] private Image[] buttonImages;

        private ISceneChanger sceneChangerInterface;
        private IMenuSystem menuSystem;
        private AudioManager audioManager;
        private short lastShape = 0;

        internal CrushingData gameData;

        public ColourPicker playerColour;
        public ColourPicker crusherColour;
        public Image[] shapes;


        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

            sceneChangerInterface = new SceneChanger();
            sceneChangerInterface.transitionsAnim = anim;

            menuSystem = new MenuSystem();
            menuSystem.menuOptions = options;

            gameData = SaveManager.LoadGame();

            musicSlider.value = gameData.musicVolume;
            sfxSlider.value = gameData.sfxVolume;

            audioMixer.SetFloat("musicVolume", gameData.musicVolume);
            audioMixer.SetFloat("sfxVolume", gameData.sfxVolume);

            buttonImages[0].color = buttonColours[1];
        }



        private void Update()
        {
            if (lastShape != gameData.playerShapeChoice)
            {
                for (int i = 0; i < shapes.Length; i++)
                {
                    if (i == (gameData.playerShapeChoice - 1))
                    {
                        shapes[i].color = Color.grey;
                    }
                    else if (i != (gameData.playerShapeChoice - 1) && shapes[i].color != Color.white)
                    {
                        shapes[i].color = Color.white;
                    }
                }
            }
        }


        /// <summary>
        /// Updates the colours for the buttons to show which one is selected etc...
        /// </summary>
        /// <param name="number">the pos that is set</param>
        private void UpdateButtonColours(int number)
        {
            for (int i = 0; i < buttonImages.Length; i++)
            {
                if ((i == number) && (buttonImages[i].color == buttonColours[0]))
                {
                    buttonImages[i].color = buttonColours[1];
                }
                else if ((i != number) && (buttonImages[i].color == buttonColours[1]))
                {
                    buttonImages[i].color = buttonColours[0];
                }
            }
        }


        /// <summary>
        /// Applies the changes made from the music and sound fx sliders (Done through UI so no code referneces)...
        /// </summary>
        public void ApplyChanges()
        {
            gameData.musicVolume = musicSlider.value;
            gameData.sfxVolume = sfxSlider.value;
            audioMixer.SetFloat("musicVolume", gameData.musicVolume);
            audioMixer.SetFloat("sfxVolume", gameData.sfxVolume);
        }


        /// <summary>
        /// Saves the changes made and returns to the main menu...
        /// </summary>
        public void Menu()
        {
            audioManager.Play("MenuButton", .75f);

            // Apply changes for gameData
            if (playerColour.GetColourPicked() != Color.black)
            {
                gameData.playerColour = Converters.ConvertColorToFloatArray(playerColour.GetColourPicked());
                gameData.playerPipPosition = ExtraSerialize.Vector3Serialize(playerColour.GetPipPosition());
            }

            if (crusherColour.GetColourPicked() != Color.black)
            {
                gameData.crusherColour = Converters.ConvertColorToFloatArray(crusherColour.GetColourPicked());
                gameData.crusherPipPosition = ExtraSerialize.Vector3Serialize(crusherColour.GetPipPosition());
            }

            ApplyChanges();

            // Save and change to main menu
            StartCoroutine(sceneChangerInterface.ChangeScene("MainMenu"));
            SaveManager.SaveGame(gameData);
        }


        /// <summary>
        /// Plays a sound when you move in the menu...
        /// </summary>
        /// <param name="newPos">the position that is been changed to...</param>
        public void ChangePos(int newPos)
        {
            menuSystem.ChangePosition(newPos);
            audioManager.Play("MenuButton", .75f);
            UpdateButtonColours(newPos);
        }


        /// <summary>
        /// Sets the shape to the new choice...
        /// </summary>
        /// <param name="number">The number that is sent to the game data...</param>
        public void SetPlayerShape(int number)
        {
            lastShape = gameData.playerShapeChoice;
            gameData.playerShapeChoice = (short)number;
        }
    }
}