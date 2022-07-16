using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo2D : MonoBehaviour
{
    public Animator ani;
    public Enemigo2D enemigo;
    public bool cerca;

    void repetirCheck(){
        
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.CompareTag("PJF")){
            
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
            enemigo.atacando = true;
            
            
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //repetirCheck();
        //Debug.Log(cerca);
    }
}
