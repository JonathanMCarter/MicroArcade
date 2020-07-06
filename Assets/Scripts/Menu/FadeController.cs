/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Menu
{
    public class FadeController : MonoBehaviour
    {
        void Start()
        {
            if (!GetComponent<Image>().enabled)
            {
                GetComponent<Image>().enabled = true;
            }
        }
    }
}