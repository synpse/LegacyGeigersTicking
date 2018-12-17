using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFPS : Singleton<DisplayFPS>
{
    [Header("Components")]
    public static GUIStyle style = new GUIStyle();

    static float deltaTime = 0.0f;

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        Rect rect = new Rect(5, 2, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 15 / 1000;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms\n{1:0.} fps", msec, fps);
        GUI.Label(rect, text, style);
    }

    void Start()
    {
        style.normal.textColor = new Color(255f, 255f, 255f, 0.0f);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
}
