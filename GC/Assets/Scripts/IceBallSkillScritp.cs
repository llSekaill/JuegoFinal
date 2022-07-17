
using UnityEngine;

public class IceBallSkillScritp : MonoBehaviour
{
    public float speedIBS;
    public float timeToDestroy;

    private Vector3 mDirection;
    private float mTimer = 0f;
    public void Start()
    {
        mDirection = GameManager.GetInstance().hero.GetDirection();
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.position += Vector3.right * speedIBS * Time.deltaTime;
        transform.position += speedIBS * Time.deltaTime * mDirection;
        mTimer += Time.deltaTime;
        if (mTimer > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
