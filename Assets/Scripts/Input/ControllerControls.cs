using UnityEngine;

namespace Arcade
{
    public enum ControllerButtons
    {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        Return,
        Confirm
    };

    public static class ControllerControls
    {
        public static bool ControllerLeft(Players Player)
        {
            if (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") < -.15f) { return true; }
            else { return false; }
        }

        public static bool ControllerRight(Players Player)
        {
            if (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") > .15f) { return true; }
            else { return false; }
        }

        public static bool ControllerUp(Players Player)
        {
            if (Input.GetAxisRaw(Player.ToString() + "-ControllerVer") < -.15f) { return true; }
            else { return false; }
        }

        public static bool ControllerDown(Players Player)
        {
            if (Input.GetAxisRaw(Player.ToString() + "-ControllerVer") > .15f) { return true; }
            else { return false; }
        }

        public static bool ControllerLeftUp(Players Player)
        {
            if ((Input.GetAxisRaw(Player.ToString() + "-ControllerVer") < -.15f) && (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") > .15f)) { return true; }
            else { return false; }
        }

        public static bool ControllerLeftDown(Players Player)
        {
            if ((Input.GetAxisRaw(Player.ToString() + "-ControllerVer") > .15f) && (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") > .15f)) { return true; }
            else { return false; }
        }

        public static bool ControllerRightUp(Players Player)
        {
            if ((Input.GetAxisRaw(Player.ToString() + "-ControllerVer") < -.15f) && (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") < -.15f)) { return true; }
            else { return false; }
        }

        public static bool ControllerRightDown(Players Player)
        {
            if ((Input.GetAxisRaw(Player.ToString() + "-ControllerVer") > .15f) && (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") < -.15f)) { return true; }
            else { return false; }
        }

        public static bool ControllerNone(Players Player)
        {
            if (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") < -.1f) { return false; }
            else if (Input.GetAxisRaw(Player.ToString() + "-ControllerHoz") > .1f) { return false; }
            else if (Input.GetAxisRaw(Player.ToString() + "-ControllerVer") < -.1f) { return false; }
            else if (Input.GetAxisRaw(Player.ToString() + "-ControllerVer") > .1f) { return false; }
            else { return true; }
        }

        public static bool ButtonPress(Players Player, ControllerButtons button)
        {
            if (Input.GetButtonDown(Player.ToString() + "-Controller" + button.ToString())) { return true; }
            else { return false; }
        }

        public static bool ButtonHeldDown(Players Player, ControllerButtons button)
        {
            if (Input.GetButton(Player.ToString() + "-Controller" + button.ToString())) { return true; }
            else { return false; }
        }

        public static bool ButtonReleased(Players Player, ControllerButtons button)
        {
            if (Input.GetButtonUp(Player.ToString() + "-Controller" + button.ToString())) { return true; }
            else { return false; }
        }
    }
}