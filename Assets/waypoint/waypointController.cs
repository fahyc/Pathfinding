using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AStarPathfinding))]
public class waypointController : MonoBehaviour {
	PlacableNode nodePrefab;
	PlacedPath path;

	PlacableNode start;
	PlacableNode end;

	AStarPathfinding pathFinding;
	// Use this for initialization
	void Start () {
		pathFinding = GetComponent<AStarPathfinding>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
		{
			Destroy(end);
			PlacableNode node = Instantiate(nodePrefab);
			node.temp = true;
			end = node;
			if (start && end)
			{
				pathFinding.FindPath(start.node, end.node);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Destroy(start);
			PlacableNode node = Instantiate(nodePrefab);
			node.temp = true;
			start = node;
			if (start && end)
			{
				pathFinding.FindPath(start.node, end.node);
			}
		}
	}
}
