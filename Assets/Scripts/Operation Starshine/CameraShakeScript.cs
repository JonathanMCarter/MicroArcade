using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starshine
{
    public class CameraShakeScript : MonoBehaviour
    {
        public enum Dimentions
        {
            Two,
            Three,
        };

        public Dimentions Dimention;

        [Header("Shake The Camera?")]
        [Tooltip("Should the camera be shaking?")]
        public bool CameraShake;

        [Header("Amount")]
        [Tooltip("Amount the Camera is shaken by")]
        public float ShakeAmount;

        [Header("Duration")]
        [Tooltip("How long should the camera shake for?")]
        public float ShakeDuration;

        private bool CameraPosSaved;
        private bool IsCoRunning;
        private Vector3 CameraPos;


        private void Update()
        {
            if (CameraShake)
            {
                switch (Dimention)
                {
                    case Dimentions.Two:
                        Camera.main.transform.localPosition = new Vector3(Random.insideUnitSphere.x * ShakeAmount, Random.insideUnitSphere.y * ShakeAmount, Camera.main.transform.position.z);
                        break;
                    case Dimentions.Three:
                        Camera.main.transform.localPosition = new Vector3(Random.insideUnitSphere.x * ShakeAmount, Random.insideUnitSphere.y * ShakeAmount, Random.insideUnitSphere.z * ShakeAmount);
                        break;
                    default:
                        break;
                }
            }

            if ((CameraShake) && (!IsCoRunning))
            {
                StartCoroutine(StopCameraShake());
            }
        }


        private IEnumerator StopCameraShake()
        {
            IsCoRunning = true;
            yield return new WaitForSeconds(ShakeDuration);
            CameraShake = false;
            IsCoRunning = false;
            transform.localPosition = CameraPos;
            CameraPosSaved = false;
        }


        public void ShakeCamera(float ShakeForce = .1f, float ShakeLenght = .25f)
        {
            // Only edited if inputted, otherwise defaulf values are entered
            ShakeAmount = ShakeForce;
            ShakeDuration = ShakeLenght;

            if (!CameraPosSaved)
            {
                CameraPos = transform.localPosition;
                CameraPosSaved = true;
            }

            if (!IsCoRunning)
            {
                CameraShake = true;
            }
        }
    }
}
