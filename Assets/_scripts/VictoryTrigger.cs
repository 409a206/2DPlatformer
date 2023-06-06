using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public int sceneToLoad;
    public float delay = 1;

    private float timeElapsed;
    private bool isTriggered;

    // Update is called once per frame
    void Update()
    {
        if(isTriggered) {
            timeElapsed = timeElapsed + Time.deltaTime;
        }

        if(timeElapsed >= delay) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            timeElapsed = 0;
            isTriggered = true;

            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            other.GetComponent<Animator>().SetFloat(Constants.animSpeed, 0);
            PlayerPrefs.SetInt(Constants.PREF_COINS, other.GetComponent<PlayerStats>().coinsCollected);
        }
    }
}
