using System.Collections.Generic;
using UnityEngine;

/*
 * 
 *								Audio Manager File (AMF)
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.3.3
 *							  Last Updated: 30/07/2020
 * 
 * 
*/

namespace CarterGames.Assets.AudioManager
{
    [System.Serializable, CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
    public class AudioManagerFile : ScriptableObject
    {
        [SerializeField] public List<string> clipName;
        [SerializeField] public List<AudioClip> audioClip;
        [SerializeField] public GameObject soundPrefab;
        [SerializeField] public bool isPopulated;
        [SerializeField] public bool hasDirectories;
        [SerializeField] public List<string> directory;
    }
}