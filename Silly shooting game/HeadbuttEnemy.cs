using System;
using UnityEngine;

public class HeadbuttEnemy : EnemyMovement
{
    [SerializeField] float headbuttCooldown;
    private float lastHeadbutt;
    private bool isTouchingPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        lastHeadbutt = Time.time;
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

            CheckOutOfBounds(gameObject);
            if (moveRandom) moveLittleRandom(enemyRb);
            if (followPlayer) goToPlayer();
            if (punched && isThrowable) StartCoroutine("PunchEffect");
            if (facesPlayer) facePlayer(player);
            headbutt();
        }
        else Destroy(gameObject);
    }
    private void headbutt()
    {
        //if (Time.time - lastHeadbutt > headbuttCooldown)
        //{
        //    Debug.Log("Fun");
        //    lastHeadbutt = Time.time;
        //}
        if (Time.time - lastHeadbutt > headbuttCooldown)
        {
            if (!isTouchingPlayer)
            {
                followPlayer = true;
            }
            else followPlayer = false;
            lastHeadbutt = Time.time;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
        else isTouchingPlayer = false;
    }
}
