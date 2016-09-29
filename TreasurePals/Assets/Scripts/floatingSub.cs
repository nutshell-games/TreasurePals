using UnityEngine;
using System.Collections;

public class floatingSub : MonoBehaviour {
	public float floatSpan = 2.0f;
	public float speed = 1.0f;
	float startY;
	public GameObject man1;
	public GameObject man2;
	public GameObject man3;
	public GameObject man4;
	// Use this for initialization
	void Start () {
		startY = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newPos = new Vector3 (transform.localPosition.x, startY + Mathf.Sin (Time.time * speed) * floatSpan / 2.0f);
		transform.localPosition = newPos;
		man1.transform.localPosition = new Vector2 (man1.transform.localPosition.x, transform.localPosition.y);
		man2.transform.localPosition = new Vector2 (man2.transform.localPosition.x, transform.localPosition.y);
		man3.transform.localPosition = new Vector2 (man3.transform.localPosition.x, transform.localPosition.y);
		man4.transform.localPosition = new Vector2 (man4.transform.localPosition.x, transform.localPosition.y);
	}



}
