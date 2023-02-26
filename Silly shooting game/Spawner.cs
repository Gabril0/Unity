using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnRate, spawnStart, objectSpeed;
    [SerializeField] GameObject thingToSpawn;
    private ObjectPool<GameObject> _pool;
    // Start is called before the first frame update
    void Start()
    {
        _pool = new ObjectPool<GameObject>(
            () =>
            { return Instantiate(thingToSpawn, transform.position, transform.rotation); },
            GameObject =>
            {
                thingToSpawn.gameObject.SetActive(true);
            },
            GameObject => {
                thingToSpawn.gameObject.SetActive(false);
            },
            GameObject => { Destroy(thingToSpawn.gameObject); },
            false, //check later
            50,100);
        InvokeRepeating("SpawnSomething", 2, spawnRate);


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnSomething()
    {
        _pool.Get();//Instantiate(thingToSpawn, transform.position, transform.rotation);
    }

}
