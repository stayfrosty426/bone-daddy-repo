using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character // allows you to inheret from another entire scripts (Mono Behaviour means it has its own script)
{


    //Stats
    public int curHealth; //ints can only be whole numbers (1, 2, 3) but floats can have decimals (1.2, 5.4, 99.2)
    public int maxHealth = 100;
    private float respawnTimer = 3f;
    private bool playerDeath = false;
    

    //better jump
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }

    }


    [SerializeField] //Jump behaviour
    private Transform[] groundPoints;

    [SerializeField] //Jump behaviour
    private float groundRadius;

    [SerializeField] //Jump behaviour
    private LayerMask whatIsGround;

    private bool isGrounded; //Jump behaviour

    [SerializeField] //Jump behaviour
    private float jumpForce;

    [SerializeField] //Jump behaviour
    private bool airControl;

    public Rigidbody2D MyRigidBody { get; set; }

    public bool Dash { get; set; }

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    private Vector2 startPos;

    //DASH STUFF
    public float dashSpeed;
    private float normalGravScale;
    //public bool dashControl;
    public bool alreadyDashed = false; // same as (!alreadyDashed) meaning you can freely dash, but dash in air = alreadyDashed=true and the input will see that you are no longer !alreadyDashed and thus will NOT allow you to dash again

    //slow stop on land
    public bool Land { get; set; }
    public float landingSpeed = 10f;


    //damage / knockback
    private bool knockbackFlag = false;
    public float InvulnTimer { get; set; }
    public int knockbackPwr;

    //for handling blinking
    public float blinkTimerMax = .08f;
    private float blinkTimer;
    public SpriteRenderer MySpriteRenderer { get; set; }
    private Color originalColor;

    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    //Use this for initialization
    public override void Start ()
    {
        startPos = transform.position;
        MyRigidBody = GetComponent<Rigidbody2D>();//get component will grab whatever is attached to the component that the script is attached to. Meaning, if you have this script on the player, and the player has a RigidBody2D on it, here you would reference it and can then call it something else to be used within this script
        curHealth = maxHealth; // when you start, you have full health
        base.Start();

        MySpriteRenderer = GetComponent<SpriteRenderer>();  //get a reference to the sprite renderer
        originalColor = MySpriteRenderer.color; //and save the original sprite color to return to it after blinking. 
	}

    private void Update()
    {
        //if (transform.position.y <= -14f)
        //{
           // MyRigidBody.velocity = Vector2.zero;
           // transform.position = startPos;
       // }
        
        if (curHealth > maxHealth)//means your current health can not exceed max health
        {
            curHealth = maxHealth;
        }
        
        //Player Death
        if (curHealth <= 0 || gameObject.transform.position.y < -7)
        {
            MyAnimator.SetBool("death", true);
            playerDeath = true;
            Die();
        }
        //if (gameObject.transform.position.y < -7)
       // {
            //curHealth = 0;
        //}

        //freezing of input (buttons)
        if(!knockbackFlag && !playerDeath)//disable input if player is being knocked back
            HandleInput();

        //better jump
        if (rb.velocity.y < 0)
        {
           rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    private void Die()
    {
        //Level Restart
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            MyAnimator.SetBool("death", false);
            playerDeath = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();      
	}

    private void HandleMovement(float horizontal)
    {
        

        if (MyRigidBody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide && !Dash && (OnGround || airControl) && !knockbackFlag && !playerDeath)// means you can't move during the !EVENT
        {          
                MyRigidBody.velocity = new Vector2(horizontal * movementSpeed, MyRigidBody.velocity.y);

        }

        if (Land && Attack)  //if we are in the process of landing
        {
            MyRigidBody.AddForce(new Vector2(-1 * MyRigidBody.velocity.x * landingSpeed, 0)); //add a small force in the opposite direction that the player is moving
            if ((MyRigidBody.velocity.x == 0))       //or our player has stopped moving

            {
                Land = false; //consider the landing to be complete
            }
        }

        if (Jump && MyRigidBody.velocity.y == 0 && !Dash)
        {
            MyRigidBody.AddForce(new Vector2(0, jumpForce));//causes you to jump 0=x axis, jumpFroce=yaxis

        }

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));


    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Attack"))
        {
            MyAnimator.SetTrigger("attack");
        }


        //Slide
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetButtonDown("Slide")) && Input.GetAxis("Horizontal") == 1)
        {
            MyAnimator.SetTrigger("slide");
        }
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetButtonDown("Slide")) && Input.GetAxis("Horizontal") == -1)
        {
            MyAnimator.SetTrigger("slide");
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Throw"))
        {
            MyAnimator.SetTrigger("throw");
            // DONT NEEED THIS ANYMORE ----> ThrowBone(0);//throws the object from the player position
        }
        //Dash
        if ((Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetButtonDown("Dash")) && Input.GetAxis("Horizontal") == 1 && (OnGround || !OnGround) && !alreadyDashed)//!alreadyDashed means we can no longer dash in the air/controls that
        {
            MyAnimator.SetTrigger("dash");
        }
        if ((Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetButtonDown("Dash")) && Input.GetAxis("Horizontal") == -1 && (OnGround || !OnGround) && !alreadyDashed)
        {
            MyAnimator.SetTrigger("dash");
        }

    }

    

    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRight || horizontal < 0 && facingRight) && !(Slide || Dash || knockbackFlag || playerDeath))//locks your sprite to move int he direction of the input/your sprite image flips properly or doesn't flip when you don't want it to
        {
            ChangeDirection();
        }

    }

    private bool IsGrounded()
    {
        if (MyRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i ++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        alreadyDashed = false; //reset alreadyDashed, since we're on the ground now

                        return true;
                    }
                }
            }
        }
        return false;
        
    }

    private void HandleLayers()
    {

        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);  
        } else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
                    
    }

    public override void ThrowBone(int value)//generates the Bone object/prefab "Bone" needs to be exact spelling of object/prefab
    {

        if (!OnGround && value == 1 || OnGround && value == 0)//Ties to the point in an animation where you want it to show up
        {
            base.ThrowBone(value);
        } 
    }

    public void StartDash()
    {
        alreadyDashed = true;

        if (!OnGround)
        {
            Jump = false;
        }

        normalGravScale = MyRigidBody.gravityScale;
        //these two lines allow you to float/ignore the gravity
        MyRigidBody.gravityScale = 0;

        int leftOrRight;

        //possible way to write below code: leftOrRight = (MyRigidBody.velociy.x > 0)? 1: -1;

        /*if (MyRigidBody.velocity.x == 0 && facingRight)//this would be for a button press for dash instead of moving in a direction to be able to dash and you can then remove the movementSpeed from the Vector2 below
        {
            leftOrRight = 1; //go right
        } else if (MyRigidBody.velocity.x == 0 && !facingRight)
        {
            leftOrRight = -1; //go left
        }*/

            if (MyRigidBody.velocity.x > 0) 
        {
            leftOrRight = 1; //go right
        } else
        {
            leftOrRight = -1; //go left
        }

        //now let's force the player's velocity to be the "dash speed" in the correct direction
        MyRigidBody.velocity = new Vector2(leftOrRight * movementSpeed * dashSpeed, 0);

    }
    public void EndDash()
    {
        MyRigidBody.gravityScale = normalGravScale;     //resets the gravity 

    }

    public void Damage(int dmg)//means that the Damage function 
    {
        curHealth -= dmg;//this means Damage(int dmg) is called, the player's current health will go down by the specified amount from the object doing the damage
        //this would be done as ** player.Damage(3); ** as the integer needs to be set as a whole number when you call it

    }

    
    //this method is called when the player is damaged by something, and manages the knockback effect and invulnerability
    public IEnumerator Knockback(float knockDur, float invulnDur, float knockbackPwr, Vector3 knockbackDir)//IEnumerator allows you to have an event happen and the name is what is called in a coroutine (player.NAME OF IENUMERATOR)
    {     

        knockbackFlag = true; // we're in a knockback, so set a flag that will disable controls
        normalGravScale = MyRigidBody.gravityScale;  //save the original gravity scale to a variable so that we can recover it after the knockback is over
        MyRigidBody.gravityScale = .2f;//slowly coming back down
        MyRigidBody.velocity = Vector2.zero;//stop all current movement
        float timer = 0; //initialize the timer that locks controls 
        InvulnTimer = invulnDur; //initialize the invulnerability timer

        //add an initial upward knock, then allow gravity to bring player down
        MyRigidBody.AddForce(new Vector3(knockbackDir.x * knockbackPwr, knockbackDir.y * knockbackPwr * 4 , 0));

        //handle blinking
        blinkTimer = blinkTimerMax;

        while (InvulnTimer >= 0f) // when the timer portion or ** float knockDur ** is finished (again this is a decimal in seconds so use "f" at the end of a number) the ienumerator will be turned off again
        {
            //Debug.Log("time is " + timer);
            InvulnTimer -= Time.deltaTime;
            blinkTimer -= Time.deltaTime;

            if ( knockDur <= timer) //once our "control lock" timer has reached the limit
            {
                MyAnimator.SetBool("takedamage", false); //stop the animation
                if (knockbackFlag) //if this is the first iteration where the timer has reached the limit
                {
                    MyRigidBody.gravityScale = normalGravScale;  //reset gravity and flag
                    knockbackFlag = false;

                } else //if the "control lock" timer has expired, but the "invuln timer" is still running, start blinking
                {
                    if (blinkTimer <= 0) //blink timer
                    {
                        //I guess since we can handle the blinking with a simple call - MySpriteRenderer.enabled = false/true, we can collapse the blinkmode flag
                        //blinkMode = !blinkMode; //toggle the blink mode

                        MySpriteRenderer.enabled = MySpriteRenderer.enabled ? false : true;

                        blinkTimer = blinkTimerMax; //reset the blink timer

                    }
                }
            } else
                timer += Time.deltaTime;

            //MyRigidBody.AddForce(new Vector3(knockbackDir.x * knockbackPwr, 0, transform.position.z));
            yield return 0;
        }

        //now our knockback AND invulnerability effect are finished
        //blinkMode = false;  //disable the blink mode
        MySpriteRenderer.enabled = true; //instead, let's just enable the sprite renderer

        //reset the invuln trigger

    }



}
