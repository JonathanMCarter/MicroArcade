using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  String Helper
 *	    A easier way to edit and alter strings.
 *			
 *	Purpose:
 *	    Mostly to save space, but designed to make editing and changing strings easier when they need ToString().
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
    /// Static Class | A class to make editing strings easier when the values need to use ToString() first.
    /// </summary>
    public static class StringHelper
    {
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Gets the lenght of the string and returns it.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="value">Int | value to check.</param>
        /// <returns>Int | Lenght of the string.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int Lenght(int value)
        {
            return value.ToString().Length;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Removes a single element at the position specified.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the value removed.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string RemoveAt(int pos, int valuetoEdit)
        {
            return valuetoEdit.ToString().Remove(pos, 1);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Removes a grouping of elements at the position specified.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="amount">Int | the amount to edit.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the section removed.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string RemoveAt(int pos, int amount, int valuetoEdit)
        {
            Debug.Log(valuetoEdit.ToString().Remove(pos, amount));
            return valuetoEdit.ToString().Remove(pos, amount);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Replaces a position in the string with a blank space.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the position edited.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string Replace(int pos, int valuetoEdit)
        {
            return valuetoEdit.ToString().Replace(As.Char(pos), ' ');
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Replaces a position in the string with whatever you enter.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="newValue">Int | The new value to insert.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the position edited.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string Replace(int pos, int newValue, int valuetoEdit)
        {
            return valuetoEdit.ToString().Replace(As.Char(pos), As.Char(newValue));
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Returns to value at the desired position.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the value requested.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string Get(int pos, int valuetoEdit)
        {
            return valuetoEdit.ToString().Substring(pos, 1);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Returns to values at the desired position.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="lenght">Int | The lenght of string you wasnt to get from the position.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the values requested.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string Get(int pos, int lenght, int valuetoEdit)
        {
            return valuetoEdit.ToString().Substring(pos, lenght);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Adds the value you wish to insert into the value you are editing.
        ///   Added In: Detective Notes.
        /// </summary>
        /// <param name="pos">Int | The positon to remove from.</param>
        /// <param name="toInsert">Int | The values to insert.</param>
        /// <param name="valuetoEdit">Int | value to change.</param>
        /// <returns>String | The string with the values added.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string Insert(int pos, int toInsert, int valueToEdit)
        {
            return valueToEdit.ToString().Insert(pos, toInsert.ToString());
        }
    }
}