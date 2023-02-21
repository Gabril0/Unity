using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField][Range(0, 100000)] float health;
    [SerializeField] float speed;
    [SerializeField] float boundsX, boundsY;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("SuperMaicao").GetComponent<PlayerMovement>(); //finds the player to get the current damage
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        //moveLeft();
        CheckOutOfBounds(gameObject);
    }

    private void OnTriggerEnter(Collider other) //comparisons between collisions
    {
        if (other.tag == "PlayerBullet") {
            health = health - player.power;
            Destroy(other.gameObject);
        }
        if (other.tag == "Parry" || other.tag == "PunchBox") {
            health = health - player.power * 1.5f;
        }
        if (other.tag == "ParriedBullet") {
            health = health - player.power * 10;
        }
        if (other.tag != "EnemyBullet")
            Debug.Log("other: " + other.tag);
    }


    private void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void moveLeft() { //temporary function for testing
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void CheckOutOfBounds(GameObject go) //need to optimize later
    {
        if (go.transform.position.x >= boundsX)
        {
            go.transform.position = new Vector3(boundsX, go.transform.position.y, go.transform.position.z);
        }
        if (go.transform.position.x <= -boundsX)
        {
            go.transform.position = new Vector3(-boundsX, go.transform.position.y, go.transform.position.z);
        }
        if (go.transform.position.y >= boundsY)
        {
            go.transform.position = new Vector3(go.transform.position.x, boundsY, go.transform.position.z);
        }
        if (go.transform.position.y <= -boundsY)
        {
            go.transform.position = new Vector3(go.transform.position.x, -boundsY, go.transform.position.z);
        }
    }
}
