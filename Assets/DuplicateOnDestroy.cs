using UnityEngine;
using System.Collections;

public class DuplicateOnDestroy : MonoBehaviour {
	void OnDestroy()
	{
		GameObject temp  = Instantiate(gameObject);
		temp.SetActive(true);
	}
}
