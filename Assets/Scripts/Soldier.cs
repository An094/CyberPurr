using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION_SOLDIER
{ 
    LEFT,
    RIGHT
}

public class Soldier : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float m_fSpeed;

    private Animator animator;
    private Rigidbody2D rb2d;

    private bool m_isEndFly;
    private bool m_isRemove;
    private DIRECTION_SOLDIER m_direction;

    private Vector3 m_ScreenBounds;
    private const float DEFAULT_GRAVITY = 0.1f;
    private const float DEFAULT_MASS = 1.0f;

    private const float FORCE_FACTOR = 300.0f;

    private float GRAVITY_AFTER_COLLISION = 0.5f;

    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void OnObjectSpawn()
    {
        
        animator = gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.None;
        m_isEndFly = false;
        m_isRemove = false;

        Vector2 pos = transform.position;
        m_direction = pos.x > 0 ? DIRECTION_SOLDIER.LEFT : DIRECTION_SOLDIER.RIGHT;
        if (m_direction == DIRECTION_SOLDIER.LEFT)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        rb2d.gravityScale = DEFAULT_GRAVITY;
        rb2d.mass = DEFAULT_MASS;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.x < -m_ScreenBounds.x || pos.x > m_ScreenBounds.x || pos.y > m_ScreenBounds.y)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plan")
        {
            if (!m_isEndFly)
            {
                animator.SetBool("endFly", true);
                m_isEndFly = true;
            }
            if (m_isRemove)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if(m_isEndFly)
            {
                collision.gameObject.SendMessage("Die");    //Call function Die of Player Object
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            if (!m_isRemove)
            {
                SoundManager.PlaySound("explosion");
                float fAngle = collision.transform.rotation.eulerAngles.z;
                float radian = Mathf.Deg2Rad * fAngle;
                Vector2 vForceAfterCollision = new Vector2(Mathf.Sin(radian) * -1, Mathf.Cos(radian));
                rb2d.AddForce(vForceAfterCollision * FORCE_FACTOR, ForceMode2D.Force);
                m_isRemove = true;
                Score.Instance.IncreaseScore();
                rb2d.gravityScale = GRAVITY_AFTER_COLLISION;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!m_isEndFly) return;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if(m_direction == DIRECTION_SOLDIER.LEFT)
        {
            rb2d.velocity = Vector2.left * m_fSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.right * m_fSpeed;
        }
    }
}
