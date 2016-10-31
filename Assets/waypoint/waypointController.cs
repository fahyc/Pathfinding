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
		//PlayerPrefs.DeleteAll();
		if (!PlayerPrefs.HasKey("cH"))
		{
			PlayerPrefs.SetInt("playerX", 64);
			PlayerPrefs.SetInt("playerY", 70);
			PlayerPrefs.SetInt("destX", 64);
			PlayerPrefs.SetInt("destY", 71);
			PlayerPrefs.SetFloat("hW", 1);
			PlayerPrefs.SetInt("cH", 0);
			PlayerPrefs.SetString("cM", "AStar/Maps/hrt201n.map");
			PlayerPrefs.Save();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.X))
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
		if (Input.GetKeyDown(KeyCode.Z))
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
