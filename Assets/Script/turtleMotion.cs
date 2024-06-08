using UnityEngine;
using System.Collections;
using Fungus;

public class turtleMotion : MonoBehaviour 
{
	public float windForce = 2.0f;

    public Flowchart flowchart;
    string isPlay = "isPlay";
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("MovedByWind", 0.0f, 0.5f);
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();


	}

	void MovedByWind()  //隨機搖擺
	{
        if (flowchart.GetBooleanVariable(isPlay) != true)
        {
            if (Random.Range(0.0f, 1.0f) > 0.5f) GetComponent<Rigidbody2D>().AddForce(Vector2.right * windForce);
            else GetComponent<Rigidbody2D>().AddForce(-1 * Vector2.right * windForce);
        }
	}

	void OnCollisionEnter2D(UnityEngine.Collision2D hit)  //摧毀
	{
		//destroy itself on collision with player (10 = number of "player" layer in this example)
		if (hit.gameObject.layer == 9) Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
