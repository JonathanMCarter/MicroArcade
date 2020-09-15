using UnityEngine;
using CarterGames.Arcade.UserInput;
using CarterGames.UltimatePinball.BallCtrl;
using CarterGames.Assets.AudioManager;
using CarterGames.Utilities;

namespace CarterGames.UltimatePinball
{
    /// <summary>
    /// CLASS | The script that controls the flippers for both players and their physics.
    /// </summary>
    public class Flip_Ctrl : MonoBehaviour
    {
        public enum FlipperSides
        {
            Left,
            Right,
        };

        // Which joystick this script is using (White / Black)
        [Header("Which Joystick is controlling this object")]
        [SerializeField] private bool isPlayer1 = true;

        // How fast the flippers should move
        [Header("How much force should the flipper use?")]
        [SerializeField] private float spd;

        // Which flipper is this flipper (Left / Right)
        [Header("Is this flipper the left or right flipper?")]
        [SerializeField] private FlipperSides thisFlipper;

        // Private variables as these just reference the components on the object the script is attached to
        private HingeJoint2D hinge2D;
        private JointMotor2D motor2D;
        private AudioManager audioManager;
        private bool playSound;



        private void Start()
        {
            hinge2D = GetComponent<HingeJoint2D>();
            motor2D = hinge2D.motor;
            audioManager = FindObjectOfType<AudioManager>();
        }


        private void FixedUpdate()
        {
            // Switches the flippers between left and right
            switch (thisFlipper)
            {
                case FlipperSides.Left:

                    if (Controls.Left(isPlayer1))
                    {
                        FlipLeftFlipper();
                    }
                    else if (Controls.None(isPlayer1))
                    {
                        ResetLeftFlipper();
                    }

                    break;
                case FlipperSides.Right:

                    if (Controls.Right(isPlayer1))
                    {
                        FlipLeftFlipper();
                    }
                    else if (Controls.None(isPlayer1))
                    {
                        ResetLeftFlipper();
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
            if (playSound)
            {
                audioManager.Play("BumperHit", .2f, 1.5f);
                playSound = false;
            }

            motor2D.motorSpeed = -spd;
            hinge2D.motor = motor2D;
        }

        /// <summary>
        /// Resets the Left Flipper to its resting position
        /// </summary>
        public void ResetLeftFlipper()
        {
            playSound = true;
            motor2D.motorSpeed = spd;
            hinge2D.motor = motor2D;
        }

        /// <summary>
        /// Flips the Right Flipper to the upward position
        /// </summary>
        public void FlipRightFlipper()
        {
            if (playSound)
            {
                audioManager.Play("BumperHit", .2f, 1.5f);
                playSound = false;
            }

            motor2D.motorSpeed = spd;
            hinge2D.motor = motor2D;
        }

        /// <summary>
        /// Resets the Right Flipper to its resting position
        /// </summary>
        public void ResetRightFlipper()
        {
            playSound = true;
            motor2D.motorSpeed = -spd;
            hinge2D.motor = motor2D;
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallMoveScript>())
            {
                collision.gameObject.GetComponent<BallMoveScript>().LastHit = (Joysticks)Converters.BoolToInt(isPlayer1);
            }
        }
    }
}
