using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pinball
{
    [CustomEditor(typeof(GameManager))]
    public class GM_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            GameManager Script = (GameManager)target;

            GUILayout.Space(15f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Stats Structs");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Player 1 Stats:");

            // Player 1 Health
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Health: ", GUILayout.MaxWidth(50));
            Script.Player1Stats.Health = EditorGUILayout.IntField(Script.Player1Stats.Health);

            EditorGUILayout.EndHorizontal();


            // Player 1 Score
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Score: ", GUILayout.MaxWidth(50));
            Script.Player1Stats.Score = EditorGUILayout.IntField(Script.Player1Stats.Score);

            EditorGUILayout.EndHorizontal();


            GUILayout.Space(10f);
            EditorGUILayout.LabelField("Player 2 Stats:");

            // Player 2 Health
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Health: ", GUILayout.MaxWidth(50));
            Script.Player2Stats.Health = EditorGUILayout.IntField(Script.Player2Stats.Health);

            EditorGUILayout.EndHorizontal();


            // Player 2 Score
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Score: ", GUILayout.MaxWidth(50));
            Script.Player2Stats.Score = EditorGUILayout.IntField(Script.Player2Stats.Score);

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(25f);

            Repaint();

            base.OnInspectorGUI();
        }

    }
}