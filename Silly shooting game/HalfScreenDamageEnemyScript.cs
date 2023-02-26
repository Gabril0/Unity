using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfScreenDamageEnemyScript : EnemyMovement
{
    [SerializeField] GameObject bullet;
    [SerializeField] float spawnRate;
    private bool isSpawned = false;
    private GameObject spawnedBullet;
    private GameObject flash;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("SuperMaicao");
        playerScript = player.GetComponent<PlayerMovement>(); //finds the player to get the current damage
        enemyRb = GetComponent<Rigidbody>();
        InvokeRepeating("SpawnSomething", 0, spawnRate);
        flash = GameObject.Find("Flash");
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
            if (isSpawned) DestroyBullet();
        }
        else Destroy(gameObject);
    }

    private void SpawnSomething()
    {
        flash.SetActive(false);
        Vector3 offset = new Vector3(transform.position.x, -1f, transform.position.z);
        spawnedBullet = Instantiate(bullet, offset, transform.rotation);
        isSpawned = true;
    }
    private void DestroyBullet() {
        
        Destroy(spawnedBullet, 0.5f);
        isSpawned = false;
        flash.SetActive(true);
    }
}

