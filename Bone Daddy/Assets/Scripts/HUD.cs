using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    //create an array - a variable where you can store multiple sprites or objects in one sprite variable
    public Sprite[] HeartSprites;

    public Image HeartUI;

    private Player player;


     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

     void Update()
    {
        if (player.curHealth >= 0)
        {
            
            HeartUI.sprite = HeartSprites[player.curHealth]; // Camera > HUD Script > Heart Sprites - the element # represents the amount in your health bar
            //if you have full health (5) then element 5 should have a sprite that shows a FULL  health bar, where element 0 (which means your health goes down) shoudl be empty

        }
                   
    }

    
}
