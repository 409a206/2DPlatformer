using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenScript : MonoBehaviour
{
    public GUISkin Skin;

    private void Update() {
        if(Input.anyKeyDown) {
            Application.LoadLevel(Constants.SCENE_LEVEL_1);
        }
    }

    private void OnGUI() {
        //사용할 스킨을 선택한다
        GUI.skin = Skin;
        GUILayout.BeginArea(new Rect(300, 480, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.Label("Press Any Key To Begin", GUILayout.ExpandWidth(true));
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
