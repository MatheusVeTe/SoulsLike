using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<GameObject> PooledObjects = new List<GameObject>();
    private int AmountToPool = 3;

    public GameObject BulletPrefab;

    private void Awake() 
    {
        if (Instance == null) {Instance = this;}
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            GameObject Bullet = Instantiate(BulletPrefab);
            Bullet.SetActive(false);
            PooledObjects.Add(Bullet);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }
        return null;
    }
}
