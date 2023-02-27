using UnityEngine;

public class EnemyEssentials : MonoBehaviour
{
    [SerializeField] float boundsX = 9, boundsY = 5;
    [Range(0, 100000)][SerializeField] protected float health;
    [Range(0.001f, 0.1f)][SerializeField] protected float moveTowardSpeed;
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float shotPower;
    [SerializeField] protected float moveRate;
    private float timePassed = 0f;

    //base functions
    protected void checkHealth()
    { //kills enemy when health goes to 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void facePlayer(GameObject player)
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
    } //makes the enemy always face the Player

    protected void CheckOutOfBounds(GameObject go) //need to optimize later
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

    //shot speed determine how hard is to escape the enemy bullets
    //all movement types

    protected void moveLittleRandom(Rigidbody enemyRb) //moves to a random location not to far away from the original position, use it in update
    {
        timePassed += Time.deltaTime;
        if (timePassed > moveRate)
        {
            Vector3 randomMovement = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            enemyRb.AddRelativeForce(randomMovement * Time.deltaTime * enemySpeed, ForceMode.VelocityChange);
            timePassed = 0f;
        }
    }

    protected void goToPlayer()
    { //moves the enemy to the player
        transform.Translate(Vector3.up * Time.deltaTime * enemySpeed * moveTowardSpeed); //considering that the enemy is facing the player
    }
}
