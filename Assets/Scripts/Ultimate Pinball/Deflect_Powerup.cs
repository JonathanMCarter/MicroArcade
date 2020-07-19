using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;

namespace CarterGames.UltimatePinball
{
    public class Deflect_Powerup : InputSettings
    {
        public Joysticks Player;

        public bool CanDeflect;
        public float DeflectDelay;

        public float RechargeDelay;
        public bool DidHitSomething;

        ParticleSystem PS;

        private void Start()
        {
            PS = GetComponentInChildren<ParticleSystem>();
            CanDeflect = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }

        protected override void Update()
        {
            base.Update();

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.ButtonPress(Player, Buttons.B1)) { DeflectPowerup(); }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.A)) { DeflectPowerup(); }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B1)) { DeflectPowerup(); }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (ConvertToPlayers() == Players.P1)
                    {
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { DeflectPowerup(); }
                    }
                    else
                    {
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { DeflectPowerup(); }
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ConvertToPlayers() == Players.P2)
                    {
                        if (KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) { DeflectPowerup(); }
                    }
                    else
                    {
                        if (ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) { DeflectPowerup(); }
                    }

                    break;
                default:
                    break;
            }
        }


        void DeflectPowerup()
        {
            PS.Play();

            if (CanDeflect)
            {
                GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine(DeflectCooldown());
            }

            if (DidHitSomething)
            {
                StartCoroutine(DeflectRecharge(1.5f));
            }
            else
            {
                StartCoroutine(DeflectRecharge(5f));
            }
        }


        IEnumerator DeflectCooldown()
        {
            CanDeflect = false;
            yield return new WaitForSeconds(DeflectDelay);
            GetComponent<CircleCollider2D>().enabled = false;
            PS.Stop();
            CanDeflect = true;
        }


        IEnumerator DeflectRecharge(float Delay)
        {
            yield return new WaitForSeconds(Delay);
        }

        Players ConvertToPlayers()
        {
            switch (Player)
            {
                case Joysticks.White:
                    return Players.P1;
                case Joysticks.Black:
                    return Players.P2;
                default:
                    return Players.P1;
            }
        }
    }
}