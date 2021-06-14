using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Sprite box;
    public Sprite defaultSprite;
    public GameObject gun;
    public GameObject cat;
    public GameObject helicopterController;

    public Text score;

    public GameObject gameOver;

    private bool isStopExplosion;
    private void Start()
    {
        cat.transform.position = transform.position;
        cat.transform.rotation = transform.rotation;
        cat.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Die()
    {
        ContinuousExplosion();
        gun.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = box;
        cat.SetActive(true);
        cat.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300.0f, ForceMode2D.Force);

        helicopterController.SetActive(false);
        //SoundManager.StopSound();

        SoundManager.PlaySound("gameover");

        gameOver.SetActive(true);

        score.text = Score.Instance.GetScore().ToString();
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
}
