using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{   
   public GameObject coinSpawnPoint;
   public Object[] coinPrefabs; 
   
   private void Start() {
       this.SpawnCoin();
   }

   void SpawnCoin() {
       int random = Random.Range(0, coinPrefabs.Length);
       GameObject coin = Object.Instantiate(coinPrefabs[(Random.Range(0, coinPrefabs.Length))],
                                            coinSpawnPoint.transform.position,
                                            coinSpawnPoint.transform.rotation) as GameObject;
       coin.GetComponent<Rigidbody2D>().AddForce(new Vector2((Random.Range(-120, 120)), 700)); 
   }
}
