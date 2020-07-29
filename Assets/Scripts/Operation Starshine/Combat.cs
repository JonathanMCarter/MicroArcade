using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class Combat : MonoBehaviour
    {

        public ShipManagement Ship;
        public GameObject Explosion;

        public GameObject DroneExplosion;

        public DamageIndicators DI;
        public  CameraShakeScript CSS;
        internal StageController SC;

        public GameManager GM;
        AudioManager am;
        bool FirstSpawned;

        private void OnEnable()
        {
            if (!CSS) { CSS = FindObjectOfType<CameraShakeScript>(); }
            if (!DI) { DI = FindObjectOfType<DamageIndicators>(); }
            if (!SC) { SC = FindObjectOfType<StageController>(); }
            if (!GM) { GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); }
            if (!am) { am = FindObjectOfType<AudioManager>(); }
            if (!Ship) { Ship = GetComponent<ShipManagement>(); }
        }


        protected virtual void OnDisable()
        {
            if (Ship)
            {
                // Adds to next stage if EM is killed
                if ((Ship.IsEm) && (Ship.Health <= 0) && (Ship.PlayerShip != Ships.CelestialDrone) && (Ship.PlayerShip != Ships.CelestialRocketDrone))
                {
                    if ((SC) && (FirstSpawned))
                    {
                        SC.ReduceStageHealth();
                        SC.NextPhase();
                    }
                    else
                    {
                        FirstSpawned = true;
                    }
                }
                else if ((Ship.IsEm) && (Ship.Health <= 0) && (Ship.PlayerShip == Ships.CelestialDrone))
                {
                    if (SC)
                    {
                        SC.ReduceStageHealth();
                    }
                }
                else if ((Ship.IsEm) && (Ship.Health <= 0) && (Ship.PlayerShip == Ships.CelestialRocketDrone))
                {
                    if (SC)
                    {
                        SC.ReduceStageHealth();
                    }
                }
            }
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Damage Hit = collision.gameObject.GetComponent<Damage>();

            if (Hit)
            {
                if (!Hit.DamageDelt)
                {
                    if ((Ship) && (!Ship.IsAlphaDodging))
                    {
                        if (!Hit.HitCWISDetection)
                        {

                            if (CSS)
                            {
                                CSS.ShakeCamera(.05f, .1f);
                            }
                            else
                            {
                                CSS = FindObjectOfType<CameraShakeScript>();
                                CSS.ShakeCamera(.05f, .1f);
                            }

                            // Remove health from ship
                            if (Ship.Shield > 0)
                            {
                                // Show DMG indicator
                                for (int i = 0; i < DI.AmountOfIndicators; i++)
                                {
                                    if (!DI.Indicators[i].activeInHierarchy)
                                    {
                                        // if correct shield type, do like 0 dmg
                                        if (Ship.ActiveShieldType == Hit.Type)
                                        {
                                            DI.Indicators[i].GetComponentInChildren<Text>().text = "- " + (Hit.DMG / 20);
                                            DI.Indicators[i].GetComponentInChildren<Text>().color = Color.cyan;

                                            if (gameObject.tag.Contains("Player"))
                                            {
                                                if (gameObject.tag.Contains("White"))
                                                {
                                                    GM.Player1Score += Hit.DMG;
                                                }
                                                else if (gameObject.tag.Contains("Black"))
                                                {
                                                    GM.Player2Score += Hit.DMG;
                                                }
                                            }
                                        }
                                        // else it hurts!!! a lot.........
                                        else
                                        {
                                            DI.Indicators[i].GetComponentInChildren<Text>().text = "- " + Hit.DMG;
                                            DI.Indicators[i].GetComponentInChildren<Text>().color = Color.cyan;

                                            if (!gameObject.tag.Contains("Player"))
                                            {
                                                // Score additions
                                                if (Hit.GetPlayerShotFrom() == 1)
                                                {
                                                    GM.Player1Score += Hit.DMG;
                                                    Ship.PlayerStats.damageDelt += Hit.DMG;
                                                }

                                                if (Hit.GetPlayerShotFrom() == 2)
                                                {
                                                    GM.Player2Score += Hit.DMG;
                                                    Ship.PlayerStats.damageDelt += Hit.DMG;
                                                }
                                            }
                                        }

                                        DI.Indicators[i].transform.position = collision.gameObject.transform.position;
                                        DI.Indicators[i].SetActive(true);
                                        break;
                                    }
                                }

                                int Dam;
                                Dam = Ship.Shield - Hit.DMG;

                                // if correct shield type, do like 0 dmg
                                if (Ship.ActiveShieldType == Hit.Type)
                                {
                                    Dam = Ship.Shield - Hit.DMG / 20;
                                    Ship.Shield -= Hit.DMG / 20;
                                }
                                // else it hurts!!! a lot.........
                                else
                                {
                                    Ship.Shield -= Hit.DMG;

                                    if (gameObject.tag.Contains("Player"))
                                    {
                                        Ship.PlayerStats.shieldLost += Hit.DMG;
                                    }
                                }


                                am.Play("ShieldHit", .15f, Random.Range(.75f, 1.15f));
                            }
                            else
                            {
                                // Show DMG indicator
                                for (int i = 0; i < DI.AmountOfIndicators; i++)
                                {
                                    if (!DI.Indicators[i].activeInHierarchy)
                                    {
                                        DI.Indicators[i].GetComponentInChildren<Text>().text = "- " + collision.gameObject.GetComponent<Damage>().DMG;
                                        DI.Indicators[i].GetComponentInChildren<Text>().color = Color.red;
                                        DI.Indicators[i].transform.position = collision.gameObject.transform.position;
                                        DI.Indicators[i].SetActive(true);
                                        break;
                                    }
                                }

                                Ship.Health -= Hit.DMG;

                                if (gameObject.tag.Contains("Player"))
                                {
                                    Ship.PlayerStats.healthLost += Hit.DMG;
                                }

                                if (!Ship.IsEm)
                                {
                                    GameObject _go = Instantiate(Explosion);
                                    _go.transform.position = transform.position;
                                    Destroy(_go, 1f);
                                }
                                else
                                {
                                    GameObject _go = Instantiate(DroneExplosion);
                                    _go.transform.position = transform.position;
                                    Destroy(_go, 1f);
                                }

                                am.Play("MissileImpact", .1f, .75f);

                                // Score additions
                                if (Hit.GetPlayerShotFrom() == 1)
                                {
                                    GM.Player1Score += Hit.DMG;
                                    Ship.PlayerStats.damageDelt += Hit.DMG;
                                }

                                if (Hit.GetPlayerShotFrom() == 2)
                                {
                                    GM.Player2Score += Hit.DMG;
                                    Ship.PlayerStats.damageDelt += Hit.DMG;
                                }
                            }
                        }
                    }
                }
            }
            else
            { 
            // do nothing
            }
        }
    }
}