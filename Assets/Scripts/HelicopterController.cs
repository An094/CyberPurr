using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private float spawnTime;
    private Vector3 screenBounds;
    private float startTime;
    private float maxspawnTime;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        startTime = Time.time;
        maxspawnTime = 3.0f;
        StartCoroutine(helicopterWave());
    }

    void spawnHelicopter()
    {
        int side = Random.Range(0, 2);
        Vector2 pos;
        float height = Random.Range(screenBounds.y * 0.7f, screenBounds.y * 0.9f);
        if (side == 0)
        {
            pos = new Vector2(-screenBounds.x, height);
        }
        else
        {
            pos = new Vector2(screenBounds.x, height);
        }

        GameObject hel = ObjectPooler.Instance.SpawnFromPool("Helicopter", pos,transform.rotation);
        if(hel == null)
        {
            Debug.Log("Helicopter is null");
            return;
        }
        hel.SetActive(true);
    }

    IEnumerator helicopterWave()
    {
        while (true)
        {
            spawnTime = Random.Range(1.0f, maxspawnTime);
            yield return new WaitForSeconds(spawnTime);
            spawnHelicopter();
        }
    }

    void Update()
    {
        float duration = Time.time - startTime;
        if(duration >=30.0f && maxspawnTime > 1.0f)
        {
            maxspawnTime -= 0.5f;
            startTime = Time.time;
        }
    }
}
