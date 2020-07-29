using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;
using CarterGames.UltimatePinball.BallCtrl;
using CarterGames.Assets.AudioManager;

namespace CarterGames.UltimatePinball
{
    public class Flip_Ctrl : InputSettings
    {
        // Which joystick this script is using (White / Black)
        [Header("Which Joystick is controlling this object")]
        public Joysticks Input;

        // How fast the flippers should move
        [Header("How much force should the flipper use?")]
        public float Spd;

        public enum FlipperSides
        {
            Left,
            Right,
        };

        // Which flipper is this flipper (Left / Right)
        [Header("Is this flipper the left or right flipper?")]
        public FlipperSides ThisFlipper;

        // Private variables as these just reference the components on the object the script is attached to
        HingeJoint2D HG;
        JointMotor2D MT;

        // am
        AudioManager am;
        bool PlaySound;

        private void Start()
        {
            HG = GetComponent<HingeJoint2D>();
            MT = HG.motor;
            am = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManager>();
        }

        private void FixedUpdate()
        {

            // Switches the flippers between left and right
            switch (ThisFlipper)
            {
                case FlipperSides.Left:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.JoystickLeft(Input)) { FlipLeftFlipper(); }
                            else if (ArcadeControls.JoystickNone(Input)) { ResetLeftFlipper(); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ControllerLeft(ConvertToPlayers())) { FlipLeftFlipper(); }
                            else if (ControllerControls.ControllerNone(ConvertToPlayers())) { ResetLeftFlipper(); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.KeyboardLeft(ConvertToPlayers())) { FlipLeftFlipper(); }
                            else if (KeyboardControls.KeyboardNone(ConvertToPlayers())) { ResetLeftFlipper(); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (ConvertToPlayers() == Players.P1)
                            {
                                if (KeyboardControls.KeyboardLeft(Players.P1)) { FlipLeftFlipper(); }
                                else if (KeyboardControls.KeyboardNone(Players.P1)) { ResetLeftFlipper(); }
                            }
                            else
                            {
                                if (ControllerControls.ControllerLeft(Players.P1)) { FlipLeftFlipper(); }
                                else if (ControllerControls.ControllerNone(Players.P1)) { ResetLeftFlipper(); }
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ConvertToPlayers() == Players.P2)
                            {
                                if (KeyboardControls.KeyboardLeft(Players.P1)) { FlipLeftFlipper(); }
                                else if (KeyboardControls.KeyboardNone(Players.P1)) { ResetLeftFlipper(); }
                            }
                            else
                            {
                                if (ControllerControls.ControllerLeft(Players.P1)) { FlipLeftFlipper(); }
                                else if (ControllerControls.ControllerNone(Players.P1)) { ResetLeftFlipper(); }
                            }

                            break;
                        default:
                            break;
                    }


                    break;
                case FlipperSides.Right:

                    switch (ControllerType)
                    {
                        case SupportedControllers.ArcadeBoard:

                            if (ArcadeControls.JoystickRight(Input)) { FlipRightFlipper(); }
                            else if (ArcadeControls.JoystickNone(Input)) { ResetRightFlipper(); }

                            break;
                        case SupportedControllers.GamePadBoth:

                            if (ControllerControls.ControllerRight(ConvertToPlayers())) { FlipRightFlipper(); }
                            else if (ControllerControls.ControllerNone(ConvertToPlayers())) { ResetRightFlipper(); }

                            break;
                        case SupportedControllers.KeyboardBoth:

                            if (KeyboardControls.KeyboardRight(ConvertToPlayers())) { FlipRightFlipper(); }
                            else if (KeyboardControls.KeyboardNone(ConvertToPlayers())) { ResetRightFlipper(); }

                            break;
                        case SupportedControllers.KeyboardP1ControllerP2:

                            if (ConvertToPlayers() == Players.P1)
                            {
                                if (KeyboardControls.KeyboardRight(Players.P1)) { FlipRightFlipper(); }
                                else if (KeyboardControls.KeyboardNone(Players.P1)) { ResetRightFlipper(); }
                            }
                            else
                            {
                                if (ControllerControls.ControllerRight(Players.P1)) { FlipRightFlipper(); }
                                else if (ControllerControls.ControllerNone(Players.P1)) { ResetRightFlipper(); }
                            }

                            break;
                        case SupportedControllers.KeyboardP2ControllerP1:

                            if (ConvertToPlayers() == Players.P2)
                            {
                                if (KeyboardControls.KeyboardRight(Players.P1)) { FlipRightFlipper(); }
                                else if (KeyboardControls.KeyboardNone(Players.P1)) { ResetRightFlipper(); }
                            }
                            else
                            {
                                if (ControllerControls.ControllerRight(Players.P1)) { FlipRightFlipper(); }
                                else if (ControllerControls.ControllerNone(Players.P1)) { ResetRightFlipper(); }
                            }

                            break;
                        default:
                            break;
                    }


                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Flips the Left Flipper to the upward position
        /// </summary>
        public void FlipLeftFlipper()
        {
            if (PlaySound)
            {
                am.Play("BumperHit", .2f, 1.5f);
                PlaySound = false;
            }

            MT.motorSpeed = -Spd;
            HG.motor = MT;
        }

        /// <summary>
        /// Resets the Left Flipper to its resting position
        /// </summary>
        public void ResetLeftFlipper()
        {
            PlaySound = true;
            MT.motorSpeed = Spd;
            HG.motor = MT;
        }

        /// <summary>
        /// Flips the Right Flipper to the upward position
        /// </summary>
        public void FlipRightFlipper()
        {
            if (PlaySound)
            {
                am.Play("BumperHit", .2f, 1.5f);
                PlaySound = false;
            }

            MT.motorSpeed = Spd;
            HG.motor = MT;
        }

        /// <summary>
        /// Resets the Right Flipper to its resting position
        /// </summary>
        public void ResetRightFlipper()
        {
            PlaySound = true;
            MT.motorSpeed = -Spd;
            HG.motor = MT;
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallMoveScript>())
            {
                collision.gameObject.GetComponent<BallMoveScript>().LastHit = Input;
            }
        }


        Players ConvertToPlayers()
        {
            switch (Input)
            {
                case Joysticks.White:
                    return Players.P1;
                case Joysticks.Black:
                    return Players.P2;
                default:
                    return Players.P1;
            }
        }
    }
}
