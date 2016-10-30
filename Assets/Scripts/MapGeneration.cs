using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MapGeneration : MonoBehaviour {

    private GameObject map;
    public string mapName;

    public GameObject player;
    public GameObject destination;
    public GameObject treeTile;
    public GameObject floorTile;
    public GameObject passableTile;
    [HideInInspector]
    public List<List<Node>> mapData;

	[Serializable]
	public class Node : IComparable<Node>
	{
		[NonSerialized]
		public Node parent;
		//Location for character movement, is the center of the tile
		public float xPos;
		public float yPos;

		public Vector3 position;

		//Location in the Graph for pathfinding
		public int xLoc;
		public int yLoc;
		public float g = 0; // Cost to this point
		public float h = 0; // Heuristic cost
		public bool passable;

		[NonSerialized]
		public List<Node> neighbors;

		public Node(Vector3 pos, bool p)
		{

			position = pos;
			xPos = position.x;
			yPos = position.y;
			xLoc = (int)position.x;
			yLoc = (int)position.y;
			passable = p;
			parent = null;
		}

        public Node( int xL, int yL, bool p)
        {

			xPos = xL + .5f;
            yPos = -yL - .5f;

			position = new Vector3(xPos, yPos, 0);
			xLoc = xL/2;
            yLoc = yL/2;
            passable = p;
            parent = null;
        }

        int IComparable<Node>.CompareTo(Node a)
        {
            Node n1 = this;
            Node n2 = a;
            float hW = PlayerPrefs.GetFloat("hW");
            if (n1.g + n1.h *hW > n2.g + n2.h*hW)
                return 1;
            if (n1.g + n1.h*hW < n2.g + n2.h*hW)
                return -1;
            else
                return 0;
        }
    }

    void Start()
    {
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

        map = new GameObject();
        mapData = new List<List<Node>>();
        Load();
        GenerateData();
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    private bool Load()
    {
        string line1;
        int xLoc;
        int yLoc = -1;
        if(SceneManager.GetActiveScene().name.Equals("AStar"))
        {
            mapName = PlayerPrefs.GetString("cM");
        }
        StreamReader mapReader = new StreamReader(Application.dataPath + "/" + mapName);
        using (mapReader)
        {
            do
            {
                yLoc++;
                line1 = mapReader.ReadLine();
                if (line1 != null)
                {
                    for (xLoc = 0; xLoc < line1.Length;)
                    {
                        if (line1[xLoc] == '.')
                        {
                            Instantiate(floorTile,new Vector3(xLoc, -yLoc, 0), Quaternion.identity, map.transform);
                        }
                        else if (line1[xLoc] == 'T')
                        {
                            Instantiate(treeTile, new Vector3(xLoc, -yLoc, 0), Quaternion.identity, map.transform);
                        }
                        ++xLoc;
                    }
                }
                    
            }
            while (line1 != null);
            mapReader.Close();
            return true;
        }
    }

    //Generate the movement tiles
    private void GenerateData()
    {
		string[] mapInfo = new StreamReader(Application.dataPath + "/" + mapName).ReadToEnd().Split('\n');
        for (int y = 0; y < mapInfo.Length-1; y += 2)
        {
            mapData.Add(new List<Node>());
            for(int x = 0; x < mapInfo[0].Length-1; x += 2)
            {
                if(mapInfo[y][x] != '.' || mapInfo[y][x+1] != '.' || mapInfo[y+1][x] != '.' || mapInfo[y+1][x+1] != '.')
                {
                    mapData[y / 2].Add(new Node(x, y, false));
                }
                else
                {
                    mapData[y / 2].Add(new Node(x, y, true));
                    Instantiate(passableTile, new Vector3(x+.5f, -y - .5f, -1), Quaternion.identity, map.transform);
                }
            }
        }
    }
}
