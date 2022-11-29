using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player; //sets a player game object 
    private Vector3 offset = new Vector3(0, 5, -8);
    void Start()
    {
        
    }

    void LateUpdate() //late update to remove stutter, because of the order that things update on the file
    {
        //sets and offsets the camera
        transform.position = player.transform.position + offset; //moves using the parameters of the aux object
    }
}
