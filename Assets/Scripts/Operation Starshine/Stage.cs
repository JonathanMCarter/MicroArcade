using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starshine
{
    [CreateAssetMenu(fileName = "Stage SO", menuName = "Arcade/Starshine/GameStage")]
    public class Stage : ScriptableObject
    {
        public string StageTitle;
        public Color32 StageHealthBarColour;
        public int MaxHealth;
        public List<string> Phases;
    }
}