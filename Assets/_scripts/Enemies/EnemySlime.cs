using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : EnemyController
{
    private void FixedUpdate() {
        if(this.isFacingRight == true) {
            GetComponent<Rigidbody2D>().velocity = 
                new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        } else {
            GetComponent<Rigidbody2D>().velocity = 
                new Vector2(maxSpeed * -1, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall") {
            Flip();
        } else if(other.tag == "Enemy") {
            // EnemyController controller = 
            //     other.gameObject.GetComponent<EnemyController>();
            //controller.Flip();
            Flip();
        }
    }
}
