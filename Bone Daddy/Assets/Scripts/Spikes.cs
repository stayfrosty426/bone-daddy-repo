using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        if (col.CompareTag("Player") && Player.Instance.InvulnTimer <= 0) // we check that we have collided with the player AND that the player is not invuln
        {
            
            player.Damage(3);

            //the rest of the code for handling knockbacks is currently split between Player.cs coroutine and TakeDamageBehaviour.cs
            player.MyAnimator.SetBool("takedamage", true);

        }
    }
}
