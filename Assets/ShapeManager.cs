using UnityEngine;
using System.IO; //to use path.combine

public class ShapeManager : MonoBehaviour
{
    private GameManager gManager;
    Rigidbody rBody;
    private CharacterController shapeController;

    private float moveForce = 2.0f;

    private Rigidbody rb;


    //private float forceAdder = 2.0f;

    private Plane plane = new Plane(Vector3.up, Vector3.zero);

    float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse

    private Vector3
        lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)

    private float totalRun = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        shapeController = GetComponent<CharacterController>();
        gManager = GetComponentInParent<GameManager>();



    }

    // Update is called once per custom
    void Update()
    {
        rBody.mass = gManager.forceReducer + 1; //could have a scriptable object database of shape weights instead of 1.
        rBody.angularDrag = gManager.forceReducer + 0.05f;
        rBody.drag = gManager.forceReducer + 0;

    }

    private void OnMouseOver() //this is called when the player move the mouse over this object's collider
    {

        Debug.Log("you clicked on" + this);


        //transform.gameObject.SetActive(false);



        Vector3 forceDirection = new Vector3(0, 0, 10);

        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        Vector3 minusForce = new Vector3(-2500, -2500, -2500);

        //if (Input.GetMouseButtonDown(0)) //if lmb pressed
        rBody.AddRelativeForce(shapeController.transform.forward,
            ForceMode.Impulse); //(lastMouse.normalized * moveForce, ForceMode.Impulse);


    }



    /*
    void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float enter;
                if (plane.Raycast(ray, out enter))
                {
                    var tPoint = ray.GetPoint(enter);
                    var mouseDir = tPoint - gameObject.transform.position;
                    mouseDir = mouseDir.normalized;
                    rBody.AddForce(mouseDir * clickForce);

                }

            }

        }
        */

    void OnCollisionEnter(Collision collision)

    {
        
       
        
        //if spawns next to another object, transport to a new spawn point.
        //collision.gameObject.CompareTag("Shape") && collision.contactCount > 1 &&& collision.gameObject.layer == gameObject.layer

        
        //check if the other collider#s mesh material is the same shape
        if (collision.collider != this.GetComponent<Collider>() && name != collision.gameObject.name
            && collision.gameObject.layer == gameObject.layer) //without the name one they always collide immediately with themselves.
        {
            
            Debug.Log(name + " colliided with " + collision.gameObject.name);
//layers.

                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
                gManager.score += 100; //could do times a multiplier per different shape, set in a database.
                gManager.scoreText.text = "SCORE: " + gManager.score;
                
                int id = Random.Range(1, 5);
                gManager.GetComponent<AudioSource>().clip = Resources.Load(Path.Combine("Sounds", "Sound " + id)) as AudioClip;
                gManager.GetComponent<AudioSource>().Play();


        }

    }

//if so deactivate both objects
            //score ++, score different for different shapes. scriptable object database, shape prefab, score, spawn frequency.


    
}
