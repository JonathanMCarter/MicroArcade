/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using CarterGames.Arcade.Saving;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace CarterGames.Arcade.Menu
{
    public class MusicPlayerScript : MonoBehaviour
    {
        [SerializeField] private int playerID;
        [SerializeField] private string tagString;
        [SerializeField] private string[] sceneNames;
        [SerializeField] private float volumeEdit;

        private AudioSource audioSource;
        public AudioMixer Mixer;


        private void Start()
        {
            Setup();

            if (playerID > 1)
            {
                RemoveForDuplicates();
            }
            else if (playerID == 1)
            {
                PlayMusic();
            }

            if (volumeEdit == 0)
            {
                volumeEdit = 1;
            }
        }


        private void Update()
        {
            if (!IsMenuScene() && audioSource.isPlaying)
            {
                audioSource.volume -= Time.deltaTime;

                if (audioSource.volume == 0)
                {
                    audioSource.Pause();
                }
            }
            else if (IsMenuScene() && !audioSource.isPlaying)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                if (audioSource.volume != volumeEdit)
                {
                    audioSource.volume += 1 * Time.deltaTime;
                }
            }
            else if (IsMenuScene() && audioSource.isPlaying && audioSource.volume != volumeEdit)
            {
                audioSource.volume += 1 * Time.deltaTime;
            }
        }


        /// <summary>
        /// Sets up the audio source reference and the playerID
        /// </summary>
        private void Setup()
        {
            playerID = GameObject.FindGameObjectsWithTag(tagString).Length;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);

            ArcadeData data = SaveManager.LoadArcadeSettings();

            Mixer.SetFloat("MusicVolume", data.CabinetMusicVolume);
        }

        /// <summary>
        /// Checks for duplicates and removes them before they play any sounds
        /// </summary>
        private void RemoveForDuplicates()
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag(tagString).Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag(tagString)[i].GetComponent<MusicPlayerScript>().playerID != 1)
                {
                    Destroy(GameObject.FindGameObjectsWithTag(tagString)[i]);
                }
            }
        }

        /// <summary>
        /// Plays the music if this instance is the correct one...
        /// </summary>
        private void PlayMusic()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }


        private bool IsMenuScene()
        {
            int _numberTrue = 0;

            for (int i = 0; i < sceneNames.Length; i++)
            {
                if (SceneManager.GetActiveScene().name == sceneNames[i])
                {
                    ++_numberTrue;
                }
            }

            if (_numberTrue >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}