using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroControllerSI : MonoBehaviour
{
    public Rigidbody2D rb;
    private float horizontal;
    void Start()
    {
        
    }

    
    void Update()
    {
        rb.velocity = new Vector2(horizontal * 5f, rb.velocity.y);
    }

    //Movimientio
    public void Move (InputAction.CallbackContext context){
        horizontal = context.ReadValue<Vector2>().x;
    }
}
