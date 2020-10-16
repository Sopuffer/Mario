using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public int life = 3;
    public float trappedTimer = 0;
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


    public bool PlayerDefeated;
    public bool OnGround;
    public bool GoingLeft;
    public bool GoingRight;
    public bool IsJumping;
    public bool CanDoubleJump;
    public bool rightJumpAllowed;
    public bool leftJumpAllowed;
    public bool GameWon;
    public bool trapped;
    bool RightButtonActive;
    bool LeftButtonActive;
    

    string RightWall = "RightWall";
    string LeftWall = "LeftWall";
    string Pole = "Pole";
    string Ground = "Ground";
    string Trap = "Trap";
    string Enemy = "Enemy";

    private void Awake()
    {
        accelpersec = MaxSpeed / TimeZeroToMax;
        decelpersec = -MaxSpeed / TimeMaxToZero;
        velocity = 0.0f;
        life = 3;
        
    }
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        rightdirtimer = CoolDownTimer;
        leftdirtimer = CoolDownTimer;
    }

    void Update()
    {
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

        if (trapped)
        {
            trappedTimer += Time.deltaTime;
        }
        else
        {
            trappedTimer = 0.0f;
        }

        PlayerDoubleJump();
        WallJump();
        PlayerLosesLife();
       
    }
    void WallJump()
    {
        if (rightJumpAllowed && IsJumping)
        {
            rb.velocity = new Vector2(jumpspeed, jumpspeed);
        }
        if (leftJumpAllowed && IsJumping)
        {
            rb.velocity = new Vector2(-jumpspeed, jumpspeed);
        }
    }

    void PlayerLosesLife()
    {
        if(trappedTimer >= 2)
        {
            life--;
            trappedTimer = 0.0f;
        }
        if(life == 0)
        {
            PlayerDefeated = true;
        }
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

        if (coll.gameObject.CompareTag(Ground))
        {
            rb.velocity = new Vector2(0, 0);
            OnGround = true;
        }
        if (coll.gameObject.CompareTag(Pole))
        {
            GameWon = true;
        }

        if (coll.gameObject.CompareTag(LeftWall))
        {
            rightJumpAllowed = true;
        }
        if (coll.gameObject.CompareTag(RightWall))
        {
            leftJumpAllowed = true;
        }

        if (coll.gameObject.CompareTag(Enemy))
        {
            trapped = true;
        }

    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(Ground))
        {
            OnGround = false;
            CanDoubleJump = true;
        }
        if (coll.gameObject.CompareTag(LeftWall))
        {
            rightJumpAllowed = false;
        }
        if (coll.gameObject.CompareTag(RightWall))
        {
            leftJumpAllowed = false;
        }

        if (coll.gameObject.CompareTag(Enemy))
        {
            trapped = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag(Trap))
        {
            trapped = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag(Trap))
        {
            trapped = false;
        }
    }
}
