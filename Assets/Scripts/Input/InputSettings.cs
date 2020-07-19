using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CarterGames.Arcade.Saving;
using System.Linq;

namespace CarterGames.Arcade.UserInput
{
    public enum SupportedControllers
    {
        ArcadeBoard,
        GamePadBoth,
        KeyboardBoth,
        KeyboardP1ControllerP2,
        KeyboardP2ControllerP1,
    };

    public class InputSettings : MonoBehaviour
    {
        [SerializeField]
        private string[] activeControllers;
        private bool isCoRunning;


        public bool isMixedControls;


        public static SupportedControllers ControllerType { get; set; }


        public void OpenPopup()
        {
            Time.timeScale = 0;

            if (!SceneManager.GetSceneByName("InputCheck").isLoaded)
            {
                SceneManager.LoadSceneAsync("InputCheck", LoadSceneMode.Additive);
            }
        }

        private void Awake()
        {
            StartCoroutine(ControlCheck());
        }


        protected virtual void Update()
        {
            if (!isCoRunning)
            {
                StartCoroutine(ControlCheck());
            }

            Debug.Log(ControllerType);
        }


        /// <summary>
        /// Runs every 2 seconds to check what controller is in play. May need to adjust this as and when I get the complete product running....
        /// </summary>
        /// <returns>Nothing, but changes the SupportedControllers enum value when the controls are changed</returns>
        private IEnumerator ControlCheck()
        {
            isCoRunning = true;

            // Gets all the joystick names, retruns a sring array with any names and blank entries if a joystick was unplugged
            activeControllers = Input.GetJoystickNames();


            // Dermines if there is a controller plugged into the PC, note it will only assign player 1
            if (activeControllers.Length > 0)
            {
                for (int i = 0; i < activeControllers.Length; i++)
                {
                    if ((activeControllers.Length == 1) && (activeControllers[0] != ""))
                    {
                        isMixedControls = true;
                    }
                    else if ((activeControllers.Length >= 2) && (activeControllers[1].Contains("")) && (activeControllers[0].Contains("Controller")))
                    {
                        isMixedControls = true;
                    }
                    else if ((activeControllers.Length >= 2) && (activeControllers[0].Contains("")) && (activeControllers[activeControllers.Length - 1].Contains("Controller")))
                    {
                        isMixedControls = true;
                    }
                    else
                    {
                        isMixedControls = false;
                    }
                }

                if (!isMixedControls)
                {
                    if ((activeControllers.Length >= 2) && (activeControllers[activeControllers.Length - 1].Contains("Controller")) && (activeControllers[0].Contains("Controller")))
                    {
                        ControllerType = SupportedControllers.GamePadBoth;
                    }
                    else if ((activeControllers.Length >= 2) && (activeControllers[0].Contains("Generic")) && (activeControllers[1].Contains("Generic")))
                    {
                        ControllerType = SupportedControllers.ArcadeBoard;
                    }
                    else if ((activeControllers.Length >= 2) && activeControllers[0].Contains("") && activeControllers[activeControllers.Length - 1].Contains(""))
                    {
                        ControllerType = SupportedControllers.KeyboardBoth;
                    }
                }
                else if (isMixedControls)
                {
                    if (activeControllers.Length == 1 && activeControllers[0].Contains("Controller"))
                    {
                        if (SaveManager.LoadArcadeControlScheme() != null)
                        {
                            ControllerType = SaveManager.LoadArcadeControlScheme().SetMixedInputConfig;
                        }
                        else
                        {
                            isMixedControls = true;

                            if ((ControllerType != SupportedControllers.KeyboardP1ControllerP2) && (ControllerType != SupportedControllers.KeyboardP2ControllerP1))
                            {
                                OpenPopup();
                            }
                        }
                    }
                    else if (activeControllers.Length >= 2 && activeControllers[0].Contains("Controller") && activeControllers[1].Contains(""))
                    {
                        if (SaveManager.LoadArcadeControlScheme() != null)
                        {
                            ControllerType = SaveManager.LoadArcadeControlScheme().SetMixedInputConfig;
                        }
                        else
                        {
                            isMixedControls = true;

                            if ((ControllerType != SupportedControllers.KeyboardP1ControllerP2) && (ControllerType != SupportedControllers.KeyboardP2ControllerP1))
                            {
                                OpenPopup();
                            }
                        }
                    }
                    else if (activeControllers.Length >= 2 && activeControllers[1].Contains("Controller") && activeControllers[0].Contains(""))
                    {
                        if (SaveManager.LoadArcadeControlScheme() != null)
                        {
                            ControllerType = SaveManager.LoadArcadeControlScheme().SetMixedInputConfig;
                        }
                        else
                        {
                            isMixedControls = true;

                            if ((ControllerType != SupportedControllers.KeyboardP1ControllerP2) && (ControllerType != SupportedControllers.KeyboardP2ControllerP1))
                            {
                                OpenPopup();
                            }
                        }
                    }
                    else if (activeControllers.Length >= 2 && activeControllers[activeControllers.Length-1].Contains("Controller") && activeControllers[0].Contains(""))
                    {
                        if (SaveManager.LoadArcadeControlScheme() != null)
                        {
                            ControllerType = SaveManager.LoadArcadeControlScheme().SetMixedInputConfig;
                        }
                        else
                        {
                            isMixedControls = true;

                            if ((ControllerType != SupportedControllers.KeyboardP1ControllerP2) && (ControllerType != SupportedControllers.KeyboardP2ControllerP1))
                            {
                                OpenPopup();
                            }
                        }
                    }
                }
            }
            else
            {
                ControllerType = SupportedControllers.KeyboardBoth;
            }


            yield return new WaitForSeconds(2);

            isCoRunning = false;
        }
    }
}