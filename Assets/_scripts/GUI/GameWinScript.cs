using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinScript : MonoBehaviour
{
    public GUISkin Skin;
    public float gapSize = 20f;

    private void OnGUI() {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect((Screen.height / 2) - Screen.height/4, (Screen.width /2) - Screen.width / 4, Screen.height, Screen.width));
        GUILayout.BeginVertical();
        GUILayout.Label("You Won!");
        GUILayout.Space(gapSize);

        if(GUILayout.Button("Restart!")) {
            Application.LoadLevel(Constants.SCENE_LEVEL_1);
        }

        GUILayout.Space(gapSize);

        #if UNITY_STANDALONE
            if(GUILayout.Button("Quit")) {
                Application.Quit();
            }
        #endif
        GUILayout.EndVertical();
        GUILayout.EndArea();

    }
}
