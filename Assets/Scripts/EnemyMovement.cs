using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 1;
    float secondtimer = 2;
    float leftWalkTimer;
    float rightWalkTimer;
    float standstillTimer;

    public bool WalkingRight;
    public bool WalkingLeft;
    public bool StandingStill;

    string Ground = "Ground";
    void Awake()
    {
        standstillTimer = timer;
        rightWalkTimer = secondtimer;
        leftWalkTimer = secondtimer;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WalkingRight = true;
    }


    void Update()
    {
        if (WalkingRight)
        {
            rightWalkTimer -= Time.deltaTime;

        }
        if (WalkingLeft)
        {
            leftWalkTimer -= Time.deltaTime;
        }

        if (StandingStill)
        {
            standstillTimer -= Time.deltaTime;
        }


        if (rightWalkTimer <= 1)
        {
            StandingStill = true;
            if(rightWalkTimer <= 0 && standstillTimer <= 0)
            {
                WalkingRight = false;
                StandingStill = false;
                rightWalkTimer = secondtimer;
                standstillTimer = timer;
                WalkingLeft = true;
            }
        }
        if ( leftWalkTimer <= 1)
        {
            StandingStill = true;
            if (leftWalkTimer <= 0 && standstillTimer <= 0)
            {
                WalkingLeft = false;
                StandingStill = false;
                leftWalkTimer = secondtimer;
                standstillTimer = timer;
                WalkingRight = true;
            }
        }


        EnemyWalking();

    }

    void EnemyWalking()
    {
        if (WalkingRight && !StandingStill)
        {
            rb.velocity = new Vector2(2, 0);
        }
        if (WalkingLeft && !StandingStill)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        if (StandingStill && WalkingRight || StandingStill && WalkingLeft)
        {
            rb.velocity = new Vector2(0, 0);
 
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(Ground))
        {
            rb.velocity = new Vector2(0, 0);
        }

    }
}
