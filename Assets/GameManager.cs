using System.Collections;
using UnityEngine;
using System.IO; //to use path.combine

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
        //changed to coroutine, more optimised.
        StartCoroutine("SpawnShape");
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;

    }

    IEnumerator SpawnShape()
    {
        yield return new WaitForSeconds(1); //give it time to create the object pool first

        while (continueSpawning)
        {
            int id = Random.Range(1, 4);
            //need to create random vector 3 for the position within the bounds of the canvas
            //need to instantiate automatically a certain amount of objects that differs per level   

            Vector3 spawnPos = RandomSpawnPosition();

            // For every spawnInterval or "second" it will spawn an item
            GameObject newObject = Pool.singleton.Get("item" + id + "(Clone)");
            newObject.SetActive(true);

            if (newObject != null)
            {
                newObject.transform.position = spawnPos;


                //newObject.name = "Shape" + shapeID; //depricated
                //shapeID++;

                Physics.IgnoreCollision(newObject.GetComponent<Collider>(), newObject.GetComponent<Collider>()); //ignore collisions with itself

                //newObject.transform.SetParent(newParent.transform); //depricated
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 RandomSpawnPosition()
    {    
            //didn't work, probably because it is a perpective camera.
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
            
            Vector3 screenPosition = new Vector3(Random.Range(-9,9), Random.Range(-5,5), 0.1f);
            
        
        return screenPosition;
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
