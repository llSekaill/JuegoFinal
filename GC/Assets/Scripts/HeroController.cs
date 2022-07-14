
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float accel;
    public float deccel;
    public float speedExp;

    [Header("Jump")]
    public float raycastDistance;
    public float jumpForce;
    public float fallMultiplier;
    public AudioSource jumpS; 

    [Header("Fire")]
    public GameObject fireball; //prefab
    private Transform mFireballPoint;
    public AudioSource attackS;

    private Rigidbody2D mRigidBody;
    private float mMovement;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    bool dobleJump;
    int number=0;
    float secondsCounter=0;
    float secondsToCount=1;
    int segCap = 0;
    bool estadoV = false;
    public AudioSource fastS;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mFireballPoint = transform.Find("FireballPoint");
    }

    private void Update()
    {
        mMovement = Input.GetAxis("Horizontal");
        mAnimator.SetInteger("Move", mMovement == 0f ? 0 : 1);
        
        secondsCounter += Time.deltaTime;
        if (secondsCounter >= secondsToCount){
            secondsCounter=0;
            number++;
        }
        KeyC();
        mAnimator.SetBool("estadoV", estadoV);
        if(number-segCap == 3 && segCap != 0){
            estadoV = false;
            segCap = 0;
        };
        if (mMovement < 0f)
        {
            //mSpriteRenderer.flipX = true;
            transform.rotation = Quaternion.Euler(
                0f,
                180f,
                0f
            );
        } else if (mMovement > 0)
        {
            //mSpriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(
                0f,
                0f,
                0f
            );
        }

        bool isOnAir = IsOnAir();
        if (Input.GetButtonDown("Jump"))
        {   
            if(!isOnAir){
                Jump();
            }else{
                if(dobleJump){
                    mRigidBody.velocity = Vector2.zero;
                    Jump();
                    mAnimator.SetBool("IsJumping2", true);
                    jumpS.Play();
                    dobleJump = false;
                }
            }
            
            
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            mAnimator.SetBool("IsAttack", true);
            attackS.Play();
            Fire();
            
        }else{
            mAnimator.SetBool("IsAttack", false);
        }

        //Doblejump
        if(!isOnAir) dobleJump = true;
    }

    



    private void FixedUpdate()
    {   
        
        Move();
        if (mRigidBody.velocity.y < 0)
        {
            // Esta cayendo
            mRigidBody.velocity += (fallMultiplier - 1) * 
                Time.fixedDeltaTime * Physics2D.gravity;
        }
    }
    private void KeyC(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            moveSpeed = 10f;
            segCap = number;
            estadoV = true;
            fastS.Play();
            
        }
    }
    private void Move()
    {   
        if(estadoV == false){
            moveSpeed = 5f;
        }
        float targetSpeed = mMovement * moveSpeed;
        float speedDif = targetSpeed - mRigidBody.velocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? accel : deccel;
        float movement = Mathf.Pow(
            accelRate * Mathf.Abs(speedDif),
            speedExp
        ) * Mathf.Sign(speedDif);

        mRigidBody.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        mRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpS.Play();
    }

    public bool IsOnAir()
    {
        Transform rayCastOrigin = transform.Find("RaycastPoint");
        RaycastHit2D hit = Physics2D.Raycast(
            rayCastOrigin.position,
            Vector2.down,
            raycastDistance
        );
        mAnimator.SetBool("IsJumping", !hit);
        if(hit) mAnimator.SetBool("IsJumping2", false);

        /*Color rayColor;
        if (hit)
        {
            rayColor = Color.red;
        }else
        {
            rayColor = Color.blue;
        }
        Debug.DrawRay(rayCastOrigin.position, Vector2.down * raycastDistance, rayColor);*/

        return !hit;
        //return hit == null ? true : false;
        
    }
    private void Fire()
    {
        mFireballPoint.GetComponent<ParticleSystem>().Play(); // ejecutamos PS
        GameObject obj = Instantiate(fireball, mFireballPoint);
        obj.transform.parent = null;
        
        
    }

    public Vector3 GetDirection()
    {
        return new Vector3(
            transform.rotation.y == 0f ? 1f : -1f,
            0f,
            0f
        );
    }
}
