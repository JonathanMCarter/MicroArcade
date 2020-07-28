using UnityEngine;
using UnityEditor;
using CarterGames.Crushing.Saving;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GameManager gM = (GameManager)target;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Create Save File"))
            {
                SaveManager.Init();
            }

            if (GUILayout.Button("Purge Save File"))
            {
                SaveManager.DeleteSaveFile();
            }

            if (GUILayout.Button("Save Game"))
            {
                SaveManager.SaveGame(gM.saveData);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            base.OnInspectorGUI();
        }
    }
}