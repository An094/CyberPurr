using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject  
{
    private Rigidbody2D rb2d;
    private float speed;
    private Vector3 screenBounds;

    private Vector2 vForce;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb2d = GetComponent<Rigidbody2D>();
        speed = 1.0f;
    }

    public void OnObjectSpawn()
    {
        float fAngle = transform.rotation.eulerAngles.z;
        float radian = Mathf.Deg2Rad * fAngle;
        vForce = new Vector2(Mathf.Sin(radian) * -1, Mathf.Cos(radian));

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

    private void FixedUpdate()
    {
        rb2d.AddForce(vForce * speed, ForceMode2D.Impulse);
    }
}
