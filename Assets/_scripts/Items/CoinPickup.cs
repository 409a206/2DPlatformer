using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
   public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
            stats.CollectCoin(this.coinValue);
            AudioSource.PlayClipAtPoint(
                GetComponent<AudioSource>().clip, this.transform.position
            );
            Destroy(this.gameObject);
        }
    }

    
}
