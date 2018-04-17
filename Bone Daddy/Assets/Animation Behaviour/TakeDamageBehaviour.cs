using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //            int xDir = (player.transform.position - this.transform.position).x > 0 ? 1 : -1;  //knock in the direction away from the center of the enemy
        int xDir = (Player.Instance.MyRigidBody.velocity.x <= 0) ? 1 : -1; //knock in opposite direction he is moving

        Player.Instance.StartCoroutine(Player.Instance.Knockback(.4f, 2f, 100, new Vector3(xDir, 1, 0)));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}



        /*
    //this method is called when the player is damaged by something, and manages the knockback effect and invulnerability
    public IEnumerator Knockback(float knockDur, float invulnDur, float knockbackPwr, Vector3 knockbackDir)//IEnumerator allows you to have an event happen and the name is what is called in a coroutine (player.NAME OF IENUMERATOR)
    {
        knockbackFlag = true; // we're in a knockback, so set a flag that will disable controls
        MyAnimator.SetBool("takedamage", true);   //start the "take damage" animation
        normalGravScale = MyRigidBody.gravityScale;  //save the original gravity scale to a variable so that we can recover it after the knockback is over
        MyRigidBody.gravityScale = .2f;//slowly coming back down
        MyRigidBody.velocity = Vector2.zero;//stop all current movement
        float timer = 0; //initialize the timer that locks controls 
        InvulnTimer = invulnDur; //initialize the invulnerability timer

        //add an initial upward knock, then allow gravity to bring player down
        MyRigidBody.AddForce(new Vector3(knockbackDir.x * knockbackPwr, knockbackDir.y * knockbackPwr * 4, 0));

        //handle blinking
        blinkTimer = blinkTimerMax;

        while (InvulnTimer >= 0f) // when the timer portion or ** float knockDur ** is finished (again this is a decimal in seconds so use "f" at the end of a number) the ienumerator will be turned off again
        {
            //Debug.Log("time is " + timer);
            InvulnTimer -= Time.deltaTime;
            blinkTimer -= Time.deltaTime;

            if (knockDur <= timer) //once our "control lock" timer has reached the limit
            {
                MyAnimator.SetBool("takedamage", false); //stop the animation
                if (knockbackFlag) //if this is the first iteration where the timer has reached the limit
                {
                    MyRigidBody.gravityScale = normalGravScale;  //reset gravity and flag
                    knockbackFlag = false;

                }
                else //if the "control lock" timer has expired, but the "invuln timer" is still running, start blinking
                {
                    if (blinkTimer <= 0) //blink timer
                    {
                        //I guess since we can handle the blinking with a simple call - MySpriteRenderer.enabled = false/true, we can collapse the blinkmode flag
                        //blinkMode = !blinkMode; //toggle the blink mode

                        MySpriteRenderer.enabled = MySpriteRenderer.enabled ? false : true;

                        blinkTimer = blinkTimerMax; //reset the blink timer

                    }
                }
            }
            else
                timer += Time.deltaTime;

            //MyRigidBody.AddForce(new Vector3(knockbackDir.x * knockbackPwr, 0, transform.position.z));
            yield return 0;
        }

        //now our knockback AND invulnerability effect are finished
        //blinkMode = false;  //disable the blink mode
        MySpriteRenderer.enabled = true; //instead, let's just enable the sprite renderer

        //reset the invuln trigger

    }

    */
}
