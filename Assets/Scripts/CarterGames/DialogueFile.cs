using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue File", menuName = "Dialogue System/Dialogue File")]

public class DialogueFile : ScriptableObject
{
    public List<string> Names;
    public List<string> Dialogue;
}
