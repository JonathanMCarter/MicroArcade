using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Arcade.UserInput;
using CarterGames.Arcade.Saving;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class ShipManagement : InputSettings
    {
        [Header("-------- { Ship Managment } --------")]
        public Ships PlayerShip;                    // Which ship the object is
        public ShipStats Ship;                      // (AUTO-REF) The Ship Stats for the selection Ship
        public int? PlayerNumber;

        [Header("Stats")]
        public int Health;                          // (AUTO-REF) The Health for the ship
        public int Shield;                          // (AUTO-REF) The Shield Health for the ship
        public WeaponStats MainWeaponStats;         // (AUTO-REF) The main weapon stats for the ship
        public WeaponStats AltWeaponStats;          // (AUTO-REF) The alt weapon stats for the ship
        public WeaponStats SpecialWeapon;

        public enum SpecialStats { Alpha_Dodge, Attack_Torp, Inter_ShieldShare, One_Q_Torps, Gun_Teleport };
        public SpecialStats SpecialMove;
        public bool CanUseSpecialMove;

        public enum ShieldTypes { Bolts, Missiles, Regen, None };
        [Header("Shields")]
        public ShieldTypes ActiveShieldType;
        public bool CanChangeSheildType;
        public bool CanRegenShield;
        public ShieldColours Colours;

        [Header("Object Pooling")]
        public int MainPoolAmount;                  // The amount of main weapon objects to spawn into the game
        public int AltPoolAmount;                   // The amount of alt weapon objects to spawn into the game
        public int SpecPoolAmount;                   // The amount of alt weapon objects to spawn into the game
        public List<GameObject> MainWeaponObjPool;  // The Main weapon object pool
        public List<GameObject> AltWeaponObjPool;   // The Alt weapon object pool
        public List<GameObject> SpecWeaponObjPool;   // The Alt weapon object pool

        [Header("Able to shoot?")]
        public bool CanShootMain = true;            // Can the main weapon be shot?
        public bool CanShootAlt = true;             // Can the Alt weapon be shot?

        [Header("HealthBarUI")]
        public GameObject HealthBarUI;              // The Health Bar UI for this ship

        public bool IsEm;

        internal GameManager GM;                             // Reference to the game manager
        internal Starshine_SaveScript SSS;

        public int SalvoNo;

        public GameObject EmLaser;


        int LastMainShotNo;
        int LastAltShotNo;
        internal bool AngelFiring;

        // Special Move bools
        internal bool IsAlphaDodging;
        bool IsAttackTorpFired;
        bool IsScarletQTorpFired;
        bool IsTeleporting;
        bool IsFrozen;
        internal Vector2 GunshipDirection;

        public AudioManager am;


        // Ship Mission Stats for Victory / End Screen....
        public PlayerStats PlayerStats;

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        protected virtual void OnEnable()
        {
            // Setting up the game manager reference
            GM = FindObjectOfType<GameManager>();
            SSS = GM.gameObject.GetComponent<Starshine_SaveScript>();

            if (GetComponent<Enemies>())
            {
                IsEm = true;
            }
            else
            {
                CanChangeSheildType = true;
                CanRegenShield = true;
                CanUseSpecialMove = true;
            }

            if (FindObjectOfType<AudioManager>())
            {
                am = FindObjectOfType<AudioManager>();
                am.UpdateLibrary();
            }
        }


        // This is run with base.Start() on scripts that use this class
        protected virtual void Start()
        {
            // Setting the ship stats
            Ship = SelectShipStats();

            // Make Health Bar
            if (IsEm)
            {
                GameObject Go = Instantiate(GM.HealthBarUIPrefab);
                Go.GetComponentsInChildren<Slider>()[0].maxValue = Shield;
                Go.GetComponentsInChildren<Slider>()[1].maxValue = Health;
                Go.transform.SetParent(GameObject.FindGameObjectWithTag("WorldSpaceCanvas").transform);
                Go.transform.position = new Vector3(100, 100, 0);
                HealthBarUI = Go;

                // Colour Health Bar
                HealthBarUI.transform.GetComponentsInChildren<Image>()[3].color = Ship.HealthBarFillColour;
            }

            // default pooling if not an enemy
            if (!IsEm)
            {
                if (MainWeaponStats)
                {
                    MainPoolAmount = Ship.MainWeapon.PoolAmountNeeded;
                }

                // If the ship has an alt weapon - set its object pool amount
                if (AltWeaponStats)
                {
                    AltPoolAmount = Ship.AltWeapon.PoolAmountNeeded;
                }

                if (SpecialWeapon)
                {
                    SpecPoolAmount = SpecialWeapon.PoolAmountNeeded;
                }
            }

            // Setting up weapon object pooling
            ObjPoolSetup();
        }


        // this is run with base.Update() on scripts that use this class
        protected override void Update()
        {
            base.Update();

            // Check to see if this ship is still alive
            // There will be a check here in the near future for revival if it is a player....
            if (IsAlive())
            {
                if (IsEm)
                {
                    if (HealthBarUI)
                    {
                        if (!HealthBarUI.activeInHierarchy)
                        {
                            HealthBarUI.SetActive(true);
                        }

                        // Updates the position of the HealthBarUI (its in world space)
                        HealthBarUI.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

                        // Updates the health bar value based on current health
                        UpdateHealthBarValue();
                    }
                    else
                    {
                        GameObject Go = Instantiate(GM.HealthBarUIPrefab);
                        Go.GetComponentsInChildren<Slider>()[0].maxValue = Shield;
                        Go.GetComponentsInChildren<Slider>()[1].maxValue = Health;
                        Go.transform.SetParent(GameObject.FindGameObjectWithTag("WorldSpaceCanvas").transform);
                        Go.transform.position = new Vector3(100, 100, 0);
                        HealthBarUI = Go;

                        // Colour Health Bar
                        HealthBarUI.transform.GetComponentsInChildren<Image>()[3].color = Ship.HealthBarFillColour;

                        // Updates the position of the HealthBarUI (its in world space)
                        HealthBarUI.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

                        // Updates the health bar value based on current health
                        UpdateHealthBarValue();
                    }
                }
                else
                {
                    GetComponent<Renderer>().material.SetColor("_OutlineColour", Color.black);

                    // Regen Shields
                    if ((ActiveShieldType == ShieldTypes.Regen) && (Shield < Ship.Shield[(int)GM.ActiveStage]) && (CanRegenShield) && (Shield > 0))
                    {
                        StartCoroutine(RegenShield());
                    }

                    if (Shield > 0)
                    {
                        if (!transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled)
                        {
                            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled = true;
                        }


                        if (!IsEm)
                        {
                            if ((GetComponent<Renderer>()) && (Colours))
                            {
                                switch (ActiveShieldType)
                                {
                                    case ShieldTypes.Bolts:
                                        transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.BoltShieldColour);
                                        break;
                                    case ShieldTypes.Missiles:
                                        transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.MissileShieldColour);
                                        break;
                                    case ShieldTypes.Regen:
                                        transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.RegenShieldColour);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled)
                        {
                            transform.GetChild(transform.childCount - 1).GetComponent<MeshRenderer>().enabled = false;
                            am.Play("ShieldDown", .25f);
                        }
                    }
                }
            }
            else
            {
                // Disables the health bar UI and ship gameobject
                if (IsEm)
                {
                    StopAllCoroutines();

                    GM.gameObject.GetComponent<StageController>().StageDamaged();

                    HealthBarUI.SetActive(false);

                    if (PlayerShip != Ships.CelestialRocket)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("IsDead", true);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the ship health to see if it is still above 0
        /// </summary>
        /// <returns>true or false</returns>
        bool IsAlive()
        {
            if (Health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Shield Methods

        IEnumerator RegenShield()
        {
            CanRegenShield = false;
            Shield += 1;
            yield return new WaitForSeconds(.5f);
            CanRegenShield = true;
        }

        #endregion

        #region Shoot Weapon Methods
        /// <summary>
        /// Shoots the main weapon for the ship in use
        /// </summary>
        protected virtual IEnumerator ShootMainWeapon(Vector2 Direction, float Spd = 10, int? ShotFrom = null)
        {
            CanShootMain = false;
            GameObject Go;

            if (!IsEm)
            {
                Go = GetMainWeaponObj();
            }
            else
            {
                Go = GetComponent<Enemies>().EmWeapons.GetOrb();
            }

            if (Go)
            {
                if (Go.name.Contains("Laser"))
                {
                    Go.GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(0).position);
                    Go.GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 30, transform.GetChild(0).position.z));
                    Go.GetComponent<LineRenderer>().enabled = false;
                }

                Go.transform.position = MainPointToShootFrom().transform.position;
                Go.SetActive(true);


                Go.GetComponent<Damage>().DMG = (int)Ship.MainWeapon.Damage[(int)GM.ActiveStage];

                if (Go.GetComponent<MissileScript>())
                {
                    Go.GetComponent<MissileScript>().ShouldFindTarget = true;
                }
                else if (PlayerShip == Ships.UnityGunship)
                {
                    if (!IsFrozen)
                    {
                        StartCoroutine(UnityGunshipFreeze(Go));
                    }
                }
                else
                {
                    Go.GetComponent<Rigidbody2D>().velocity += Direction * Spd;
                }

                // Sets who shot the bullet if it has a value
                if (ShotFrom != null)
                {
                    Go.GetComponent<Damage>().SetPlayerShotFrom((int)ShotFrom);
                }

                // Wait for delay time before lettings the player shoot again
                yield return new WaitForSeconds(Ship.MainWeapon.Delay);

                CanShootMain = true;

            }
            else
            {
                yield return new WaitForSeconds(0);
                CanShootMain = true;
            }

            PlayerStats.mainShotsFired++;
        }


        IEnumerator UnityGunshipFreeze(GameObject Go)
        {
            IsFrozen = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<PlayerController>().enabled = false;
            Go.GetComponent<LineRenderer>().startColor = new Color32(217, 195, 219, 50);
            Go.GetComponent<LineRenderer>().enabled = true;
            am.Play("LaserPowerUp", .4f);
            yield return new WaitForSeconds(.35f);
            Go.GetComponent<BoxCollider2D>().enabled = true;
            Go.GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(0).position);
            Go.GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 30, transform.GetChild(0).position.z));
            Go.GetComponent<LineRenderer>().startColor = new Color32(194, 86, 204, 150);
            am.Play("LaserShoot", .4f);
            yield return new WaitForSeconds(.35f);
            GetComponent<PlayerController>().enabled = true;
            Go.GetComponent<BoxCollider2D>().enabled = false;
            IsFrozen = false;
        }


        /// <summary>
        /// Shoots the alt weapon for the ship in use
        /// </summary>
        protected virtual IEnumerator ShootAltWeapon(Vector2 Direction, float Spd = 10, int? ShotFrom = null)
        {
            CanShootAlt = false;

            GameObject Go;

            if (!IsEm)
            {
                Go = GetAltWeaponObj();
            }
            else
            {
                Go = GetComponent<Enemies>().EmWeapons.GetOrbVariant();
            }

            if (Go)
            {
                Go.transform.position = AltPointToShootFrom().transform.position;
                Go.SetActive(true);

                if (Go.GetComponent<MissileScript>())
                {
                    Go.GetComponent<MissileScript>().ShouldFindTarget = true;
                }
                else
                {
                    Go.GetComponent<Rigidbody2D>().velocity += Direction * Spd;
                }


                Go.GetComponent<Damage>().DMG = (int)Ship.AltWeapon.Damage[(int)GM.ActiveStage];


                // Sets who shot the bullet if it has a value
                if (ShotFrom != null)
                {
                    Go.GetComponent<Damage>().SetPlayerShotFrom((int)ShotFrom);
                }

                // Wait for delay time before lettings the player shoot again
                yield return new WaitForSeconds(Ship.AltWeapon.Delay);

                CanShootAlt = true;
            }
            else
            {
                yield return new WaitForSeconds(0);
                CanShootAlt = true;
            }

            PlayerStats.altShotsFired++;
        }


        /// <summary>
        /// Shoots the main weapon for the ship in use
        /// </summary>
        protected virtual IEnumerator ShootRocketAngelBurst(Vector2 Target)
        {
            Debug.Log("Co Running");

            while (SalvoNo < MainPoolAmount)
            {
                Debug.Log("Looping");

                CanShootMain = false;


                for (int i = 0; i < 8; i++)
                {
                    am.Play("MissileShoot", .15f);
                    GameObject currentMissile = GetComponent<Enemies>().EmWeapons.GetMissile();
                    currentMissile.GetComponent<MissileScript>().isRocketAngel = true;
                    currentMissile.transform.position = transform.position;
                    currentMissile.GetComponent<Damage>().DMG = (int)Ship.MainWeapon.Damage[(int)GM.ActiveStage];
                    currentMissile.GetComponent<BoxCollider2D>().enabled = true;
                    currentMissile.SetActive(true);
                    StartCoroutine(currentMissile.GetComponent<Damage>().MoveCurve(currentMissile, transform.position, new Vector2(Random.Range(-1.5f, 1.5f) + ChooseSide().x, Random.Range(-1.5f, 1.5f) + ChooseSide().y), Target));
                    SalvoNo++;
                    yield return new WaitForSeconds(Ship.MainWeapon.Delay);
                }
            }

            // Wait for delay time before the burst fires again
            yield return new WaitForSeconds(5);
            SalvoNo = 0;
            CanShootMain = true;
            AngelFiring = false;
        }
        #endregion




        #region Ship Setup Methods
        /// <summary>
        /// Selects the correct ship stats based on the player ship enum selection
        /// </summary>
        /// <returns>The selected ship stats</returns>
        ShipStats SelectShipStats()
        {
            switch (PlayerShip)
            {
                case Ships.AetherAlpha:
                    Health = GM.PlayerShips[0].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[0].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[0].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[0].AltWeapon;
                    SpecialMove = SpecialStats.Alpha_Dodge;
                    return GM.PlayerShips[0];
                case Ships.AetherAttack:
                    Health = GM.PlayerShips[1].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[1].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[1].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[1].AltWeapon;
                    SpecialMove = SpecialStats.Attack_Torp;
                    return GM.PlayerShips[1];
                case Ships.ScarletOne:
                    Health = GM.PlayerShips[2].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[2].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[2].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[2].AltWeapon;
                    SpecialMove = SpecialStats.One_Q_Torps;
                    return GM.PlayerShips[2];
                case Ships.ScarletInter:
                    Health = GM.PlayerShips[3].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[3].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[3].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[3].AltWeapon;
                    SpecialMove = SpecialStats.Inter_ShieldShare;
                    return GM.PlayerShips[3];
                case Ships.UnityGunship:
                    Health = GM.PlayerShips[4].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[4].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[4].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[4].AltWeapon;
                    SpecialMove = SpecialStats.Gun_Teleport;
                    return GM.PlayerShips[4];
                case Ships.CelestialYari:
                    Health = GM.PlayerShips[5].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[5].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[5].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[5].AltWeapon;
                    return GM.PlayerShips[5];
                case Ships.CelestialStandard:
                    Health = GM.PlayerShips[6].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[6].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[6].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[6].AltWeapon;
                    return GM.PlayerShips[6];
                case Ships.CelestialRocket:
                    Health = GM.PlayerShips[7].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[7].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[7].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[7].AltWeapon;
                    return GM.PlayerShips[7];
                case Ships.CelestialDrone:
                    Health = GM.PlayerShips[8].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[8].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[8].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[8].AltWeapon;
                    return GM.PlayerShips[8];
                case Ships.None:
                    Debug.LogError("Operation: Starshine - No Ship Selected [Player controller - SelectShipStats()]");
                    return null;
                case Ships.CelestialRocketDrone:
                    Health = GM.PlayerShips[9].Health[(int)GM.ActiveStage];
                    Shield = GM.PlayerShips[9].Shield[(int)GM.ActiveStage];
                    MainWeaponStats = GM.PlayerShips[9].MainWeapon;
                    AltWeaponStats = GM.PlayerShips[9].AltWeapon;
                    return GM.PlayerShips[9];
                default:
                    Debug.LogError("Operation: Starshine - Ship Selection Failed [Player controller - SelectShipStats()]");
                    return null;
            }
        }



        /// <summary>
        /// Sets up the object pooling for the players weapons...
        /// </summary>
        void ObjPoolSetup()
        {
            // Main Obj Pool 
            for (int i = 0; i < MainPoolAmount; i++)
            {
                GameObject Go = Instantiate(Ship.MainWeaponPrefab);

                if ((!IsEm) && (PlayerShip != Ships.UnityGunship))
                {
                    Go.GetComponent<SpriteRenderer>().color = Ship.HealthBarFillColour;
                }

                Go.SetActive(false);
                MainWeaponObjPool.Add(Go);
            }


            // Alt Obj Pool (Only if one exsists)
            if (AltWeaponStats)
            {
                for (int i = 0; i < AltPoolAmount; i++)
                {
                    GameObject Go = Instantiate(Ship.AltWeaponPrefab);

                    if (!IsEm)
                    {
                        Go.GetComponent<SpriteRenderer>().color = Ship.HealthBarFillColour;
                    }

                    Go.SetActive(false);
                    AltWeaponObjPool.Add(Go);
                }
            }


            // if special move has a weapon!
            if (SpecialWeapon)
            {
                for (int i = 0; i < SpecPoolAmount; i++)
                {
                    GameObject Go = Instantiate(SpecialWeapon.Prefab);
                    Go.SetActive(false);
                    SpecWeaponObjPool.Add(Go);
                }
            }
        }

        #endregion


        #region leaderboardData Weapon & Positon Methods

        /// <summary>
        /// Gets the next avalible main weapon object from the pool
        /// </summary>
        /// <returns>A main weapon Gameobject</returns>
        internal GameObject GetMainWeaponObj()
        {
            for (int i = 0; i < MainWeaponObjPool.Count; i++)
            {
                if (!MainWeaponObjPool[i].activeInHierarchy)
                {
                    return MainWeaponObjPool[i];
                }
            }

            Debug.Log("Operation: Starshine - Couldn't get a Main Weapon Object to use [Player controller - GetMainWeaponOBJ()]");
            return null;
        }


        /// <summary>
        /// Gets the next avalible alt weapon object from the pool
        /// </summary>
        /// <returns>A alt weapon Gameobject</returns>
        internal GameObject GetAltWeaponObj()
        {
            for (int i = 0; i < AltWeaponObjPool.Count; i++)
            {
                if (!AltWeaponObjPool[i].activeInHierarchy)
                {
                    return AltWeaponObjPool[i];
                }
            }

            Debug.Log("Operation: Starshine - Couldn't get a Alt Weapon Object to use [Player controller - GetAltWeaponOBJ()]");
            return null;
        }


        /// <summary>
        /// Gets the next avalible spec weapon object from the pool
        /// </summary>
        /// <returns>A alt weapon Gameobject</returns>
        GameObject GetSpecWeaponObj()
        {
            for (int i = 0; i < SpecWeaponObjPool.Count; i++)
            {
                if (!SpecWeaponObjPool[i].activeInHierarchy)
                {
                    return SpecWeaponObjPool[i];
                }
            }

            Debug.Log("Operation: Starshine - Couldn't get a Spec Weapon Object to use [Player controller - GetSpecWeaponOBJ()]");
            return null;
        }



        GameObject MainPointToShootFrom()
        {
            if ((transform.childCount > 0) && (PlayerShip != Ships.CelestialRocket))
            {
                switch (PlayerShip)
                {
                    case Ships.AetherAlpha:

                        if (LastMainShotNo % 6 <= 2)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(0).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(1).gameObject;
                        }

                    case Ships.AetherAttack:

                        if (LastMainShotNo % 2 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(0).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(1).gameObject;
                        }

                    case Ships.ScarletOne:

                        if (LastMainShotNo % 4 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(0).gameObject;
                        }
                        else if (LastMainShotNo % 4 == 1)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(1).gameObject;
                        }
                        else if (LastMainShotNo % 4 == 2)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(3).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(2).gameObject;
                        }

                    case Ships.ScarletInter:

                        if (LastMainShotNo % 2 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(0).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(1).gameObject;
                        }

                    case Ships.UnityGunship:
                        return transform.GetChild(0).gameObject;

                    case Ships.CelestialStandard:

                        if (LastMainShotNo % 2 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(0).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(1).gameObject;
                        }

                    default:
                        return gameObject;
                }
            }
            else
            {
                return gameObject;
            }
        }


        GameObject AltPointToShootFrom()
        {
            if ((transform.childCount > 0) && (PlayerShip != Ships.CelestialRocket))
            {
                switch (PlayerShip)
                {
                    case Ships.AetherAlpha:

                        return transform.GetChild(2).gameObject;

                    case Ships.AetherAttack:

                        if (LastAltShotNo % 2 == 0)
                        {
                            LastAltShotNo++;
                            return transform.GetChild(2).gameObject;
                        }
                        else
                        {
                            LastAltShotNo++;
                            return transform.GetChild(3).gameObject;
                        }

                    case Ships.ScarletOne:

                        if (LastAltShotNo % 2 == 0)
                        {
                            LastAltShotNo++;
                            return transform.GetChild(4).gameObject;
                        }
                        else
                        {
                            LastAltShotNo++;
                            return transform.GetChild(5).gameObject;
                        }

                    case Ships.ScarletInter:

                        if (LastAltShotNo % 4 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(3).gameObject;
                        }
                        else if (LastAltShotNo % 4 == 1)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(4).gameObject;
                        }
                        else if (LastAltShotNo % 4 == 2)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(5).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(6).gameObject;
                        }

                    case Ships.UnityGunship:

                        if (LastAltShotNo % 2 == 0)
                        {
                            LastAltShotNo++;
                            return transform.GetChild(1).gameObject;
                        }
                        else
                        {
                            LastAltShotNo++;
                            return transform.GetChild(2).gameObject;
                        }

                    case Ships.CelestialStandard:

                        if (LastAltShotNo % 4 == 0)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(3).gameObject;
                        }
                        else if (LastAltShotNo % 4 == 1)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(4).gameObject;
                        }
                        else if (LastAltShotNo % 4 == 2)
                        {
                            LastMainShotNo++;
                            return transform.GetChild(5).gameObject;
                        }
                        else
                        {
                            LastMainShotNo++;
                            return transform.GetChild(6).gameObject;
                        }

                    default:
                        return gameObject;
                }
            }
            else
            {
                return gameObject;
            }
        }

        Vector2 ChooseSide()
        {
            if (SalvoNo % 2 == 0)
            {
                return gameObject.transform.GetChild(0).transform.position;
            }
            else
            {
                return gameObject.transform.GetChild(1).transform.position;
            }
        }

        #endregion


        /// <summary>
        /// Updates the Health Bar UI Value to match the current health
        /// </summary>
        void UpdateHealthBarValue()
        {
            if (Ship.Shield[(int)GM.ActiveStage] > 0)
            {
                HealthBarUI.GetComponentsInChildren<Slider>()[0].value = Shield;
            }
            else
            {
                HealthBarUI.GetComponentsInChildren<Slider>()[0].GetComponentsInChildren<Image>()[0].color = new Color32(0, 0, 0, 0);
            }

            HealthBarUI.GetComponentsInChildren<Slider>()[1].value = Health;
        }


        #region Special Move Methods

        // Special Methods
        public void ExecuteSpecialMove()
        {
            if (CanUseSpecialMove)
            {
                switch (SpecialMove)
                {
                    case SpecialStats.Alpha_Dodge:

                        AlphaDodgeMove();

                        break;
                    case SpecialStats.Attack_Torp:

                        AttackTorpedo();

                        break;
                    case SpecialStats.Inter_ShieldShare:

                        InterShieldShare();

                        break;
                    case SpecialStats.One_Q_Torps:

                        OneQTorpedo();

                        break;
                    case SpecialStats.Gun_Teleport:

                        GunshipTeleport();

                        break;
                    default:
                        break;
                }
            }
        }


        IEnumerator SpecialMoveCooldown(float Cooldown)
        {
            CanUseSpecialMove = false;
            yield return new WaitForSeconds(Cooldown);
            CanUseSpecialMove = true;
        }


        // Aether Alpha Special Methods
        void AlphaDodgeMove()
        {
            if ((CanUseSpecialMove) && (!IsAlphaDodging))
            {
                StartCoroutine(AlphaDodge());
                StartCoroutine(SpecialMoveCooldown(15f));
            }
        }

        IEnumerator AlphaDodge()
        {
            IsAlphaDodging = true;
            am.Play("Alpha", .5f);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = ColourAlphaChangeToFrom50(GetComponent<SpriteRenderer>().color);
            CanShootMain = false;
            CanShootAlt = false;
            yield return new WaitForSeconds(3 * ((int)GM.ActiveStage + 1));
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = ColourAlphaChangeToFrom50(GetComponent<SpriteRenderer>().color, false);
            CanShootMain = true;
            CanShootAlt = true;
            IsAlphaDodging = false;
            PlayerStats.specialShotsFired++;
        }


        Color ColourAlphaChangeToFrom50(Color CurrentCol, bool Reduce = true)
        {
            if (Reduce)
            {
                return new Color(CurrentCol.r, CurrentCol.g, CurrentCol.b, CurrentCol.a / 2);
            }
            else
            {
                return new Color(CurrentCol.r, CurrentCol.g, CurrentCol.b, 1f);
            }
        }



        // Aether Attack Special Methods
        void AttackTorpedo()
        {
            if ((CanUseSpecialMove) && (!IsAttackTorpFired))
            {
                StartCoroutine(LaunchAttackTorpedos());
                StartCoroutine(SpecialMoveCooldown(25f));
            }
        }

        IEnumerator LaunchAttackTorpedos()
        {
            IsAttackTorpFired = true;

            am.PlayFromTime("TorpedoSounds", 7.9f, .5f);

            if (SpecialWeapon)
            {
                GameObject Go = GetSpecWeaponObj();
                Vector3 pos = new Vector3(transform.position.x, transform.GetChild(0).transform.position.y, 0);

                Go.transform.position = pos;
                Go.SetActive(true);

                Go.GetComponent<Damage>().DMG = (int)SpecialWeapon.Damage[(int)GM.ActiveStage];

                Go.GetComponent<Rigidbody2D>().velocity += Vector2.up * 14;

                // Sets who shot the bullet if it has a value
                Go.GetComponent<Damage>().SetPlayerShotFrom((int)PlayerNumber);
            }

            yield return new WaitForSeconds(.1f);
            IsAttackTorpFired = false;
            PlayerStats.specialShotsFired++;
        }



        // Scarlet One Special Methods
        void OneQTorpedo()
        {
            if ((CanUseSpecialMove) && (!IsScarletQTorpFired))
            {
                StartCoroutine(LaunchScarletQTorpedos());
                StartCoroutine(SpecialMoveCooldown(25f));
            }
        }

        IEnumerator LaunchScarletQTorpedos()
        {
            IsScarletQTorpFired = true;

            if (SpecialWeapon)
            {
                for (int i = 0; i < 3; i++)
                {
                    OneVolley();
                    am.PlayFromTime("TorpedoSounds", 7.9f, .5f);
                    yield return new WaitForSeconds(.4f);
                }

            }

            yield return new WaitForSeconds(.1f);
            IsScarletQTorpFired = false;
            PlayerStats.specialShotsFired++;
        }


        void OneVolley()
        {
            List<GameObject> Torps = new List<GameObject>();

            for (int i = 0; i < 2; i++)
            {
                GameObject Go = GetSpecWeaponObj();
                Torps.Add(Go);

                if (i % 2 == 0)
                {
                    Go.transform.position = transform.GetChild(0).position;
                    Go.SetActive(true);
                }
                else
                {
                    Go.transform.position = transform.GetChild(1).position;
                    Go.SetActive(true);
                }
            }

            for (int i = 0; i < Torps.Count; i++)
            {
                Torps[i].GetComponent<Damage>().DMG = (int)SpecialWeapon.Damage[(int)GM.ActiveStage];

                Torps[i].GetComponent<Rigidbody2D>().velocity += Vector2.up * 14;

                // Sets who shot the bullet if it has a value
                Torps[i].GetComponent<Damage>().SetPlayerShotFrom((int)PlayerNumber);
            }
        }



        // Scarlet Inter Shield Share Methods
        void InterShieldShare()
        {
            if ((CanUseSpecialMove))
            {
                am.Play("Inter", .5f);

                if (Shield > 10)
                {
                    if (PlayerNumber == 1)
                    {
                        GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<PlayerController>().Shield += (100 * ((int)GM.ActiveStage + 1));
                        Shield -= 50;
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<PlayerController>().Shield += (100 * ((int)GM.ActiveStage + 1));
                        Shield -= 50;
                    }

                    PlayerStats.specialShotsFired++;
                    StartCoroutine(SpecialMoveCooldown(30f));
                }
            }
        }



        // Unity Gunship Teleport Methods
        void GunshipTeleport()
        {
            if ((CanUseSpecialMove) && (!IsTeleporting))
            {
                StartCoroutine(UnityTeleport());
                StartCoroutine(SpecialMoveCooldown(30f));
            }
        }


        IEnumerator UnityTeleport()
        {
            IsTeleporting = true;
            am.Play("Unity", .5f);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = ColourAlphaChangeToFrom50(GetComponent<SpriteRenderer>().color);
            transform.position = new Vector3(transform.position.x + GunshipDirection.x * 4, transform.position.y + GunshipDirection.y * 4, transform.position.z);
            yield return new WaitForSeconds(1f);
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = ColourAlphaChangeToFrom50(GetComponent<SpriteRenderer>().color, false);
            IsTeleporting = false;
            PlayerStats.specialShotsFired++;
        }

        #endregion



        internal void UpdatePlayerHealthStats()
        {
            int _oldHealth = Health;
            int _oldShield = Shield;

            int _newhealth = Ship.Health[(int)GM.ActiveStage] - _oldHealth;
            int _newShield = Ship.Shield[(int)GM.ActiveStage] - _oldShield;

            if (_newhealth > Ship.Health[(int)GM.ActiveStage])
            {
                _newhealth = Ship.Health[(int)GM.ActiveStage];
            }

            if (_newShield > Ship.Shield[(int)GM.ActiveStage])
            {
                _newShield = Ship.Shield[(int)GM.ActiveStage];
            }

            Health = _newhealth;
            Shield = _newShield;
        }
    }
}