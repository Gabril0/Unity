using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : EnemyMovement

{
    [SerializeField] GameObject enemy;
    [SerializeField] float repeatRate;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("SuperMaicao");
        playerScript = player.GetComponent<PlayerMovement>(); //finds the player to get the current damage
        enemyRb = GetComponent<Rigidbody>();

        offset = new Vector3(transform.position.x, -2, transform.position.z);
        InvokeRepeating("SpawnEnemy",1,repeatRate);
    }

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
    private void SpawnEnemy() {
        Instantiate(enemy, transform.position - offset, transform.rotation);
    }
}
