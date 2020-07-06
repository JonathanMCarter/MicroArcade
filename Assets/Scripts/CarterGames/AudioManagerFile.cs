using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *								Audio Manager File (AMF)
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.3.1
 *							  Last Updated: 06/05/2020
 * 
 * 
*/

[System.Serializable, CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
public class AudioManagerFile : ScriptableObject
{
    public List<string> clipName;
    public List<AudioClip> audioClip;
    public GameObject soundPrefab;
    public bool isPopulated;
    public string directory;
}