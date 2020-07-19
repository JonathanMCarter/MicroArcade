using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CarterGames.UltimatePinball.BallCtrl
{
    public class CurveCtrl : MonoBehaviour
    {
        public PolygonCollider2D catchBox;
        public CurveTriggerPoint triggerPoint;

        private void Update()
        {
            if (!catchBox.enabled)
            {
                StartCoroutine(WaitAndEnable());
            }
        }

        private IEnumerator WaitAndEnable()
        {
            yield return new WaitForSeconds(1f);
            catchBox.enabled = true;
        }
    }
}