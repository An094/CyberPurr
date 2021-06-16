using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HelicopterController : MonoBehaviour
{
    private float m_fSpawnTime;
    private Vector3 m_ScreenBounds;
    private float m_fLastTime;
    private float m_fMaxSpawnTime;
    private const float MIN_HEIGHT_FACTOR = 0.7f;
    private const float MAX_HEIGHT_FACTOR = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        m_fLastTime = 0.0f;
        m_fMaxSpawnTime = 4.0f;
        StartCoroutine(helicopterWave());
    }

    void spawnHelicopter()
    {
        int side = Random.Range(0, 2);
        Vector2 pos;
        float height = Random.Range(m_ScreenBounds.y * MIN_HEIGHT_FACTOR, m_ScreenBounds.y * MAX_HEIGHT_FACTOR);
        if (side == 0)
        {
            pos = new Vector2(-m_ScreenBounds.x, height);
        }
        else
        {
            pos = new Vector2(m_ScreenBounds.x, height);
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
            m_fSpawnTime = Random.Range(1.0f, m_fMaxSpawnTime);
            yield return new WaitForSeconds(m_fSpawnTime);
            spawnHelicopter();
        }
    }

    void Update()
    {
        m_fLastTime += Time.deltaTime;
        if(m_fLastTime >=30.0f && m_fMaxSpawnTime > 1.5f)
        {
            m_fMaxSpawnTime -= 0.5f;
            m_fLastTime = m_fLastTime - 30.0f;
        }
    }
}
