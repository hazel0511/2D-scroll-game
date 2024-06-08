using UnityEngine;
using System.Collections;

public class turtleGenerator : MonoBehaviour   //生成烏龜
{
	public GameObject turtlePrefab;
	private GameObject newItems;

	public float Xmin = -10.0f;
	public float Xmax = 10.0f;
	public float generateHeight = 5.0f;
	public float startTime = 1.0f;
	public float generateRate = 0.2f;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("TurtleGenerating", startTime, generateRate);
	}

	void TurtleGenerating()
	{
		Instantiate (turtlePrefab, new Vector3(Random.Range(Xmin, Xmax), generateHeight, 0.0f), turtlePrefab.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
