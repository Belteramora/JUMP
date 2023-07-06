using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class NoteEditorWindow : EditorWindow
{
    private float bpm;

    [MenuItem("NoteEditor/Open Editor")]
    public static void ShowWindow()
    {
        GetWindow<NoteEditorWindow>();
    }


    private void Awake()
    {

    }

    private void OnGUI()
    {
        bpm = EditorGUI.FloatField(new Rect(10, 10, 100, 100), "BMP" , 0);
    }


}

#endif