using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, IPooledObject
{
    private float speed = 3.0f;
    Animator animator;
    Rigidbody2D rb2d;

    private bool endFly;
    private bool isRemove;
    int direction;

    private Vector3 screenBounds;
    // Start is called before the first frame update

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        animator = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void OnObjectSpawn()
    {
        endFly = false;
        Vector2 pos = transform.position;
        direction = pos.x > 0 ? 0 : 1; //0 go to left, 1 go to right
        isRemove = false;
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            if(pos.x < screenBounds.x * -1 || pos.x > screenBounds.x)
            {
                this.gameObject.SetActive(false);
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
            if (isRemove)
            {
                //Destroy(gameObject);
                this.gameObject.SetActive(false);
            }
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
