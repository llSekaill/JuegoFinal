using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombateJugadorBoss : MonoBehaviour
{
    [Header("Vida HP")]
    [SerializeField] private float vidaBoss;
    [SerializeField] public float maximoVidaBoss;
    [SerializeField] public BarraDeVidaBoss barraDeVidaBoss;
    public GameObject Reiniciar;
    public Image GameOver;

    void Start()
    {
        GameOver.enabled = false;
        Reiniciar.gameObject.SetActive(false);
        vidaBoss = maximoVidaBoss;
        barraDeVidaBoss.InicializarBarraDeVidaBoss(vidaBoss);
        
    }
    
    public void TomarDañoBoss(float dañoBoss){
        vidaBoss -= dañoBoss;
        barraDeVidaBoss.CambiarVidaActualBoss(vidaBoss);
        if(vidaBoss<-0){
            Destroy(gameObject);
            Debug.Log("RIP");
            Reiniciar.gameObject.SetActive(true);
            GameOver.enabled = true;

        }
    }
}
