using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    private float m_fLastTime;

    private const float LOAD_TIME = 0.5f;
    void Start()
    {
        m_fLastTime = 0.0f;
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

        m_fLastTime += Time.deltaTime;
        if(m_fLastTime >= LOAD_TIME)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if(Input.GetButton("Fire1"))
            {
                GameObject bulletObject = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, transform.rotation);
                bulletObject.SetActive(true);
                m_fLastTime = 0.0f;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
