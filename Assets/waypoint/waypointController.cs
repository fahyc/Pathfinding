using UnityEngine;
using System.Collections;

public class waypointController : MonoBehaviour {
	PlacableNode nodePrefab;
	PlacedPath path;

	AStarPathfinding pathFinding;
	// Use this for initialization
	void Start () {
		pathFinding = GetComponent<AStarPathfinding>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			PlacableNode node = Instantiate(nodePrefab);
			node.temp = true;
			
		}
	}
}
