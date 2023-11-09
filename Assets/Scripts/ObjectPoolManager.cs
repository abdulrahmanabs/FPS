using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance; // Singleton instance

    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int initialPoolSize;
        public Transform parent;
    }

    public List<PoolItem> poolItems = new List<PoolItem>();

    private Dictionary<GameObject, List<GameObject>> objectPools = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var item in poolItems)
        {
            if (item.prefab != null)
            {
                if (!objectPools.ContainsKey(item.prefab))
                {
                    objectPools[item.prefab] = new List<GameObject>();
                }

                for (int i = 0; i < item.initialPoolSize; i++)
                {
                    var obj = Instantiate(item.prefab, item.parent);
                    obj.SetActive(false);
                    objectPools[item.prefab].Add(obj);
                }
            }
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (objectPools.ContainsKey(prefab))
        {
            var pooledObjects = objectPools[prefab];
            foreach (var obj in pooledObjects)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // If no inactive objects are found, create a new one
            var newObj = Instantiate(prefab);
            newObj.transform.SetParent(null);
            objectPools[prefab].Add(newObj);
            return newObj;
        }

        Debug.LogWarning("Object pool for " + prefab.name + " not found.");
        return null;
    }

    public void UnPoolObject(GameObject obj)
    {
        if (objectPools.ContainsKey(obj))
        {
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object pool for " + obj.name + " not found.");
            Destroy(obj);
        }
    }
}