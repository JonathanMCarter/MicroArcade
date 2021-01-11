using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Keep
 *	    A few methods to help 'keep' values within ranges or set to certain values.
 *			
 *	Purpose:
 *	    Mainly to help save writing the same if statement set loads of times.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *			        
 *	Last Updated: 09/01/2021 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Utilities
{
    /// <summary>
    /// Static Class | Keep a value or values to a set value or within a range.
    /// </summary>
    public static class Keep
    {
        ///------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Keeps the int within the defined bounds.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="valueToEdit">the value to edit</param>
        /// <param name="min">the min bound</param>
        /// <param name="max">the max bound</param>
        /// <returns>the int kept within its bounds.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int WithinBounds(int valueToEdit, int min, int max)
        {
            if (valueToEdit >= min && valueToEdit <= max)
                return valueToEdit;
            else
            {
                if (valueToEdit < min)
                    return min;
                else if (valueToEdit > max)
                    return max;
                else
                    return 0;
            }
        }
    }
}