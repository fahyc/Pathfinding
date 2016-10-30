using UnityEngine;
using System.Collections;
using ExtensionMethods;

[RequireComponent (typeof (AStarPathfinding))]
public class waypointController : MonoBehaviour {
	public PlacableNode nodePrefab;
//	PlacedPath path;

	public PlacableNode start;
	public PlacableNode end;

	AStarPathfinding pathFinding;
	// Use this for initialization
	void Start () {
		pathFinding = GetComponent<AStarPathfinding>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
		{
			if (end)
			{
				Destroy(end.gameObject);
			}
			PlacableNode node = (PlacableNode)Instantiate(nodePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition).xy(),Quaternion.identity);
			node.temp = true;
			end = node;

			StartCoroutine("findPath");
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (start)
			{
				Destroy(start.gameObject);
			}
			PlacableNode node = (PlacableNode)Instantiate(nodePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition).xy(), Quaternion.identity);
			node.temp = true;
			start = node;
			StartCoroutine("findPath");
		}
	}

	IEnumerator findPath()
	{
		yield return new YieldInstruction();
		if (start && end)
		{
			print("Start: " + start + " end: " + end);
			pathFinding.FindPath(start.getNode(), end.getNode());
		}
	}
}
