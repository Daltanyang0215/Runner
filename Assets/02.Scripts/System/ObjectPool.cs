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
    public Dictionary<string, Stack<GameObject>> objectPools = new Dictionary<string, Stack<GameObject>>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (ObjectPoolElement element in elements)
        {
            if (!objectPools.ContainsKey(element.poolObject.name))
            {
                Stack<GameObject> addlist = new Stack<GameObject>();
                for (int i = 0; i < element.spwanCount; i++)
                {
                    GameObject go = Instantiate(element.poolObject, Vector3.zero, Quaternion.identity, transform);
                    go.name = element.poolName;
                    go.SetActive(false);
                    addlist.Push(go);
                }

                objectPools.Add(element.poolName, addlist);
            }
        }
    }

    public GameObject Spwan(string name, Vector3 pos, Quaternion rotate, Transform parnet)
    {
        if (objectPools.ContainsKey(name))
        {
            if (objectPools[name].TryPop(out GameObject result))
            {
                result.SetActive(true);
                result.transform.SetPositionAndRotation(pos, rotate);
                result.transform.SetParent(parnet);
                return result;
            }
            else
            {
                ObjectPoolElement addpool = elements.Find((x) => x.poolName == name);
                for (int i = 0; i < 4; i++)
                {
                    GameObject go = Instantiate(addpool.poolObject, Vector3.zero, Quaternion.identity, transform);
                    go.name = addpool.poolName;
                    go.SetActive(false);
                    objectPools[name].Push(go);
                }

                
                return Instantiate(addpool.poolObject, pos, rotate, parnet);
            }
        }
        else
        {
            Debug.Log("not pooling name");
            return null;
        }
    }

    public bool PoolRetrun(GameObject poolobject)
    {
        if (objectPools.ContainsKey(poolobject.name))
        {
            poolobject.SetActive(false);
            poolobject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            poolobject.transform.SetParent(transform);

            objectPools[poolobject.name].Push(poolobject);

            return true;
        }
        else
        {
            Debug.Log("not pooling name");
            return false;
        }
    }

}
[Serializable]
public class ObjectPoolElement
{
    public string poolName;
    public GameObject poolObject;
    public int spwanCount;
}
