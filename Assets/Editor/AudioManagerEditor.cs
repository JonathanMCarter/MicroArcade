using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.CodeDom;

/*
 * 
 *							Audio Manager Editor Script
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.3.1
 *							  Last Updated: 06/05/2020						
 * 
 * 
*/

[CustomEditor(typeof(AudioManager)), CanEditMultipleObjects]
public class AudioManagerEditor : Editor
{
	// Colours for the Editor Buttons
	private Color32 scanCol = new Color32(41, 176, 97, 255);
	private Color32 scannedCol = new Color32(189, 191, 60, 255);
	private Color32 redCol = new Color32(190, 42, 42, 255);

	private string scanButtonString;	// String for the value of the scan button text
    private bool shouldShowMessage;

	private List<AudioClip> audioList;	// List of Audioclips used to add the audio to the library in the Audio Manager Script
	private List<string> audioStrings;	// List of Strings used to add the names of the audio clips to the library in the Audio Manager Script

	private AudioManager audioManagerScript;        // Reference to the Audio Manager Script that this script overrides the inspector for


    // When the script first enables - do this stuff!
    void OnEnable()
	{
		// References the Audio Manager Script
		audioManagerScript = (AudioManager)target;

		// Adds an Audio Source to the gameobject this script is on if its not already there (used for previewing audio only) 
		// * Hide flags hides it from the inspector so you don't notice it there *
		if (audioManagerScript.gameObject.GetComponent<AudioSource>())
		{
			audioManagerScript.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
			audioManagerScript.GetComponent<AudioSource>().playOnAwake = false;
		}
		else
		{
			audioManagerScript.gameObject.AddComponent<AudioSource>();
			audioManagerScript.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
			audioManagerScript.GetComponent<AudioSource>().playOnAwake = false;
		}

        // Init Setup if needed (makes an audio folder and audio manager file for starts
        FirstSetup();
	}


    // Overrides the Inspector of the Audio Manager Script with this stuff...
    public override void OnInspectorGUI()
    {
        // New in V:2.3 - trying to fix erasing of data errors by making sure the script holds all the data
        serializedObject.Update();

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();


        // Shows either the Carter Games Logo or an alternative for if the icon is deleted when you import the package
        if (Resources.Load<Texture2D>("CarterGames/Logo"))
        {
            if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Logo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
            {
                GUI.FocusControl(null);
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Carter Games", EditorStyles.boldLabel, GUILayout.Width(100));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        // Label that shows the name of the script / tool & the Version number for reference sake.
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Audio Manager | Version: 2.3.1", GUILayout.Width(185f));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Documentation"))
        {
            Application.OpenURL("http://carter.games/audiomanager/");
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();



        GUILayout.Space(10);


        EditorGUILayout.BeginHorizontal();
        SerializedProperty fileProp = serializedObject.FindProperty("audioManagerFile");
        EditorGUILayout.PropertyField(fileProp, new GUIContent("File In Use: "));
        EditorGUILayout.EndHorizontal();



        if (audioManagerScript.audioManagerFile)
        {
            if (!audioManagerScript.lastAudioManagerFile)
            {
                audioManagerScript.lastAudioManagerFile = audioManagerScript.audioManagerFile;

                // Assigns the directory if there is one in the SO, this can still be edited by the user
                if ((audioManagerScript.audioManagerFile.directory != audioManagerScript.directory))
                {
                    audioManagerScript.directory = audioManagerScript.audioManagerFile.directory;
                }
            }
            else if (audioManagerScript.lastAudioManagerFile != audioManagerScript.audioManagerFile)
            {
                audioManagerScript.lastAudioManagerFile = audioManagerScript.audioManagerFile;
                audioManagerScript.directory = audioManagerScript.audioManagerFile.directory;
                audioManagerScript.UpdateLibrary();
            }


            EditorGUILayout.BeginHorizontal();

            SerializedProperty prefabProp = serializedObject.FindProperty("soundPrefab");
            EditorGUILayout.PropertyField(prefabProp, new GUIContent("Prefab: "));

            // Saves the selection into the SO for future use... in theory
            if (audioManagerScript.soundPrefab)
            {
                audioManagerScript.audioManagerFile.soundPrefab = audioManagerScript.soundPrefab;
            }

            EditorGUILayout.EndHorizontal();

            // Directory Field
            EditorGUILayout.BeginHorizontal();

            SerializedProperty dirProp = serializedObject.FindProperty("directory");
            EditorGUILayout.PropertyField(dirProp, new GUIContent("Path: "));

            EditorGUILayout.EndHorizontal();


            // Checks to see if the Audio Manager Library is not empty
            // * If its not empty then you've pressed scan before, therefore it won't show the scan button again *
            if ((audioManagerScript.audioManagerFile) && (audioManagerScript.audioManagerFile.isPopulated))
            {
                serializedObject.FindProperty("hasScannedOnce").boolValue = true;
            }

            // Assigns the sound prefab if there is one in the SO, this can still be edited by the user
            if ((audioManagerScript.audioManagerFile) && (audioManagerScript.audioManagerFile.soundPrefab))
            {
                audioManagerScript.soundPrefab = audioManagerScript.audioManagerFile.soundPrefab;
            }

            GUILayout.Space(10);

            // Changes the text & colour of the first button based on if you've pressed it before or not
            if (serializedObject.FindProperty("hasScannedOnce").boolValue) { scanButtonString = "Re-Scan"; GUI.color = scannedCol; }
            else { scanButtonString = "Scan"; GUI.color = scanCol; }


            // Begins a grouping for the buttons to stay on one line
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // The actual Scan button - Runs functions to get the audio from the directory and add it to the library on the Audio Manager Script
            if (GUILayout.Button(scanButtonString, GUILayout.Width(80)))
            {
                if (audioManagerScript.audioManagerFile)
                {
                    audioManagerScript.audioManagerFile.isPopulated = false;

                    // Init Lists
                    audioList = new List<AudioClip>();
                    audioStrings = new List<string>();

                    // Auto filling the lists 
                    AddAudioClips();
                    AddStrings();

                    // Updates the lists
                    audioManagerScript.audioManagerFile.clipName = audioStrings;
                    audioManagerScript.audioManagerFile.audioClip = audioList;

                    audioManagerScript.UpdateLibrary();

                    EditorUtility.SetDirty(audioManagerScript.audioManagerFile);
                }
                else
                {
                    Debug.LogAssertion("(*Audio Manager*): Please select a Audio Manager audioManagerFile before scanning, I can't scan without somewhere to put it all :)");
                }
            }

            // Resets the color of the GUI
            GUI.color = Color.white;


            // The actual Clear button - This just clear the Lists and Library in the Audio Manager Script and resets the Has Scanned bool for the Scan button reverts to green and "Scan"
            if (GUILayout.Button("Clear", GUILayout.Width(60)))
            {
                if (audioManagerScript.audioManagerFile)
                {
                    audioManagerScript.soundLibrary.Clear();
                    audioManagerScript.audioManagerFile.clipName.Clear();
                    audioManagerScript.audioManagerFile.audioClip.Clear();
                    audioManagerScript.audioManagerFile.isPopulated = false;
                    serializedObject.FindProperty("hasScannedOnce").boolValue = false;
                    shouldShowMessage = false;
                }
                else
                {
                    Debug.Log("(*Audio Manager*): No Audio Manager (Audio Manager File) selected, ignoring request.");
                }
            }

            GUI.color = Color.white;


            // Ends the grouping for the buttons
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("Please assign a Audio Manager File to use this asset.", MessageType.Info, true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }




        // *** Labels ***
        if (audioManagerScript.audioManagerFile)
        {
            if ((audioManagerScript.audioManagerFile.isPopulated))
            {
                if (audioManagerScript.soundLibrary.Count > 0 && audioManagerScript.soundLibrary.Count == CheckAmount())
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Items Scanned:", EditorStyles.boldLabel, GUILayout.Width(105f));
                    EditorGUILayout.LabelField(audioManagerScript.audioManagerFile.audioClip.Count.ToString());
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
                else if (audioManagerScript.soundLibrary.Count > 0 && audioManagerScript.soundLibrary.Count != CheckAmount())
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("There are un-saved changes, press 're-scan' to update", MessageType.Info, true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
                else if (audioManagerScript.audioManagerFile.audioClip.Count != CheckAmount())
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("There are un-saved changes, press 're-scan' to update.", MessageType.Info, true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
                else if (CheckAmount() == 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("No clips found in: " + "assets/audio/" + audioManagerScript.directory, MessageType.Info, true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
                else
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Items Scanned:", EditorStyles.boldLabel, GUILayout.Width(105f));
                    EditorGUILayout.LabelField(audioManagerScript.audioManagerFile.audioClip.Count.ToString());
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }
            else if (!audioManagerScript.audioManagerFile.isPopulated && !shouldShowMessage)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Press Scan to Populate!", MessageType.Info, true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            else if (shouldShowMessage)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("There was a problem scanning, please check the console for warnings/errors", MessageType.Warning, true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
        }

        if (serializedObject.FindProperty("hasScannedOnce").boolValue)
        {
            DisplayNames();
        }


        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }


    /// <summary>
    /// Runs the Init setup for the manager if needed....
    /// </summary>
    void FirstSetup()
    {
        // Makes the audio directoy if it doesn't exist in your project
        // * This will not create a new folder if you already have an audio folder *
        // * As of V2 it will also create a new Audio Manager audioManagerFile if there isn't one in the audio folder *
        if (!Directory.Exists(Application.dataPath + "/Audio"))
        {
            AssetDatabase.CreateFolder("Assets", "Audio");

            if (!Directory.Exists(Application.dataPath + "/Audio"))
            {
                AssetDatabase.CreateFolder("Assets/Audio", "Files");
            }

            AssetDatabase.CreateFolder("Assets/Audio", "Files");

            AudioManagerFile _newAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
            AssetDatabase.CreateAsset(_newAMF, "Assets/Audio/Files/Audio Manager audioManagerFile.asset");
            audioManagerScript.audioManagerFile = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager audioManagerFile.asset", typeof(AudioManagerFile));
        }
        else if (((Directory.Exists(Application.dataPath + "/Audio")) && (!Directory.Exists(Application.dataPath + "/Audio/Files"))))
        {
            AssetDatabase.CreateFolder("Assets/Audio", "Files");
            AudioManagerFile _newAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
            AssetDatabase.CreateAsset(_newAMF, "Assets/Audio/Files/Audio Manager audioManagerFile.asset");
            audioManagerScript.audioManagerFile = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager audioManagerFile.asset", typeof(AudioManagerFile));
        }
    }



	// Adds all the audio clips to the list
	void AddAudioClips()
	{
        // Makes a new lsit the size of the amount of objects in the path
        // In V:2.3 - Updated to allow for custom folders the hold audio clips (so users can organise their clips and still use the manager, default will just get all sounds in "/audio")
        List<string> _allFiles;

        if (audioManagerScript.directory == "")
        {
            _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));
        }
        else
        {
            if (Directory.Exists(Application.dataPath + "/audio/" + audioManagerScript.directory))
            {
                _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio/" + audioManagerScript.directory));
            }
            else
            {
                // !Warning Message - shown in the console should there not be a directory named what the user entered
                _allFiles = new List<string>();
                Debug.LogWarning("(*Audio Manager*): Path does not exist! please make sure you spelt your path correctly: " + Application.dataPath + "/audio/" + audioManagerScript.directory);
                shouldShowMessage = true;
            }
        }


		// Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
		if (_allFiles.Any())
        {
            serializedObject.FindProperty("hasScannedOnce").boolValue = true;  // Sets the has scanned once to true so the scan button turns into the re-scan button

            AudioClip _source;

			foreach (string _thingy in _allFiles)
			{
				string _path = "Assets" + _thingy.Replace(Application.dataPath, "").Replace('\\', '/');

				if (AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip)))
				{
					_source = (AudioClip)AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip));
					audioList.Add(_source);
				}
			}

            audioManagerScript.audioManagerFile.isPopulated = true;
        }
		else
		{
			// !Warning Message - shown in the console should there be no audio in the directory been scanned
			Debug.LogWarning("(*Audio Manager*): Please ensure there are Audio files in the directory: " + Application.dataPath + "/Audio");
		}
	}



    int CheckAmount()
    {
        int _amount = 0;
        List<string> _allFiles;

        if (audioManagerScript.directory == "")
        {
            _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));
        }
        else
        {
            if (Directory.Exists(Application.dataPath + "/audio/" + audioManagerScript.directory))
            {
                _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio/" + audioManagerScript.directory));
            }
            else
            {
                // !Warning Message - shown in the console should there not be a directory named what the user entered
                _allFiles = new List<string>();
                shouldShowMessage = true;
            }
        }

        // Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
        if (_allFiles.Any())
        {
            serializedObject.FindProperty("hasScannedOnce").boolValue = true;  // Sets the has scanned once to true so the scan button turns into the re-scan button

            foreach (string Thingy in _allFiles)
            {
                string _path = "Assets" + Thingy.Replace(Application.dataPath, "").Replace('\\', '/');

                if (AssetDatabase.LoadAssetAtPath(_path, typeof(AudioClip)))
                {
                    ++_amount;
                }
            }

            audioManagerScript.audioManagerFile.isPopulated = true;
        }

        return _amount;
    }


	// Adds all the strings for the clip names to its list
	void AddStrings()
	{
		for (int i = 0; i < audioList.Count; i++)
		{
			if (audioList[i] == null)
			{
				audioList.Remove(audioList[i]);
			}
		}

		int _ignored = 0;

		for (int i = 0; i < audioList.Count; i++)
		{
			if (audioList[i].ToString().Contains("(UnityEngine.AudioClip)"))
			{
				audioStrings.Add(audioList[i].ToString().Replace(" (UnityEngine.AudioClip)", ""));
			}
			else
			{
				_ignored++;
			}
		}

		if (_ignored > 0)
		{
			// This message should never show up, but its here just incase
			Debug.LogAssertion("(*Audio Manager*): " + _ignored + " entries ignored, this is due to the files either been in sub directories or other files that are not Unity AudioClips.");
		}
	}


	// Returns the number of files in the audio directory
	int CheckNumberOfFiles()
	{
		int _finalCount = 0;
        List<string> _allFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));

        foreach (string _thingy in _allFiles)
        {
            if ((_thingy.Contains(".aif")) || (_thingy.Contains(".mp3")) || (_thingy.Contains(".wav")) || (_thingy.Contains(".ogg")))
            {
                _finalCount++;
            }
            else
            {
                // Ignore Thingy
            }
        }

		// divides the final result by 2 as it includes the .meta files in this count which we don't use
		return _finalCount / 2;
	}



	// Displayes all the audio clips with the name and a button to preview said clips
	void DisplayNames()
	{
        // Used as a placeholder for the clip name each loop
        string _elements;

        // Going through all the audio clips and making an element in the Inspector for them
        if (audioManagerScript.audioManagerFile && audioManagerScript.audioManagerFile.clipName.Any())
        {
            for (int i = 0; i < audioManagerScript.audioManagerFile.clipName.Count; i++)
            {
                _elements = audioManagerScript.audioManagerFile.clipName[i];

                // Starts the ordering
                EditorGUILayout.BeginHorizontal();

                // Changes the GUI colour to green for the buttons
                GUI.color = scanCol;

                // If there are no clips playing it will show "preview clip" buttons for all elements
                if (!audioManagerScript.GetComponent<AudioSource>().isPlaying)
                {
                    if (Resources.Load<Texture2D>("CarterGames/Play"))
                    {
                        if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                            audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                            audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                        }
                    }
                }
                // if a clip is playing, the clip that is playing will have a "stop clip" button instead of "preview clip" 
                else if (audioManagerScript.GetComponent<AudioSource>().clip == audioManagerScript.audioManagerFile.audioClip[i])
                {
                    GUI.color = redCol;

                    if (Resources.Load<Texture2D>("CarterGames/Stop"))
                    {
                        if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().Stop();
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("S", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().Stop();
                        }
                    }
                }
                // This just ensures the rest of the elements keep a button next to them
                else
                {
                    if (Resources.Load<Texture2D>("CarterGames/Play"))
                    {
                        if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                            audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            audioManagerScript.GetComponent<AudioSource>().clip = audioManagerScript.audioManagerFile.audioClip[i];
                            audioManagerScript.GetComponent<AudioSource>().PlayOneShot(audioManagerScript.GetComponent<AudioSource>().clip);
                        }
                    }
                }

                // Resets the GUI colour
                GUI.color = Color.white;

                // Adds the text for the clip
                EditorGUILayout.TextArea(_elements, GUILayout.ExpandWidth(true));

                // Ends the GUI ordering
                EditorGUILayout.EndHorizontal();
            }
        }
	}
}
