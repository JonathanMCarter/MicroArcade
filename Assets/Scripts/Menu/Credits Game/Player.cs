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
        [SerializeField] private GameObject[] hearts;
        [SerializeField] private int health = 3;

        [Header("Player Variables")]
        [SerializeField] private SpriteRenderer sr = default;
        [SerializeField] private float moveSpd = default;

        [Header("Bullet Variables")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject heavyBulletPrefab;
        [SerializeField] private Canvas NameInput;

        private bool shootHeavy = false;
        private GameObject[] bulletPool;
        private GameObject[] heavyBulletPool;
        private Rigidbody2D rb;
        private Actions actions;
        private bool canShootBullet = false;
        private WaitForSeconds wait;
        private WaitForSeconds flickerWait;
        private int numberShot;
        private CameraShakeScript shake;
        private Scoring score;


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
            flickerWait = new WaitForSeconds(.2f);
            shake = FindObjectOfType<CameraShakeScript>();
            score = FindObjectOfType<Scoring>();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Start Method | Sets up the object pooling for the bullets.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            BulletPoolSetup();
            canShootBullet = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Update Method | Checks for the user input on the shoot button.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (NewInputSystemHelper.ButtonPressed(actions.Controls.Button1) && canShootBullet)
                if (!shootHeavy)
                    SpawnBullet();
                else
                    SpawnHeavyBullet();


            if (health <= 0)
            {
                Time.timeScale = 0;
                NameInput.enabled = true;
                NameInput.GetComponentInChildren<OnScreenKeyboard.OnScreenKeyboard>().enabled = true;
                gameObject.SetActive(false);
            }
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
            transform.position = Keep.WithinBounds(transform.position, new Vector2(-8f, -4f), new Vector2(8f, -4f));
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (hearts[i].activeSelf)
                    {
                        hearts[i].SetActive(false);
                        score.DecrementScore(500);
                        break;
                    }
                }

                collision.gameObject.SetActive(false);
                shake.ShakeCamera(true);
                health--;
                FlickerShip();
            }

            if (collision.CompareTag("Powerup"))
            {
                if (collision.gameObject.GetComponent<Powerups>().powerup.Equals(Powerups.Powerup.HeavyBullet))
                    StartCoroutine(HeavyBulletPowerUpCoolDown());
            }
        }



        private void FlickerShip()
        {
            StartCoroutine(FlickerCoroutine());
        }


        private IEnumerator FlickerCoroutine()
        {
            sr.color = Color.red;
            yield return flickerWait;
            sr.color = Color.white;
            yield return flickerWait;
            sr.color = Color.red;
            yield return flickerWait;
            sr.color = Color.white;
        }



        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Sets up the bullet object pool for use.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BulletPoolSetup()
        {
            bulletPool = new GameObject[75];
            heavyBulletPool = new GameObject[75];

            for (int i = 0; i < bulletPool.Length; i++)
            {
                bulletPool[i] = Instantiate(bulletPrefab);
                bulletPool[i].SetActive(false);
                bulletPool[i].name = "* Bullet (OBJ-Pool)";

                heavyBulletPool[i] = Instantiate(heavyBulletPrefab);
                heavyBulletPool[i].SetActive(false);
                heavyBulletPool[i].name = "* Heavy Bullet (OBJ-Pool)";
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
                    if ((numberShot % 6) <= 2)
                        bulletPool[i].transform.position = Rand.Vector3(transform.GetChild(0).position, .15f, .15f, 0f, 0f, 1f, 1f);
                    else
                        bulletPool[i].transform.position = Rand.Vector3(transform.GetChild(1).position, .15f, .15f, 0f, 0f, 1f, 1f);

                    numberShot++;

                    if (numberShot > 5)
                        numberShot = 0;

                    bulletPool[i].SetActive(true);
                    StartCoroutine(BulletCo());
                    break;
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method | Spawns a bullet and start the coroutine to recharge for the next shot.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void SpawnHeavyBullet()
        {
            for (int i = 0; i < heavyBulletPool.Length; i++)
            {
                if (!heavyBulletPool[i].activeSelf)
                {
                    if ((numberShot % 6) <= 2)
                        heavyBulletPool[i].transform.position = Rand.Vector3(transform.GetChild(0).position, .15f, .15f, 0f, 0f, 1f, 1f);
                    else
                        heavyBulletPool[i].transform.position = Rand.Vector3(transform.GetChild(1).position, .15f, .15f, 0f, 0f, 1f, 1f);

                    numberShot++;

                    if (numberShot > 5)
                        numberShot = 0;

                    heavyBulletPool[i].SetActive(true);
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


        private IEnumerator HeavyBulletPowerUpCoolDown()
        {
            shootHeavy = true;
            yield return new WaitForSeconds(8f);
            shootHeavy = false;
        }


        public void EnableHeavyBullets()
        {
            if (!shootHeavy)
                StartCoroutine(HeavyBulletPowerUpCoolDown());
            else
            {
                StopCoroutine(HeavyBulletPowerUpCoolDown());
                StartCoroutine(HeavyBulletPowerUpCoolDown());
            }
        }


        public void AddToHealth(int value)
        {
            if (health < 3)
            {
                health += value;
                hearts[health - 1].SetActive(true);
            }
        }
    }
}