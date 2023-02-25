using System.Collections;
using UnityEngine;

public class ShootThing : MonoBehaviour
{
    [SerializeField] float speed, pauseFrameParry;
    [SerializeField] [Range(0f,1f)]float lerpInterpolation;
    private Vector3 originalPosition;
    private bool parried;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        SendFlying();
        if (parried)
        {
            StartCoroutine("ParryEffect");
        }

    }


    private void SendFlying()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    private void CheckBounds()
    {
        if (transform.position.y > 5 || transform.position.y < -5 || transform.position.x > 10 || transform.position.x < -10)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other) //check collisions
    {
        if (other.CompareTag("Parry") && !other.CompareTag("PunchBox") && !gameObject.CompareTag("PlayerBullet"))
        {
            parried = true;
        }

        if (other.CompareTag("PunchBox") && !other.CompareTag("Parry") && !gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator ParryEffect()
    {
        gameObject.tag = "ParriedBullet";
        //the bullet goes back to the caster
        //when lerp = 1, goes back to caster, when 0 goes to the position that was parried and the path in between
        transform.localScale = new Vector3(1.2f,1.2f,1.2f); //makes the bullet larger for visual effect


        Time.timeScale = 0.1f; //pauses the game
        transform.position = Vector3.Lerp(transform.position, originalPosition, lerpInterpolation = lerpInterpolation + 0.1f);
        float pauseEndTime = Time.realtimeSinceStartup + pauseFrameParry; //makes an time and add the elapsed time to the desired pause time
        while (Time.realtimeSinceStartup < pauseEndTime) //when the real time is greater than the pause, the game unpause
        {
            yield return 0; //ending the pause
        }
        Time.timeScale = 1f;
        Destroy(gameObject); //destroy the game object
        parried = false;
        Debug.Log("GamePaused");

    }
}
