using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public bool coll;
    public PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coll && Input.GetKey(KeyCode.S))
        {
            effector.surfaceArc = 0f; 
            StartCoroutine(Wait());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        coll = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        coll = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.8f);
        effector.surfaceArc = 180f;
    }
}
