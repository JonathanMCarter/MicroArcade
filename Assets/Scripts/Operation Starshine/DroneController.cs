using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class DroneController : MonoBehaviour
    {

        public int NumberofDronesActive;
        bool HasBuffered;
        StageController SC;
        GameManager GM;


        private void OnEnable()
        {
            NumberofDronesActive = CheckActiveDrones();

            if (HasBuffered)
            {
                if (NumberofDronesActive > 0)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (GetComponentsInChildren<Enemies>()[i].HealthBarUI)
                        {
                            GetComponentsInChildren<Enemies>()[i].HealthBarUI.transform.position = new Vector3(transform.GetChild(i).transform.position.x, transform.GetChild(i).transform.position.y + 1.5f, transform.GetChild(i).transform.position.z);
                            GetComponentsInChildren<Enemies>()[i].HealthBarUI.SetActive(true);
                        }
                    }
                }
            }
        }

        private void Start()
        {
            SC = FindObjectOfType<StageController>();
            GM = SC.gameObject.GetComponent<GameManager>();
        }

        private void Update()
        {
            // Checks how many drones are actives
            NumberofDronesActive = CheckActiveDrones();

            if (NumberofDronesActive == 0)
            {
                Debug.Log("Calling");
                SC.NextPhase();
                gameObject.SetActive(false);
            }
        }


        int CheckActiveDrones()
        {
            int Number = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    ++Number;
                }
            }

            return Number;
        }



        private void OnDisable()
        {
            if (!HasBuffered)
            {
                HasBuffered = true;
            }

            if (NumberofDronesActive == 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Enemies>().Health = transform.GetChild(i).GetComponent<Enemies>().Ship.Health[(int)GM.ActiveStage];
                    transform.GetChild(i).GetComponent<Enemies>().Shield = transform.GetChild(i).GetComponent<Enemies>().Ship.Shield[(int)GM.ActiveStage];
                    transform.GetChild(i).GetComponent<Enemies>().ShouldShoot = false;
                    transform.GetChild(i).GetComponent<Enemies>().CanShootMain = true;
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}