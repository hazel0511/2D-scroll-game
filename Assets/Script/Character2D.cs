using UnityEngine;
using System.Collections;

public class Character2D : MonoBehaviour 
{
	public float maxSpeed = 10.0f;
    //public float jumpForce = 800.0f;
    public float jumpForce = 600.0f;

    public bool airControl = true;

	bool facingRight;

	public LayerMask groundLayer;

	Transform groundCheck;  //太空人腳下那個圓形groundCheck
	float groundRadius;
	bool onGround;

	Animator anim;

	void Awake()
	{
		//get references
		groundCheck = transform.Find ("GroundCheck");
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () 
	{
		facingRight = true;
        groundRadius = 0.1f;
        onGround = false;
	}

	void FixedUpdate () 
	{
		//detect if the character is standing on the ground
		//bool OverlapCircle(point, rad, LayerMask)  圓心 半徑 layerMask->碰撞layer   回傳布林值
		//returns true if there's anything in "layerMask" exist inside a circle centers at "point" (Vector2) with radius="rad" (float)
		onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, groundLayer);

		//change the character animation by onGround state
		anim.SetBool("onGround", onGround);
	}

	public void Move(float movingSpeed, bool jump)  //給Characrer2DControl.cs呼叫用
	{
		//left / right moving actived only when the character is on the ground or air control is premitted
		if (onGround || airControl) 
		{
			//change the character animation by moving speed  改變動畫狀態
			anim.SetFloat("Speed", Mathf.Abs(movingSpeed));  ///動畫參數1

			//move the character
			//only change its velocity on x axis 
			GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			//flip the character image if player input direction is different with character's facing direction
			if (movingSpeed > 0 && !facingRight || movingSpeed < 0 && facingRight) Flip ();
		}

		//let character jump when it's on the ground and player hits jump button
		if (onGround && jump) 
		{
			anim.SetBool ("onGround", false);//變更animation   ///動畫參數2

			//make character jump by adding force
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpForce));  //Vector2(x,y)  y在2d為向上
		}
	}

	void Flip()
	{
		//reverse the direction
		facingRight = !facingRight;

		//flip the character by multiplying x local scale with -1
		Vector3 characterScale = transform.localScale;
		characterScale.x *= -1;               //可用transform.localScale的正負來判斷目前是左是右
		transform.localScale = characterScale;
	}
}
