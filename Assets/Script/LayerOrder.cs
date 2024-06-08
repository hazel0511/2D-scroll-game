using UnityEngine;
using System.Collections;

public class LayerOrder : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		//set up the sorting layer and order in layer of this prefab
		GetComponent<Renderer>().sortingLayerName = "Foreground";
		GetComponent<Renderer>().sortingOrder = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
