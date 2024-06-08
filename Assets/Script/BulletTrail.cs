using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour 
{
	public int shootingSpeed = 150;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//moving the bullet to the right of its local coordinate
		transform.Translate (Vector3.right * Time.deltaTime * shootingSpeed);

		//destory the shot bullet after 1 sec
		Destroy (this.gameObject, 1);
	}
}
