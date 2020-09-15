using UnityEngine;
using CarterGames.Arcade.UserInput;

namespace CarterGames.UltimatePinball
{
    /// <summary>
    /// CLASS | Contorls the user input for the down input.
    /// </summary>
    public class PlayerCtrl : MonoBehaviour
    {
        // Which joystick player is this?
        [Header("Which player this gameobject is for?")]
        public bool isPlayer1;

        // The flippers this player controls
        [Header("The players flippers (left first, right second)")]
        public Flip_Ctrl[] PlayerFlippers;


        private void Update()
        {
            // if down is pressed, move both flippers at once.
            if (Controls.Down(isPlayer1))
            {
                FlipBoth();
            }
        }

        /// <summary>
        /// Flips both flippers at once.
        /// </summary>
        private void FlipBoth()
        {
            // Flip both flippers up
            PlayerFlippers[0].FlipLeftFlipper();
            PlayerFlippers[1].FlipRightFlipper();
        }
    }
}