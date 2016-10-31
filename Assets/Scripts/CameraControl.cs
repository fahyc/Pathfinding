using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z"))
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
            //Debug.Log("Player x " + xVal / 2 + " y " + yVal / -2); 

            if(Camera.main.GetComponent<MapGeneration>().mapData[(int)Mathf.Floor(yVal / -2)][(int)(xVal / 2)].passable)
            {
                PlayerPrefs.SetInt("playerX", (int)(xVal / 2));
                PlayerPrefs.SetInt("playerY", (int)Mathf.Floor(yVal / -2));
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetKeyDown("x"))
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
            Debug.Log("Dest x " + xVal / 2 + " y " + yVal / -2);
            if (Camera.main.GetComponent<MapGeneration>().mapData[(int)Mathf.Floor(yVal / -2)][(int)(xVal / 2)].passable)
            {
                PlayerPrefs.SetInt("destX", (int)(xVal / 2));
                PlayerPrefs.SetInt("destY", (int)Mathf.Floor(yVal / -2));
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
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