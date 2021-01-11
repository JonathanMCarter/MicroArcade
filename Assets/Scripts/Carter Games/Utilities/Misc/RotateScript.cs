﻿using UnityEngine;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *	Rotate Script
 *      Rotates the object based on the values the user defines.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *			        
 *	Last Updated: 18/12/2020 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Utilities
{
    /// <summary>
    /// Class | Rotate Script, rotates objects based on the inputted values.
    /// </summary>
    public class RotateScript : MonoBehaviour
    {
        /// <summary>
        /// Defines whether or not the X axis should be rotated.
        /// </summary>
        [Header("Which axis should be rotated?")]
        [Tooltip("Should the X axis be rotated?")]
        [SerializeField] private bool xAxis = false;

        /// <summary>
        /// Defines whether or not the Y axis should be rotated.
        /// </summary>
        [Tooltip("Should the Y axis be rotated?")]
        [SerializeField] private bool yAxis = false;

        /// <summary>
        /// Defines whether or not the Z axis should be rotated.
        /// </summary>
        [Tooltip("Should the Z axis be rotated?")]
        [SerializeField] private bool zAxis = false;

        /// <summary>
        /// Defines the speed that the object is rotated at.
        /// </summary>
        [Header("Rotation Speed.")]
        [Tooltip("The speed of which the object will rotated at.")]
        [SerializeField] private float speed = 1;

        /// <summary>
        /// Boolean that enables or disables the rotation of an object (also sets it to true as default)
        /// </summary>
        private bool shouldRotateObject = true;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity | Fixed Update Method
        ///   Rotates the object in fixed update if the rotation boolean is true.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void FixedUpdate()
        {
            // Checks to see if the object should be rotated.
            if (shouldRotateObject)
            {
                // Roates the object with whatever rotation selected at the desired speed (note there is not time.deltatime here so its small changes
                transform.Rotate(ConvertBool(xAxis) * speed, ConvertBool(yAxis) * speed, ConvertBool(zAxis) * speed);
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Convert Boolean
        ///   Function to convert boolean to int for use in the rotation.
        /// </summary>
        /// <param name="input">the inputted boolean value</param>
        /// <returns>an int value for the inputted boolean</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private int ConvertBool(bool input)
        {
            int convert;
            convert = input ? 1 : 0;
            return (convert);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Enable Rotation
        ///   Enables the rotation of the object this is attached to.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void EnableRotation()
        {
            if (!shouldRotateObject)
            {
                shouldRotateObject = true;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Disable Rotation
        ///   Disables the rotation of the object this is attached to.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void DisableRotation()
        {
            if (shouldRotateObject)
            {
                shouldRotateObject = false;
            }
        }
    }
}