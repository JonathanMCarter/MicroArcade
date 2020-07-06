using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using System;
using System.Diagnostics;

/*
 * 
 *								   Build Versions
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 1.0.0
 *							  Last Updated: 18/05/2020
 * 
 *			Script writing inspired by multiple Unity forum threads on the topic. 
 *			                   made my own tweaks with it. 
 * 
*/

namespace CarterGames.EditorTools
{
    public class BuildVersions : EditorWindow
    {
        private Rect deselectWindow;
        private Vector2 scrollPos;

        private int tabOption;
        private bool buildBool;
        private bool debugBool;
        private bool patchBool;
        private bool needUpdating;


        [MenuItem("Tools/Build Versions")]
        public static void ShowWindow()
        {
            GetWindow<BuildVersions>("Build Versions");
        }


        // --- Editor Window ---
        private void OnGUI()
        {
            TabBar();
            SetMinMaxWindowSize();

            if (tabOption == 0)
            {
                ControlsInfo();
                UpdateBooleans();
            }
            else if (tabOption == 1)
            {
                AssetInfo();
            }

            WindowDeselect();
        }


        private void TabBar()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            tabOption = GUILayout.Toolbar(tabOption, new string[] { "Controls", "About Asset" }, GUILayout.MaxWidth(250f), GUILayout.MaxHeight(25f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        private void SetMinMaxWindowSize()
        {
            EditorWindow editorWindow = this;
            editorWindow.minSize = new Vector2(300f, 150f);
            editorWindow.maxSize = new Vector2(300f, 150f);
        }


        private void ControlsInfo()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Choose a version that will be incremented", EditorStyles.boldLabel, GUILayout.Width(277));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.HelpBox("tick the box for the value you wish to increment on your next project build.", MessageType.Info);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("X  .  X  .  X", GUILayout.Width(76.5f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            buildBool = EditorGUILayout.Toggle(buildBool, GUILayout.Width(22.5f));
            debugBool = EditorGUILayout.Toggle(debugBool, GUILayout.Width(22.5f));
            patchBool = EditorGUILayout.Toggle(patchBool, GUILayout.Width(22.5f));

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        private void UpdateBooleans()
        {
            //if (needUpdating)
            //{
                if (buildBool && (!debugBool || !patchBool))
                {
                    debugBool = false;
                    patchBool = false;
                }
                if (debugBool && (!buildBool || !patchBool))
                {
                    buildBool = false;
                    patchBool = false;
                }
                if (patchBool && (!buildBool || !debugBool))
                {
                    buildBool = false;
                    debugBool = false;
                }

                //needUpdating = false;
            //}
        }


        private void AssetInfo()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField("Build Versions", EditorStyles.boldLabel, GUILayout.Width(100));

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 1.0.0", GUILayout.Width(90));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Released: 18/05/2020", GUILayout.Width(130));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Documentation", GUILayout.Width(100)))
            {

            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }


        private void WindowDeselect()
        {
            deselectWindow = new Rect(0, 0, position.width, position.height);

            // Makes it so you can deselect elements in the window by adding a button the size of the window that you can't see under everything
            //make sure the following code is at the very end of OnGUI Function
            if (GUI.Button(deselectWindow, "", GUIStyle.none))
            {
                GUI.FocusControl(null);
            }
        }


        [PostProcessBuild(0)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
            string _currentVersion;
            int[] _versionNumbers = new int[3];

            // Gets the current build version so it can increment it 
            _currentVersion = PlayerSettings.bundleVersion;


            // Sliptting the numbers into something usable...
            _versionNumbers[0] = int.Parse(_currentVersion.Split('.')[0]);
            _versionNumbers[1] = int.Parse(_currentVersion.Split('.')[1]);
            _versionNumbers[2] = int.Parse(_currentVersion.Split('.')[2]) + 1;


            // Updates the bundle version to the new version
            PlayerSettings.bundleVersion = _versionNumbers[0] + "." + _versionNumbers[1] + "." + _versionNumbers[2];
        }
    }
}