using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class AutoDisableObject : MonoBehaviour
    {

        // How long until the attached object should be active for once enabled....
        public float ObjPoolResetDelay = 3;


        void OnEnable()
        {
            // Start the IEnumerator to disable the attached object
            StartCoroutine(DisableObjectAfterDaley());
        }


        private void FixedUpdate()
        {
            if ((transform.position.y > 13f) || (transform.position.x > 24f))
            {
                StopAllCoroutines();
                transform.rotation = Quaternion.identity;
                gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// Disables the attached object after the set time from the variable on this script
        /// </summary>
        IEnumerator DisableObjectAfterDaley()
        {
            yield return new WaitForSeconds(ObjPoolResetDelay);
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}