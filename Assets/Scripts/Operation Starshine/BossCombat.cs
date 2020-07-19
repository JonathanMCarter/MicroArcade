using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Starshine
{
    public class BossCombat : Combat
    {
        // Overrides the base.OnTriggerEnter2D to work for the boss in particular (just saves re-writing the same code considering it uses a lot of the same stuff...)
        private new void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.GetComponent<Damage>())
            {
                Debug.Log("damage script");

                if (!collision.gameObject.GetComponent<Damage>().DamageDelt)
                {
                    if (!collision.gameObject.GetComponent<Damage>().HitCWISDetection)
                    {
                        Debug.Log("damage script + delt");

                        if (!collision.gameObject.layer.ToString().Contains("OP:SS:EM"))
                        {
                            if (!Ship)
                            {
                                Ship = GetComponent<ShipManagement>();
                            }

                            //if (Explosion)
                            //{
                            //    GameObject Go = Instantiate(Explosion, transform.position, transform.rotation);
                            //    Destroy(Go, .5f);
                            //}
                            //else
                            //{
                            //    Explosion = Ship.Ship.Explosion;
                            //    GameObject Go = Instantiate(Explosion, transform.position, transform.rotation);
                            //    Destroy(Go, .5f);
                            //}

                            CSS.ShakeCamera(.01f, .05f);

                            if (GetComponent<BossScript>())
                            {
                                if (GetComponent<BossScript>().ActiveShieldType != collision.gameObject.GetComponent<Damage>().Type)
                                {
                                    // Show DMG indicator
                                    for (int i = 0; i < DI.AmountOfIndicators; i++)
                                    {
                                        //if (!DI.Indicators[i].activeInHierarchy)
                                        //{
                                        //    DI.Indicators[i].GetComponentInChildren<Text>().text = "- " + collision.gameObject.GetComponent<Damage>().DMG;
                                        //    DI.Indicators[i].GetComponentInChildren<Text>().color = Color.grey;
                                        //    DI.Indicators[i].transform.position = collision.gameObject.transform.position;
                                        //    DI.Indicators[i].SetActive(true);
                                        //    break;
                                        //}
                                    }

                                    // reduces the stage health bar by the amount of damage
                                    SC.ReduceStageHealth(collision.gameObject.GetComponent<Damage>().DMG);
                                    GetComponent<BossScript>().Health -= collision.gameObject.GetComponent<Damage>().DMG;
                                }
                                else
                                {
                                    // Show DMG indicator
                                    for (int i = 0; i < DI.AmountOfIndicators; i++)
                                    {
                                        //if (!DI.Indicators[i].activeInHierarchy)
                                        //{
                                        //    DI.Indicators[i].GetComponentInChildren<Text>().text = "- 0";
                                        //    DI.Indicators[i].GetComponentInChildren<Text>().color = Color.cyan;
                                        //    DI.Indicators[i].transform.position = collision.gameObject.transform.position;
                                        //    DI.Indicators[i].SetActive(true);
                                        //    break;
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("no damage script");
            }
        }
    }
}