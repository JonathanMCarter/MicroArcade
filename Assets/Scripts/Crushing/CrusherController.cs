using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class CrusherController : MonoBehaviour
    {
        [SerializeField] private GameObject[] gridObjects;
        [SerializeField] internal GameObject[] crusherObjects;
        [SerializeField] private float[] gameDiff;
        [SerializeField] private List<int> crushersUsed;
        [SerializeField] private bool isMenuScene = false;

        private AudioManager audioManager;
        private GameManager gameManager;
        private int crusherToUse;
        private int lastActiveCrusher = -1;
        private bool isCrusherActive = false;

        internal int numberDodged;

        public GameObject player;
        public bool isGameRunning;


        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
            crushersUsed = new List<int>();
            UpdateGameSpeed(1);
        }


        private void Update()
        {
            // Are the !!! NOT !!! crushers the menu ones?
            if (!isMenuScene)
            {
                // is the game active? & are there any active crushers right now?
                if ((isGameRunning) && (!isCrusherActive))
                {
                    // 1/2 chance to target player
                    ++numberDodged;

                    // 1/2 Chance to use left right crushers
                    if (Chance(0, 1) > 0)
                    {
                        for (int i = 0; i < gridObjects.Length - 9; i++)
                        {
                            if (CheckCoords(player.transform.position.y, crusherObjects[i].transform.position.y, 1f))
                            {
                                crusherToUse = i;
                                break;
                            }
                        }
                    }
                    // 1/2 chance to use left right crushers
                    else
                    {
                        for (int i = 5; i < gridObjects.Length; i++)
                        {
                            if (CheckCoords(player.transform.position.x, crusherObjects[i].transform.position.x, 1f))
                            {
                                crusherToUse = i;
                                break;
                            }
                        }
                    }


                    if (lastActiveCrusher == crusherToUse)
                    {
                        // Removed duplicate calls of crushers
                        while (lastActiveCrusher == crusherToUse)
                        {
                            RemoveDupCrushers();
                        }
                    }
                    else
                    {
                        // Does the crushing!
                        crushersUsed.Add(crusherToUse);
                        ActiveCrusher();
                    }
                }
            }


            // Diffuculty Stuff
            // Needs to increase and level off at a hardcore mode where endurance becomes the challenge!!!
            // Using time elapsed or crushers dodged...
            DiffucultyRampup();
        }


        /// <summary>
        /// Manages the diffuculty of the crushers...
        /// </summary>
        private void DiffucultyRampup()
        {
            switch (numberDodged)
            {
                case 3:
                    UpdateGameSpeed(gameDiff[1]);
                    break;
                case 7:
                    UpdateGameSpeed(gameDiff[2]);
                    break;
                case 15:
                    UpdateGameSpeed(gameDiff[3]);
                    break;
                case 40:
                    UpdateGameSpeed(gameDiff[4]);
                    break;
                case 50:
                    UpdateGameSpeed(gameDiff[5]);
                    break;
                case 70:
                    UpdateGameSpeed(gameDiff[6]);
                    break;
                case 100:
                    UpdateGameSpeed(gameDiff[7]);
                    break;
                case 125:
                    UpdateGameSpeed(gameDiff[8]);
                    break;
                case 150:
                    UpdateGameSpeed(gameDiff[9]);
                    break;
                case 200:
                    UpdateGameSpeed(gameDiff[10]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Runs the crusher animation as well as enabeling and disabeling the trigger boxes to not missfire on the player
        /// </summary>
        /// <returns></returns>
        private IEnumerator Smash()
        {
            // Flashing Grid warning

            gridObjects[crusherToUse].GetComponent<Animator>().SetBool("shouldWarn", true);
            audioManager.PlayFromTime("Alarm", 1f, .75f, Random.Range(1f, 1.025f));

            yield return new WaitForSeconds(gameManager.gameSpeed);

            gridObjects[crusherToUse].GetComponent<Animator>().SetBool("shouldWarn", false);

            // enables trigger boxes
            foreach (BoxCollider2D B in crusherObjects[crusherToUse].GetComponentsInChildren<BoxCollider2D>())
            {
                if (B.isTrigger)
                {
                    B.enabled = true;
                }
            }

            // enables the crusher (crusher script not needed?)
            crusherObjects[crusherToUse].GetComponent<Animator>().SetBool("Crush", true);
            audioManager.Play("Crush-Hit", .65f, Random.Range(1f, 1.025f));

            // waits time before disabling triggers
            yield return new WaitForSeconds(gameManager.gameSpeed);

            foreach (BoxCollider2D B in crusherObjects[crusherToUse].GetComponentsInChildren<BoxCollider2D>())
            {
                if (B.isTrigger)
                {
                    B.enabled = false;
                }
            }

            yield return new WaitForSeconds(gameManager.gameSpeed);

            // Allows another crusher to be selected
            isCrusherActive = false;
        }


        /// <summary>
        /// Rolls for chance with the inputted values
        /// </summary>
        /// <param name="Lowest">The lowest value allowed</param>
        /// <param name="Highest">The highest value allowed (This will +1 to make sure it can be acheived, as Random.Range cuts the value off otherwise)</param>
        /// <returns></returns>
        private int Chance(int Lowest, int Highest)
        {
            return Random.Range(Lowest, Highest + 1);
        }


        /// <summary>
        /// Checks for where the player is in the game level against the crushers positions to see which ones are in a position to hit the player
        /// </summary>
        /// <param name="PlayerCoord">X or Y value of the player to check against</param>
        /// <param name="CrusherCoord">X or Y value of the crusher that is been checked</param>
        /// <param name="Offset">Amount of margin allowed either side of the player X or Y inputted value</param>
        /// <returns></returns>
        private bool CheckCoords(float PlayerCoord, float CrusherCoord, float Offset)
        {
            if ((PlayerCoord - Offset < CrusherCoord) && (PlayerCoord + Offset > CrusherCoord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Removes any duplicate crusher calls, so if it calls 8 and 8 was called last time it will roll again until it gets a different crusher.
        /// </summary>
        private void RemoveDupCrushers()
        {
            crusherToUse = Random.Range(0, gridObjects.Length);
            crushersUsed.Add(crusherToUse);
            crushersUsed.RemoveAt(crushersUsed.Count - 1);
        }


        /// <summary>
        /// Actives the called crusher and adds it to the list of called crushers 
        /// </summary>
        private void ActiveCrusher()
        {
            crusherObjects[crusherToUse].GetComponent<CrusherScript>().isCrushing = true;
            isCrusherActive = true;
            lastActiveCrusher = crusherToUse;
            StartCoroutine(Smash());
        }


        /// <summary>
        /// Updates the game speed to the new speed
        /// </summary>
        /// <param name="toCheck">the value to check</param>
        private void UpdateGameSpeed(float toCheck)
        {
            if (gameManager.gameSpeed != toCheck)
            {
                gameManager.gameSpeed = toCheck;
            }
        }
    }
}