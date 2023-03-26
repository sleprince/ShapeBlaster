using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //so you can add these variables in editor
public class PoolItem
{
    public GameObject prefab;
    public int amount;
}

//singleton - you create one object in memory, if anything changes it, it only changes in one spot

public class Pool : MonoBehaviour
{

    public static Pool singleton; //static is a singleton, one instance only
    public List<PoolItem> items; //prefabs we can pool
    private List<GameObject> pooledItems; //all the things we have created and put in a pool so we can use them


    void Awake()
    {
        singleton = this; //this instance is the singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledItems = new List<GameObject>();
        foreach (PoolItem item in items) //instantiate all the items into the pool at the start
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false); //inactive to start, not using any memory
                pooledItems.Add(obj);
            }
        }
    }

    public GameObject Get(string name) //call this from the object spawning script to get items from the pool, will pass in eg. Get(item1)
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].name == name) //check they are inactive and match the tag
            {
                return pooledItems[i];
            }
        }

        return null; //there are no available pooled items
    }

}
