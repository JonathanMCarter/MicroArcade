using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    public class OrbScript : MonoBehaviour
    {
        void Start()
        {
            int Number = Random.Range(0, 4);
            transform.GetChild(Number).gameObject.SetActive(true);
        }
    }
}