using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    //Rigidbody2D rb2d;
    public List<GameObject> listChildrenObject;
    public int sizeList;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        for(int i=0;i<sizeList;i++)
        {
            GameObject tmpObject = transform.GetChild(i).gameObject;
            //tmpObject.SetActive(true);
            listChildrenObject.Add(tmpObject);
       
        }    
    }

    void DestroyFragment()
    {
        for (int i = 0; i < sizeList; i++)
        {
            listChildrenObject[i].SetActive(false);
            //Destroy(listChildrenObject[i]);
        }
        //Destroy(this);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < sizeList; i++)
        {
            //listChildrenObject[i].SetActive(true);
            float randValue1 = Random.Range(-5.0f, 5.0f);
            float randValue2 = Random.Range( -5.0f, 0.0f);
            listChildrenObject[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(randValue1, randValue2));
        }
    }
}
