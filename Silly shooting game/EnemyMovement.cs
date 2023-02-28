using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (other.CompareTag("PlayerBullet"))
        {
            health = health - playerScript.power;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Parry") || other.CompareTag("PunchBox"))
        {
            health = health - playerScript.power * 1.5f;
            punched = true;
        }
        if (other.CompareTag("ParriedBullet"))
        {
            health = health - shotPower * 10;
        }
        if (!other.CompareTag("EnemyBullet"))
            Debug.Log("other: " + other.tag);
    }

    IEnumerator PunchEffect()
    {
        if ((health - playerScript.power * 1.5f) <= 0) yield return null;
        Time.timeScale = 0.01f; //pauses the game
        //enemyRb.AddRelativeForce(Vector3.down * Time.deltaTime * 7500, ForceMode.Impulse);
        transform.Translate(Vector3.down * Time.deltaTime * 2000);

        float pauseEndTime = Time.realtimeSinceStartup + punchSlowdown; //makes an time and add the elapsed time to the desired pause time
        while (Time.realtimeSinceStartup < pauseEndTime) //when the real time is greater than the pause, the game unpause
        {
            yield return 0; //ending the pause
        }

        Time.timeScale = 1f;
        punched = false;
    }
}
