
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Cinemachine;

//using UnityEngine.SceneManagement;

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
    public GameObject fireball;
    private Transform mFireballPoint;
    public AudioSource attackS;

    [Header("Ice")]
    public GameObject iceball;
    private Transform mIceballPoint;

    private Rigidbody2D mRigidBody;
    private float mMovement;
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    bool dobleJump;
    int number=0;
    float secondsCounter=0;
    float secondsToCount=1;
    
    public AudioSource fastS;
    public GameObject springIce;

    public GameObject targetHeroI;
    public GameObject origenHeroI;
    public GameObject heroI;

    private float cooldown = 5f;
    private float timeInicio = 0;
    public Image skill1;
    private bool estadoCooldown = false;
    private int segCap = 0;
    private bool estadoS1 = false;

    private float cooldown2 = 8f;
    private float timeInicio2 = 0;
    public Image skill2;
    private bool estadoCooldown2 = false;
    private int segCap2 = 0;
    private bool estadoS2 = false;

    private float cooldown3 = 20f;
    private float timeInicio3 = 0;
    public Image skill3;
    private bool estadoCooldown3 = false;
    private int segCap3 = 0;
    private bool estadoS3 = false;

    public GameObject camareVirtual;
    public AudioSource iceBallSound;
    public AudioSource iceBallSound2;

    private Vector3 respawn;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        mFireballPoint = transform.Find("FireballPoint");
        mIceballPoint = transform.Find("IceBallPoint");
        skill1.fillAmount = 0;
        skill2.fillAmount = 0;
        skill3.fillAmount = 0;
        
    }

    private void Update()
    {
        mMovement = Input.GetAxis("Horizontal");
        mAnimator.SetInteger("Move", mMovement == 0f ? 0 : 1);
        KeyC();
        
        secondsCounter += Time.deltaTime;
        if (secondsCounter >= secondsToCount){
            secondsCounter=0;
            number++;
        }
        // Imagen grande del personaje y zoom de la camara con la habilidad 2
        if(estadoS2){
            //ImagenAndZoom2();
            //camareVirtual.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 2.5f;
            Vector3 a = heroI.transform.position;
            Vector3 b = targetHeroI.transform.position;
            heroI.transform.position = Vector3.Lerp(a,b,0.01f);
            mAnimator.SetBool("IsHeal", true);
        }else{
            if(!estadoS2){
                //NoImagenAndZoom2();
                //camareVirtual.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
                Vector3 c = heroI.transform.position;
                Vector3 d = origenHeroI.transform.position;
                heroI.transform.position = Vector3.Lerp(c,d,0.01f);
                mAnimator.SetBool("IsHeal", false);
            }
        }
        if(number-segCap2 == 3 && segCap2 !=0){
            estadoS2 = false;
            segCap2 = 0;
        }
        // Habilidad 3 cancelar
        Debug.Log(estadoS3);
        if(number-segCap3 == 5 && segCap3 !=0){
            
            estadoS3 = false;
            segCap3 = 0;
        }
        if(estadoS3){
            ImagenAndZoom();
            mAnimator.SetBool("IsIceBall", true);
        }else{
            if(!estadoS3){
                NoImagenAndZoom();
                mAnimator.SetBool("IsIceBall", false);

            }
        }

        // Movimiento del personaje acelerado 
        mAnimator.SetBool("estadoV", estadoS1);
        if(number-segCap == 3 && segCap != 0){
            estadoS1 = false;
            segCap = 0;
        };

        if (mMovement < 0f)
        {
            transform.rotation = Quaternion.Euler(
                0f,
                180f,
                0f
            );
        } else if (mMovement > 0)
        {
            transform.rotation = Quaternion.Euler(
                0f,
                0f,
                0f
            );
        }
        // Para ver la animacion del doble salto
        bool isOnAir = IsOnAir();
        if (Input.GetButtonDown("Jump"))
        {   
            springIce.GetComponent<SpriteRenderer>().enabled = false;
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
        if(estadoS1 == true && !isOnAir ){
            springIce.GetComponent<SpriteRenderer>().enabled = true;
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
        if(Time.time > timeInicio){
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                moveSpeed = 9f;
                segCap = number;
                fastS.Play();
                springIce.GetComponent<SpriteRenderer>().enabled = true;
                timeInicio = Time.time + cooldown;
                estadoS1 = true;
                estadoCooldown = true;
                skill1.fillAmount = 1;
            }
        }
        if(estadoCooldown){
            skill1.fillAmount -= 1 / cooldown * Time.deltaTime;
            if(skill1.fillAmount <= 0){
                skill1.fillAmount = 0;
                estadoCooldown = false;
            }
        }
        if(Time.time > timeInicio2){
            if(Input.GetKeyDown(KeyCode.Alpha2)){
                GetComponent<CombateJugador>().CurarVida(10);
                segCap2 = number;
                timeInicio2 = Time.time + cooldown2;
                estadoS2 = true;
                estadoCooldown2 = true;
                skill2.fillAmount = 1;
            }
        }
        if(estadoCooldown2){
            skill2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if(skill2.fillAmount <= 0){
                skill2.fillAmount = 0;
                estadoCooldown2 = false;
            }
        }
        if(Time.time > timeInicio3){
            if(Input.GetKeyDown(KeyCode.Alpha3)){
                segCap3 = number;
                timeInicio3 = Time.time + cooldown3;
                skill3.fillAmount = 1;
                estadoS3 = true;
                estadoCooldown3 = true;
            }
        }
        if(estadoCooldown3){
            skill3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if(skill3.fillAmount <= 0){
                skill3.fillAmount = 0;
                estadoCooldown3 = false;
            }
        }
        
    }
    private void Move()
    {   
        if(estadoS1 == false){
            moveSpeed = 6f;
            springIce.GetComponent<SpriteRenderer>().enabled = false;
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
        springIce.GetComponent<SpriteRenderer>().enabled = false;
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

        Color rayColor;
        if (hit)
        {
            rayColor = Color.red;
        }else
        {
            rayColor = Color.blue;
        }
        Debug.DrawRay(rayCastOrigin.position, Vector2.down * raycastDistance, rayColor);
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
    
    public void AttackIceBall(){
        iceBallSound2.Play();
        GameObject objI =Instantiate(iceball, mIceballPoint);
        objI.transform.parent = null;
        Debug.Log("Entro");
    }
    public void SoundAttackIceBall(){
        iceBallSound.Play();

    }
    public void ImagenAndZoom(){
        camareVirtual.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 2.5f;
        Vector3 a = heroI.transform.position;
        Vector3 b = targetHeroI.transform.position;
        heroI.transform.position = Vector3.Lerp(a,b,0.01f);
    }
    public void NoImagenAndZoom(){
        camareVirtual.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6.5f;
        Vector3 c = heroI.transform.position;
        Vector3 d = origenHeroI.transform.position;
        heroI.transform.position = Vector3.Lerp(c,d,0.01f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Espinas")
        {
            transform.position = respawn;
        }
        else if (collision.tag == "CheckPoint")
        {
            respawn = transform.position;

        }
        /*else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/
    }

}   
