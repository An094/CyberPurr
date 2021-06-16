using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    LEFT,
    RIGT
}
public class Helicopter : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float m_fSpeed;

    private DIRECTION m_direction;
    private float m_fTimeDropSoldier;
    private float m_fLastTime;
    private bool m_wasDropSolder;

    private Vector3 m_ScreenBounds;
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void OnObjectSpawn()
    {
        Vector3 pos = transform.position;
        m_direction = (pos.x) > 0 ? DIRECTION.LEFT : DIRECTION.RIGT;
        if (m_direction.Equals(DIRECTION.LEFT)) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        else transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        m_fTimeDropSoldier = Random.Range(1.0f, 8.0f);
        m_fLastTime = 0.0f;
        m_wasDropSolder = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (m_direction == DIRECTION.LEFT)
        {
            pos.x -= m_fSpeed * Time.deltaTime;
            if (pos.x < -m_ScreenBounds.x)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            pos.x += m_fSpeed * Time.deltaTime;
            if (pos.x > m_ScreenBounds.x)
            {
                this.gameObject.SetActive(false);
            }
        }
        transform.position = pos;

        m_fLastTime += Time.deltaTime;
        if (m_fLastTime >= m_fTimeDropSoldier && !m_wasDropSolder)
        {
            //Debug.Log("Spawn Soldier");
            GameObject soldier = ObjectPooler.Instance.SpawnFromPool("Soldier", transform.position,transform.rotation);
            if(soldier == null)
            {
                Debug.Log("Soldier is null");
                return;
            }
            soldier.SetActive(true);
            m_wasDropSolder = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GameObject explosion = ObjectPooler.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
            explosion.SetActive(true);

            //Drop fragment
            GameObject fragment = ObjectPooler.Instance.SpawnFromPool("Fragment", transform.position, transform.rotation);
            fragment.SetActive(true);

            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);  //bullet
            Score.Instance.IncreaseScore();         
            SoundManager.PlaySound("explosion");
        }
    }

}
