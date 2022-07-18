using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movimiento : MonoBehaviour
{
    public float speed = 5f;
    public float jumpspeed = 8f;
    private float direccion = 0f;
    private Rigidbody2D Player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool TocarSuelo;

    private Animator playeranimation;

    private Vector3 respawn;
    //public GameObject Espina1;
    //public GameObject Espina2;


    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        playeranimation = GetComponent<Animator>();
        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        TocarSuelo = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direccion = Input.GetAxis("Horizontal");

        if (direccion > 0f)
        {
            Player.velocity = new Vector2(direccion * speed, Player.velocity.y);
        }
        else if (direccion < 0f)
        {
            Player.velocity = new Vector2(direccion * speed, Player.velocity.y);
        }
        else
        {
            Player.velocity = new Vector2(0, Player.velocity.y);
        }
        if(Input.GetButtonDown("Jump") && TocarSuelo)
        {
            Player.velocity = new Vector2(Player.velocity.x, jumpspeed);
        }
        playeranimation.SetFloat("Speed", Mathf.Abs(Player.velocity.x));
        playeranimation.SetBool("EnSuelo",TocarSuelo);

        //Espina1.transform.position = new Vector2(transform.position.x, Espina1.transform.position.y);
        //Espina2.transform.position = new Vector2(transform.position.x, Espina2.transform.position.y);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Espinas")
        {
            transform.position = respawn;
        }
        else if(collision.tag == "CheckPoint")
        {
            respawn = transform.position;

        }else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}

