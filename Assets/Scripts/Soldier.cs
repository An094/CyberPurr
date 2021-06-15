using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour, IPooledObject
{
    private float speed = 1.0f;
    Animator animator;
    private Rigidbody2D rb2d;

    private bool endFly;
    private bool isRemove;
    int direction;

    private Vector3 screenBounds;
    private const float defaultGravity = 0.1f;
    private const float defaultMass = 3.0f;

    private float gravityAfterCollision = 0.5f;

    public void OnObjectSpawn()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        animator = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.None;
        endFly = false;
        isRemove = false;

        Vector2 pos = transform.position;
        direction = pos.x > 0 ? 0 : 1; //0 go to left, 1 go to right
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        rb2d.gravityScale = defaultGravity;
        rb2d.mass = defaultMass;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.x < screenBounds.x * -1 || pos.x > screenBounds.x)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plan")
        {
            if (!endFly)
            {
                animator.SetBool("endFly", true);
                endFly = true;
            }
            if (isRemove)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if(endFly)
            {
                collision.gameObject.SendMessage("Die");
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            if (!isRemove)
            {
                SoundManager.PlaySound("explosion");
                //float fAngle = transform.rotation.eulerAngles.z;
                //float radian = Mathf.Deg2Rad * fAngle;
                //Vector2 vForce = new Vector2(Mathf.Sin(radian) * -1, Mathf.Cos(radian));
                rb2d.AddForce(Vector2.up * 500.0f, ForceMode2D.Force);
                isRemove = true;
            }
            Score.Instance.IncreaseScore();
            rb2d.gravityScale = gravityAfterCollision;
            
        }
    }

    private void FixedUpdate()
    {
        if (!endFly) return;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if(direction == 0)
        {
            rb2d.velocity = Vector2.left * speed;
        }
        else
        {
            rb2d.velocity = Vector2.right * speed;
        }
    }
}
