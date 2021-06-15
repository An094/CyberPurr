using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randValue1 = Random.Range(-2.0f, 2.0f);
        float randValue2 = Random.Range(3.0f, 5.0f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randValue1, randValue2) * 50.0f, ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Plan")
        {
            StartCoroutine(DestroyAfterCollision());
        }
    }

    IEnumerator DestroyAfterCollision()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        
    }
}
