using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health = 6;
    public int coinsCollected = 0;
    public bool isImmune;
    public float immunityDuration = 1.5f;
    private float immunityTime = 0f;
    private float flickerDuration = 0.1f;
    private float flickerTime = 0f;
    private SpriteRenderer spriteRenderer;
    public bool isDead;
    private float deathTimeElapsed;
    private GameObject HUDCamera;
    private GameObject HUDSprite;
    public ParticleSystem particleHitLeft;
    public ParticleSystem particleHitRight;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        HUDCamera = GameObject.FindGameObjectWithTag("HUDCamera");
        HUDSprite = GameObject.FindGameObjectWithTag("HUDSprite");

        if(Application.loadedLevel != Constants.SCENE_LEVEL_1) {
            coinsCollected = PlayerPrefs.GetInt(Constants.PREF_COINS);
        }
        PlayerPrefs.SetInt(Constants.PREF_CURRENT_LEVEL, Application.loadedLevel);
    }
    private void Update() {
        if(this.isImmune) {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;

            if(immunityTime >= immunityDuration) {
                this.isImmune = false;
                Debug.Log("Immunity has ended");
                spriteRenderer.enabled = true;
            }
        }

        if(isDead) {
            this.deathTimeElapsed = deathTimeElapsed + Time.deltaTime;
            if(deathTimeElapsed > 2.0f) {
                SceneManager.LoadScene(1);
            }
        }
    }
    

    void SpriteFlicker() {
        if(flickerTime < flickerDuration) {
            this.flickerTime = this.flickerTime + Time.deltaTime;
        } else if(flickerTime >= flickerDuration) {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            flickerTime = 0;
        }
    }

    public void CollectCoin(int coinValue) {
        this.coinsCollected = this.coinsCollected + coinValue;
        this.HUDSprite.GetComponent<CoinCounter>().value = this.coinsCollected;
    }

    public void TakeDamage(int damage, bool playHitReaction) {
        if(!isImmune && !isDead) {
            health = health - damage;
            Debug.Log("Player Health : " + health.ToString());
            this.HUDCamera.GetComponent<GUIGame>().UpdateHealth(this.health);

            if(health <= 0) {
                PlayerIsDead(playHitReaction);
            }

            if(playHitReaction == true) {
                Debug.Log("Hit reaction called");
                PlayHitReaction();
            }
        }

    }

    private void PlayerIsDead(bool playDeathAnim)
    {
        isDead = true;
        //GetComponent<Animator>().SetTrigger("Damage");
        PlayerController controller = 
            GetComponent<PlayerController>();
        Rigidbody2D myrigid = GetComponent<Rigidbody2D>();
        if(playDeathAnim) {
            myrigid.velocity = new Vector2(0,0);
            myrigid.AddForce(new Vector2(0, 600));
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        
         controller.enabled = false;
        // controller.PlayDamageAudio();
        // myrigid.velocity = new Vector2(0,0);
        // if(controller.isFacingRight) {
        //     myrigid.AddForce(new Vector2(-400, 400));
        // } else {
        //     myrigid.AddForce(new Vector2(400, 400));
        // }
    }

    private void PlayHitReaction()
    {
        isImmune = true;
        immunityTime = 0f;
        GetComponent<Animator>().SetTrigger("Damage");

        PlayerController playerController = 
            gameObject.GetComponent<PlayerController>();
        playerController.PlayDamageAudio();
        
        if(playerController.isFacingRight) {
            particleHitLeft.Play();
            
        } else {
            particleHitLeft.Play();
            
        }
    }
}
