using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //to use path.combine
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    //protected float gameTimer;
    public float gameTimer;
    public float score;
    public float spawnInterval {get; set;}
    [SerializeField] bool continueSpawning = true;
    
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject newParent;
    [SerializeField] private GameObject screenCollider;
    private int shapeID = 0;

    [SerializeField] public TMPro.TextMeshProUGUI scoreText;
    
    public float forceReducer {get; set;} //value could be decreased, as a powerup
    
    //need max objects allowed to spawn per level
    
    // Start is called before the first frame update
    void Start()
    {
        forceReducer = 100;
        spawnInterval = 1.0f;
        //InvokeRepeating("SpawnShape", spawnInterval, spawnInterval);
        //change to coroutine, more optimised.
        StartCoroutine("SpawnShape");
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        




    }

    IEnumerator SpawnShape()
    {
        while(continueSpawning)
        {
            int id = Random.Range(1, 4);
            //need to create random vector 3 for the position within the bounds of the canvas
            //need to instantiate automatically a certain amount of objects that differs per level
            
            //gameTimer = 0f;

            Vector3 spawnPos = RandomSpawnPosition();
            
            // For every spawnInterval or "second" it will spawn an item
            GameObject newObject = Instantiate(Resources.Load(Path.Combine("itemPrefabs", "item" + id)), spawnPos,
                Quaternion.identity) as GameObject;

            
            newObject.name = "Shape" + shapeID;
            shapeID++;

            Physics.IgnoreCollision(newObject.GetComponent<Collider>(), newObject.GetComponent<Collider>()); //ignore collisions with itself
            
            newObject.transform.SetParent(newParent.transform);

            yield return new WaitForSeconds(spawnInterval);
        }

        
      
        //newObject.transform.SetParent(newParent.transform)
        
        //spawnPos
        

    }

    Vector3 RandomSpawnPosition()
    {
        //for (int i = 0; i < 10; i++)
        //{
            /*float spawnY = Random.Range
                (mainCam.ScreenToWorldPoint(new Vector3(0, 0)).y, mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height)).y);
            float spawnX = Random.Range
                (mainCam.ScreenToWorldPoint(new Vector3(0, 0)).x, mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x);
            float spawnZ = 0.1f;
            */
            
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5)); //will get the middle of the screen

            Collider screen = screenCollider.GetComponentInChildren<Collider>();
            Vector3 screenBounds = screen.bounds.size;
            
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            
            Vector3 screenPosition = new Vector3(Random.Range(-9,9), Random.Range(-5,5), 0.1f);
 
            float spawnZ = 0.1f;
            

        Vector3 spawnPosition = new Vector3(screenPosition.x, screenPosition.y, spawnZ);
        
        return screenPosition;
        //return spawnPosition;
    }

    void itemMovement()
    {
        //setup random movement direction within bounds of the screen, check 2d platformer for code inspiration
    }

    public void ZeroGravity()
    {

        if (forceReducer != 0)
            forceReducer = 0;
        else
        {
            forceReducer = 100;
        }

    }


    
}
