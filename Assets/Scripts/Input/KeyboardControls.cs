using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.UserInput
{
    public enum Players
    {
        P1,
        P2
    };

    public static class KeyboardControls
    {
        public static bool KeyboardLeft(Players player)
        {
            if (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") < -.15f) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        public static bool KeyboardRight(Players player)
        {
            if (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") > .15f) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }

        public static bool KeyboardUp(Players player)
        {
            if (Input.GetAxisRaw(player.ToString() + "-KeyboardVer") < -.15f) { return true; }
            else { return false; }
        }

        public static bool KeyboardDown(Players player)
        {
            if (Input.GetAxisRaw(player.ToString() + "-KeyboardVer") > .15f) { return true; }
            else { return false; }
        }

        public static bool KeyboardLeftUp(Players player)
        {
            if ((Input.GetAxisRaw(player.ToString() + "-KeyboardVer") < -.15f) && (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") > .15f)) { return true; }
            else { return false; }
        }

        public static bool KeyboardLeftDown(Players player)
        {
            if ((Input.GetAxisRaw(player.ToString() + "-KeyboardVer") > .15f) && (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") > .15f)) { return true; }
            else { return false; }
        }

        public static bool KeyboardRightUp(Players player)
        {
            if ((Input.GetAxisRaw(player.ToString() + "-KeyboardVer") < -.15f) && (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") < -.15f)) { return true; }
            else { return false; }
        }

        public static bool KeyboardRightDown(Players player)
        {
            if ((Input.GetAxisRaw(player.ToString() + "-KeyboardVer") > .15f) && (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") < -.15f)) { return true; }
            else { return false; }
        }

        public static bool KeyboardNone(Players player)
        {
            if (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") < -.1f) { return false; }
            else if (Input.GetAxisRaw(player.ToString() + "-KeyboardHoz") > .1f) { return false; }
            else if (Input.GetAxisRaw(player.ToString() + "-KeyboardVer") < -.1f) { return false; }
            else if (Input.GetAxisRaw(player.ToString() + "-KeyboardVer") > .1f) { return false; }
            else { return true; }
        }

        public static bool ButtonPress(Players player, Buttons button)
        {
            if (Input.GetButtonDown(player.ToString() + "-Keyboard" + button.ToString())) { return true; }
            else { return false; }
        }

        public static bool ButtonHeldDown(Players player, Buttons button)
        {
            if (Input.GetButton(player.ToString() + "-Keyboard" + button.ToString())) { return true; }
            else { return false; }
        }

        public static bool ButtonReleased(Players player, Buttons button)
        {
            if (Input.GetButtonUp(player.ToString() + "-Keyboard" + button.ToString())) { return true; }
            else { return false; }
        }
    }
}