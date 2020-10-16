using UnityEngine;

public class Flag : MonoBehaviour
{
    GameObject Player;
    GameObject Pole;
    PlayerMovement pm;
    Rigidbody2D rb;

    float poleOffset = 1;
    public bool theGameIsComplete;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pm = Player.GetComponent<PlayerMovement>();
        Pole = GameObject.FindGameObjectWithTag("Pole");
      
    }

    void FixedUpdate()
    {
        if (pm.GameWon)
        {
            FlagMovement();
        }
    }

    void FlagMovement()
    {
        if (transform.position.y > Pole.transform.position.y-poleOffset)
        {
            rb.velocity = new Vector2(0, -2);

        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            theGameIsComplete = true;
        }
    }
}
