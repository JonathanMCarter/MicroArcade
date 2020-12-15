using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS.Menu
{
    public class UIButtonSwitch : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] internal GameObject[] buttons;
        [SerializeField] private bool isUD;
        [SerializeField] private bool isKeyboard;
        [SerializeField] private UnityEvent effects;

        [SerializeField] private bool isCoR;

        internal Actions action;
        internal bool isUsingController;
        internal int pos;
        internal int maxPos;

        public InputDevice currentActiveDevice;


        private void OnEnable()
        {
            action = new Actions();
            action.Enable();
        }


        private void OnDisable()
        {
            StopAllCoroutines();
            action.Disable();
            isCoR = false;
        }


        private void Start()
        {
            pos = 0;
            maxPos = buttons.Length - 1;
            isCoR = false;

            if (effects != null)
            {
                effects.Invoke();
            }
        }


        private void Update()
        {
            isUsingController = IsControllerActive();

            if (isUsingController)
            {
                if (!isCoR)
                {
                    if (isUD)
                    {
                        if (action.Menu.Joystick.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Joystick.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (action.Menu.Joystick.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Joystick.ReadValue<Vector2>().x < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                }
            }
            else
            {
                if (!isCoR)
                {
                    if (isUD)
                    {
                        if (isKeyboard && action.Menu.Keys.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (isKeyboard && action.Menu.Keys.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (isKeyboard && action.Menu.Keys.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (isKeyboard && action.Menu.Keys.ReadValue<Vector2>().x < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                }
            }

            if (!isCoR)
            {
                if (action.Menu.Accept.phase.Equals(InputActionPhase.Performed))
                {
                    buttons[pos].GetComponent<Button>().onClick.Invoke();
                    StartCoroutine(ButtonDelay());
                }


                if (action.Menu.Back.phase.Equals(InputActionPhase.Performed))
                {

                    StartCoroutine(ButtonDelay());
                }
            }
        }


        private void FixedUpdate()
        {
            InputSystem.onActionChange += (obj, change) =>
            {
                if (change == InputActionChange.ActionPerformed)
                {
                    var inputAction = (InputAction)obj;
                    var lastControl = inputAction.activeControl;
                    currentActiveDevice = lastControl.device;
                }
            };
        }


        /// <summary>
        /// Updates the position in the menu.
        /// </summary>
        /// <param name="value">value to change to.</param>
        private void UpdatePos(int value)
        {
            pos += value;

            if (pos.Equals(maxPos + 1))
            {
                pos = 0;
            }
            else if (pos.Equals(-1))
            {
                pos = maxPos;
            }

            if (effects != null)
            {
                effects.Invoke();
            }

            StartCoroutine(ButtonDelay());
        }

        /// <summary>
        /// Checks to see if a controller (either XB or PS is plugged in).
        /// </summary>
        /// <returns>True if there is a supported controller plugged in, false if not.</returns>
        private bool IsControllerActive()
        {
            if (currentActiveDevice != null)
                if (currentActiveDevice.displayName.Contains("Xbox") || currentActiveDevice.displayName.Contains("Play"))
                    return true;
                else
                    return false;
            else
                return false;
        }


        /// <summary>
        /// Runs a short button delay on the menu buttons.
        /// </summary>
        internal IEnumerator ButtonDelay()
        {
            isCoR = true;
            yield return new WaitForSecondsRealtime(.3f);
            isCoR = false;
        }
    }
}