using System.Collections;
using UnityEngine;

public class EnemyMovement : EnemyEssentials
{
    protected PlayerMovement playerScript;
    protected GameObject player;
    protected Rigidbody enemyRb;
    protected bool punched = false;

    [SerializeField] protected bool moveRandom, followPlayer, facesPlayer, isThrowable;
    [SerializeField] float punchSlowdown;
    protected void OnTriggerEnter(Collider other) //comparisons between collisions
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
