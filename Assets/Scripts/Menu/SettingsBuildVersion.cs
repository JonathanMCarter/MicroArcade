/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;
using UnityEngine.UI;

namespace Arcade.Menu
{
    public class SettingsBuildVersion : MonoBehaviour
    {
        private Text buildVersionText;
        public enum options { Version, DateBuild };
        public options option;

        private void Start()
        {
            if (option == options.Version)
            {
                buildVersionText = GetComponent<Text>();
                buildVersionText.text = "Build Version: " + Application.version;
            }
            else if (option == options.DateBuild)
            {

            }
        }
    }
}