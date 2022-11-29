using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour //MonoBehaviour is for the unity library, ":" it's like extends in java
{
    public float speed = 30f; //public variables are can be changed in the editor itself
    public float turnSpeed = 35f;
    public float horizontalIn;
    public float forwardIn;
    void Start()
    {
        
    }

    void Update()
    {
        horizontalIn = Input.GetAxis("Vertical");
        forwardIn = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * horizontalIn); //transform with lower case because this refers to the object itself,
                                                               //the "translate" is for the (X,Y,Z) axys
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * forwardIn); //turning the car by using rotation
        //it could also be: transform.Translate(Vector3.forward);
    }
}
