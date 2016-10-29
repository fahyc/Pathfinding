using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacableNode : MonoBehaviour {
	public MapGeneration.Node node;

	public bool temp;

	static List<PlacableNode> nodes = new List<PlacableNode>();

	void Awake()
	{
		nodes.Add(this);
		node = new MapGeneration.Node((int)transform.position.x, (int)transform.position.y, false);
		node.neighbors = new List<MapGeneration.Node>();
	}
	// Use this for initialization
	void Start () {
		for(int i = 0; i < nodes.Count; i++)
		{
			RaycastHit2D info = Physics2D.Raycast(transform.position, nodes[i].transform.position - transform.position);
			if(info.transform == nodes[i].transform)
			{
				node.neighbors.Add(info.transform.GetComponent<PlacableNode>().node);
				if (temp)
				{
					info.transform.GetComponent<PlacableNode>().node.neighbors.Add(node);
				}
			}
		}
	}

	void OnDestroy()
	{
		//if this is a destination node, it should be removed from the graph when it is destroyed. 
		for(int i = 0; i < node.neighbors.Count; i++)
		{
			node.neighbors[i].neighbors.Remove(node);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
