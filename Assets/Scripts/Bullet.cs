using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject  
{
    private Rigidbody2D rb2d;
    public float speed = 10.0f;
    private Vector3 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //rb2d = GetComponent<Rigidbody2D>();
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        float fAngle = transform.rotation.eulerAngles.z;
        Debug.Log("Angle of bullet: " + fAngle);
        float radian = Mathf.Deg2Rad * fAngle;
        Vector2 vector = new Vector2(Mathf.Sin(radian) * -1, Mathf.Cos(radian));
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(vector * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < screenBounds.x * -1 || transform.position.x > screenBounds.x
            || transform.position.y > screenBounds.y)
        {
            this.gameObject.SetActive(false);
        }
    }
}
