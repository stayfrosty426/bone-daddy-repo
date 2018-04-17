//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Player : MonoBehaviour {}
    //Uses its own script. The script ITSELF can not be accessed by others and doesn't pull the entirety from other scripts
//public class Player : PARENT abstract class {}
    //Allows you to inheret from that PARENT anstract class script. You can assign things to the "Character : MonoBehaviour" script so others can use its functions.
//public abstract class PARENT : MonoBehaviour {}
    //Serves as the parent script for others to pull from. For example, if I had enemies and players share basic functions, put them here.
    //public virtual void Name () {} and public override void Name () { base.Name; THEN REST OF THE FUNCTION }
    //This means the Character has a function that will be placed on the script referencing it, but that script can override that function and add something else to it
    //Example: public virtual void Startup ---> public override void Startup { base.Startup; REST OF FUNCTION }
    //Whatever is in the abstract class, that script referncing will get it. Think about what you want to place in the abstract class. What is necessary and what can go on an individual script.
    //Might be easiest/best to just put things you know whoever inherets from the abstract class will need without changing. Put specific stuff onto the inheriting script then.
    //If adding to something in an inheriting script, put "base.Name" after the additional functions you want. You could even build base.Name into a conditional statement.
    //You could even call the abstract function in an area elsewhere.
    //Example: abstract-public void Test(){}. Then in inheriting script-public void Die(){ Test():}

//void Awake() {}
    //This means when the level boots up, these things will happens right away
//void Start () {}
    // Use this for initialization
//void Update () {}
    //This means these actions change or are updated each moment of the game





//JUMP ANIMATION
    // If you only want 1 frame: right-click sprite, Create, Animation, rename to "Jump"
