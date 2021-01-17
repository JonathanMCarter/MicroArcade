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
    public class AED_GameObject : MonoBehaviour, IAutoEnableDisable
    {
        public AED_Options options;
        public float delay;
        private WaitForSeconds wait;


        public void OnDisable()
        {
            StopAllCoroutines();
        }


        public void OnEnable()
        {
            wait = new WaitForSeconds(delay);
            StartCoroutine(DelayCo());
        }


        public IEnumerator DelayCo()
        {
            yield return wait;

            switch (options)
            {
                case AED_Options.Enable:
                    this.gameObject.SetActive(true);
                    break;
                case AED_Options.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case AED_Options.None:
                    break;
                default:
                    break;
            }
        }
    }
}