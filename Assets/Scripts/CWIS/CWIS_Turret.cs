using CarterGames.Assets.AudioManager;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    public class CWIS_Turret : MonoBehaviour
    {
        [SerializeField] private CWIS_Controller.Controller thisTurret;
        [SerializeField] internal GameManager.Ranks thisRank;
        [SerializeField] private ParticleSystem ps;

        private Camera cam;
        private ShipController ship;
        private CWIS_Controller control;

        private bool canShoot;
        private bool hasAmmo;
        private bool isFiring;

        private GameManager gm;

        [SerializeField] internal float timeHeldDown;
        [SerializeField] internal float maxTime;

        [SerializeField] internal int rateOfFire = 0;
        [SerializeField] internal int coolerEff = 0;
        [SerializeField] internal int ammoCap = 0;
        private int lastAmmoCap = 0;
        private int lastCool = 0;


        private AudioManager am;


        private void Start()
        {
            cam = Camera.main;
            control = FindObjectOfType<CWIS_Controller>();
            ship = FindObjectOfType<ShipController>();
            canShoot = true;
            hasAmmo = true;
            gm = FindObjectOfType<GameManager>();
            am = FindObjectOfType<AudioManager>();
        }


        private void Update()
        {
            if (control.activeTurret == thisTurret)
            {
                RotateToMousePos();

                if (timeHeldDown > maxTime)
                {
                    canShoot = false;

                    if (!ps.isPlaying)
                    {
                        ps.Play();
                        am.Play("missilesmoke", .5f);
                    }

                    gm.ReduceScore(1);
                }
                else
                {
                    canShoot = true;

                    if (ps.isPlaying)
                    {
                        ps.Stop();
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    IncrementTimeHeldDown();
                    isFiring = true;

                    if (hasAmmo && canShoot)
                    {
                        RaycastHit hit;
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            if (rateOfFire > 0)
                            {
                                control.ShootBullet(transform.position, (hit.point - transform.position).normalized, thisTurret, (0.05f / rateOfFire));
                            }
                            else
                            {
                                control.ShootBullet(transform.position, (hit.point - transform.position).normalized, thisTurret, 0.075f);
                            }
                        }
                    }
                }
                else
                {
                    if (isFiring)
                    {
                        isFiring = false;
                    }
                }
            }

            if (!isFiring)
            {
                if (timeHeldDown >= 0f)
                {
                    if (coolerEff > 0)
                    {
                        timeHeldDown -= Time.deltaTime / (6 - coolerEff);
                    }
                    else
                    {
                        timeHeldDown -= Time.deltaTime / 6;
                    }
                }
            }

            if (ammoCap > lastAmmoCap)
            {
                switch (thisTurret)
                {
                    case CWIS_Controller.Controller.CWIS1:
                        control.maxAmmo1 += 100;
                        lastAmmoCap = ammoCap;
                        break;
                    case CWIS_Controller.Controller.CWIS2:
                        control.maxAmmo2 += 100;
                        lastAmmoCap = ammoCap;
                        break;
                    default:
                        break;
                }
            }

            if (coolerEff > lastCool)
            {
                maxTime += 1;
                lastCool = coolerEff;
            }
        }


        private void RotateToMousePos()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = target - transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 90f;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }
        }


        private void IncrementTimeHeldDown()
        {
            timeHeldDown += Time.deltaTime;
        }
    }
}