using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject objectToPool;

        private List<GameObject> pooledObject;

        public int amountToPool;

        void Start()
        {
            GameObject tmpObject;
            pooledObject = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++)
            {
                tmpObject = Instantiate(objectToPool);
                tmpObject.SetActive(false);
                pooledObject.Add(tmpObject);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledObject.Count; i++)
            {
                if (!pooledObject[i].activeInHierarchy)
                {
                    return pooledObject[i];
                }
            }
            GameObject tmpObj = Instantiate(objectToPool);
            tmpObj.SetActive(false);
            pooledObject.Add(tmpObj);
            return tmpObj;

        }
    }
}