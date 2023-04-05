using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Change from translate to addforce
    //Collision bugs with the head
    //Change base speed to something lower

    private float horizontalMovement, secondTouched, baseSpeed, maxSpeed, lastJumpTime;
    private bool isJumpBuffered = false;
    [SerializeField] float valueBoost = 2f, jumpTiming = 0.2f, extraSpeedTime = 1.5f, jumpBufferTime;

    [SerializeField] bool isOnGround = false;
    [SerializeField] float speed = 2, jumpHeight = 10;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = speed;
        maxSpeed = baseSpeed * 2f;
    }


    void Update()
    {
        Move();
    }

    public void Move() {
        horizontalMovement = Input.GetAxis("Horizontal");

        if (Time.time - secondTouched > extraSpeedTime && isOnGround) { speed = baseSpeed; }

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpBuffered = true;
            lastJumpTime = Time.time;
        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space) || isJumpBuffered && Time.time - lastJumpTime <= jumpBufferTime && isOnGround) {
            if (Time.time - secondTouched < jumpTiming) {
                if (speed >= maxSpeed) { speed = maxSpeed; }
                else speed = speed + valueBoost; }
            else speed = baseSpeed;
            rb.AddForce(new Vector2(0f,jumpHeight),ForceMode2D.Impulse);
            isOnGround = false;
            isJumpBuffered = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jumpable"))
        {
            secondTouched = Time.time;
            isOnGround = true;
        }
    }

}
