using System.Collections;
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
    public List<GameObject> pooledItems; //all the things we have created and put in a pool so we can use them


    // Start is called before the first frame update
    void Start()
    {
        //gameobject - when inactive, uses no memory
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
