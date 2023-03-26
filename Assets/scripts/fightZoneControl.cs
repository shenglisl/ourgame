using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightZoneControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public player_control player;
    public Animator animFightZone;
    public Animator sword;
    void stop()
    {
        rb.velocity = new Vector2(0f, 0f);
        player.isBigFight = false;
        player.isFight = false;
        animFightZone.SetBool("fighting", false);
        animFightZone.SetBool("bigfighting", false);
        sword.SetBool("fighting", false);
        sword.SetBool("bigfighting", false);
    }
}
