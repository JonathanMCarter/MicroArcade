using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;
using CarterGames.Arcade.Saving;
using CarterGames.Assets.AudioManager;

public enum DuckPlayers
{
    P1,
    P2,
    P3,
    P4,
    None,
};

public enum Powerups
{
    Explodie,
    Speedie,
    Lockie,
    None,
}

public enum DuckHats
{
    None,
    TopHat,
    Santa,
    Party,
    Duck,
    Sum,
    Viking,
}

namespace CarterGames.QuackingTime
{
    public class DuckScript : InputSettings
    {
        [Header(" ----- { Duck Script } -----")]
        public DuckPlayers Ducks;
        public Powerups Power;

        public float MoveSpd = 6;
        public float JumpHeight = 20;

        public float RotSpd;

        public bool IsCoRunning = false;
        public bool CanDuckJump;

        public bool CanPressAgain;

        public Color32 DuckColour;

        private GameManager GM;

        public GameObject PowerupLock;
        public GameObject PowerupSpd;

        public List<GameObject> Hats;
        public DuckHats SelectedHat;

        [SerializeField] private bool IsSpeedie = false;

        [SerializeField] internal bool LockHex = false;

        internal AudioManager am;
        private ReadySetGo RSG;
        [SerializeField] private bool JumpCoolRunning;

        [SerializeField] private MapController mapController;

        private void Start()
        {
            GM = FindObjectOfType<GameManager>();
            am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            RSG = FindObjectOfType<ReadySetGo>();
            Power = Powerups.None;
            UpdateHat();
        }

        protected override void Update()
        {
            // The base update
            base.Update();

            JumpSmoothing();

            if (!JumpCoolRunning)
            {
                if (Physics.Linecast(transform.position, transform.position + new Vector3(0, -1.5f, 0), 1 << LayerMask.NameToLayer("Quacking:Ground")))
                {
                    CanDuckJump = true;
                }
                else
                {
                    CanDuckJump = false;
                }
            }

            switch (Power)
            {
                case Powerups.Explodie:
                    MoveSpd = 8;
                    PowerupLock.SetActive(false);
                    PowerupSpd.SetActive(false);
                    LockHex = false;
                    break;
                case Powerups.Speedie:
                    MoveSpd = 12;
                    PowerupLock.SetActive(false);
                    PowerupSpd.SetActive(true);
                    LockHex = false;

                    if (!IsSpeedie)
                    {
                        StartCoroutine(SpdDuration());
                    }

                    mapController.SetControlledHexagonsToUnlocked(Ducks);

                    break;
                case Powerups.Lockie:
                    MoveSpd = 8;
                    LockHex = true;
                    PowerupLock.SetActive(true);
                    PowerupSpd.SetActive(false);
                    break;
                case Powerups.None:
                    MoveSpd = 8;
                    LockHex = false;
                    PowerupLock.SetActive(false);
                    PowerupSpd.SetActive(false);
                    break;
                default:
                    break;
            }

            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (ArcadeControls.JoystickUp(Joysticks.White)) { GoFoward(); }
                            else if (ArcadeControls.JoystickDown(Joysticks.White)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ArcadeControls.ButtonPress(Joysticks.White, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                        case DuckPlayers.P2:

                            if (ArcadeControls.JoystickUp(Joysticks.Black)) { GoFoward(); }
                            else if (ArcadeControls.JoystickDown(Joysticks.Black)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ArcadeControls.ButtonPress(Joysticks.Black, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (ControllerControls.ControllerUp(Players.P1)) { GoFoward(); }
                            else if (ControllerControls.ControllerDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                        case DuckPlayers.P2:

                            if (ControllerControls.ControllerUp(Players.P2)) { GoFoward(); }
                            else if (ControllerControls.ControllerDown(Players.P2)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ControllerControls.ButtonPress(Players.P2, ControllerButtons.A)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (KeyboardControls.KeyboardUp(Players.P1)) { GoFoward(); }
                            else if (KeyboardControls.KeyboardDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                        case DuckPlayers.P2:

                            if (KeyboardControls.KeyboardUp(Players.P2)) { GoFoward(); }
                            else if (KeyboardControls.KeyboardDown(Players.P2)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((KeyboardControls.ButtonPress(Players.P2, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (KeyboardControls.KeyboardUp(Players.P1)) { GoFoward(); }
                            else if (KeyboardControls.KeyboardDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                        case DuckPlayers.P2:

                            if (ControllerControls.ControllerUp(Players.P1)) { GoFoward(); }
                            else if (ControllerControls.ControllerDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    switch (Ducks)
                    {
                        case DuckPlayers.P2:

                            if (KeyboardControls.KeyboardUp(Players.P1)) { GoFoward(); }
                            else if (KeyboardControls.KeyboardDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B1)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                        case DuckPlayers.P1:

                            if (ControllerControls.ControllerUp(Players.P1)) { GoFoward(); }
                            else if (ControllerControls.ControllerDown(Players.P1)) { GoBackwards(); }
                            else { GoNowhere(); }

                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.A)) && (CanDuckJump))
                            {
                                am.PlayFromTime("Jump", .05f);
                                GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
                                StartCoroutine(JumpCooldown());
                            }

                            break;
                    }

                    break;
                default:
                    break;
            }

            RotatePlayer();
        }


        private IEnumerator JumpCooldown()
        {
            JumpCoolRunning = true;
            CanDuckJump = false; 
            //GetComponent<Rigidbody>().AddForce(transform.up * JumpHeight, ForceMode.Impulse);
            yield return new WaitForSeconds(.5f);
            CanDuckJump = true;
            JumpCoolRunning = false;
        }

        void GoFoward()
        {
            GetComponent<Rigidbody>().mass = 1;
            GetComponent<Rigidbody>().drag = 0.2f;
            GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.x * MoveSpd, GetComponent<Rigidbody>().velocity.y, transform.forward.z * MoveSpd);
        }

        void GoBackwards()
        {
            GetComponent<Rigidbody>().mass = 1;
            GetComponent<Rigidbody>().drag = 0.2f;
            GetComponent<Rigidbody>().velocity = new Vector3(-transform.forward.x * MoveSpd / 2, GetComponent<Rigidbody>().velocity.y, -transform.forward.z * MoveSpd / 2);
        }

        void GoNowhere()
        {
            GetComponent<Rigidbody>().mass = 1;
            GetComponent<Rigidbody>().drag = .25f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Contains("Gimme"))
            {
                am.Play("Collect", .5f);
                GM.SetDuckScoreToFinal(Ducks, other.GetComponent<ScoringBoxScript>().scoringBoxMultiplier);
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Contains("Lockie"))
            {
                StopAllCoroutines();
                am.Play("Metal", 1.5f);
                Power = Powerups.Lockie;
                LockDuckSquares(other.gameObject);
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Contains("Expl"))
            {
                Power = Powerups.Explodie;
            }

            if (other.gameObject.tag.Contains("Speed"))
            {
                StopAllCoroutines();
                am.Play("Speed", .75f);
                Power = Powerups.Speedie;
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.tag.Contains("Wat"))
            {
                StopAllCoroutines();
                am.Play("Splat", .75f);
                Power = Powerups.None;
                GM.RespawnDuck(Ducks);
            }

        }

        private void JumpSmoothing()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * Physics.gravity.y / 15, ForceMode.Impulse);
        }

        private void RotatePlayer()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (ArcadeControls.JoystickLeft(Joysticks.White)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (ArcadeControls.JoystickRight(Joysticks.White)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                        case DuckPlayers.P2:

                            if (ArcadeControls.JoystickLeft(Joysticks.Black)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (ArcadeControls.JoystickRight(Joysticks.Black)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (ControllerControls.ControllerLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (ControllerControls.ControllerRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                        case DuckPlayers.P2:

                            if (ControllerControls.ControllerLeft(Players.P2)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (ControllerControls.ControllerRight(Players.P2)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (KeyboardControls.KeyboardLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (KeyboardControls.KeyboardRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                        case DuckPlayers.P2:

                            if (KeyboardControls.KeyboardLeft(Players.P2)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (KeyboardControls.KeyboardRight(Players.P2)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    switch (Ducks)
                    {
                        case DuckPlayers.P1:

                            if (KeyboardControls.KeyboardLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (KeyboardControls.KeyboardRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                        case DuckPlayers.P2:

                            if (ControllerControls.ControllerLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (ControllerControls.ControllerRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    switch (Ducks)
                    {
                        case DuckPlayers.P2:

                            if (KeyboardControls.KeyboardLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * RotSpd, 0); }
                            else if (KeyboardControls.KeyboardRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * RotSpd, 0); }

                            break;
                        case DuckPlayers.P1:

                            if (ControllerControls.ControllerLeft(Players.P1)) { transform.localEulerAngles += new Vector3(0, -1 * 3, 0); }
                            else if (ControllerControls.ControllerRight(Players.P1)) { transform.localEulerAngles += new Vector3(0, 1 * 3, 0); }

                            break;
                    }

                    break;
                default:
                    break;
            }
        }

        private IEnumerator SpdDuration()
        {
            IsSpeedie = true;

            yield return new WaitForSeconds(10);

            if (Power == Powerups.Speedie)
            {
                Power = Powerups.None;
            }

            IsSpeedie = false;
        }

        private void UpdateHat()
        {
            QuackingTimeData _data = SaveManager.LoadQuackingTime();

            switch (Ducks)
            {
                case DuckPlayers.P1:
                    SelectedHat = (DuckHats)_data.player1HatSelection;
                    break;
                case DuckPlayers.P2:
                    SelectedHat = (DuckHats)_data.player2HatSelection;
                    break;
                default:
                    break;
            }

            switch (SelectedHat)
            {
                case DuckHats.TopHat:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(true);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.Santa:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(true);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.Party:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(true);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.None:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.Duck:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(true);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.Sum:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(true);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(false);

                    break;
                case DuckHats.Viking:

                    transform.Find("HatSlot").transform.Find("TopHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SantaHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("PartyHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("DuckHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("SumHat").gameObject.SetActive(false);
                    transform.Find("HatSlot").transform.Find("VikingHat").gameObject.SetActive(true);

                    break;
                default:
                    break;
            }
        }



        private void LockDuckSquares(GameObject go)
        {
            StartCoroutine(LockHexForDuration(go));
        }


        private IEnumerator LockHexForDuration(GameObject go)
        {
            mapController.SetControlledHexagonsToLocked(Ducks);

            yield return new WaitForSeconds(10);

            mapController.SetControlledHexagonsToUnlocked(Ducks);

            Power = Powerups.None;
        }
    }
}