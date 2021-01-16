using UnityEngine.InputSystem;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  New Input System Helper
 *	    Makes the new input system a little easier.
 *			
 *	Purpose:
 *	    To save writing the same code over and over mostly.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *			        
 *	Last Updated: 16/01/2021 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Utilities
{
    /// <summary>
    /// Static Class | New Input System Helper, makes the new input system somewhat managable.
    /// </summary>
    public static class NewInputSystemHelper
    {
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Static Method | Checks to see if the Input Action has been performed.
        /// </summary>
        /// <param name="act">InputAction | The action to check.</param>
        /// <returns>True or False</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool ButtonPressed(InputAction act)
        {
            return act.phase.Equals(InputActionPhase.Performed);
        }
    }
}