using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisionManager : MonoBehaviour
{
    Rigidbody rBody;

    private float moveForce = 2.0f;

    public Rigidbody rb;
    public int clickForce = 2500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);


    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per custom
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) //if left mouse button held down.
            //0 is left, 1 is right, 2 is middle.
        {
            rBody.AddForce ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * moveForce, ForceMode.Impulse);


            // OnClick();
        }

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
            //check if the other collider#s mesh material is the same shape
            //if so deactivate both objects
            //score ++, score different for different shapes. scriptable object database, shape prefab, score, spawn frequency.
        }

    
}
