using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour, IPooledObject
{
    public List<GameObject> listChildrenObject;
    public int sizeList;
    // Start is called before the first frame update

    //void Start()
    //{
    //    for (int i = 0; i < sizeList; i++)
    //    {
    //        GameObject tmpObject = transform.GetChild(i).gameObject;
    //        tmpObject.AddComponent<FragmentChildren>();
    //        tmpObject.SetActive(true);
    //        listChildrenObject.Add(tmpObject);

    //    }
    //}

    public void OnObjectSpawn()
    {
        for (int i = 0; i < sizeList; i++)
        {
            GameObject tmpObject = transform.GetChild(i).gameObject;
            tmpObject.AddComponent<FragmentChildren>();
            tmpObject.transform.position = transform.position;
            tmpObject.transform.rotation = transform.rotation;
            tmpObject.SetActive(true);
            listChildrenObject.Add(tmpObject);
            
        }
    }   

    
    //private void FixedUpdate()
    //{
    //    for (int i = 0; i < sizeList; i++)
    //    {
    //        float randValue1 = Random.Range(-5.0f, 5.0f);
    //        float randValue2 = Random.Range( -10.0f, 0.0f);
    //        listChildrenObject[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(randValue1, randValue2));
    //        //Debug.Log("Force:" + randValue1 + "," + randValue2);
    //    }
    //}
}
