using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenDelayed : MonoBehaviour
{
   public float delayTime = 5f;

   private void Start() {
       StartCoroutine("Delay");
   }

   IEnumerator Delay() {
       yield return new WaitForSeconds(delayTime);
       Application.LoadLevel(Constants.SCENE_TITLE);
       Debug.Log("Time's up");
   }

   private void Update() {
       if(Input.anyKeyDown) {
           Application.LoadLevel(Constants.SCENE_TITLE);
           Debug.Log("A key or mouse click has been detected");
       }
   }
}
