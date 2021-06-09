using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float speed = 3.0f;
    Animator animator;
    Rigidbody2D rb2d;

    private bool endFly;
    private bool isRemove;
    int direction;
    // Start is called before the first frame update

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        endFly = false;
        Vector2 pos = transform.position;
        direction = pos.x > 0 ? 0 : 1; //0 go to left, 1 go to right
        isRemove = false;
        if(direction ==0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    animator.SetBool("endFly", true);

        //}
        if(endFly)
        {
            Vector2 pos = transform.position;
            if(direction == 1)
            {
                pos.x += speed * Time.deltaTime;
            }
            else
            {
                pos.x -= speed * Time.deltaTime;
            }
            transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plan")
        {
            if(!endFly)
            {
                animator.SetBool("endFly", true);
                endFly = true;
            }
            if (isRemove) Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("End Game");
        }
        if(collision.gameObject.tag == "Bullet")
        {
            rb2d.gravityScale = 5 * rb2d.gravityScale;
            isRemove = true;
        }
    }
}
