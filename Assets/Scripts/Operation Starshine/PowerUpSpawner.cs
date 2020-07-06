using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starshine
{
    public class PowerUpSpawner : MonoBehaviour
    {
        public ShipManagement Player1, Player2;

        public GameObject HealthPackGo;
        public GameObject ShieldPackGo;

        public int HealthPackPoolAmount;
        public List<GameObject> HealthPackPool;

        public int ShieldPackPoolAmount;
        public List<GameObject> ShieldPackPool;

        public float XValue, Spd, RotSpd;
        public bool CanSpawnHealth, CanSpawnShield;
        bool CoRunning;


        void Start()
        {
            Player1 = GameObject.FindGameObjectWithTag("WhitePlayer").GetComponent<ShipManagement>();
            Player2 = GameObject.FindGameObjectWithTag("BlackPlayer").GetComponent<ShipManagement>();

            CanSpawnHealth = true;
            CanSpawnShield = true;

            ObjectPool();
        }


        void Update()
        {
            if ((Player1.Health < 50) || (Player2.Health < 50))
            {
                if ((CanSpawnHealth) && (!CoRunning))
                {
                    SpawnHealthPack();
                    StartCoroutine(CoolDown());
                }
            }
            else if ((Player1.Shield < 40) || (Player2.Shield < 40))
            {
                if ((CanSpawnShield) && (!CoRunning))
                {
                    SpawnShieldPack();
                    StartCoroutine(CoolDown());
                }
            }
        }


        void ObjectPool()
        {
            for (int i = 0; i < HealthPackPoolAmount; i++)
            {
                GameObject Go = Instantiate(HealthPackGo);
                Go.SetActive(false);
                HealthPackPool.Add(Go);
            }

            for (int i = 0; i < ShieldPackPoolAmount; i++)
            {
                GameObject Go = Instantiate(ShieldPackGo);
                Go.SetActive(false);
                ShieldPackPool.Add(Go);
            }
        }


        void SpawnHealthPack()
        {
            for (int i = 0; i < HealthPackPoolAmount; i++)
            {
                if (!HealthPackPool[i].activeInHierarchy)
                {
                    HealthPackPool[i].transform.position = new Vector3(ChooseX(), 15, 0);
                    HealthPackPool[i].SetActive(true);
                    HealthPackPool[i].GetComponent<Rigidbody2D>().velocity = Vector2.down * Spd * Time.deltaTime;
                    HealthPackPool[i].transform.Rotate(new Vector3(0, 0, 1 * RotSpd * Time.deltaTime));
                    CanSpawnHealth = true;
                    break;
                }
            }
        }


        void SpawnShieldPack()
        {
            for (int i = 0; i < ShieldPackPoolAmount; i++)
            {
                if (!ShieldPackPool[i].activeInHierarchy)
                {
                    ShieldPackPool[i].transform.position = new Vector3(ChooseX(), 15, 0);
                    ShieldPackPool[i].SetActive(true);
                    ShieldPackPool[i].GetComponent<Rigidbody2D>().velocity = Vector2.down * Spd * Time.deltaTime;
                    ShieldPackPool[i].transform.Rotate(new Vector3(0, 0, 1 * RotSpd * Time.deltaTime));
                    CanSpawnShield = true;
                    break;
                }
            }
        }


        float ChooseX()
        {
            return Random.Range(-XValue, XValue);
        }


        IEnumerator CoolDown(float Delay = 0)
        {
            CoRunning = true;

            if (Delay == 0)
            {
                Delay = Random.Range(10f, 30f);
            }

            yield return new WaitForSeconds(Delay);
            CoRunning = false;
        }
    }
}