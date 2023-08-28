using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();

            return instance;
        }
    }

    public List<ObjectPoolElement> elements = new List<ObjectPoolElement>();
    public Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();

    public void Init()
    {
        foreach (ObjectPoolElement element in elements)
        {
            if (!objectPools.ContainsKey(element.poolObject.name))
            {
                List<GameObject> addlist = new List<GameObject>();
                for (int i = 0; i < element.spwanCount; i++)
                {
                    addlist.Add(Instantiate(element.poolObject, transform));
                }

                objectPools.Add(element.poolObject.name, addlist);
            }
        }

    }


}
[Serializable]
public class ObjectPoolElement
{
    public GameObject poolObject;
    public int spwanCount;
}
