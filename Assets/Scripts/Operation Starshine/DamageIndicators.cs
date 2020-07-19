using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class DamageIndicators : MonoBehaviour
    {
        public GameObject IndicatorPrefab;
        public int AmountOfIndicators;
        public List<GameObject> Indicators;

        private void Start()
        {
            ObjPoolSetup();
        }

        void ObjPoolSetup()
        {
            for (int i = 0; i < AmountOfIndicators; i++)
            {
                GameObject Go = Instantiate(IndicatorPrefab);
                Go.transform.SetParent(transform);
                Go.SetActive(false);
                Indicators.Add(Go);
            }
        }
    }
}