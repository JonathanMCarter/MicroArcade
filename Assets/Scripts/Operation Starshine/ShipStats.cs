using UnityEngine;

namespace CarterGames.Starshine
{
    [CreateAssetMenu(fileName = "Ship Stats SO", menuName = "Arcade/Starshine/Ship Stats")]
    public class ShipStats : ScriptableObject
    {
        public int[] Health = new int[3];
        public int[] Shield = new int[3];
        public WeaponStats MainWeapon;
        public GameObject MainWeaponPrefab;
        public WeaponStats AltWeapon;
        public GameObject AltWeaponPrefab;
        public Color32 HealthBarFillColour;
        public float ShipSpd;
        public GameObject ShipPrefab;
        public GameObject Explosion;
    }
}