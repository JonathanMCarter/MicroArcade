using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class RotateOBJ : MonoBehaviour
    {
        [Header("Choose an axis to rotate on")]
        [Tooltip("Rotate on the X Axis?")]
        public bool XAxis;      // Bool for the XAxis
        [Tooltip("Rotate on the Y Axis?")]
        public bool YAxis;      // Bool for the YAxis
        [Tooltip("Rotate on the Z Axis?")]
        public bool ZAxis;      // Bool for the ZAxis

        [Header("Rotation Speed")]
        [Tooltip("set the speed of the rotation, or backwards rotation just set a negative value")]
        public float Speed;     // Float for the speed of the rotation

        private bool RotateObject = true;       // boolean that enables or disables the rotation of an object (also sets it to true as default)

        // Update is called once per display frame
        void FixedUpdate()
        {
            if (RotateObject)
            {
                // Roates the object with whatever rotation selected at the desired speed (note there is not time.deltatime here so its small changes
                transform.Rotate(ConvertBool(XAxis) * Speed, ConvertBool(YAxis) * Speed, ConvertBool(ZAxis) * Speed);
            }
        }

        // Function to convert boolean to int for use in the ratation
        private int ConvertBool(bool input)
        {
            int convert;
            convert = input ? 1 : 0;
            return (convert);
        }

        // Enables the rotation
        public void EnableRotation()
        {
            if (!RotateObject)
            {
                RotateObject = true;
            }
        }

        // Disables the rotation
        public void DisableRotation()
        {
            if (RotateObject)
            {
                RotateObject = false;
            }
        }
    }
}