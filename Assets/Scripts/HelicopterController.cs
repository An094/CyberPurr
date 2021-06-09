using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public GameObject helicopterPrefab;
    private float spawnTime;
    private Vector3 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(helicopterWave());
    }

    void spawnHelicopter()
    {
        GameObject hel = Instantiate(helicopterPrefab);
        int side = Random.Range(0, 2);
        float height = Random.Range(screenBounds.y * 0.7f, screenBounds.y*0.9f);
        if (side == 0)
        {
            hel.transform.position = new Vector2(-screenBounds.x, height);
        }
        else
        {
            hel.transform.position = new Vector2(screenBounds.x, height);
        }
    }

    IEnumerator helicopterWave()
    {
        while (true)
        {
            spawnTime = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(spawnTime);
            spawnHelicopter();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
