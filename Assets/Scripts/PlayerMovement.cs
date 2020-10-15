using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 jump;
    Vector2 reversejump;

    GameObject Floor;
    

    public float CoolDownTimer;
    public float rightdirtimer;
    public float leftdirtimer;
    public float normalspeed;

    public float MaxSpeed;
    public float TimeZeroToMax;
    public float TimeMaxToZero;
    public float jumpspeed;
    float accelpersec;
    float decelpersec;
    
    float velocity;
    public bool HasCollided;
    public bool OnGround;
    public bool GoingLeft;
    public bool GoingRight;
    public bool IsJumping;
    public bool InAir;
    public bool CanDoubleJump;

    bool RightButtonActive;
    bool LeftButtonActive;

    public bool GameWon;

    private void Awake()
    {
        accelpersec = MaxSpeed / TimeZeroToMax;
        decelpersec = -MaxSpeed / TimeMaxToZero;
        velocity = 0.0f;
        
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        //Floor = GameObject.FindGameObjectWithTag("Ground");
        rightdirtimer = CoolDownTimer;
        leftdirtimer = CoolDownTimer;
    }

    void Update()
    {
        //if (OnGround)
        //{
        //    rb.velocity = new Vector2(0, 0);
        //}
        ////else
        ////{
        ////    CanDoubleJump = true;
        ////}
        UserInput();

        if (OnGround)
        {
            if (RightButtonActive)
            {
                GoingRight = true;
                MoveRight();
            }
            if (LeftButtonActive)
            {
                GoingLeft = true;
                MoveLeft();
            }

            if (IsJumping)
            {
                PlayerJump();
            }

            PlayerDecelerate();
        }

        PlayerDoubleJump();
       
    }

    void UserInput()
    {
        KeyCode Jump = KeyCode.Space;
        KeyCode WalkRight = KeyCode.D;
        KeyCode WalkLeft = KeyCode.A;
        
        if (Input.GetKey(WalkRight))
        {
            RightButtonActive = true;

        }
        else
        {
            RightButtonActive = false;
        }

        if (Input.GetKey(WalkLeft))
        {
            LeftButtonActive = true;
        }
        else
        {
            LeftButtonActive = false;
        }

        if (Input.GetKeyDown(Jump))
        {
            IsJumping = true;
        }
        else
        {
            IsJumping = false;
        }
    }

    void PlayerDecelerate()
    {

        if (GoingRight && !RightButtonActive)
        {
            Accelerate(decelpersec);
            rb.velocity = new Vector2(normalspeed*velocity, 0);
            rightdirtimer -= Time.deltaTime;

            if (rightdirtimer <= 0)
            {
                GoingRight = false;
                rightdirtimer = CoolDownTimer;
                if (rb.velocity.x != 0)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }

        }

        if (GoingLeft && !LeftButtonActive)
        {
            Accelerate(decelpersec);
            rb.velocity = new Vector2(-normalspeed * velocity, 0);
            leftdirtimer -= Time.deltaTime;

            if (leftdirtimer <= 0)
            {
                GoingLeft = false;
                leftdirtimer = CoolDownTimer;

                if (rb.velocity.x != 0)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }
    }

    void MoveRight()
    {
        Accelerate(accelpersec);
        rb.velocity = new Vector2(2*velocity, 0);
        rightdirtimer = CoolDownTimer;
    }
    void MoveLeft()
    {
        Accelerate(accelpersec);
        rb.velocity = new Vector2(-2*velocity, 0);
        leftdirtimer = CoolDownTimer;

    }

    void PlayerJump()
    {
        
            rb.velocity = new Vector2(0, jumpspeed);
            OnGround = false;

            if (RightButtonActive)
            {
                rb.velocity = new Vector2(jumpspeed, jumpspeed);

            }
            if (LeftButtonActive)
            {
                rb.velocity = new Vector2(-jumpspeed, jumpspeed);

            }
        CanDoubleJump = true;
   
    }

    void PlayerDoubleJump()
    {
        if(CanDoubleJump && IsJumping)
        {
            CanDoubleJump = false;
            rb.velocity = new Vector2(0, jumpspeed);
            OnGround = false;

            if (RightButtonActive)
            {
                rb.velocity = new Vector2(jumpspeed, jumpspeed);

            }
            if (LeftButtonActive)
            {
                rb.velocity = new Vector2(-jumpspeed, jumpspeed);

            }

        }

    }

    public void Accelerate(float Accel)
    {
        velocity += Accel * Time.deltaTime;
        velocity = Mathf.Clamp(velocity, 0, MaxSpeed);
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            rb.velocity = new Vector2(0, 0);
            OnGround = true;
        }
        if (coll.gameObject.tag == "Pole")
        {
            GameWon = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            OnGround = false;
            CanDoubleJump = true;
        }
        
    }

}
