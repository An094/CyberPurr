using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour, IPooledObject
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

    private const float defaultVelocity = 0.5f;
    private const float timeExplosion = 0.5f;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
    }
    public void OnObjectSpawn()
    {
        Vector3 pos = transform.position;
        direction = (pos.x) > 0 ? 0 : 1;  // 0 go to left, 1 go to right
        if (direction == 0) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        else transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        timeDropSoldier = Random.Range(1.0f, 5.0f);
        startTime = Time.time;
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
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            pos.x += speed * Time.deltaTime;
            if (pos.x > screenBounds.x)
            {
                this.gameObject.SetActive(false);
            }
        }
        transform.position = pos;

        float time = Time.time - startTime;
        if (time >= timeDropSoldier && !wasDropSolder)
        {
            //Debug.Log("Spawn Soldier");
            GameObject soldier = ObjectPooler.Instance.SpawnFromPool("Soldier", transform.position,transform.rotation);
            if(soldier == null)
            {
                Debug.Log("Soldier is null");
                return;
            }
            soldier.SetActive(true);
            wasDropSolder = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(fragment, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }
}
