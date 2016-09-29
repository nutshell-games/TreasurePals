using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CavasScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<CanvasScaler> ().referenceResolution = new Vector2 (Screen.width, Screen.height);
	}
	/*
	void Awake(){
		GetComponent<CanvasScaler> ().referenceResolution = new Vector2 (Screen.width, Screen.height);

	}*/

}
