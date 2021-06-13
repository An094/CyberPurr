using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    private float startTime;
    void Start()
    {
        startTime = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mousePos - transform.position;

        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90.0f;
        //Debug.Log("rotationZ: " + rotationZ);

        if (rotationZ <= -180.0f) rotationZ = 90.0f;
        else if (rotationZ <= -90.0f) rotationZ = -90.0f;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        float duration = Time.time - startTime;
        if(duration >= 0.5f)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if(Input.GetButton("Fire1"))
            {
                GameObject bulletObject = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, transform.rotation);
                bulletObject.SetActive(true);
                startTime = Time.time;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
