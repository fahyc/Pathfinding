using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinding : MonoBehaviour {

    MapGeneration mapInfo;
    int xPos;
    int yPos;
    public int currentHeuristic;

    MapGeneration.Node target;
    List<MapGeneration.Node> pathToUse;

	// Use this for initialization
	void Start ()
    {
        //Get the map data from the Map Generation Script
        mapInfo = Camera.main.GetComponent<MapGeneration>();

        //Random start location for the actor
        do
        {
            xPos = (int) Mathf.Floor(mapInfo.mapData[0].Count * Random.value);
            yPos = (int) Mathf.Floor(mapInfo.mapData.Count * Random.value);
        } while (!mapInfo.mapData[yPos][xPos].passable);

        //Set the player at the center of the square
        transform.position = new Vector3(mapInfo.mapData[yPos][xPos].xPos,
                                         mapInfo.mapData[yPos][xPos].yPos, -15);

        //Generate a node for the target
        target = new MapGeneration.Node(0, 0, true);

        //Random location for the target
        do
        {
            target.xLoc = (int)Mathf.Floor(mapInfo.mapData[0].Count * Random.value);
            target.yLoc = (int)Mathf.Floor(mapInfo.mapData.Count * Random.value);
        } while (!mapInfo.mapData[target.yLoc][target.xLoc].passable);

        //Place the destination on the map
        Instantiate(mapInfo.destination, new Vector3(mapInfo.mapData[target.yLoc][target.xLoc].xPos, mapInfo.mapData[target.yLoc][target.xLoc].yPos, -15f), Quaternion.identity);

        Camera.main.transform.parent = this.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -25);

        MapGeneration.Node destNode = FindPath(target.xLoc, target.yLoc);
        pathToUse = new List<MapGeneration.Node>();
        while (destNode != null)
        {
            pathToUse.Add(destNode);
            destNode = destNode.parent;
        }
        pathToUse.Reverse();
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

    MapGeneration.Node FindPath(int xDest, int yDest)
    {
        List<MapGeneration.Node> open = new List<MapGeneration.Node>();
        List<MapGeneration.Node> closed = new List<MapGeneration.Node>();
        open.Add(mapInfo.mapData[this.yPos][this.xPos]);
        while(open.Count > 0)
        {
            MapGeneration.Node current = open[0];
            open.RemoveAt(0);
            if(current.xLoc == xDest && current.yLoc == yDest)
            {
                return current;
            }
            else
            {
                closed.Add(current);

                foreach (MapGeneration.Node neighbor in findNeighbors(current))
                {
                    bool inClosed = closed.Contains(neighbor);
                    bool inOpen = open.Contains(neighbor);

                    if (inClosed && current.g + 1 < neighbor.g)
                    {
                        Debug.Log("Closed");
                        neighbor.g = current.g+1;
                        neighbor.parent = current;

                    }
                    else if (inOpen && current.g + 1 < neighbor.g)
                    {
                        Debug.Log("open");
                        neighbor.g = current.g+1;
                        neighbor.parent = current;
                    }
                    else if(!inOpen && !inClosed)
                    {
                        neighbor.h = useHeuristic(neighbor);
                        neighbor.g = neighbor.g + 1;
                        neighbor.parent = current;
                       
                        int index = open.BinarySearch(neighbor);
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
        return new MapGeneration.Node(this.xPos, this.yPos, true);
    }
}
