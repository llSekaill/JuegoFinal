using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2D : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public RangoEnemigo2D rangg;
    public GameObject Hit;
    public AudioSource dectSound;
    public AudioSource pushSound;
    private float direccionF;
    public Transform rayAttack;
    //private Vector3 verto;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        RayoDire();
        //Comportamientos();
        //Debug.Log(rangg.cerca);
        //Debug.Log(Vector3.right * speed_walk * Time.deltaTime);
    }
    public void Comportamientos(){
        
        if(Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando ){
            ani.SetBool("run",false);
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 4){
                rutina = Random.Range(0,2);
                cronometro = 0;
            }
            switch(rutina){
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    direccion= Random.Range(0,2);
                    rutina++;
                    break;
                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            //Debug.Log("1");
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            direccionF = 1f;
                            //verto = Vector3.right;
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            //Debug.Log("2");
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            direccionF = -1f;
                            //verto = Vector3.left;
                            break;
                    }
                    ani.SetBool("walk", true);
                    break;
            }
        }else{
            if(Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando){
                if(transform.position.x < target.transform.position.x){
                    //Debug.Log(Mathf.Sign(target.transform.position.x));
                    //Debug.Log("1");
                    dectSound.Play();
                    ani.SetBool("attack", false);
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    direccionF = 1f;
                }else{
                    //Debug.Log(Mathf.Sign(target.transform.position.x));
                    //Debug.Log("2");
                    dectSound.Play();
                    ani.SetBool("walk", false);
                    ani.SetBool("attack", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    direccionF = -1f;
                }
            }else{
                if(!atacando){
                    if(transform.position.x < target.transform.position.x){
                        transform.rotation = Quaternion.Euler(0,0,0);
                        direccionF = 1f;
                        //Debug.Log("3");
                        dectSound.Play();
                    }else{
                        transform.rotation = Quaternion.Euler(0,180,0);
                        direccionF = -1f;
                        //Debug.Log("4");
                        dectSound.Play();
                    }
                    ani.SetBool("walk", false);
                    ani.SetBool("run", false);
                }
            }
        }
    }

    public void RayoDire(){
        RaycastHit2D hitF = Physics2D.Linecast(rayAttack.position, rayAttack.position + Vector3.right *direccionF);

        if(hitF.collider != null){
            //Debug.DrawLine(rayAttack.position,hitF.point, Color.red);
            if(hitF.collider.tag == "PJ"){
                Debug.Log("Heroe golepado");
                ani.SetBool("walk", false);
                ani.SetBool("run", false);
                ani.SetBool("attack", true);
                //Aturnido(); 
                // 5 seg corutina, funcion para detener, flag
            }else{
                Debug.Log("Random golepado");
                Comportamientos();
            }
            //Debug.Log(hitF.collider.gameObject.name);
            //Debug.Log(hitF.collider.gameObject.tag);
            
        }else{
            //Debug.DrawLine(rayAttack.position,rayAttack.position + Vector3.right * direccionF, Color.blue);
            Debug.Log("Vacio no hay nada");
            Comportamientos();
        }
    }
    public void Final_Ani(){
        if(Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision){
            ani.SetBool("attack", false);
            //atacando = false;
            //rango.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ColliderWeaponTrue(){
        //Hit.GetComponent<BoxCollider2D>().enabled = true;
        target.GetComponent<CombateJugador>().TomarDaño(10);
        pushSound.Play();
    }
    public void ColliderWeaponFalse(){
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            GetComponent<CombateJugadorBoss>().TomarDañoBoss(5);
        }
    }
}
