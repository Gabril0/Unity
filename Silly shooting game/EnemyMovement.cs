using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class EnemyMovement : EnemyEssentials
{
    private PlayerMovement playerScript;
    private GameObject player;
    private Rigidbody enemyRb;
    private bool punched = false;

    [SerializeField] bool moveRandom, followPlayer;
    [SerializeField] float punchSlowdown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("SuperMaicao");
        playerScript = player.GetComponent<PlayerMovement>(); //finds the player to get the current damage
        enemyRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.gameIsOver)
        {
            checkHealth();
            facePlayer(player);
            CheckOutOfBounds(gameObject);
            if(moveRandom) moveLittleRandom(enemyRb);
            if(followPlayer) goToPlayer();
            if(punched) StartCoroutine("PunchEffect");
        }
        else Destroy(gameObject);
        
    }
    private void OnTriggerEnter(Collider other) //comparisons between collisions
    {
        if (other.tag == "PlayerBullet")
        {
            health = health - playerScript.power;
            Destroy(other.gameObject);
        }
        if (other.tag == "Parry" || other.tag == "PunchBox")
        {
            health = health - playerScript.power * 1.5f;
            punched = true;
        }
        if (other.tag == "ParriedBullet")
        {
            health = health - playerScript.power * 10;
        }
        if (other.tag != "EnemyBullet")
            Debug.Log("other: " + other.tag);
    }

    IEnumerator PunchEffect()
    {
        
        Time.timeScale = 0.01f; //pauses the game
        enemyRb.AddRelativeForce(Vector3.down * Time.deltaTime * 7500, ForceMode.Impulse);
        float pauseEndTime = Time.realtimeSinceStartup + punchSlowdown; //makes an time and add the elapsed time to the desired pause time
        while (Time.realtimeSinceStartup < pauseEndTime) //when the real time is greater than the pause, the game unpause
        {
            yield return 0; //ending the pause
        }
        Time.timeScale = 1f;
        punched = false;
    }
}
