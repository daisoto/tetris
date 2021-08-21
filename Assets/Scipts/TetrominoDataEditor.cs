﻿using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TetrominoData))]
public class TetrominoDataEditor : Editor
{
    private bool[,] rawShape = default;

    private readonly string targetArrayName = "editorArray";

    private readonly Vector2Int size = new Vector2Int(4, 4);

    private void OnEnable()
    {
        Load();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawTetrominoData();
        DrawApplyButton();
        DrawCancelButton();
    }

    private void DrawTetrominoData()
    {
        if (rawShape == null)
        {
            rawShape = new bool[size.x, size.y];
        }

        if (rawShape.GetLength(0) != size.x || rawShape.GetLength(1) != size.y)
        {
            Array.Clear(rawShape, 0, rawShape.Length);

            rawShape = new bool[size.x, size.y];
        }

        for (int i = 0; i < size.x; i++)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(40));

            for (int j = 0; j < size.y; j++)
            {
                rawShape[i, j] = EditorGUILayout.Toggle(rawShape[i, j]);
            }

            EditorGUILayout.EndHorizontal();
        }

        //SerializedProperty targetArrayProperty = serializedObject.FindProperty(targetArrayName);

        //for (int i = 0; i < size.x; i++)
        //{
        //    EditorGUILayout.BeginHorizontal(GUILayout.Width(40));

        //    for (int j = 0; j < size.y; j++)
        //    {
        //        EditorGUILayout.PropertyField(targetArrayProperty.GetArrayElementAtIndex(size.y * i + j), GUIContent.none);
        //    }

        //    EditorGUILayout.EndHorizontal();
        //}
    }

    private void DrawApplyButton()
    {
        if (GUILayout.Button("Apply"))
        {
            Apply();
        };
    }

    private void DrawCancelButton()
    {
        if (GUILayout.Button("Cancel"))
        {
            Load();
        };
    }

    private void Apply()
    {
        SerializedProperty targetArray = serializedObject.FindProperty(targetArrayName);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                targetArray.GetArrayElementAtIndex(size.y * i + j).boolValue = rawShape[i, j];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void Load()
    {
        //SerializableRectangularArray<bool> targetShape = ((TetrominoData)target).shape;
        SerializedProperty targetArray = serializedObject.FindProperty(targetArrayName);

        if (targetArray.arraySize == 0)
        {
            return;
        }

        rawShape = new bool[size.x, size.y];

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                rawShape[i, j] = targetArray.GetArrayElementAtIndex(size.y * i + j).boolValue;
            }
        }
    }
}