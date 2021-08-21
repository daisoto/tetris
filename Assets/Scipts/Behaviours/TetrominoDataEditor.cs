using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TetrominoData))]
public class TetrominoDataEditor : Editor
{
    private bool[,] rawShape = default;
    
    private readonly string targetShapeName = "_shape";
    private readonly string targetShapeValuesName = "_values";
    private readonly string targetShapeSizeName = "_size";

    private Vector2Int size = default;

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
        SerializedProperty targetShape = serializedObject.FindProperty(targetShapeName);

        SerializedProperty targetShapeSize = targetShape.FindPropertyRelative(targetShapeSizeName);
        SerializedProperty targetShapeValues = targetShape.FindPropertyRelative(targetShapeValuesName);

        targetShapeSize.vector2IntValue = size;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int index = size.y * i + j;

                if (targetShapeValues.arraySize < index + 1)
                {
                    targetShapeValues.InsertArrayElementAtIndex(index);
                }

                targetShapeValues.GetArrayElementAtIndex(index).boolValue = rawShape[i, j];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void Load()
    {
        SerializableRectangularArray<bool> targetShape = ((TetrominoData)target).shape;

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