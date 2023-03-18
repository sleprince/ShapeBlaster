using UnityEngine;
using System.IO; //to use path.combine

public class ShapeManager : MonoBehaviour
{
    private GameManager gManager;
    private CharacterController shapeController;
    private Rigidbody rBody;


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        shapeController = GetComponent<CharacterController>();
        gManager = GameManager.GetInstance();

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

        Debug.Log("you pointed at" + this);

        rBody.AddRelativeForce(shapeController.transform.forward,
            ForceMode.Impulse);

    }

    void OnCollisionEnter(Collision collision)

    {

        int that = collision.gameObject.GetInstanceID();
        int thisOne = this.gameObject.GetInstanceID();

        //check if the other collider's mesh material is the same shape
        if (that != thisOne && collision.gameObject.layer == gameObject.layer) //without the name one they always collide immediately with themselves.
        {
            
            Debug.Log(name + " colliided with " + collision.gameObject.name);

                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
                gManager.score += 100; //could do times a multiplier per different shape, set in a database.
                gManager.scoreText.text = "SCORE: " + gManager.score;
                
                int id = Random.Range(1, 5);
                gManager.GetComponent<AudioSource>().clip = Resources.Load(Path.Combine("Sounds", "Sound " + id)) as AudioClip;
                gManager.GetComponent<AudioSource>().Play();

        }

    }

            //score ++, score different for different shapes. scriptable object database, shape prefab, score, spawn frequency.


    
}
