using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class RocketDroneAI : MonoBehaviour
    {
        public int NumberShot;
        //public int Amount;
        //public List<GameObject> RocketsPool;
        //public GameObject RocketPrefab;
        public GameObject Target;
        public GameObject[] Players;
        public GameObject[] Launchers;

        public EnemyWeapons EmWeapons;

        GameManager GM;
        AudioManager AM;

        void Start()
        {
            GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            AM = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueScript>().AM;

            EmWeapons = GameObject.FindGameObjectWithTag("StarshineEnemyWeapons").GetComponent<EnemyWeapons>();

            Players = new GameObject[2];

            Players[0] = GameObject.FindGameObjectWithTag("WhitePlayer");
            Players[1] = GameObject.FindGameObjectWithTag("BlackPlayer");

            Target = ChooseTarget();
            NumberShot = 0;
        }


        void LateUpdate()
        {
            Debug.Log("this is running!!!!");
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (Target.transform.position - transform.position).normalized);
        }



        GameObject ChooseTarget()
        {
            // if both players are alive
            if ((Players[0]) && (Players[1]) && (Players[0].activeInHierarchy) && (Players[1].activeInHierarchy))
            {
                float Player1Distance = Vector2.Distance(Players[0].transform.position, transform.position);
                float Player2Distance = Vector2.Distance(Players[1].transform.position, transform.position);

                if (Player1Distance < Player2Distance)
                {
                    return Players[0];
                }
                else
                {
                    return Players[1];
                }
            }
            else if ((Players[0]) && (Players[1]) && (Players[0].activeInHierarchy) && (!Players[1].activeInHierarchy))
            {
                return Players[0];
            }
            else if ((Players[0]) && (Players[1]) && (!Players[0].activeInHierarchy) && (Players[1].activeInHierarchy))
            {
                return Players[1];
            }
            // if only 1 player is alive
            else if ((Players[0]) && (Players[0].activeInHierarchy) && (!Players[1]))
            {
                return Players[0];
            }
            // if only 2 player is alive
            else if ((Players[1]) && (!Players[0]) && (Players[1].activeInHierarchy))
            {
                return Players[1];
            }
            else
            {
                //ShouldShoot = false;
                return null;
            }
        }


        public void FireMissile()
        {
            GameObject newMissile = EmWeapons.GetMissile();

            newMissile.GetComponent<MissileScript>().isRocketAngel = false;
            Target = ChooseTarget();

            if (NumberShot % 2 == 0)
            {
                newMissile.transform.position = Launchers[0].transform.position;
                newMissile.transform.rotation = Launchers[0].transform.rotation;
                NumberShot++;
            }
            else
            {
                newMissile.transform.position = Launchers[1].transform.position;
                newMissile.transform.rotation = Launchers[1].transform.rotation;
                NumberShot++;
            }

            //newMissile.GetComponent<Rigidbody2D>().velocity = (Target.transform.position - transform.position).normalized * 10f * Time.deltaTime;
            //newMissile.transform.rotation = Quaternion.LookRotation(Vector3.forward, -(Target.transform.position - transform.position).normalized);
            newMissile.GetComponent<Damage>().DMG = (int)GetComponent<ShipManagement>().Ship.MainWeapon.Damage[(int)GM.ActiveStage];
            newMissile.SetActive(true);
            AM.PlayFromTime("MissileShoot", .25f, .5f, 2f);
        }
    }
}