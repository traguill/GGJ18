using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor: Editor
{
    #region Member Variables
    private SerializedObject gridMapSO;
    private Grid gridMap;

    bool isShowing;

    int width;
    int height;
    #endregion

    #region Member Properties
    #endregion

    #region Monobehaviour Methods
    void OnEnable()
    {
        gridMapSO = new SerializedObject(target);
        gridMap = (Grid)target;
        width = gridMap.grid_int.GetLength(0);
        height = gridMap.grid_int.GetLength(1);
    }

    public override void OnInspectorGUI()
    {
        gridMapSO.Update();

        base.OnInspectorGUI();

        isShowing = EditorGUILayout.Foldout(isShowing, "Real Solution");
        if (isShowing)
        {

            for (int i = 0; i < gridMap.grid_int.GetLength(1); ++i)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < gridMap.grid_int.GetLength(0); j++)
                {
                    gridMap.grid_int[j, i] = EditorGUILayout.IntField(gridMap.grid_int[j, i], GUILayout.Width(20));
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.LabelField("WIDTH");
        width = EditorGUILayout.IntField(width);
        EditorGUILayout.LabelField("HEIGHT");
        height = EditorGUILayout.IntField(height);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            EditorUtility.SetDirty(gridMap);
            int[,] aa = gridMap.grid_int;

            gridMap.grid_int = new int[width, height];
            width = gridMap.grid_int.GetLength(0);
            height = gridMap.grid_int.GetLength(1);

            for (int i = 0; i < aa.GetLength(0); i++)
            {
                for (int j = 0; j < aa.GetLength(1); j++)
                {
                    gridMap.grid_int[i, j] = aa[i, j];
                }
            }
        }

        gridMapSO.ApplyModifiedProperties();
    }
    #endregion
}

