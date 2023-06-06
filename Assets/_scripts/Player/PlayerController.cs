using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  #region  VARIABLES
  #region  PUBLIC HIDDEN VARIABLES
   [HideInInspector]
   public bool isFacingRight = true;
   [HideInInspector]
   public bool isJumping = true;
    [HideInInspector]
   public bool isGrounded = true;
    #endregion
  #region  PUBLIC VARIABLES
   public float jumpForce = Constants.playerJumpForce;
   public float maxSpeed = Constants.playerMaxSpeed;

    public AudioClip[] footstepSounds;

    public AudioClip jumpSound;
    public AudioClip damageSound;

   public Transform groundCheck;
   public LayerMask groundLayers;
    #endregion
  #region  PRIVATE VARIABLES
   private float groundCheckRadius = Constants.playerGroundCheckRadius;
   private Rigidbody2D myRigid;
   private Animator anim;
    private bool isDoubleJumping = false;
    private PhysicsMaterial2D jumpMaterial;
    private AudioSource audioSource;
    #endregion
  #endregion
  #region  INHERENT METHODS[Awake, Update, FixedUpdate]
    private void Awake() {
       myRigid = transform.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }
   private void Update() {
       if(Input.GetButtonDown(Constants.inputJump)) {
           if(isGrounded == true) {
               myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
               myRigid.AddForce(new Vector2(0, jumpForce));  
               anim.SetTrigger(Constants.animJump);  
               PlayJumpAudio();       
            } else if(isDoubleJumping == false) {
                isDoubleJumping = true;
                myRigid.velocity = new Vector2(myRigid.velocity.x, 0);
                myRigid.AddForce(new Vector2(0, jumpForce));
                PlayJumpAudio();
            } 
            else {
                Debug.Log("Jump pressed while not grounded");
            }
       }
   }

    private void FixedUpdate() {

       isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);

        PhysicsMaterial2D material = 
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial;

            if(isGrounded == true) {
                isDoubleJumping = false;
            }
            if(isGrounded == true && material == this.jumpMaterial) {
                CircleCollider2D collision = 
                    gameObject.GetComponent<CircleCollider2D>();

                    collision.sharedMaterial = null;

                    collision.enabled = false;
                    collision.enabled = true;
            } else if(isGrounded == false && gameObject.GetComponent<CircleCollider2D>().sharedMaterial == null) {
                CircleCollider2D collision = gameObject.GetComponent<CircleCollider2D>();
                collision.sharedMaterial = this.jumpMaterial;

                collision.enabled = false;
                collision.enabled = true;
            }

         try {
            float move = Input.GetAxis(Constants.inputMove);
           
            myRigid.velocity = new Vector2(move * maxSpeed, myRigid.velocity.y);

            anim.SetFloat(Constants.animSpeed, Mathf.Abs(move));

            if((move>0.0f && isFacingRight == false) || (move < 0.0f && isFacingRight == true)) {
                Flip();
            }
            //Debug.Log(move);

         } catch(UnityException error) {
             Debug.LogError(error.ToString());
         } finally {
             //Debug.LogWarning("Our input check failed!");
         }
   }
   #endregion
  #region  UTILITY METHODS[Flip]
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x = playerScale.x * -1;
        transform.localScale = playerScale;
    }
    #endregion
  #region  AUDIO METHODS[PlayFootstepAudio, PlayJumpAudio, PlayDamageAudio]
   public void PlayDamageAudio() {
       audioSource.clip = damageSound;
       audioSource.Play();
   }
    
    private void PlayJumpAudio()
    {
        //PlayClipAtPoint()를 사용한 이유는 점프 사운드가 다른 사운드에 의해 멈추는 것을 방지하기 위함.
       AudioSource.PlayClipAtPoint(this.jumpSound, this.transform.position);
    }



    void PlayFootstepAudio() {
        this.audioSource.clip = footstepSounds[(UnityEngine.Random.Range(0, footstepSounds.Length))];
        this.audioSource.Play();
    }
    #endregion
}
