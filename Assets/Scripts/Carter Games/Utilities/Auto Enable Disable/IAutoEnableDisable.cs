using System.Collections;

/****************************************************************************************************************************
 * 
 *  --{   Carter Games Utilities Script   }--
 *							  
 *  Auto Enable Disable Interface
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
    public enum AED_Options { Enable, Disable, None };

    public interface IAutoEnableDisable
    {
        void OnEnable();
        void OnDisable();
        IEnumerator DelayCo();
    }
}