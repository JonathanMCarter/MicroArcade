using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arcade;

namespace Starshine
{
    [RequireComponent(typeof(Combat))]
    public class PlayerController : ShipManagement
    {
        [Header("{ Player controller }")]
        public Joysticks Player;

        public float MoveSpd;
        public bool IsEnabled;

        Rigidbody2D RB;

        Camera Cam;
        Vector2 CamBounds;
        float OBJWidth, OBJHeight;

        public AudioManager audioManager;

        protected override void Start()
        {
            RB = GetComponent<Rigidbody2D>();

            PlayerNumber = ((int)(Player + 1));

            base.Start();

            Cam = Camera.main;
            CamBounds = Cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Cam.transform.position.z));
            OBJWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
            OBJHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

            MoveSpd = Ship.ShipSpd;

            if (!audioManager) { audioManager = FindObjectOfType<AudioManager>(); }
        }



        protected override void Update()
        {
            if (IsEnabled)
            {
                switch (ControllerType)
                {
                    case SupportedControllers.ArcadeBoard:

                        // Shoot Main Weapon on B1 Press
                        if ((ArcadeControls.ButtonHeldDown(Player, Buttons.B1)) && (CanShootMain))
                        {
                            audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                            CanShootMain = false;
                            StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Shoot Alt Weapon on B2 Press
                        if ((ArcadeControls.ButtonPress(Player, Buttons.B2)) && (CanShootAlt))
                        {
                            audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                            CanShootAlt = false;
                            StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Use Special Move on B3 Press
                        if ((ArcadeControls.ButtonPress(Player, Buttons.B3)) && (CanUseSpecialMove))
                        {
                            ExecuteSpecialMove();
                        }

                        // Shield - Protect from bolts
                        if ((ArcadeControls.ButtonPress(Player, Buttons.B4)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Bolts;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Protect from misisles
                        if ((ArcadeControls.ButtonPress(Player, Buttons.B5)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Missiles;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Regen Shields
                        if ((ArcadeControls.ButtonPress(Player, Buttons.B6)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Regen;
                            PlayerStats.shieldSwitches++;
                        }

                        break;
                    case SupportedControllers.GamePadBoth:

                        // Shoot Main Weapon on B1 Press
                        if ((ControllerControls.ButtonHeldDown(ConvertToPlayers(), ControllerButtons.A)) && (CanShootMain))
                        {
                            audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                            CanShootMain = false;
                            StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Shoot Alt Weapon on B2 Press
                        if ((ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.B)) && (CanShootAlt))
                        {
                            audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                            CanShootAlt = false;
                            StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Use Special Move on B3 Press
                        if ((ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.X)) && (CanUseSpecialMove))
                        {
                            ExecuteSpecialMove();
                        }

                        // Shield - Protect from bolts
                        if ((ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.LB)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Bolts;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Protect from misisles
                        if ((ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.RB)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Missiles;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Regen Shields
                        if ((ControllerControls.ButtonPress(ConvertToPlayers(), ControllerButtons.Y)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Regen;
                            PlayerStats.shieldSwitches++;
                        }

                        break;
                    case SupportedControllers.KeyboardBoth:

                        // Shoot Main Weapon on B1 Press
                        if ((KeyboardControls.ButtonHeldDown(ConvertToPlayers(), Buttons.B1)) && (CanShootMain))
                        {
                            audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                            CanShootMain = false;
                            StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Shoot Alt Weapon on B2 Press
                        if ((KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B2)) && (CanShootAlt))
                        {
                            audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                            CanShootAlt = false;
                            StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                        }

                        // Use Special Move on B3 Press
                        if ((KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B3)) && (CanUseSpecialMove))
                        {
                            ExecuteSpecialMove();
                        }

                        // Shield - Protect from bolts
                        if ((KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B4)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Bolts;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Protect from misisles
                        if ((KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B5)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Missiles;
                            PlayerStats.shieldSwitches++;
                        }

                        // Shield - Regen Shields
                        if ((KeyboardControls.ButtonPress(ConvertToPlayers(), Buttons.B6)) && (CanChangeSheildType))
                        {
                            ActiveShieldType = ShieldTypes.Regen;
                            PlayerStats.shieldSwitches++;
                        }

                        break;
                    case SupportedControllers.KeyboardP1ControllerP2:

                        if (ConvertToPlayers() == Players.P1)
                        {
                            // Shoot Main Weapon on B1 Press
                            if ((KeyboardControls.ButtonHeldDown(Players.P1, Buttons.B1)) && (CanShootMain))
                            {
                                audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                                CanShootMain = false;
                                StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Shoot Alt Weapon on B2 Press
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B2)) && (CanShootAlt))
                            {
                                audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                                CanShootAlt = false;
                                StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Use Special Move on B3 Press
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B3)) && (CanUseSpecialMove))
                            {
                                ExecuteSpecialMove();
                            }

                            // Shield - Protect from bolts
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B4)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Bolts;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Protect from misisles
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B5)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Missiles;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Regen Shields
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B6)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Regen;
                                PlayerStats.shieldSwitches++;
                            }
                        }
                        else
                        {
                            // Shoot Main Weapon on B1 Press
                            if ((ControllerControls.ButtonHeldDown(Players.P1, ControllerButtons.A)) && (CanShootMain))
                            {
                                audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                                CanShootMain = false;
                                StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Shoot Alt Weapon on B2 Press
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) && (CanShootAlt))
                            {
                                audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                                CanShootAlt = false;
                                StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Use Special Move on B3 Press
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.X)) && (CanUseSpecialMove))
                            {
                                ExecuteSpecialMove();
                            }

                            // Shield - Protect from bolts
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.LB)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Bolts;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Protect from misisles
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.RB)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Missiles;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Regen Shields
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.Y)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Regen;
                                PlayerStats.shieldSwitches++;
                            }
                        }

                        break;
                    case SupportedControllers.KeyboardP2ControllerP1:

                        if (ConvertToPlayers() == Players.P2)
                        {
                            // Shoot Main Weapon on B1 Press
                            if ((KeyboardControls.ButtonHeldDown(Players.P1, Buttons.B1)) && (CanShootMain))
                            {
                                audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                                CanShootMain = false;
                                StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Shoot Alt Weapon on B2 Press
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B2)) && (CanShootAlt))
                            {
                                audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                                CanShootAlt = false;
                                StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Use Special Move on B3 Press
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B3)) && (CanUseSpecialMove))
                            {
                                ExecuteSpecialMove();
                            }

                            // Shield - Protect from bolts
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B4)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Bolts;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Protect from misisles
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B5)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Missiles;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Regen Shields
                            if ((KeyboardControls.ButtonPress(Players.P1, Buttons.B6)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Regen;
                                PlayerStats.shieldSwitches++;
                            }
                        }
                        else
                        {
                            // Shoot Main Weapon on B1 Press
                            if ((ControllerControls.ButtonHeldDown(Players.P1, ControllerButtons.A)) && (CanShootMain))
                            {
                                audioManager.Play("Shoot", Random.Range(.04f, .1f), Random.Range(.9f, 1f));
                                CanShootMain = false;
                                StartCoroutine(base.ShootMainWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Shoot Alt Weapon on B2 Press
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.B)) && (CanShootAlt))
                            {
                                audioManager.PlayFromTime("MissileShoot", .5f, .2f);
                                CanShootAlt = false;
                                StartCoroutine(base.ShootAltWeapon(Vector2.up, 10, (int)Player + 1));
                            }

                            // Use Special Move on B3 Press
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.X)) && (CanUseSpecialMove))
                            {
                                ExecuteSpecialMove();
                            }

                            // Shield - Protect from bolts
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.LB)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Bolts;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Protect from misisles
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.RB)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Missiles;
                                PlayerStats.shieldSwitches++;
                            }

                            // Shield - Regen Shields
                            if ((ControllerControls.ButtonPress(Players.P1, ControllerButtons.Y)) && (CanChangeSheildType))
                            {
                                ActiveShieldType = ShieldTypes.Regen;
                                PlayerStats.shieldSwitches++;
                            }
                        }

                        break;
                    default:
                        break;
                }
            }

            // Runs the base update function on ship movement
            base.Update();


            // Clamp the players to the scene view (so they can't dodge off screen)

        }

        private void FixedUpdate()
        {
            if (IsEnabled)
            {
                // Player Movement
                PlayerMovement();
            }
        }


        /// <summary>
        /// Allow the player to move around in the scene
        /// </summary>
        void PlayerMovement()
        {
            switch (ControllerType)
            {
                case SupportedControllers.ArcadeBoard:

                    if (ArcadeControls.JoystickLeft(Player))
                    {
                        RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.left);
                    }

                    if (ArcadeControls.JoystickRight(Player))
                    {
                        RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.right);
                    }

                    if (ArcadeControls.JoystickUp(Player))
                    {
                        RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                        //UpdateGunshipDirection(Vector2.up);
                    }

                    if (ArcadeControls.JoystickDown(Player))
                    {
                        RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                        //UpdateGunshipDirection(Vector2.down);
                    }

                    if (ArcadeControls.JoystickNorthEast(Player))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                    }

                    if (ArcadeControls.JoystickNorthWest(Player))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                    }

                    if (ArcadeControls.JoystickSouthEast(Player))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                    }

                    if (ArcadeControls.JoystickSouthWest(Player))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                    }

                    if (ArcadeControls.JoystickNone(Player))
                    {
                        RB.velocity = Vector2.zero;
                    }

                    break;
                case SupportedControllers.GamePadBoth:

                    if (ControllerControls.ControllerLeft(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.left);
                    }

                    if (ControllerControls.ControllerRight(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.right);
                    }

                    if (ControllerControls.ControllerUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                        //UpdateGunshipDirection(Vector2.up);
                    }

                    if (ControllerControls.ControllerDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                        //UpdateGunshipDirection(Vector2.down);
                    }

                    if (ControllerControls.ControllerLeftUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                    }

                    if (ControllerControls.ControllerRightUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                    }

                    if (ControllerControls.ControllerLeftDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                    }

                    if (ControllerControls.ControllerRightDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                    }

                    if (ControllerControls.ControllerNone(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.zero;
                    }

                    break;
                case SupportedControllers.KeyboardBoth:

                    if (KeyboardControls.KeyboardLeft(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.left);
                    }
                    if (KeyboardControls.KeyboardRight(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.right);
                    }
                    if (KeyboardControls.KeyboardUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                       // UpdateGunshipDirection(Vector2.up);
                    }
                    if (KeyboardControls.KeyboardDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                        //UpdateGunshipDirection(Vector2.down);
                    }
                    if (KeyboardControls.KeyboardLeftUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                    }
                    if (KeyboardControls.KeyboardRightUp(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                    }
                    if (KeyboardControls.KeyboardLeftDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                    }
                    if (KeyboardControls.KeyboardRightDown(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                        UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                    }
                    if (KeyboardControls.KeyboardNone(ConvertToPlayers()))
                    {
                        RB.velocity = Vector2.zero;
                    }

                    break;
                case SupportedControllers.KeyboardP1ControllerP2:

                    if (ConvertToPlayers() == Players.P1)
                    {
                        if (KeyboardControls.KeyboardLeft(Players.P1))
                        {
                            RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.left);
                        }
                        if (KeyboardControls.KeyboardRight(Players.P1))
                        {
                            RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.right);
                        }
                        if (KeyboardControls.KeyboardUp(Players.P1))
                        {
                            RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                            //UpdateGunshipDirection(Vector2.up);
                        }
                        if (KeyboardControls.KeyboardDown(Players.P1))
                        {
                            RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                            //UpdateGunshipDirection(Vector2.down);
                        }
                        if (KeyboardControls.KeyboardLeftUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                        }
                        if (KeyboardControls.KeyboardRightUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                        }
                        if (KeyboardControls.KeyboardLeftDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                        }
                        if (KeyboardControls.KeyboardRightDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                        }
                        if (KeyboardControls.KeyboardNone(Players.P1))
                        {
                            RB.velocity = Vector2.zero;
                        }
                    }
                    else
                    {
                        if (ControllerControls.ControllerLeft(Players.P1))
                        {
                            RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.left);
                        }

                        if (ControllerControls.ControllerRight(Players.P1))
                        {
                            RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.right);
                        }

                        if (ControllerControls.ControllerUp(Players.P1))
                        {
                            RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                            //UpdateGunshipDirection(Vector2.up);
                        }

                        if (ControllerControls.ControllerDown(Players.P1))
                        {
                            RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                            //UpdateGunshipDirection(Vector2.down);
                        }

                        if (ControllerControls.ControllerLeftUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                        }

                        if (ControllerControls.ControllerRightUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                        }

                        if (ControllerControls.ControllerLeftDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                        }

                        if (ControllerControls.ControllerRightDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                        }

                        if (ControllerControls.ControllerNone(Players.P1))
                        {
                            RB.velocity = Vector2.zero;
                        }
                    }

                    break;
                case SupportedControllers.KeyboardP2ControllerP1:

                    if (ConvertToPlayers() == Players.P2)
                    {
                        if (KeyboardControls.KeyboardLeft(Players.P1))
                        {
                            RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.left);
                        }
                        if (KeyboardControls.KeyboardRight(Players.P1))
                        {
                            RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.right);
                        }
                        if (KeyboardControls.KeyboardUp(Players.P1))
                        {
                            RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.up);
                        }
                        if (KeyboardControls.KeyboardDown(Players.P1))
                        {
                            RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.down);
                        }
                        if (KeyboardControls.KeyboardLeftUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                        }
                        if (KeyboardControls.KeyboardRightUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                        }
                        if (KeyboardControls.KeyboardLeftDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                        }
                        if (KeyboardControls.KeyboardRightDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                        }
                        if (KeyboardControls.KeyboardNone(Players.P1))
                        {
                            RB.velocity = Vector2.zero;
                        }
                    }
                    else
                    {
                        if (ControllerControls.ControllerLeft(Players.P1))
                        {
                            RB.velocity = Vector2.left * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.left);
                        }

                        if (ControllerControls.ControllerRight(Players.P1))
                        {
                            RB.velocity = Vector2.right * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.right);
                        }

                        if (ControllerControls.ControllerUp(Players.P1))
                        {
                            RB.velocity = Vector2.up * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.up);
                        }

                        if (ControllerControls.ControllerDown(Players.P1))
                        {
                            RB.velocity = Vector2.down * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.down);
                        }

                        if (ControllerControls.ControllerLeftUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f));
                        }

                        if (ControllerControls.ControllerRightUp(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f));
                        }

                        if (ControllerControls.ControllerLeftDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f));
                        }

                        if (ControllerControls.ControllerRightDown(Players.P1))
                        {
                            RB.velocity = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f) * MoveSpd * Time.deltaTime;
                            UpdateGunshipDirection(Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f));
                        }

                        if (ControllerControls.ControllerNone(Players.P1))
                        {
                            RB.velocity = Vector2.zero;
                        }
                    }

                    break;
                default:
                    break;
            }


        }


        void UpdateGunshipDirection(Vector2 V)
        {
            if (PlayerShip == Ships.UnityGunship)
            {
                GunshipDirection = V;
            }
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