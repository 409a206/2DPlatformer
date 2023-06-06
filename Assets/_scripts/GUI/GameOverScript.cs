using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GUISkin Skin;
    public float gapSize = 20f;

    private void OnGUI() {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect((Screen.height / 2)
                                    - Screen.height / 4, (Screen.width/2) - Screen.width/4 ,
                                    Screen.height, Screen.width));
        GUILayout.BeginVertical();
        GUILayout.Label("Game Over");
        GUILayout.Space(gapSize);

        //첫번째 버튼을 만든다. 이 버튼이 눌러 지면 현재 레벨을 다시 로드한다
        if(GUILayout.Button("Retry!")) {
            //제 로드 로직
            Application.LoadLevel(PlayerPrefs.GetInt(Constants.PREF_CURRENT_LEVEL));
        }

        GUILayout.Space(gapSize);

        //두 번째 버튼을 만든다. 이 버튼이 눌러지면 게임을 재 시작한다
        if(GUILayout.Button("Restart!")) {
            //제 시작 로직
            Application.LoadLevel(Constants.SCENE_LEVEL_1);
        }

        #if UNITY_STANDALONE
            if(GUILayout.Button("Quit")) {
                Application.Quit();
            }
        #endif

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
