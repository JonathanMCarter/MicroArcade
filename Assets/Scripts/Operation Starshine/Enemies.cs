using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Arcade.UserInput;
using System.Linq;

namespace CarterGames.Starshine
{
    [RequireComponent(typeof(Combat))]
    public class Enemies : ShipManagement
    {
        public enum EnemyShipType
        {
            Drone,
            RocketDrone,
            Yari,
            Standard,
            Rocket,
        };


        public enum DroneMoves
        {
            MoveInFromAboveAndStayPut,
        };


        public enum YariMoves
        {
            Spawning,
            MoveToAttack,
            AttackMain,
            ChargeAttack,
        };

        public enum StandardMoves
        {
            Spawning,
            Moving,
            Attack,
        };

        public enum RocketMoves
        {
            Spawning,
            Moving,
            Attack,
        }


        [Header("-------- { Enemies } --------")]
        public EnemyShipType ShipType;

        public EnemyWeapons EmWeapons;

        public GameObject PlayerToTarget;

        public float MoveSpd;


        [Header("Drone Stuff")]
        public bool ShouldShoot;
        public GameObject Player1;
        public GameObject Player2;


        [Header("Rocket Moves")]
        public RocketMoves RMoves;

        [Header("Spawning into the scene")]
        public bool Spawning;
        public float SpawnY;

        public GameObject Target;


        public bool IsInRange;
        Vector3 ToTarget;

        Camera Cam;
        Vector2 CamBounds;
        Rigidbody2D RB;
        Animator Anim;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (PlayerShip == Ships.CelestialRocket)
            {
                Target = ChooseTarget();
                StopAllCoroutines();
            }

            if (PlayerShip == Ships.CelestialRocket)
            {
                Anim = GetComponent<Animator>();

                if (Anim.GetBool("IsDead"))
                {
                    Anim.SetBool("IsDead", false);
                }
            }
        }



        protected override void Start()
        {
            Cam = Camera.main;
            CamBounds = Cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Cam.transform.position.z));

            EmWeapons = GameObject.FindGameObjectWithTag("StarshineEnemyWeapons").GetComponent<EnemyWeapons>();

            RB = GetComponent<Rigidbody2D>();

            Player1 = GameObject.FindGameObjectWithTag("WhitePlayer");
            Player2 = GameObject.FindGameObjectWithTag("BlackPlayer");

            IsEm = true;


            if (ShipType == EnemyShipType.Yari)
            {
                PlayerToTarget = ChooseTarget().gameObject;
            }

            if (GetComponent<Animator>())
            {
                Anim = GetComponent<Animator>();
            }


            switch (ShipType)
            {
                case EnemyShipType.Drone:
                    break;
                case EnemyShipType.Yari:
                    break;
                case EnemyShipType.Standard:
                    break;
                case EnemyShipType.Rocket:

                    RocketStart();

                    break;
                default:
                    break;
            }

            base.Start();

            if (IsEm)
            {
                if (Shield > 0)
                {
                    if (PlayerShip == Ships.CelestialDrone && transform.childCount > 0)
                    {
                        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                        ActiveShieldType = GetRandomShieldType();
                    }
                    else if (PlayerShip == Ships.CelestialRocketDrone)
                    {
                        transform.GetChild(4).gameObject.SetActive(true);
                        ActiveShieldType = GetRandomShieldType();
                    }

                    switch (ActiveShieldType)
                    {
                        case ShieldTypes.Bolts:
                            transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.BoltShieldColour);
                            break;
                        case ShieldTypes.Missiles:
                            transform.GetChild(transform.childCount - 1).GetComponent<Renderer>().material.SetColor("shieldColor", Colours.MissileShieldColour);
                            break;
                        default:
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// Aquires a target when the rocket angel enemy is spawned
        /// </summary>
        void RocketStart()
        {
            Target = ChooseTarget();
        }


        protected override void Update()
        {
            if (ShipType == EnemyShipType.Drone)
            {
                DroneAI();
            }


            if (ShipType == EnemyShipType.Rocket)
            {
                RocketAI();
            }


            base.Update();


            if (Shield <= 0)
            {
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            }
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerController>())
            {
                IsInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerController>())
            {
                IsInRange = false;
            }
        }


        #region --- { Drone Stuff } ---

        /// <summary>
        /// Main function that controls what the Drone's will do
        /// </summary>
        void DroneAI()
        {
            if (ShouldShoot)
            {
                if ((ChooseWeapon()) && (CanShootMain) && (CanShootAlt))
                {
                    StartCoroutine(base.ShootMainWeapon((GetNearestTarget() - new Vector2(transform.position.x, transform.position.y)).normalized, 7.5f));
                }
                else if ((!ChooseWeapon()) && (CanShootAlt) && (CanShootMain))
                {
                    StartCoroutine(base.ShootAltWeapon((GetNearestTarget() - new Vector2(transform.position.x, transform.position.y)).normalized, 7.5f));
                }
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)GetNearestTarget() - transform.position);
        }


        /// <summary>
        /// Decides if the alt weapon is used or the main weapon 80% Main, 20% Alt
        /// </summary>
        /// <returns>True or False</returns>
        bool ChooseWeapon()
        {
            int R = Random.Range(0, 11);

            if (R > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Returns the nearest Player to target, used for both drones...
        /// </summary>
        /// <returns>The Vector2 of the Target Player Ship</returns>
        Vector2 GetNearestTarget()
        {
            // if both players are alive
            if ((Player1) && (Player2) && (Player1.activeInHierarchy) && (Player2.activeInHierarchy))
            {
                float Player1Distance = Vector2.Distance(Player1.transform.position, transform.position);
                float Player2Distance = Vector2.Distance(Player2.transform.position, transform.position);

                ShouldShoot = true;

                if (Player1Distance < Player2Distance)
                {
                    return Player1.transform.position;
                }
                else
                {
                    return Player2.transform.position;
                }
            }
            else if ((Player1) && (Player2) && (Player1.activeInHierarchy) && (!Player2.activeInHierarchy))
            {
                return Player1.transform.position;
            }
            else if ((Player1) && (Player2) && (!Player1.activeInHierarchy) && (Player2.activeInHierarchy))
            {
                return Player2.transform.position;
            }
            // if only 1 player is alive
            else if ((Player1) && (Player1.activeInHierarchy) && (!Player2))
            {
                ShouldShoot = true;
                return Player1.transform.position;
            }
            // if only 2 player is alive
            else if ((Player2) && (!Player1) && (Player2.activeInHierarchy))
            {
                ShouldShoot = true;
                return Player2.transform.position;
            }
            else
            {
                //ShouldShoot = false;
                return Vector2.zero;
            }
        }


        #endregion

        #region --- { Rocket Angel Stuff } ---
        /// <summary>
        /// The Main function for the Rocket Angel AI
        /// </summary>
        void RocketAI()
        {
            switch (RMoves)
            {
                case RocketMoves.Spawning:

                    RMoves = RocketMoves.Moving;

                    break;
                case RocketMoves.Moving:

                    // Look at the target
                    if ((Target) && (Target.activeInHierarchy))
                    {
                        ToTarget = transform.position - Target.transform.position;
                        transform.rotation = Quaternion.LookRotation(Vector3.forward, ToTarget);
                    }
                    else
                    {
                        Target = ChooseTarget();
                    }

                    // if the target is not active then switch to the other player
                    if (Target)
                    {
                        if (!Target.activeInHierarchy)
                        {
                            Target = ChooseTarget();
                        }
                    }

                    if (!IsInRange)
                    {
                        MoveTowardsTarget();

                        // Animation --- Moving --- on
                        if (!Anim.GetBool("IsMoving"))
                        {
                            Anim.SetBool("IsMoving", true);
                        }
                    }
                    else
                    {
                        // Animation --- Moving --- off
                        if (Anim.GetBool("IsMoving"))
                        {
                            Anim.SetBool("IsMoving", false);
                        }

                        RB.velocity = Vector2.zero;
                        RMoves = RocketMoves.Attack;
                    }


                    break;
                case RocketMoves.Attack:

                    if (IsInRange)
                    {

                        // Look at the target
                        ToTarget = transform.position - Target.transform.position;
                        transform.rotation = Quaternion.LookRotation(Vector3.forward, ToTarget);


                        // Selects a target at random
                        if (Target)
                        {
                            if (!Target.activeInHierarchy)
                            {
                                Target = ChooseTarget();
                            }
                        }


                        // Shoots rockets
                        if (CanShootMain)
                        {
                            // Animation --- Attack --- On
                            if (!Anim.GetBool("IsAttacking"))
                            {
                                Anim.SetBool("IsAttacking", true);
                            }

                            if (!AngelFiring)
                            {
                                AngelFiring = true;
                                StartCoroutine(ShootRocketAngelBurst(Target.transform.position));
                            }
                        }
                        else
                        {
                            if (Anim.GetBool("IsAttacking"))
                            {
                                Anim.SetBool("IsAttacking", false);
                            }
                        }
                    }
                    else
                    {
                        RMoves = RocketMoves.Moving;
                    }



                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Chooses a target for the rocket angel to target, doesn't choose the cloest like the drones do
        /// </summary>
        /// <returns>The gameobject of the target player</returns>
        GameObject ChooseTarget()
        {
            int Number = Random.Range(0, 2);

            if (FindObjectsOfType<PlayerController>().Length == 2)
            {
                if (Number % 2 == 1)
                {
                    return FindObjectsOfType<PlayerController>()[1].gameObject;
                }
                else
                {
                    return FindObjectsOfType<PlayerController>()[0].gameObject;
                }
            }
            else if (FindObjectsOfType<PlayerController>().Length == 1)
            {
                return FindObjectOfType<PlayerController>().gameObject;
            }
            else if (FindObjectsOfType<PlayerController>().Length == 0)
            {
                return null;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Used for the rocket angel to move it so it is in range of the target player while staying above Y,0
        /// </summary>
        void MoveTowardsTarget()
        {
            Debug.Log("running");

            if (transform.position.y > 0f)
            {
                Debug.Log("Above Y 0");
                GetComponent<Rigidbody2D>().velocity = (Target.transform.position - transform.position).normalized * MoveSpd * Time.deltaTime;
            }
            else
            {
                Debug.Log("Not Above Y 0");
                Vector2 Vec = (Target.transform.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = new Vector2(Vec.x, 0) * MoveSpd * Time.deltaTime;
            }
        }


        #endregion


        private ShieldTypes GetRandomShieldType()
        {
            return (ShieldTypes)Random.Range(0, 2);
        }
    }
}