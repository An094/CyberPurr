using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    Animator animator;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        animator = GetComponent<Animator>();
        this.gameObject.SetActive(true);
        StartCoroutine(StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isFinish", true);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
