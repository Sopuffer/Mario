using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    GameObject Player;
    PlayerMovement pm;
    Rigidbody2D rb;
    Rigidbody2D playerrb;
    public bool TheGameIsComplete;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pm = Player.GetComponent<PlayerMovement>();
        playerrb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pm.GameWon)
        {
            FlagMovement();
        }
    }

    void FlagMovement()
    {
        if (transform.position.y > -2.2)
        {
            rb.velocity = new Vector2(0, -2);

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            TheGameIsComplete = true;
        }
    }
}
