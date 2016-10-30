using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start() { 
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xVal = Mathf.Round(p.x);
            float yVal = Mathf.Round(p.y);
            if(xVal % 2 == 1)
            {
                xVal--;
            }
            if(yVal % 2 == 1)
            {
                yVal++;
            }
            Debug.Log("x " + xVal / 2 + " y " + yVal / -2); 
            PlayerPrefs.SetInt("playerX", (int)xVal / 2);
            PlayerPrefs.SetInt("playerY", (int)yVal / -2);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float xVal = Mathf.Round(p.x);
            float yVal = Mathf.Round(p.y);
            if (xVal % 2 == 1)
            {
                xVal--;
            }
            if (yVal % 2 == 1)
            {
                yVal++;
            }
            
            PlayerPrefs.SetInt("destX", (int)xVal/2);
            PlayerPrefs.SetInt("destY", (int)yVal / -2);
        }

        if (Input.GetKey("q"))
        {
            Camera.main.orthographicSize += Time.deltaTime*2;
        }
        if(Input.GetKey("e"))
        {
            Camera.main.orthographicSize -= Time.deltaTime*2;
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * 10);
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * Time.deltaTime * 10);
        }
    }
}