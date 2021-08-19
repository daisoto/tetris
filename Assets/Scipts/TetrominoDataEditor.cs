using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TetrominoData))]
public class TetrominoDataEditor : Editor
{
    private Vector2Int size = default;

    private bool[,] rawShape = default;

    private void OnEnable()
    {
        Load();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        DrawSize();
        DrawTetrominoData();
        DrawSaveButton();
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

    private void DrawSaveButton()
    {
        if (GUILayout.Button("Save"))
        {
            Save();
        };
    }

    private void Save()
    {
        ((TetrominoData)target).shape = (bool[,])rawShape.Clone();
    }

    private void Load()
    {
        bool[,] originalShape = ((TetrominoData)target).shape;

        if (originalShape == null)
        {
            return;
        }

        size = new Vector2Int(originalShape.GetLength(0), originalShape.GetLength(1));

        rawShape = new bool[size.x, size.y];

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                rawShape[i, j] = originalShape[i, j];
            }
        }
    }
}