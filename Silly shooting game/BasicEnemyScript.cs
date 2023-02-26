using UnityEngine;

public class BasicEnemyScript : EnemyMovement
//used in some enemies
{
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

            CheckOutOfBounds(gameObject);
            if (moveRandom) moveLittleRandom(enemyRb);
            if (followPlayer) goToPlayer();
            if (punched && isThrowable) StartCoroutine("PunchEffect");
            if (facesPlayer) facePlayer(player);
        }
        else Destroy(gameObject);
    }
}
