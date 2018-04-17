//THINGS TO FIX
    //Holding down a joystick button to do long jump 
    //Sliding forward when throw attacking before hitting ground 
    //Dash in the air animation
    //Controls on controller are snappier than keyboard
    //Able to interrupt dash and jump
    //Sticking to bottom of underside of blocks/objects
    //Jumping from sloped surfaces
    //Getting stuck on random spots and weird jumping issues
    //Can't jump or do certain actions when overlapping with enemy
    //build the game - camera shows off screen objects and cuts part of the health bar off
    //Player death - the current health dropping to 0 works well, but falling off the level you restart right away and lose all your control
    //Getting Hurt
        //Attach the knockback and invuln behaviour to an Animation Behaviour script on Take Damage
        //takedamage is now a bool that can be set to true or false (not a trigger since we won't be inputing it like jump or attack)
        //each enemy can then just call to the animation behaviour & set the player.Damage(however much you want them to damage you)
            //this would be the line you have to insert into the collider being entered/triggered: player.MyAnimator.SetBool("takedamage", true);
        //gets rid of the IEnumerator from the player
        //Take Damage Animation Behaviour
            //OnStateEnter
                //the knockback
                //invulnerable
                //pause controls
            //OnStateExit
                //invulnerable with timer
                //blinking sprite and timer
        //This avoids me setting this every time: StartCoroutine(player.Knockback(.4f, 2f, 100, new Vector3(xDir, 1, 0))); 


//THINGS TO ADD
    //"Canvas Scaler" - keep this in mind when changing the game resolution - ** make sure you have it set to the correct aspect ratio and the camera is positioned for that ratio
    //stamina bar - limits dash, slide, melee attack
    //ammo for weapon throw
    //possibly double jump
    //drop down from platform - down+jump
    //more health meters to represent 100 health (currently at 5)
    //create a hud design (vertical or horizontal bars) - create a single overlay to put as a layer on top of the bars, so they sit underneath peaking out and can change
    //story
    //better UI and HUD sprites
    //enemy diversity
    //game over screen

// here is a test at the end

/* 
 * you can also write comments like this
 * 
 * */
