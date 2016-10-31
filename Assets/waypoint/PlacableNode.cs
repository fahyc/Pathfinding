using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacableNode : MonoBehaviour {
	public MapGeneration.Node node;
	public MapGeneration.Node getNode()
	{
		return node;
	}
	public void setNode( MapGeneration.Node newNode)
	{
		node = newNode;
	}

	//public PlacableNode[] nodesDemo;

	public bool temp;

	static List<PlacableNode> nodes = new List<PlacableNode>();

	void Awake()
	{
		//print("placing");
		nodes.Add(this);
		node = new MapGeneration.Node(transform.position, false);
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
		//nodesDemo = nodes.ToArray();
	}

	void OnDestroy()
	{
		//if this is a destination node, it should be removed from the graph when it is destroyed. 
		for(int i = 0; i < node.neighbors.Count; i++)
		{
			node.neighbors[i].neighbors.Remove(node);
		}
		for(int i = 0; i < nodes.Count; i++)
		{
			if(nodes[i] == this)
			{
				//print("found self.");
			}
		}
		nodes.Remove(this);
		for (int i = 0; i < nodes.Count; i++)
		{
			if (nodes[i] == this)
			{
				print("Error! found self.");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
