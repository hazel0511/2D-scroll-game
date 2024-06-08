using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {


	public GameObject enemyPrefab;
	private GameObject newItems;

	public float Xmin = -10.0f;
	public float Xmax = 10.0f;
	public float generateHeight = -5.0f;
	public float startTime = 6.0f;
	public float generateRate = 6.0f;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("EnemyGenerating", startTime, generateRate);
	}

	void EnemyGenerating()
	{
		newItems=Instantiate (enemyPrefab, new Vector3(Random.Range(Xmin, Xmax), generateHeight, 0.0f), enemyPrefab.transform.rotation);
		Destroy (newItems, generateRate*2.0f);
	}

	// Update is called once per frame
	void Update () {

	}
}
