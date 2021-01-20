using UnityEngine;
using System.Collections;
using CarterGames.Utilities;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Arcade.Credits
{
    /// <summary>
    /// Class | Controls the player functionality in the credits game.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Header("Player Variables")]
        [SerializeField] private SpriteRenderer sr = default;
        [SerializeField] private float moveSpd = default;

        [Header("Bullet Variables")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletMoveSpd = default;

        [Header("Missile Variables")]
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private float missileMoveSpd = default;

        private GameObject[] bulletPool;
        private GameObject[] missilePool;
        private Rigidbody2D rb;
        private Actions actions;
        private bool canShootBullet = false;
        private bool canShootMissile = false;
        private WaitForSeconds wait;
        private WaitForSeconds missileWait;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity OnEnable Method | Setting up the input controls
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            actions = new Actions();
            actions.Enable();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity OnDisable Method | Disables the input and stop and coroutines from runnig once the script/gameobject is not active.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnDisable()
        {
            actions.Disable();
            StopAllCoroutines();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Awake Method | Sets up any references needed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            wait = new WaitForSeconds(.05f);
            missileWait = new WaitForSeconds(3f);
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start Method | Sets up the object pooling for the bullets.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            BulletPoolSetup();
            MissilePoolSetup();
            canShootBullet = true;
            canShootMissile = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update Method | Checks for the user input on the shoot button.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (NewInputSystemHelper.ButtonPressed(actions.Controls.Button1) && canShootBullet)
                SpawnBullet();

            if (NewInputSystemHelper.ButtonPressed(actions.Controls.Button2) && canShootMissile)
                SpawnMissile();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity FixedUpdate Method | Checks for player input on the joystick and moves the ship.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void FixedUpdate()
        {
            rb.velocity = actions.Controls.Joystick.ReadValue<Vector2>() * moveSpd * Time.deltaTime;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity LateUpdate Method | Keeps the player ship in the bounds of the level.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void LateUpdate()
        {
            transform.position = Keep.WithinBounds(transform.position, new Vector2(-8f, -3f), new Vector2(0f, 3f));
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Sets up the bullet object pool for use.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BulletPoolSetup()
        {
            bulletPool = new GameObject[75];

            for (int i = 0; i < bulletPool.Length; i++)
            {
                bulletPool[i] = Instantiate(bulletPrefab);
                bulletPool[i].SetActive(false);
                bulletPool[i].name = "* Bullet (OBJ-Pool)";
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Sets up the missile object pool for use.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void MissilePoolSetup()
        {
            missilePool = new GameObject[10];

            for (int i = 0; i < missilePool.Length; i++)
            {
                missilePool[i] = Instantiate(missilePrefab);
                missilePool[i].SetActive(false);
                missilePool[i].name = "* Missile (OBJ-Pool)";
            }
        }



        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Spawns a bullet and start the coroutine to recharge for the next shot.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void SpawnBullet()
        {
            for (int i = 0; i < bulletPool.Length; i++)
            {
                if (!bulletPool[i].activeSelf)
                {
                    if ((i % 6) <= 2)
                        bulletPool[i].transform.position = transform.GetChild(0).position;
                    else
                        bulletPool[i].transform.position = transform.GetChild(1).position;

                    bulletPool[i].SetActive(true);
                    StartCoroutine(BulletCo());
                    break;
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Coroutine | Toggles the canShoot bool so that the player can shoot all their bullets at once.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private IEnumerator BulletCo()
        {
            canShootBullet = false;
            yield return wait;
            canShootBullet = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Spawns a bullet and start the coroutine to recharge for the next shot.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void SpawnMissile()
        {
            for (int i = 0; i < missilePool.Length; i++)
            {
                if (!missilePool[i].activeSelf)
                {
                    if ((i % 6) <= 2)
                        missilePool[i].transform.position = transform.GetChild(0).position;
                    else
                        missilePool[i].transform.position = transform.GetChild(1).position;

                    missilePool[i].SetActive(true);
                    StartCoroutine(MissileCo());
                    break;
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Coroutine | Toggles the canShoot bool so that the player can shoot all their bullets at once.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private IEnumerator MissileCo()
        {
            canShootMissile = false;
            yield return missileWait;
            canShootMissile = true;
        }
    }
}