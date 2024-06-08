using UnityEngine;
using System.Collections;

public class LaserGun : MonoBehaviour 
{
	public GameObject bulletPrefab;
	public GameObject shotPoint;

	public LayerMask shootableLayer;   //指定被消滅敵人的layer

	float cooldown;
	float cdtime;
	
	// Update is called once per frame
	void Update () 
	{
		if (cdtime > 0)	cdtime -= Time.deltaTime;

		if (cdtime < 0)	cdtime = 0;

		if (Input.GetButton ("Fire1") && cdtime == 0) 
		{
			Shoot ();
			cdtime = cooldown;
		}
	}

	void Shoot()
	{
		//get the shooting angle
		float rotZ = shotPoint.transform.rotation.eulerAngles.z;

		//fix the angle if character facing left
		if (transform.parent.parent.localScale.x < 0) rotZ += 180;  //parent的parent要是player 所以此cs檔要放在Gun

		Instantiate (bulletPrefab, shotPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, rotZ));

		//calculate raycast direction
		Vector2 firePos = new Vector2 (shotPoint.transform.position.x, shotPoint.transform.position.y);
		Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 rayDirection = mousePos - firePos;  //方向

		//raycast2D serach range = 100, onlt hit the gameObject belongs to shootableLayer 回傳第一個碰撞到的RayCastHit2D
		RaycastHit2D hit = Physics2D.Raycast (firePos, rayDirection, 100, shootableLayer); //Physics2D.Raycast (起點, 方向, 100,偵測layer)

		//destroy the shootable gameObject with "Enemy" tag
		if (hit.collider != null && hit.collider.tag == "Enemy") 
		{
			Destroy (hit.collider.gameObject);
		}
	}
		
	// Use this for initialization
	void Start () 
	{
		cooldown = 0.1f;   //預設0.1秒再射一次
		cdtime = 0.0f;
	}
}
