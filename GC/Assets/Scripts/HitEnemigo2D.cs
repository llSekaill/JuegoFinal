using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{   
    public GameObject padre;
    
    void OnTriggerEnter2D(Collider2D coll){

        if(coll.CompareTag("PJF")){
            padre.GetComponent<CombateJugador>().TomarDaño(10);
            //Debug.Log("Golpeo");
        }
    }
    
}
