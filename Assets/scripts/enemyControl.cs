using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControl : MonoBehaviour
{
    public player_control player;
    public battleControl control;
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator anim;
    public float fightForce;
    bool isAttacked = false;

    float hurtCD = 0.2f;
    float hurtCDHelper;
    void Start()
    {

    }


    void Update()
    {
        situationCheck();
    }
    void situationCheck()
    {
        if (isAttacked && Time.time > hurtCDHelper)
        {
            isAttacked = false;
            anim.SetBool("hurting", false);
        }
    }

    public void isAttackedSet()
    {
        isAttacked = true;
        rb.velocity = new Vector2(fightForce * player.rb.transform.localScale.x, rb.velocity.y);
        anim.SetBool("hurting", true);
        hurtCDHelper = Time.time + hurtCD;
    }
}
