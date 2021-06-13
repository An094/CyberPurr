using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour, IPooledObject
{
    public List<GameObject> listChildrenObject;
    public int sizeList;
    // Start is called before the first frame update


    public void OnObjectSpawn()
    {
        for (int i = 0; i < sizeList; i++)
        {
            GameObject tmpObject = transform.GetChild(i).gameObject;
            tmpObject.AddComponent<FragmentChildren>();
            tmpObject.transform.position = transform.position;
            tmpObject.transform.rotation = transform.rotation;
            ///tmpObject.AddComponent<PhysicMaterial>().bounciness = 1;
            tmpObject.SetActive(true);
            listChildrenObject.Add(tmpObject);
            
        }
    }   
}
