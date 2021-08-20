using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TetrominoData))]
public class TetrominoDataEditor : Editor
{
    private Vector2Int size = default;

    private bool[,] rawShape = default;

    private readonly string targetShapeName = "_shape";

    private void OnEnable()
    {
        Load();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        DrawSize();
        DrawTetrominoData();
        DrawApplyButton();
        DrawCancelButton();
    }

    private void DrawSize()
    {
        size = EditorGUILayout.Vector2IntField("Size", size);
    }

    private void DrawTetrominoData()
    {
        if (rawShape == null)
        {
            rawShape = new bool[size.x, size.y];
        }

        if (size.x < 1 || size.y < 1)
        {
            Array.Clear(rawShape, 0, rawShape.Length);

            return;
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
        SerializedProperty targetShapeProperty = serializedObject.FindProperty(targetShapeName);

        //TODO

        serializedObject.ApplyModifiedProperties();
    }

    private void Load()
    {
        SerializableRectangularArray<bool> targetShape = ((TetrominoData)target).shape;

        if (targetShape == null)
        {
            return;
        }

        size = targetShape.size;

        rawShape = new bool[size.x, size.y];

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                rawShape[i, j] = targetShape[i, j];
            }
        }
    }
}