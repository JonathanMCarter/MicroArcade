using UnityEngine;
using System.Collections;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Auto Enable Disable Canvases
 *	    Allows the creation of Auto Enable Disable for any object type needed.
 *			
 *	Purpose:
 *	    To help auto enable or disabel elements if needed.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *			        
 *	Last Updated: 16/01/2021 (d/m/y)						
 * 
****************************************************************************************************************************/

namespace CarterGames.Utilities
{
    public class AED_Canvas : MonoBehaviour, IAutoEnableDisable
    {
        private Canvas canvas;
        private WaitForSeconds wait;
        public AED_Options options;
        public float delay;


        public void OnDisable()
        {
            StopAllCoroutines();
        }


        private void Start()
        {
            canvas = GetComponent<Canvas>();
        }


        public void OnEnable()
        {
            StartCoroutine(DelayCo());
            wait = new WaitForSeconds(delay);
        }


        public IEnumerator DelayCo()
        {
            yield return wait;

            switch (options)
            {
                case AED_Options.Enable:
                    canvas.enabled = true;
                    break;
                case AED_Options.Disable:
                    canvas.enabled = false;
                    break;
                case AED_Options.None:
                    break;
                default:
                    break;
            }
        }
    }
}