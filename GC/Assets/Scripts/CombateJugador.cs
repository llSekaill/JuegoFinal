using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombateJugador : MonoBehaviour
{
    [Header("Vida HP")]
    [SerializeField] private float vida;
    [SerializeField] public float maximoVida;
    [SerializeField] public BarraDeVida barraDeVida;
    public GameObject ReiniciarJ;
    public Image GameOverJ;
    void Start()
    {
        GameOverJ.enabled = false;
        ReiniciarJ.gameObject.SetActive(false);
        vida = maximoVida;
        barraDeVida.InicializarBarraDeVida(vida);
    }
    
    public void TomarDaño(float daño){
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);
        if(vida<-0){
            Destroy(gameObject);
            Debug.Log("RIP");
            ReiniciarJ.gameObject.SetActive(true);
            GameOverJ.enabled = true;
        }
    }

    public void CurarVida(float cura){
        vida += cura;
        barraDeVida.CambiarVidaActual(vida);
    }
}
