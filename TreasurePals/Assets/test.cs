using UnityEngine;
using System.Collections;
using System;
public class test : MonoBehaviour {
	static string location;
	static DateTime time = null;
	// Use this for initialization
	void Start () {
		Debug.LogError (location == null ? "location is null" : location);
		Debug.LogError (time == null ? "time is null" : time.ToString ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
