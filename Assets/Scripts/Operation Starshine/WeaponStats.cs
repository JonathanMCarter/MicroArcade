using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarterGames.Starshine
{
    [CreateAssetMenu(fileName = "Weapon Stats SO", menuName = "Arcade/Starshine/Weapon Stats")]
    public class WeaponStats : ScriptableObject
    {
        public float[] Damage = new float[3];
        public float Delay;
        public int PoolAmountNeeded;
        public GameObject Prefab;
    }
}