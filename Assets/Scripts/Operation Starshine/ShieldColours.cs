using UnityEngine;

namespace Starshine
{
    [CreateAssetMenu(fileName = "Shield Colours", menuName = "Arcade/Starshine/Shield Colours")]
    public class ShieldColours : ScriptableObject
    {
        [ColorUsage(true, true)]
        public Color BoltShieldColour;
        [ColorUsage(true, true)]
        public Color MissileShieldColour;
        [ColorUsage(true, true)]
        public Color RegenShieldColour;
        [ColorUsage(true, true)]
        public Color32 DownShieldColour;
    }
}