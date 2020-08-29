using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class MusicPlayerScript : MonoBehaviour
    {
        [SerializeField] private int playerID;
        [SerializeField] private string tagString;
        [SerializeField] private string[] sceneNames;
        [SerializeField] private float volumeEdit;
        [SerializeField] private AudioMixer gameAudioMixer;

        private AudioSource audioSource;
        private GameObject[] musicPlayers;

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
            if (!IsDefinedScene() && audioSource.isPlaying)
            {
                audioSource.volume -= Time.deltaTime;

                if (audioSource.volume == 0)
                {
                    audioSource.Pause();
                }
            }
            else if (IsDefinedScene() && !audioSource.isPlaying)
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
            else if (IsDefinedScene() && audioSource.isPlaying && audioSource.volume != volumeEdit)
            {
                audioSource.volume += 1 * Time.deltaTime;
            }
        }


        /// <summary>
        /// Sets up the audio source reference and the playerID
        /// </summary>
        private void Setup()
        {
            musicPlayers = GameObject.FindGameObjectsWithTag(tagString);
            playerID = musicPlayers.Length;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);

            //loadedData = SaveManager.LoadGame();

            //gameAudioMixer.SetFloat("musicVolume", loadedData.musicVolume);
        }


        /// <summary>
        /// Checks for duplicates and removes them before they play any sounds
        /// </summary>
        private void RemoveForDuplicates()
        {
            for (int i = 0; i < musicPlayers.Length; i++)
            {
                if (musicPlayers[i].GetComponent<MusicPlayerScript>().playerID != 1)
                {
                    Destroy(musicPlayers[i]);
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


        /// <summary>
        /// Checks to see if the scene that is currently loaded is one of the defined scenes for the music player to use
        /// </summary>
        /// <returns>true if the scene is lised, false if not</returns>
        private bool IsDefinedScene()
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