using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using ExtensionMethods;
using System;

[RequireComponent (typeof(LineManager))]
public class AStarPathfinding : MonoBehaviour {

    MapGeneration mapInfo;
    int xPos;
    int yPos;
    public int currentHeuristic;

	public bool tileBased = false;

    MapGeneration.Node target;
    List<MapGeneration.Node> pathToUse;

	LineManager lineManager;


	public MapGeneration.Node[] nodeArray;

	// Use this for initialization
	void Start()
	{
		lineManager = GetComponent<LineManager>();
		if (tileBased)
		{
			//Get the map data from the Map Generation Script
			mapInfo = Camera.main.GetComponent<MapGeneration>();

            xPos = PlayerPrefs.GetInt("playerX");
            yPos = PlayerPrefs.GetInt("playerY");

			//Set the player at the center of the square
			transform.position = new Vector3(mapInfo.mapData[yPos][xPos].xPos,
											 mapInfo.mapData[yPos][xPos].yPos, -15);

			//Generate a node for the target
			target = new MapGeneration.Node(2*PlayerPrefs.GetInt("destX"), PlayerPrefs.GetInt("destY"), true);


			//Place the destination on the map
			Instantiate(mapInfo.destination, new Vector3(mapInfo.mapData[target.yLoc][target.xLoc].xPos, mapInfo.mapData[target.yLoc][target.xLoc].yPos, -15f), Quaternion.identity);

			Camera.main.transform.parent = this.transform;
			Camera.main.transform.localPosition = new Vector3(0, 0, -25);
            Camera.main.transform.parent = null;

			MapGeneration.Node destNode = FindPath(target.xLoc, target.yLoc);
			pathToUse = new List<MapGeneration.Node>();
			print("setting path to use.");
			while (destNode != null)
			{
				pathToUse.Add(destNode);
				destNode = destNode.parent;
			}
			pathToUse.Reverse();
		}
		else
		{

		}
    }

    void Update()
    {
        if(Input.GetKeyDown("k") && pathToUse.Count > 0)
        {
            Debug.Log(pathToUse[0].xLoc + " " + pathToUse[0].yLoc);
            pathToUse.RemoveAt(0);
        }
    }

    //Check the four adjacent neighborsand add them to a temproary list
    List<MapGeneration.Node> findNeighbors(MapGeneration.Node current)
    {
        List<MapGeneration.Node> neighbors = new List<MapGeneration.Node>();
        if (current.xLoc != 0 && mapInfo.mapData[current.yLoc][current.xLoc - 1].passable)
        {
            neighbors.Add(mapInfo.mapData[current.yLoc][current.xLoc - 1]);
        }
        if(current.xLoc != mapInfo.mapData[0].Count-1&& mapInfo.mapData[current.yLoc][current.xLoc + 1].passable)
        {
            neighbors.Add(mapInfo.mapData[current.yLoc][current.xLoc + 1]);
        }
        if(current.yLoc != 0 && mapInfo.mapData[current.yLoc -1][current.xLoc ].passable)
        {
            neighbors.Add(mapInfo.mapData[current.yLoc - 1][current.xLoc]);
        }
        if(current.yLoc != mapInfo.mapData.Count - 1 && mapInfo.mapData[current.yLoc + 1][current.xLoc].passable)
        {
            neighbors.Add(mapInfo.mapData[current.yLoc + 1][current.xLoc]);
        }
        return neighbors;
    }

	List<MapGeneration.Node> findNeighborsTileless(MapGeneration.Node current)
	{
		return current.neighbors;
	}

    //Determines which heuristic to use
    float useHeuristic(MapGeneration.Node current)
    {
        switch (currentHeuristic)
        {
            case 1:
                return Mathf.Abs(current.xLoc - target.xPos) + Mathf.Abs(current.yLoc - target.yPos);
            default:
                return Vector2.Distance(new Vector2(current.xLoc, current.yLoc), new Vector2(target.xLoc, target.yLoc));
        }

    }

    public MapGeneration.Node FindPath(MapGeneration.Node start, MapGeneration.Node end)
    {
		StartCoroutine(pathFindCoRoutine(start,end));
		return new MapGeneration.Node(0, 0, false);
		/*
		lineManager.Clear();
		target = end;
		//print("going from start " + start.position + " to " + end.position);
        List<MapGeneration.Node> open = new List<MapGeneration.Node>();
        List<MapGeneration.Node> closed = new List<MapGeneration.Node>();
        open.Add(start);
        while(open.Count > 0)
        {
			//print("running pathfinding.");
            MapGeneration.Node current = open[0];
            open.RemoveAt(0);
            if(current == end)
            {
				MapGeneration.Node next;// = current.parent;
				MapGeneration.Node cur = current;
				while(cur.parent != null)
				{
					next = cur.parent;
					lineManager.DrawLine(cur.position.xy().append(-2), next.position.xy().append(-2), Color.blue);
					cur = next;
				}
				//print("returning.");
                return current;
            }
            else
            {
                closed.Add(current);
				if (current.parent != null)
				{
					// print(lineManager + " : " + current + " : " + current.parent);
					lineManager.DrawLine(current.position.xy().append(-1), current.parent.position.xy().append(-1), Color.red);
				}
                foreach (MapGeneration.Node neighbor in tileBased ? findNeighbors(current) : findNeighborsTileless(current))
                {

					lineManager.DrawLine(current.position.xy().append(-.5f), neighbor.position.xy().append(-.5f), Color.yellow);
					bool inClosed = closed.Contains(neighbor);
                    bool inOpen = open.Contains(neighbor);
					float newG = Mathf.Abs(Vector3.Distance(current.position, neighbor.position));

					if (inClosed && current.g + newG < neighbor.g)
                    {
                        Debug.Log("Closed");
                        neighbor.g = current.g + newG; 
                        neighbor.parent = current;
                    }
                    else if (inOpen && current.g + newG < neighbor.g)
                    {
                        Debug.Log("open");
                        neighbor.g = current.g + newG;
                        neighbor.parent = current;
                    }
                    else if(!inOpen && !inClosed)
                    {
                        neighbor.h = useHeuristic(neighbor);
                        neighbor.g = neighbor.g + newG;
                        neighbor.parent = current;
						
                        int index = open.FindIndex(x => x==neighbor);
                        if(index < 0)
                        {
                            open.Insert(~index, neighbor); //Add at the theoretical location
                        }
                        else
                        {
                            open.Insert(index, neighbor);
                        }
                        
                    }
                }
            }
        }
        return new MapGeneration.Node(this.xPos, this.yPos, true);*/
    }

	MapGeneration.Node FindPath(int xDest,int yDest)
	{
		List<MapGeneration.Node> open = new List<MapGeneration.Node>();
		List<MapGeneration.Node> closed = new List<MapGeneration.Node>();
		open.Add(mapInfo.mapData[this.yPos][this.xPos]);
		while (open.Count > 0)
		{
			MapGeneration.Node current = open[0];
			open.RemoveAt(0);
			if (current.xLoc == xDest && current.yLoc == yDest)
			{
				MapGeneration.Node next;// = current.parent;
				MapGeneration.Node cur = current;
				while (cur.parent != null)
				{
					next = cur.parent;
					lineManager.DrawLine(cur.position.xy().append(-2), next.position.xy().append(-2), Color.blue);
					cur = next;
				}
				//print("returning.");
				return current;
			}
			else
			{
				closed.Add(current);
				if (current.parent != null)
				{
					// print(lineManager + " : " + current + " : " + current.parent);
					lineManager.DrawLine(current.position.xy().append(-1), current.parent.position.xy().append(-1), Color.red);
				}
				foreach (MapGeneration.Node neighbor in tileBased ? findNeighbors(current) : findNeighborsTileless(current))
				{

					lineManager.DrawLine(current.position.xy().append(-.5f), neighbor.position.xy().append(-.5f), Color.yellow);
					bool inClosed = closed.Contains(neighbor);
					bool inOpen = open.Contains(neighbor);

					if (inClosed && current.g + 1 < neighbor.g)
					{
						Debug.Log("Closed");
						neighbor.g = current.g + 1;
						neighbor.parent = current;

					}
					else if (inOpen && current.g + 1 < neighbor.g)
					{
						Debug.Log("open");
						neighbor.g = current.g + 1;
						neighbor.parent = current;
					}
					else if (!inOpen && !inClosed)
					{
						neighbor.h = useHeuristic(neighbor);
						neighbor.g = neighbor.g + 1;
						neighbor.parent = current;

						int index = open.BinarySearch(neighbor);
						if (index < 0)
						{
							open.Insert(~index, neighbor); //Add at the theoretical location
						}
						else
						{
							open.Insert(index, neighbor);
						}

					}
				}
			}
		}
		return new MapGeneration.Node(this.xPos, this.yPos, true);
	}


	IEnumerator pathFindCoRoutine(MapGeneration.Node start, MapGeneration.Node end)
	{
		lineManager.Clear();
		target = end;
		//print("going from start " + start.position + " to " + end.position);
		List<MapGeneration.Node> closed = new List<MapGeneration.Node>();
		SortedList<MapGeneration.Node,int> open = new SortedList<MapGeneration.Node, int>();
		open.Add(start,0);
		while (open.Count > 0)
		{
			yield return new YieldInstruction();
			nodeArray = new MapGeneration.Node[open.Keys.Count];
			for(int i = 0; i < open.Keys.Count; i++)
			{
				nodeArray[i] = open.Keys[i];
			}
			//print("running pathfinding.");
			MapGeneration.Node current = open.Keys[0];
			open.Remove(current);
			if (current == end)
			{
				MapGeneration.Node next;// = current.parent;
				MapGeneration.Node cur = current;
				while (cur.parent != null)
				{
					next = cur.parent;
					lineManager.DrawLine(cur.position.xy().append(-2), next.position.xy().append(-2), Color.blue);
					cur = next;
				}
				break;
				//print("returning.");
				//return current;
			}
			else
			{
				closed.Add(current);
				if (current.parent != null)
				{
					// print(lineManager + " : " + current + " : " + current.parent);
					lineManager.DrawLine(current.position.xy().append(-1), current.parent.position.xy().append(-1), Color.red);
				}
				foreach (MapGeneration.Node neighbor in tileBased ? findNeighbors(current) : findNeighborsTileless(current))
				{
					
					lineManager.DrawLine(current.position.xy().append(-.5f), neighbor.position.xy().append(-.5f), Color.yellow);
					bool inClosed = closed.Contains(neighbor);
					bool inOpen = open.ContainsKey(neighbor);
					float newG = Vector3.Distance(current.position, neighbor.position);

					if (inClosed && current.g + newG < neighbor.g)
					{
						Debug.Log("Closed");
						neighbor.g = current.g + newG;
						neighbor.parent = current;
					}
					else if (inOpen && current.g + newG < neighbor.g)
					{
						open.Remove(neighbor);
						Debug.Log("open");
						neighbor.g = current.g + newG;
						neighbor.parent = current;
						open.Add(neighbor, 0);
						//open.
					}
					else if (!inOpen && !inClosed)
					{
						//MapGeneration.Node temp = Array.Find(open.ToArray(), x => x == neighbor);
						//int index = open.FindIndex(x => x == neighbor);
						/*if (temp != null) 
						{
							Debug.Log("Error. this is already in the open list.");
							temp.g = Mathf.Min(current.g + newG, temp.g);
						} else {*/
							neighbor.h = useHeuristic(neighbor);
							neighbor.g = current.g + newG;
							neighbor.parent = current;
							open.Add(neighbor,0);
					///	}
					}
				}
			}
		}
		//return new MapGeneration.Node(this.xPos, this.yPos, true);
	}

}


