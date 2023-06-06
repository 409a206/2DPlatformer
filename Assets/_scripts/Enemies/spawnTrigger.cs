using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrigger : MonoBehaviour
{
    public GameObject[] gameObjects;
    public bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !isTriggered) {
            isTriggered = true;
            foreach(GameObject gameObject in gameObjects) {
                gameObject.SetActive(true);
            }
        }
    } 
}
