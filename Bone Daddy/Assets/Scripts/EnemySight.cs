using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    void OnTriggerEnter2D(Collider2D other) //when entering sight of enemy == add empty child to enemy, add 2d box collider, set it to area you want it to detect
    {
        if (other.tag == "Player")
        {
            enemy.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)//when exiting sight of enemy
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
