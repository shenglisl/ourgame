
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using TMPro;
public class player_control : MonoBehaviour
{
    public  Rigidbody2D rb;
    public CinemachineVirtualCamera cinemachine_virtual_camera;
    public float speed;
    public float jumpForce;
    public float backwardForce;
    public float fightForce;
    [SerializeField] private Animator anim;
    public Animator animFightZone;
    public Animator sword;
    public LayerMask ground;
    public LayerMask enemy;
    public Transform groundCheck;
    public Transform fightZone;
    public Collider2D coll_down;
    public BoxCollider2D coll_up;
    bool isCrouch = false;
    public bool isFight = false;
    bool isGround = true;
    bool isHurt = false;
    bool isAttacked = false;
    bool isAccumulate = false;
    public bool isBigFight = false;
    private int extraJump;
    bool isDash = false;
    float dashTime = 2f;
    float fightTime = 1f;
    float dashTimeHelper;
    float fightTimeHelper;
    float dashCD = 1f;
    float fightCD = 1f;
    float attackCD = 1f;
    float dashCDHelper;
    float fightCDHelper;
    float hurtCDHelper;
    float attackCDHelper;
    float stdx;
    public  battleControl control;
    public enemyControl myEnemy;
    int keyFrameJ;
    int finalFrameJ;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cinemachine_virtual_camera.Follow = transform;
        stdx = transform.localScale.x;
    }

    void FixedUpdate()
    {
        situationCheck();
        attack();
        if(!isHurt && !isFight && !isBigFight)
            Movement();
        Switch_Anim();
     //   crouch();

    }
    void Update()
    {
        if (!isHurt && !isFight && !isBigFight)
        {
            jump();
            dash();
        }
        if (!isHurt)
        {
            fight();
        }


        hurtDebug();
        hurt();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy" && !isDash)
        {
            isAttacked = true;
        }
        else if (collision.gameObject.tag == "enemy" && isDash)
        {
            rb.velocity = new Vector2(-backwardForce * rb.transform.localScale.x, rb.velocity.y);
        }
        else
        {
            isAttacked = false;
        }
    }
    void situationCheck()
    {
        control.playerEnergyChange(0.0005f);
        if (control.playerExhaustedCheck())
        {
            isDash = false;
            anim.SetBool("dashing", isDash);
            animFightZone.SetBool("dashing", isDash);
            sword.SetBool("dashing", isDash);
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        anim.SetBool("isground", isGround);
        if (isDash)
            speed = 1000;
        else
            speed = 500;
        
    }

    void attack()
    {
        if(isBigFight || isFight)
        {
            rb.velocity = new Vector2(fightForce * rb.transform.localScale.x, rb.velocity.y);
        }

        if (Physics2D.OverlapCircle(fightZone.position, 1f, enemy) && Time.time > attackCDHelper)
        {
            if (isFight)
            {
                control.enemyBloodChange(-0.1f);
                myEnemy.isAttackedSet();
                attackCDHelper = Time.time + attackCD;
            }
            else if (isBigFight)
            {
                control.enemyBloodChange(-0.2f);
                myEnemy.isAttackedSet();
                attackCDHelper = Time.time + attackCD;
            }


        }

    }

    void hurtDebug()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            control.playerBloodChange(1);
        if(Input.GetKeyDown(KeyCode.X))
            control.enemyBloodChange(1f);
    }
    void hurt()
    {

        if (isAttacked && !isDash && Time.time>hurtCDHelper)
        {
            isHurt = true;
            hurtCDHelper = Time.time + 0.8f;
            anim.SetBool("attacked", isHurt);
            control.playerBloodChange(-1);
            rb.velocity = new Vector2(- backwardForce * rb.transform.localScale.x, rb.velocity.y);
            isAttacked = false;
        }
        else if(Time.time > hurtCDHelper)
        {
            isHurt = false;
            anim.SetBool("attacked", isHurt);
        }
    }
    void fight()
    {
        finalFrameJ = 0;
        if (Input.GetKey(KeyCode.J))
        {
            keyFrameJ++;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            finalFrameJ = keyFrameJ;
            Debug.Log("finalFrame = " + finalFrameJ);
            keyFrameJ = 0;
        }

        if (isFight)
        {
            if (Time.time > fightTimeHelper)
            {
                isFight = false;
                anim.SetBool("fighting", isFight);
                animFightZone.SetBool("fighting", isFight);
                sword.SetBool("fighting", isFight);

            }
        }
        else if (finalFrameJ > 0 && finalFrameJ < 40)
        {
            if (Time.time > fightCDHelper)
            {
                isFight = true;
                animFightZone.SetBool("fighting", isFight);
                sword.SetBool("fighting", isFight);
                anim.SetBool("fighting", isFight);
                fightTimeHelper = Time.time + fightTime;
                fightCDHelper = Time.time + fightCD;
            }
        }


        if (isBigFight)
        {
            if (Time.time > fightTimeHelper)
            {
                isBigFight = false;
                animFightZone.SetBool("bigfighting", isBigFight);
                sword.SetBool("bigfighting", isBigFight);

            }
        }
        else if (finalFrameJ > 40)
        {
            if (Time.time > fightCDHelper) 
            { 
                isBigFight = true;
                animFightZone.SetBool("bigfighting", isBigFight);
                sword.SetBool("bigfighting", isBigFight);
                fightTimeHelper = Time.time + fightTime;
                fightCDHelper = Time.time + fightCD;
            }
        }


        if (isAccumulate)
        {
            if (Time.time > fightTimeHelper)
            {
                isAccumulate = false;
                animFightZone.SetBool("accumulating", isAccumulate);
                sword.SetBool("accumulating", isAccumulate);
            }
        }
        if (keyFrameJ > 40)
        {
            if (!isAccumulate)
            {
                isAccumulate = true;
                animFightZone.SetBool("accumulating", isAccumulate);
                sword.SetBool("accumulating", isAccumulate);
            }
        }
        else
        {
            isAccumulate = false;
            animFightZone.SetBool("accumulating", isAccumulate);
            sword.SetBool("accumulating", isAccumulate);
        }
    }

    void dash()
    {
        if (isDash)
        {
            control.playerEnergyChange(-0.001f);
            if(Time.time > dashTimeHelper)
            {
                isDash = false;
                anim.SetBool("dashing", isDash);
                animFightZone.SetBool("dashing", isDash);
                sword.SetBool("dashing", isDash);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time > dashCDHelper)
            {
                isDash = true;
                anim.SetBool("dashing", isDash);
                animFightZone.SetBool("dashing", isDash);
                sword.SetBool("dashing", isDash);
                dashTimeHelper = Time.time + dashTime;
                dashCDHelper = Time.time + dashCD;
            }

        }

    }
    //crouching
    void crouch()
    {
        float crouch_switch;
        bool head_stuck;
        crouch_switch = Input.GetAxisRaw("Vertical");
        head_stuck = coll_up.IsTouchingLayers(ground);
        if (!isCrouch)
        {
            if (crouch_switch < 0 && anim.GetBool("jumping") == false && anim.GetBool("falling") == false)
            {
                isCrouch = true;
                anim.SetBool("crouching", true);
                coll_up.offset = new Vector2(coll_up.offset.x, -0.21f);
                coll_up.size = new Vector2(coll_up.size.x, coll_up.size.y/2);
            }   
        }
        else
        {
            if (crouch_switch > 0 && !head_stuck)
            {
                isCrouch = false;
                anim.SetBool("crouching", false);
                coll_up.offset = new Vector2(coll_up.offset.x, -0.1f);
                coll_up.size = new Vector2(coll_up.size.x, coll_up.size.y*2);
            }
        }

    }
    //horizontal moving
    void jump()
    {
        if (!isDash)
        {
            rb.gravityScale = 3;
            if (isGround)
            {
                extraJump = 1;
            }
            if (Input.GetButtonDown("Jump") && (extraJump > 0 || isDash))
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJump -= 1;
                anim.SetBool("jumping", true);
            }
            if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("jumping", true);
            }
        }
        else
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);
        }
    }
    void Movement()
    {
        
        float horizontal_move;
        float face_direction;
        //bool jump;
        
        horizontal_move = Input.GetAxis("Horizontal");
        face_direction = Input.GetAxisRaw("Horizontal");
        
        //jump = Input.GetButton("Jump");
        

       
        if(horizontal_move != 0)
        {
            rb.velocity = new Vector2(horizontal_move * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(face_direction));
        }
        if(face_direction != 0)
        {
            transform.localScale = new Vector3(face_direction * stdx, transform.localScale.y, transform.localScale.z);
            //canv.transform.localScale = new Vector3(face_direction, 1, 1);
        }
        /*
        if(jump && !is_crouch)
        {
            if(jump_limit != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
                jump_limit -= 1;
            }
            
        }*/
    }
 
    //animation switch
    void Switch_Anim()
    {
        anim.SetBool("idling", false);
        if (anim.GetBool("jumping"))
        {
            anim.SetBool("falling", false);
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (coll_down.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idling", true);
        }
    }
    //collect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            collision.tag = "null";
            Destroy(collision.gameObject);
            
        }
    }
}
