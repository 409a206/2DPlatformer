using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
   public int damage = 1;
   public bool playHitReaction = false;

   private void OnTriggerEnter2D(Collider2D other) {
       if(other.tag == "Player") {
           PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
           stats.TakeDamage(damage, playHitReaction);
       }
   }
}
