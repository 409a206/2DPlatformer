using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPoints : MonoBehaviour
{
    public GameObject waypointA;
    public GameObject waypointB;
    public float speed = 1;
    private bool directionAB = true;
    private bool shouldChangeFacing = true;

    private void FixedUpdate() {
        if(transform.position == waypointA.transform.position && directionAB == false 
        || transform.position == waypointB.transform.position && directionAB == true) {
            directionAB = !directionAB;
            if(shouldChangeFacing) {
                GetComponent<EnemyController>().Flip();
            }
        }

        if(directionAB == true) {
            transform.position = 
                Vector3.MoveTowards(transform.position, waypointB.transform.position, speed * Time.fixedDeltaTime);
        } else {
            transform.position = 
                Vector3.MoveTowards(transform.position, waypointA.transform.position, speed * Time.fixedDeltaTime);
        }
    }
}
