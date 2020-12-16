using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;
using CarterGames.Arcade;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class UIButtonSwitch : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] internal GameObject[] buttons;
        [Space(5f)]
        [SerializeField] private bool isUD;
        [SerializeField] private bool resetPos;
        [Space(5f)]
        [SerializeField] private bool useGrid;
        [SerializeField] private int maxColumns;
        [Space(5f)]
        [SerializeField] private UnityEvent effects;

        [SerializeField] internal bool isCoR;

        internal Actions action;
        internal bool isUsingController;
        internal int pos;
        internal int maxPos;

        public InputDevice currentActiveDevice;


        private void OnEnable()
        {
            action = new Actions();
            action.Enable();

            ForceButtonDelay();

            if (resetPos)
            {
                pos = 0;
                effects.Invoke();
            }
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
                        if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
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
                        if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                    else
                    {
                        if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                        {
                            UpdatePos(1);
                        }
                        else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                        {
                            UpdatePos(-1);
                        }
                    }
                }
            }


            if (!isCoR)
            {
                if (useGrid)
                {
                    if (action.Menu.Movement.ReadValue<Vector2>().y > .1f)
                    {
                        UpdatePos(-maxColumns, true);
                    }
                    else if (action.Menu.Movement.ReadValue<Vector2>().y < -.1f)
                    {
                        UpdatePos(maxColumns, true);
                    }

                    if (action.Menu.Movement.ReadValue<Vector2>().x > .1f)
                    {
                        UpdatePos(1, true);
                    }
                    else if (action.Menu.Movement.ReadValue<Vector2>().x < -.1f)
                    {
                        UpdatePos(-1, true);
                    }
                }
            }


            if (!isCoR)
            {
                if (action.Menu.Accept.phase.Equals(InputActionPhase.Performed))
                {
                    buttons[pos].GetComponent<UIBSButtonActions>().action.Invoke();
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
        private void UpdatePos(int value, bool useGrid = false)
        {
            pos += value;

            if (useGrid)
            {
                if (pos <= -1)
                {
                    pos = (pos + 1) + maxPos;
                }
                else if (pos >= (maxPos + 1))
                {
                    pos = (pos - 1) - maxPos;
                }
            }
            else
            {
                if (pos.Equals(maxPos + 1))
                {
                    pos = 0;
                }
                else if (pos.Equals(-1))
                {
                    pos = maxPos;
                }
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



        private void ForceButtonDelay()
        {
            StartCoroutine(ButtonDelay());
        }
    }
}