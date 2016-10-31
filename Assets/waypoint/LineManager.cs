using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class LineManager : MonoBehaviour {

	public List<LineRenderer> lines = new List<LineRenderer>();
	public List<Vecpair> positions = new List<Vecpair>();

	public LineRenderer linePrefab;

	public bool isOn = true;

	public class Vecpair
	{
		public Vector3 start;
		public Vector3 end;
		public Vecpair(Vector3 s, Vector3 e)
		{
			start = s;
			end = e;
		}
	}

	public void Clear()
	{
		for(int i =0; i < lines.Count; i++)
		{
			Destroy(lines[i].gameObject);
		}
		lines.Clear();
		positions.Clear();
	}


	public void DrawLine(Vector3 start, Vector3 end, Color color)
	{


		//print("placing line");
		if (!isOn)
		{
			return;
		}
		LineRenderer obj = Instantiate(linePrefab);
		lines.Add(obj);
		obj.SetPosition(0, start);
		obj.SetPosition(1, end);
		obj.SetColors(color, color);
		
		positions.Add(new Vecpair(start, end));
	}

	public void remove(Vector3 start, Vector3 end)
	{
		for(int i =0; i < positions.Count; i++)
		{
			if(positions[i].start == start && positions[i].end == end)
			{
				positions.RemoveAt(i);
				Destroy(lines[i].gameObject);
				lines.RemoveAt(i);
			}
		}
	}
	
}
