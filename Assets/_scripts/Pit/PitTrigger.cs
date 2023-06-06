using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            GameObject trigger = GetNearestActiveCheckpoint();
            if(trigger != null && !other.gameObject.GetComponent<PlayerStats>().isDead) {
                other.transform.position = trigger.transform.position;
            } else {
                Debug.LogError("No valid checkpoint was found!");
            }
        } else {
            Destroy(other.gameObject);
        }
    }

    private GameObject GetNearestActiveCheckpoint()
    {
        GameObject[] checkpoints = 
            GameObject.FindGameObjectsWithTag("Checkpoint");
            GameObject nearestCheckpoint = null;
            float shortestDistance = Mathf.Infinity;

            foreach(GameObject checkpoint in checkpoints) {
                Vector3 checkpointPosition = checkpoint.transform.position;
                float distance = 
                    (checkpointPosition - transform.position).sqrMagnitude;
                
                CheckpointTrigger trigger = 
                    checkpoint.GetComponent<CheckpointTrigger>();
                
                if(distance < shortestDistance && trigger.isTriggered) {
                    nearestCheckpoint = checkpoint;
                    shortestDistance = distance;
                }
            }
        return nearestCheckpoint;
    }
}
