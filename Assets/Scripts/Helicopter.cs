using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float speed;
    public GameObject fragment;
    public GameObject explosion;
    public GameObject soldier;

    private int direction;
    private float timeDropSoldier;
    private float startTime;
    private bool wasDropSolder;

    private Vector3 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 pos = transform.position;
        direction = (pos.x) > 0 ? 0 : 1;  // 0 go to left, 1 go to right
        if (direction == 0) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        timeDropSoldier = Random.Range(1.0f, 5.0f);
        startTime = Time.realtimeSinceStartup;
        wasDropSolder = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (direction == 0)
        {
            pos.x -= speed * Time.deltaTime;
            if (pos.x < screenBounds.x * -1)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            pos.x += speed * Time.deltaTime;
            if (pos.x > screenBounds.x)
            {
                Destroy(this.gameObject);
            }
        }
        transform.position = pos;

        float time = Time.time - startTime;
        if (time >= timeDropSoldier && !wasDropSolder)
        {
            Instantiate(soldier, transform.position, transform.rotation);
            wasDropSolder = true;
        }

    }
}
