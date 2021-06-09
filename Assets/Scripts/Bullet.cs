using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        float fAngle = transform.rotation.eulerAngles.z;
        float radian = Mathf.Deg2Rad * fAngle;
        Vector2 vector = new Vector2(Mathf.Sin(radian) * -1, Mathf.Cos(radian));
        rb2d.AddForce(vector * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
