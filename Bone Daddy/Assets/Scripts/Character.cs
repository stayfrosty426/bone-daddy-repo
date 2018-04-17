using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {


    

    [SerializeField]
    protected Transform bonePos;// Click Player and create an empty child. Move this empty child to the position you want the weapon to appear. Drag the empty child to "Bone Pos" in the player

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]//provides place in player inspector to drop in object and is given this title in the inspector "Bone Prefab"
    private GameObject bonePrefab;

    public bool Attack { get; set; }

    public Animator MyAnimator { get; private set; }

    // Use this for initialization

    public virtual void Start () {

        facingRight = true;
        MyAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ThrowBone(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(bonePrefab, bonePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Bone>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(bonePrefab, bonePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Bone>().Initialize(Vector2.left);
        }
    }
}
