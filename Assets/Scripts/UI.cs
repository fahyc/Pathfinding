using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Button hB;
    public Text hWS;
    public void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("AStar"))
        {
            if(PlayerPrefs.GetInt("cH") == 1)
            {
                hB.GetComponentInChildren<Text>().text = "Switch to Euclidian";
            }
            else
            {
                hB.GetComponentInChildren<Text>().text = "Switch to Manhattan";
            }
        }
        else
        {
            hB.enabled = false;
        }
    }

	public void SwitchHeuristic()
    {
        if(SceneManager.GetActiveScene().name.Equals("AStar"))
        {
            PlayerPrefs.SetInt("cH", 1 - PlayerPrefs.GetInt("cH"));
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SwitchMap()
    {
        if (SceneManager.GetActiveScene().name.Equals("AStar"))
        {
            if(PlayerPrefs.GetString("cM").Equals("AStar/Maps/hrt201n.map"))
            {
                PlayerPrefs.SetInt("playerX", 64);
                PlayerPrefs.SetInt("playerY", 70);
                PlayerPrefs.SetInt("destX", 64);
                PlayerPrefs.SetInt("destY", 71);
                PlayerPrefs.SetString("cM", "AStar/Maps/arena2.map");
            }
            else
            {
                PlayerPrefs.SetInt("playerX", 64);
                PlayerPrefs.SetInt("playerY", 70);
                PlayerPrefs.SetInt("destX", 64);
                PlayerPrefs.SetInt("destY", 71);
                PlayerPrefs.SetString("cM", "AStar/Maps/hrt201n.map");
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (SceneManager.GetActiveScene().name.Equals("hrt"))
		{
			SceneManager.LoadScene("arena");
		}
		else
		{
			SceneManager.LoadScene("hrt");
		}
	}

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwichPathFinding()
    {
        if (SceneManager.GetActiveScene().name.Equals("AStar"))
        {
            if (PlayerPrefs.GetString("cM").Equals("AStar/Maps/hrt201n.map"))
            {
                SceneManager.LoadScene("hrt");
            }
            else
            {
                SceneManager.LoadScene("arena");
            }
        }
        else
        {
            SceneManager.LoadScene("AStar");
        }
    }

    public void HeuristicWeight()
    {
        PlayerPrefs.SetFloat("hW", float.Parse(hWS.text));
        ReloadScene();
    }
}