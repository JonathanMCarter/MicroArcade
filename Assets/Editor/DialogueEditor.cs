using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

/*
 * 
 *							Dialogue Manager Editor Script
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 1.0.0
 *							  Last Updated: 07/07/2019						
 * 
 * 
*/

[CustomEditor(typeof(DialogueScript))]
public class DialogueEditor : Editor
{
    private DialogueScript Script;

    // Overrides the Inspecotr GUI for the Dialogue Script
    public override void OnInspectorGUI()
	{
        var Script = target as DialogueScript;

        CGHeader();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

		if (GUILayout.Button("Open Editor"))
		{
            DialogueEditorWindow.ShowWindow();
        }

		if (GUILayout.Button("Documentation"))
		{
            //Application.OpenURL("");
		}

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("File Active: ", GUILayout.Width(65));
        Script.File = (DialogueFile)EditorGUILayout.ObjectField(Script.File, typeof(DialogueFile), false);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name Txt: ", GUILayout.Width(80));
        Script.NameTxt = (Text)EditorGUILayout.ObjectField(Script.NameTxt, typeof(Text), false);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dialogue Txt: ", GUILayout.Width(80));
        Script.DialTxt = (Text)EditorGUILayout.ObjectField(Script.DialTxt, typeof(Text), false);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Display Mode: ", GUILayout.Width(85));
        Script.DisplayStyle = (Styles)EditorGUILayout.EnumPopup(Script.DisplayStyle, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
	}

    void CGHeader()
    {
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
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

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Dialouge System | Version: 1.0", GUILayout.Width(185f));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
