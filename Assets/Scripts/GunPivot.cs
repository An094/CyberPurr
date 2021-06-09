using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
