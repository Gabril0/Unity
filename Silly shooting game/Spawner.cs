using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnRate, spawnStart, objectSpeed;
    [SerializeField] GameObject thingToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSomething", 0, spawnRate);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSomething() {
        Instantiate(thingToSpawn, transform.position, transform.rotation);
    }

}
