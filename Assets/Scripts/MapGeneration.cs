using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class MapGeneration : MonoBehaviour {

    private GameObject map;
    public string mapName;
    public GameObject treeTile;
    public GameObject floorTile;
    public GameObject passableTile;
    [HideInInspector]
    public List<List<Node>> mapData;

    public class Node
    {
        public float xPos;
        public float yPos;
        public bool passable;

        public Node(float xL, float yL, bool p)
        {
            xPos = xL ;
            yPos = yL;
            passable = p;
        }
    }

    void Start()
    {
        map = new GameObject();
        mapData = new List<List<Node>>();
        Load();
        genData();
    }

    private bool Load()
    {
        string line1;
        int xLoc;
        int yLoc = -1;
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

    private void genData()
    {            
        string[] mapInfo = new StreamReader(Application.dataPath + "/" + mapName).ReadToEnd().Split('\n');
        for (int y = 0; y < mapInfo.Length-1; y += 2)
        {
            mapData.Add(new List<Node>());
            for(int x = 0; x < mapInfo[0].Length-1; x += 2)
            {
                if(mapInfo[y][x] != '.' || mapInfo[y][x+1] != '.' || mapInfo[y+1][x] != '.' || mapInfo[y+1][x+1] != '.')
                {
                    mapData[y / 2].Add(new Node(y, x, false));
                }
                else
                {
                    mapData[y / 2].Add(new Node(x + .5f, -y - .5f, true));
                    Instantiate(passableTile, new Vector3(x+.5f, -y - .5f, -1), Quaternion.identity, map.transform);
                }
            }
        }
    }
}
