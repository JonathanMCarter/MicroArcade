using System.Collections;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class CameraShakeScript : MonoBehaviour
    {
        [SerializeField] private float shakeAmount;
        [SerializeField] private bool shouldCameraShake;
        [SerializeField] private float shakeDuration;

        private bool isCameraPositionSaved;
        private bool isCoRunning;
        private Vector3 cameraPosition;
        private Camera mainCamera;


        private void Start()
        {
            mainCamera = Camera.main;
        }


        private void Update()
        {
            if (shouldCameraShake)
            {
                mainCamera.transform.localPosition = new Vector3(Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * shakeAmount, mainCamera.transform.position.z);
            }

            if ((shouldCameraShake) && (!isCoRunning))
            {
                StartCoroutine(StopCameraShake());
            }
        }


        /// <summary>
        /// Stops the camera shake effect after the set duration is completed
        /// </summary>
        private IEnumerator StopCameraShake()
        {
            isCoRunning = true;
            yield return new WaitForSeconds(shakeDuration);
            shouldCameraShake = false;
            isCoRunning = false;
            transform.localPosition = cameraPosition;
            isCameraPositionSaved = false;
        }


        /// <summary>
        /// Running the method will shake the camera with the defined values
        /// </summary>
        /// <param name="shakeForce">the amount of force to apply</param>
        /// <param name="shakeLenght">the duration for the effect</param>
        public void ShakeCamera(float shakeForce = .1f, float shakeLenght = .25f)
        {
            // Only edited if inputted, otherwise defaulf values are entered
            shakeAmount = shakeForce;
            shakeDuration = shakeLenght;

            if (!isCameraPositionSaved)
            {
                cameraPosition = transform.localPosition;
                isCameraPositionSaved = true;
            }

            if (!isCoRunning)
            {
                shouldCameraShake = true;
            }
        }
    }
}