using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plan : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Fragment")
        {
            //StartCoroutine(DestroyObject(collision));
            collision.transform.parent.SendMessage("DestroyFragment");
            //collision.gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyObject(Collision2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        collision.transform.parent.SendMessage("DestroyFragment");

    }
}
