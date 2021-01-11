/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  As
 *	    A easier to type conversion of values to another type that is done with parsing.
 *			
 *	Purpose:
 *	    To replace the <datatype>.Parse with something a little eaiser to type in a hurry.
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
    /// Static Class | As value, replaces the datatype.Parse method for something easier to type.
    /// </summary>
    public static class As
    {
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts a float to an int.
        /// </summary>
        /// <param name="value">Float | value to convert to an int.</param>
        /// <returns>Int | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int Int(float value)
        {
            return int.Parse(value.ToString());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts a string to an int.
        /// </summary>
        /// <param name="value">String | value to convert to an int.</param>
        /// <returns>Int | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static int Int(string value)
        {
            return int.Parse(value);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an int to a float.
        /// </summary>
        /// <param name="value">Int | value to convert to an float.</param>
        /// <returns>Float | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static float Float(int value)
        {
            return float.Parse(value.ToString());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts a string to a float.
        /// </summary>
        /// <param name="value">String | value to convert to an float.</param>
        /// <returns>Float | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static float Float(string value)
        {
            return float.Parse(value);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an int to a string.
        /// </summary>
        /// <param name="value">Int | value to convert to an string.</param>
        /// <returns>String | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string String(int value)
        {
            return value.ToString();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an float to a string.
        /// </summary>
        /// <param name="value">Float | value to convert to an string.</param>
        /// <returns>String | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string String(float value)
        {
            return value.ToString();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an int to a bool.
        /// </summary>
        /// <param name="value">Int | value to convert to an bool.</param>
        /// <returns>Bool | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool Bool(int value)
        {
            return bool.Parse(value.ToString());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an float to a bool.
        /// </summary>
        /// <param name="value">Float | value to convert to an bool.</param>
        /// <returns>Bool | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool Bool(float value)
        {
            return bool.Parse(value.ToString());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an string to a bool.
        /// </summary>
        /// <param name="value">String | value to convert to an bool.</param>
        /// <returns>Bool | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static bool Bool(string value)
        {
            return bool.Parse(value);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts an int to a char.
        /// </summary>
        /// <param name="value">Int | value to convert to an char.</param>
        /// <returns>Char | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static char Char(int value)
        {
            return char.Parse(value.ToString());
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Converts a string to a char.
        /// </summary>
        /// <param name="value">String | value to convert to an char.</param>
        /// <returns>Char | The converted value.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static char Char(string value)
        {
            return char.Parse(value);
        }
    }
}