using UnityEngine;

namespace CarterGames.QuackingTime
{
    public class RotateScript : MonoBehaviour
    {
        public Camera Cam;
        public bool Rotate;
        public float Spd;

        void Update()
        {
            if (Rotate)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * Spd);
                Cam.enabled = true;
            }
            else
            {
                transform.Rotate(Vector3.zero);
                Cam.enabled = false;
            }
        }
    }
}