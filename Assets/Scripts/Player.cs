using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Sprite m_sprBox;
    
    [SerializeField]
    private GameObject m_objGun;

    [SerializeField]
    private GameObject m_objCat;

    [SerializeField]
    private GameObject m_HelicopterController;

    [SerializeField]
    private Text m_txtScore;

    [SerializeField]
    private GameObject m_GameOver;

    private bool m_isDead;
    private void Start()
    {
        m_objCat.transform.position = transform.position;
        m_objCat.transform.rotation = transform.rotation;
        m_objCat.SetActive(false);
        m_isDead = false;
    }

    public void Die()
    {
        if (m_isDead) return;
        m_isDead = true;

        ContinuousExplosion();

        m_objGun.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = m_sprBox;
        m_objCat.SetActive(true);
        m_objCat.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300.0f, ForceMode2D.Force);

        m_HelicopterController.SetActive(false);

        SoundManager.PlaySound("meow");

        StartCoroutine(Gameover());
    }

    private void ContinuousExplosion()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        while(true)
        {
            GameObject explosion = ObjectPooler.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(2.0f);
        m_GameOver.SetActive(true);

        m_txtScore.text = Score.Instance.GetScore().ToString();

        SoundManager.PlaySound("gameover");
        yield return new WaitForSeconds(3.0f);
        SoundManager.StopSound();

       
    }
}
